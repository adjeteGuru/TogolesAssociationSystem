using Microsoft.EntityFrameworkCore;
using TogoleseAssociationSystem.Application.Database;
using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext dbContext;

        public MemberRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
        public Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MembershipContribution>> GetContributionsAsync()
        {
            return await dbContext.MembershipContributions.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var member = await dbContext.Members.Include(x => x.Memberships).FirstOrDefaultAsync(x =>x.Id.Equals(id));           
            return member;
        }

        public Task<Member> GetMemberByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HasRole>> GetMemberRolesByIdAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Member>> GetMembersAsync(string? filter = null)
        {
            if (filter != null)
            {
                var filteredMembers = await dbContext.Members
                .Where(member => member.LastName.ToLower()
                .Contains(filter.ToLower())).ToListAsync();

                return filteredMembers;
            }
            return await dbContext.Members.ToListAsync();
            //return await dbContext.Members.Select(x => new Member
            //{
            //    LastName = x.LastName,
            //    FirstName= x.FirstName,
            //    IsChair = x.IsChair,
            //    MembershipDate=x.MembershipDate,
            //    DateOfBirth=x.DateOfBirth,
            //    PhotoUrl=x.PhotoUrl,
            //    Id=x.Id,
            //    Title=x.Title
            //}).ToListAsync();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
