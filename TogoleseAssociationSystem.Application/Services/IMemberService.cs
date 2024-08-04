using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member> GetMemberByIdAsync(int id);
        Task<Member> GetMemberByNameAsync(string name);
        Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd);
        Task<IEnumerable<HasRole>> GetMemberRolesByIdAsync(int memberId);
        Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(int memberId);
        Task<IEnumerable<MembershipContribution>> GetContributionsAsync();
        bool SaveChanges();
    }
}
