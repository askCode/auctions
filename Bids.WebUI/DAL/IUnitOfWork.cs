using System;
namespace Bids.WebUI.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<Bids.WebUI.Models.Bid> BidRepository { get; }
        IGenericRepository<Bids.WebUI.Models.Item> ItemRepository { get; }
        IGenericRepository<Bids.WebUI.Models.UserProfile> MemberRepository { get; }
        void Save();
    }
}
