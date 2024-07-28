using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogoleseAssociationSystem.Core.DTOs;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberRead>> GetMembersAsync(string? filter);
        Task<MemberRead> GetMemberByIdAsync(int id);
        Task<MemberRead> GetMemberByNameAsync(string name);      
    }
}
