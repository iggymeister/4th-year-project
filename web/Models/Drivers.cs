using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class Drivers
    {
        public Drivers()
        {
            JobOffers = new HashSet<JobOffers>();
            Jobs = new HashSet<Jobs>();
        }

        public int DriverId { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string Username { get; set; }

        [StringLength(40, MinimumLength = 5)]
        [EmailAddress]
        [Required]
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

        public string FrequentDrivingLocations { get; set; }
        public int? TotalJobsCompleted { get; set; }
        public int? Rating { get; set; }

        public ICollection<JobOffers> JobOffers { get; set; }
        public ICollection<Jobs> Jobs { get; set; }
    }
}
