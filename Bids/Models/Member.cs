using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bids.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string LoginName { get; set; }
        public int ReputationPoints { get; set; }
    }
}