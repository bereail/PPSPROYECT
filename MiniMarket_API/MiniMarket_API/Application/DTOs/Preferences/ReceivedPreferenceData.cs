using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Preferences
{
    public class ReceivedPreferenceData
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
