using MiniMarket_Server_dev.Application.DTOs.Requests;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Application.Services.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string?> Authenticate(LoginRequestDTO loginRequest);
    }
}
