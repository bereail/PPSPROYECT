using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Model.Entities;
using MiniMarket_API.Data.Interfaces;

namespace MiniMarket_API.Application.Services.Implementations
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration configuration;                                                                //Ideally, we'd use IOptions<>, but, that hasn't worked so far. 

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            this.configuration = configuration;                                                                       //We inject the data from AppSettings.Json
        }

        private async Task<User?> ValidateUser(LoginRequestDTO loginRequest)
        {
            //We check if the loginRequest is empty
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
                return null;

            //We wait for the GetUserByEmail to find the user
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            if (user == null) return null;

            //We check that the request UserType is valid
            if (loginRequest.UserType == typeof(Customer).Name || loginRequest.UserType == typeof(Seller).Name || loginRequest.UserType == typeof(SuperAdmin).Name)
            {
                //We check that the user we retrieved matches the data from the request
                if (user.UserType == loginRequest.UserType && user.Password == loginRequest.Password) return user;
            }

            return null;
        }

        public async Task<string?> Authenticate(LoginRequestDTO loginRequest)
        {
            var user = await ValidateUser(loginRequest);

            if (user == null) return null;      //Should be later modified to return an exception, so we'll need custom exception handling.

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]));                   //We retrieve the Key.

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();                                                           //We form the Claims.    
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


        //This is to work with IOptions<>, in case it does get implemented.

        //public class AuthenticationServiceOptions
        //{
        //    public const string AuthenticationService = "AuthenticationService";

        //    public string SecretForKey { get; set; }
        //    public string Issuer { get; set; }
        //    public string Audience { get; set; }
        //}


    }
}
