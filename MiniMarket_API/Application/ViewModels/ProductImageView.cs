namespace MiniMarket_API.Application.ViewModels
{
    public class ProductImageView
    {
        public Guid Id { get; set; }

        public string ImageName { get; set; }

        public string ImageExtension { get; set; }

        public long ImageSize { get; set; }

        public string ImageUrl { get; set; }
    }
}
