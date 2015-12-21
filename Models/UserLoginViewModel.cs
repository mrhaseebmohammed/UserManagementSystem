using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Username must be less than 25 characters.")]
        [MinLength(3, ErrorMessage = "Username must be more than 3 characters")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(25, ErrorMessage = "Password must be less than 25 characters.")]
        [MinLength(3, ErrorMessage = "Password must be more than 3 characters")]
        public string Password { get; set; }
    }
}