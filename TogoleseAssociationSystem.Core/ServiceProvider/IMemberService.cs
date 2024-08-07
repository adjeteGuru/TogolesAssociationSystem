using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public interface IMemberService
    {
        //Task<IEnumerable<MemberRead>> GetMembersAsync(string? filter);
        //Task<MemberRead> GetMemberByIdAsync(int id);
        //Task<MemberRead> GetMemberByNameAsync(string name);      
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member> GetMemberByIdAsync(int id);
        Task<Member> GetMemberByNameAsync(string name);
        Task<Member> CreateMemberAsync(MemberToAdd memberToAdd);
        Task<Member> UpdateMemberDetails(Member member);
        Task<MembershipContribution> CreateMembershipAsync(MembershipContributionToAdd contributionToAdd);
    }
}
