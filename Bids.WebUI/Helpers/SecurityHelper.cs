using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bids.WebUI.Helpers
{
    public class SecurityHelper : Bids.WebUI.Helpers.ISecurityHelper
    {
        public int CurrentUserId
        {
            get
            {
                return WebMatrix.WebData.WebSecurity.CurrentUserId;
            }
        }

        public string CurrentUserName
        {
            get
            {
                return WebMatrix.WebData.WebSecurity.CurrentUserName;
            }
        }
    }
}