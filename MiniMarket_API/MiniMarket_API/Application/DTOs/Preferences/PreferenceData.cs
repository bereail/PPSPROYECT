namespace MiniMarket_API.Application.DTOs.Preferences
{
    public class PreferenceData
    {
        public string PreferenceId { get; set; }

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }
    }
}
