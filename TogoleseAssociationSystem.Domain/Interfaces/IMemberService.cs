﻿using TogoleseSolidarity.Domain.Models;

namespace TogoleseSolidarity.Domain.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member?>> GetMembersAsync(string? filter);
        Task<Member?> GetMemberByIdAsync(Guid id);
        Task<MembershipContribution?> GetMembershipByIdAsync(Guid id);
        Task<Member?> RetrieveMember(string firsname, string lastname);      
        Task<IEnumerable<MembershipContribution?>> GetMemberContributionsByIdAsync(Guid memberId);
        Task<IEnumerable<MembershipContribution?>> GetContributionsAsync();
        Task CreateClaimAsync(Claim claim);
        void UpdateMember(Member member);
        Task CreateMembership(MembershipContribution membership);
        Task<IEnumerable<Member?>> GetAllExisitingMembersAsync();      
        Task<Claim?> GetClaimByIdAsync(Guid id);
        Task<IEnumerable<Claim?>> GetClaimsAsync();
        Task CreateMember(Member member);
        Task<IEnumerable<Claim?>> GetClaimsByMemberIdAsync(Guid id);
    }
}
