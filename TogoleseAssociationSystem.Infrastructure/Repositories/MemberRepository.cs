using Microsoft.EntityFrameworkCore;
using TogoleseSolidarity.Domain.Interfaces;
using TogoleseSolidarity.Domain.Models;
using TogoleseSolidarity.Infrastructure.Database;

namespace TogoleseSolidarity.Infrastructure.Repositories;

public class MemberRepository(AppDbContext dbContext) : IMemberRepository
{
    public async Task CreateClaimAsync(Claim claim)
    {
        var member = await GetMemberByIdAsync(claim.MemberId);

        if (member == null || !member.IsActive || member.TotalClaimRemain <= 0)
        {
            return;
        }

        if (claim.ClaimType == ClaimType.Death)
        {
            HandleDeathClaim(member);
        }
        else
        {
            var claimExists = ClaimExists(member, ClaimType.Disability);

            if (claimExists)
            {
                return;
            }
            member.TotalClaimRemain -= 1;
        }

        dbContext.Claims.Add(claim);
        SaveChanges();

        UpdateMember(member);
    }

    public async Task CreateMember(Member member)
    {
        await dbContext.Members.AddAsync(member);
        SaveChanges();
    }

    public async Task CreateMembership(MembershipContribution membership)
    {
        var member = await GetMemberByIdAsync(membership.MemberId);
        if (member == null)
        {
            return;
        }
        await dbContext.MembershipContributions.AddAsync(membership);
        SaveChanges();
        UpdateMember(member);
    }

    public async Task<IEnumerable<Member>> GetAllExisitingMembersAsync()
    {
        return await dbContext.Members.ToListAsync();
    }

    public async Task<Claim?> GetClaimByIdAsync(Guid id)
    {
        var claim = await dbContext.Claims.FindAsync(id);
        return claim;
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        var claims = await dbContext.Claims.ToListAsync();
        return claims;
    }

    public async Task<IEnumerable<Claim>> GetClaimsByMemberIdAsync(Guid memberId)
    {
        var member = await GetMemberByIdAsync(memberId);
        if (member == null)
        {
            return [];
        }
        var claims = await dbContext.Claims.Where(x => x.MemberId.Equals(member.Id)).ToListAsync();
        return claims;
    }

    public async Task<IEnumerable<MembershipContribution>> GetContributionsAsync()
    {
        return await dbContext.MembershipContributions.ToListAsync();
    }

    public async Task<Member?> GetMemberByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return null;
        }
        return await dbContext.Members
            .Include(x => x.Memberships)
            .Include(x => x.Claims)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(Guid memberId)
    {
        var member = await GetMemberByIdAsync(memberId);
        if (member == null)
        {
            return [];
        }
        var memberships = await dbContext.MembershipContributions.Where(x => x.MemberId.Equals(member.Id)).ToListAsync();
        return memberships;
    }

    public async Task<IEnumerable<Member>> GetMembersAsync(string? filter = null)
    {
        if (!string.IsNullOrEmpty(filter))
        {
            var filteredMembers = await dbContext.Members
            .Where(member => member.IsActive == true && member.LastName.ToLower()
            .Contains(filter.ToLower()
            .Trim())).ToListAsync();

            return filteredMembers;
        }
        return await dbContext.Members.Where(member => member.IsActive.Equals(true)).ToListAsync();
    }

    public async Task<MembershipContribution?> GetMembershipByIdAsync(Guid id)
    {
        return await dbContext.MembershipContributions.FindAsync(id);
    }

    public async Task<Member> RetrieveMember(string firsname, string lastname)
    {
        var member = await dbContext.Members
            .Where(x => x.FirstName.Equals(firsname.ToLower())
            && x.LastName.Equals(lastname.ToLower()))
            .FirstOrDefaultAsync();
        return member;
    }

    public bool SaveChanges()
    {
        return dbContext.SaveChanges() >= 0;
    }

    public void UpdateMember(Member member)
    {
        if (IsValid(member))
        {
            dbContext.Members.Update(member);
            SaveChanges();
        }
        else
        {
            return;
        }
    }

    private bool IsValid(Member member)
    {
        return dbContext.Members.Any(x => x.Id == member.Id && x.IsEligibleToClaim);
    }

    private static void HandleDeathClaim(Member member)
    {
        member.IsActive = false;
        member.TotalClaimRemain = 0;
    }

    private bool ClaimExists(Member member, ClaimType claimType)
    {
        return dbContext.Claims.Any(x => x.MemberId == member.Id && x.ClaimType == claimType);
    }
}
