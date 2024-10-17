using TogoleseAssociationSystem.Infrastructure.Database;
using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TogoleseAssociationSystem.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext dbContext;

        public MemberRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateMember(Member member)
        {
            dbContext.Members.Add(member);
            SaveChanges();
        }

        public void CreateMembership(MembershipContribution membership)
        {
            dbContext.MembershipContributions.Add(membership);
            SaveChanges();
        }

        public async Task<IEnumerable<Member>> GetAllExisitingMembersAsync()
        {
            return await dbContext.Members.ToListAsync();
        }

        public async Task<IEnumerable<MembershipContribution>> GetContributionsAsync()
        {
            return await dbContext.MembershipContributions.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            var member = await dbContext.Members.Include(x => x.Memberships).FirstOrDefaultAsync(x => x.Id.Equals(id));
            return member;
        }

        public Task<Member> GetMemberByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(Guid memberId)
        {
            throw new NotImplementedException();
        }
       
        public async Task<IEnumerable<Member>> GetMembersAsync(string? filter = null)
        {
            if (filter != null)
            {
                var filteredMembers = await dbContext.Members
                .Where(member => member.IsActive == true && member.LastName.ToLower()
                .Contains(filter.ToLower().Trim())).ToListAsync();

                return filteredMembers;
            }
            return await dbContext.Members.Where(member => member.IsActive.Equals(true)).ToListAsync();
        }

        public async Task<MembershipContribution> GetMembershipByIdAsync(Guid id)
        {
            return await dbContext.MembershipContributions.FindAsync(id);
        }

        public async Task<Member> RetrieveMember(string firsname, string lastname)
        {
            var member = await dbContext.Members
                .Where(x => x.FirstName.ToLower() == firsname.ToLower().Trim()
                && x.LastName.ToLower() == lastname.ToLower().Trim())
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
            return dbContext.Members.Any(x => x.Id == member.Id);
        }
    }
}
