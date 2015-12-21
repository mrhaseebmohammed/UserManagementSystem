using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models
{
    public class UserCreateViewModel
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Username must be less than 25 characters.")]
        [MinLength(3, ErrorMessage = "Username must be more than 3 characters")]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Password must be less than 25 characters.")]
        [MinLength(3, ErrorMessage = "Password must be more than 3 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string Email { get; set; }
    }
}