using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bids.WebUI.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile Owner { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime AuctionEndDate { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
    }
}