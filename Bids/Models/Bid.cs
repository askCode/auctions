﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bids.Models
{
    public class Bid
    {
        public int BidID { get; set; }
        public int MemberID { get; set; }
        public int ItemID { get; set; }
        public virtual Member Member { get; set; }
        public virtual Item Item { get; set; }
        public DateTime DatePlaced { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal BidAmount { get; set; }
    }
}
