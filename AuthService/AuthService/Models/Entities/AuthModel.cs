using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class AuthModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime TimeOfIssuingToken { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
