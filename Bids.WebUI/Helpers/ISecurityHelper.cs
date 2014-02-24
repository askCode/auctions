using System;
namespace Bids.WebUI.Helpers
{
    public interface ISecurityHelper
    {
        int CurrentUserId { get; }
        string CurrentUserName { get; }
    }
}
