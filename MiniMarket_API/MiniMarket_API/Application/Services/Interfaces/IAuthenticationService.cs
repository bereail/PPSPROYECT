using MiniMarket_API.Application.DTOs.Requests.Credentials;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string?> Authenticate(LoginRequestDTO loginRequest);
        Task HandlePasswordRecoveryRequest(string email);

        byte[] PasswordHasher(string password);
    }
}
