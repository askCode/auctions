using Bids.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bids.WebUI.DAL
{

    public class UnitOfWork : IDisposable, Bids.WebUI.DAL.IUnitOfWork
    {

        //private AuctionContext context = new AuctionContext();
        private UsersContext context = new UsersContext();
        private IGenericRepository<UserProfile> memberRepository;
        private IGenericRepository<Item> itemRepository;
        private IGenericRepository<Bid> bidRepository;


        public IGenericRepository<Item> ItemRepository
        {
            get
            {
                if (itemRepository == null)
                {
                    itemRepository = new GenericRepository<Item>(context);
                }
                
                return itemRepository;
            }
        }

        public IGenericRepository<Bid> BidRepository
        {
            get
            {
                if (bidRepository == null)
                { 
                    bidRepository = new GenericRepository<Bid>(context); 
                }
                
                return bidRepository;
            }
        }


        public IGenericRepository<UserProfile> MemberRepository
        {
            get
            {
                if (this.memberRepository == null)
                {
                    this.memberRepository = new GenericRepository<UserProfile>(context);
                }
                return this.memberRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
    }
}