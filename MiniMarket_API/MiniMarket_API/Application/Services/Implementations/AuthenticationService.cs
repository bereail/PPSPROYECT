using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Model.Exceptions;
using MiniMarket_API.Application.DTOs.Requests.Credentials;
using System.Security.Cryptography;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration configuration;
        private readonly IEmailSenderService _emailSenderService;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, IEmailSenderService emailSenderService)
        {
            _userRepository = userRepository;
            //We inject the data from AppSettings.Json
            this.configuration = configuration;                                                                       
            _emailSenderService = emailSenderService;
        }

        private async Task<User?> ValidateUser(LoginRequestDTO loginRequest)
        {
            //We check if the loginRequest is empty
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
                throw new ValidationException("Credentials are Empty!");

            //We wait for the GetUserByEmail to find the user
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);

            //We perform the hash of the request's password
            var passwordHash = PasswordHasher(loginRequest.Password);

            //We check that the user we retrieved matches the data from the request
            if (user != null && user.PasswordHash.SequenceEqual(passwordHash)) return user;


            throw new UnauthenticatedException("Authentication Failed: Credentials aren't valid!");
        }

        public async Task<string?> Authenticate(LoginRequestDTO loginRequest)
        {
            var user = await ValidateUser(loginRequest);

            if (user == null) return null;      

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]));                   //We retrieve the Key.

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //We form the claim.
            var claimsForToken = new List<Claim>();                                                             
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("name", user.Name));
            claimsForToken.Add(new Claim("role", user.UserType));

            //We create the token and provide it with all the data retrieved previously
            var jwtSecurityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),                                                                 //Will expire after 1 hour.
                credentials);

            var newToken = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return newToken.ToString();
        }

        private async Task<User?> ValidateRecoveryRequest(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null || user.Email != email) return null;

            return user;
        }

        private async Task<string?> GeneratePasswordRecoveryToken(string email)
        {
            var userToRecover = await ValidateRecoveryRequest(email);

            if (userToRecover == null) return null;

            //We retrieve the Key.
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Recovery:SecretForKey"]));                   

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //We form the claim.
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", userToRecover.Id.ToString()));
            claimsForToken.Add(new Claim("name", userToRecover.Name));

            //We create the token and provide it with all the data retrieved previously
            var jwtSecurityToken = new JwtSecurityToken(
                configuration["Recovery:Issuer"],
                configuration["Recovery:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                //Will expire after 30 minutes.
                DateTime.UtcNow.AddMinutes(30),                                                                 
                credentials);

            var newRecoveryToken = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return newRecoveryToken.ToString();
        }

        public async Task HandlePasswordRecoveryRequest(string email)
        {
            var recoveryToken = await GeneratePasswordRecoveryToken(email);

            if (recoveryToken == null) return;

            string param = "?token=" + recoveryToken;

            string url = @"http://localhost:3000/ResetPasswordForm" + param;

            var receivingEmail = email;
            var subject = "Password Recovery Request";
            var message = $"Hello! It seems that you have requested to recover your account's password. " +
                $"If that's the case, then follow the link provided below: " +
                $"{url}" +
                $" " +
                $"If you didn't request for password recovery, you can ignore this message.";

            await _emailSenderService.SendEmailAsync(receivingEmail, subject, message);
        }

        public byte[] PasswordHasher(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(password));

            return hashValue;
        }
    }
}
