﻿namespace MiniMarket_API.Application.ViewModels
{
    public class SaleOrderView
    {
        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }
    }
}
