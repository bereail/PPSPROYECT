﻿using System.ComponentModel.DataAnnotations;

namespace MiniMarket_API.Application.DTOs.Requests
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "User Name is Required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be at least 3 characters, or shorter than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User Email is Required.")]
        [EmailAddress(ErrorMessage = "User Email must be in a valid Email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Password is Required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "User Password must be at least 8 characters, or shorter than 50 characters.")]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "User Phone Number mustn't be longer than 75 characters.")]
        [RegularExpression(@"^(?([0-9]{3}))?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number format.")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "User Address mustn't be longer than 200 characters.")]
        public string Address { get; set; }
    }
}
