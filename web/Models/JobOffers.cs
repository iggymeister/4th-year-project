using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class JobOffers
    {
        public int JobOfferId { get; set; }
        public int JobId { get; set; }
        public int DriverId { get; set; }

        [Required]
        public double ProposedDeliveryPrice { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string ProposedPickupDate { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string ApproxDeliveryDate { get; set; }

        public bool? JobOfferConfirmed { get; set; }

        public Drivers DriverUsernameNavigation { get; set; }
        public Jobs Job { get; set; }
    }
}
