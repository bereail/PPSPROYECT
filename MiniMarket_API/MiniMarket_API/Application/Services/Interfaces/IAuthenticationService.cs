using MiniMarket_API.Application.DTOs.Requests;

namespace MiniMarket_API.Application.Services.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string?> Authenticate(LoginRequestDTO loginRequest);
    }
}
