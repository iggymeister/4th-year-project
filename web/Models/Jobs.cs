using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class Jobs
    {
        public Jobs()
        {
            JobOffers = new HashSet<JobOffers>();
        }

        public int JobId { get; set; }
        public int CreatorId { get; set; }

        [Required]
        public double PackageSizeInWeight { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string PackageSizeInDimensions { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string PickUpArea { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string DeliveryArea { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string AdditionalInfo { get; set; }

        public bool? JobConfirmed { get; set; }
        public string DriverUsername { get; set; }
        public double? Price { get; set; }
        public string DeliveryDate { get; set; }

        public JobCreators Creator { get; set; }
        public Drivers DriverUsernameNavigation { get; set; }
        public ICollection<JobOffers> JobOffers { get; set; }
    }
}
