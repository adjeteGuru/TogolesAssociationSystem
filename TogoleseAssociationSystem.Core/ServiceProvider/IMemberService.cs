using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public interface IMemberService
    {           
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member> GetMemberByIdAsync(Guid id);
        Task<Member> GetMemberByNameAsync(string name);
        Task<Member> CreateMemberAsync(MemberToAdd memberToAdd);
        Task<Member> UpdateMemberDetails(Member member);
        Task<MembershipContribution> CreateMembershipAsync(MembershipContributionToAdd contributionToAdd);
    }
}
