using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembersAsync(string? filter);
        Task<Member> GetMemberByIdAsync(int id);
        Task<MembershipContribution> GetMembershipByIdAsync(int id);
        //Task<Member> GetMemberByNameAsync(string name);
        Task<Member> RetrieveMember(string firsname, string lastname);
        //Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd);
        Task<IEnumerable<HasRole>> GetMemberRolesByIdAsync(int memberId);
        Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(int memberId);
        Task<IEnumerable<MembershipContribution>> GetContributionsAsync();
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void CreateMembership(MembershipContribution membership);
        bool SaveChanges();
    }
}
