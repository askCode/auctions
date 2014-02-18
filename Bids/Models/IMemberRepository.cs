using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bids.Models
{
    public interface IMemberRepository :IDisposable
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberById(int id);
        void InsertMember(Member member);
        void DeleteMember(int memberID);
        void UpdateMember(Member member);
        void Save();
    }
}