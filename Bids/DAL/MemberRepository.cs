using Bids.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bids.DAL
{
    public class MemberRepository :IMemberRepository
    {
        private AuctionContext context;

        public MemberRepository(AuctionContext ctx)
        {
            context = ctx;
        }

        IEnumerable<Member> IMemberRepository.GetMembers()
        {
            return context.Members.ToList();
        }

        Member IMemberRepository.GetMemberById(int id)
        {
            return context.Members.Find(id);
        }

        void IMemberRepository.InsertMember(Member member)
        {
            context.Members.Add(member);
        }

        void IMemberRepository.DeleteMember(int memberID)
        {
            var member = context.Members.Find(memberID);
            context.Members.Remove(member);
        }

        void IMemberRepository.UpdateMember(Member member)
        {
            context.Entry(member).State = System.Data.EntityState.Modified;
        }

        void IMemberRepository.Save()
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