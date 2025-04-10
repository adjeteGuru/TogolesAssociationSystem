﻿using TogoleseSolidarity.Domain.Interfaces;
using TogoleseSolidarity.Domain.Interfaces;
using TogoleseSolidarity.Domain.Models;

namespace TogoleseSolidarity.Application.Services;

public class MemberService(IMemberRepository memberRepository) : IMemberService
{
    public async Task CreateMember(Member member)
    {
        await memberRepository.CreateMember(member);
    }

    public async Task CreateMembership(MembershipContribution membership)
    {
        await memberRepository.CreateMembership(membership);           
    }

    public async Task<IEnumerable<MembershipContribution?>> GetContributionsAsync()
    {
        return await memberRepository.GetContributionsAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(Guid id)
    {
        var member = await memberRepository.GetMemberByIdAsync(id);

        if (member == null)
        {
            throw new Exception($"member with id:{id} is not found!");
        }
        return member;
    }

    public async Task<Member?> RetrieveMember(string firsname, string lastname)
    {
        var member = await memberRepository.RetrieveMember(firsname, lastname);
        if (member == null)
        {
            throw new Exception($"member with id:{member.FirstName} is not found!");
        }
        return member;
    }

    public Task<IEnumerable<MembershipContribution?>> GetMemberContributionsByIdAsync(Guid memberId)
    {
        throw new NotImplementedException();
    }
   
    public async Task<IEnumerable<Member?>> GetMembersAsync(string? filter = null)
    {
        var searchMembers = new List<Member>();

        var members = await memberRepository.GetMembersAsync(filter);
      
        if (members.Any())
        {
            if (!string.IsNullOrEmpty(filter))
            {
                searchMembers = members
               .Where(x => x.LastName.ToLower() == filter.ToLower())
               .ToList();
                return searchMembers;
            }
            searchMembers = members.ToList();
        }

      return searchMembers;
    }

    public async Task<MembershipContribution?> GetMembershipByIdAsync(Guid id)
    {
       return await memberRepository.GetMembershipByIdAsync(id);
    }

    public void UpdateMember(Member member)
    {
        memberRepository.UpdateMember(member);
    }

    public async Task<IEnumerable<Member?>> GetAllExisitingMembersAsync()
    {
        return await memberRepository.GetAllExisitingMembersAsync();
    }

    public async Task<Claim?> GetClaimByIdAsync(Guid id)
    {
        return await memberRepository.GetClaimByIdAsync(id);
    }

    public async Task<IEnumerable<Claim?>> GetClaimsAsync()
    {
        return await memberRepository.GetClaimsAsync();
    }

    public Task CreateClaimAsync(Claim claim)
    {
        return memberRepository.CreateClaimAsync(claim);
    }

    public async Task<IEnumerable<Claim?>> GetClaimsByMemberIdAsync(Guid id)
    {
        return await memberRepository.GetClaimsByMemberIdAsync(id);
    }
}
