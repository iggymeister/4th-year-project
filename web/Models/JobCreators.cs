using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class JobCreators
    {
        public int CreatorId { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 5)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 5)]
        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", ErrorMessage = "Please enter valid phone no.")]
        public int PhoneNo { get; set; }

        public Jobs Jobs { get; set; }
    }
}
