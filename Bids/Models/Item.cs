using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bids.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime AuctionEndDate { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}