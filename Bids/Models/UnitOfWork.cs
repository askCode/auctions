using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bids.Models
{
    public class UnitOfWork : IDisposable
    {
        private AuctionContext context = new AuctionContext();
        private GenericRepository<Member> memberRepository;


        public GenericRepository<Member> MemberRepository
        {
            get 
            {
                if (this.memberRepository == null)
                {
                    this.memberRepository = new GenericRepository<Member>(context);
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