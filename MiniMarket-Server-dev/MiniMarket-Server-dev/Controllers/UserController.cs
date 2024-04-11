using Microsoft.AspNetCore.Mvc;
using MiniMarket_Server_dev.Data.Interfaces;

namespace MiniMarket_Server_dev.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        { 
            _userRepository = userRepository;
        }

        
    }
}
