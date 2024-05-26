﻿namespace MiniMarket_API.Application.ViewModels
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
    }
}
