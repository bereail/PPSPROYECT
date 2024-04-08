using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model.DTOs.Requests
{
    public class UpdateCategoryDto
    {
        public string CategoryName { get; set; }
        bool IsActive { get; set; }
    }
}
