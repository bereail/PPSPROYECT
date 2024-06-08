﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket_API.Model.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Column(TypeName = "nvarchar(35)")]
        public string ImageName { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string ImageExtension { get; set; }

        public long ImageSize { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string ImageUrl { get; set; }

        // Navigation Property
        public Product Product { get; set; } = null!;
    }
}
