using Microsoft.EntityFrameworkCore;
using TogoleseAssociationSystem.Application.Database;
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

        //public Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd)
        //{
        //    throw new NotImplementedException();
        //}

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

        public async Task<IEnumerable<MembershipContribution>> GetContributionsAsync()
        {
            return await dbContext.MembershipContributions.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var member = await dbContext.Members.Include(x => x.Memberships).FirstOrDefaultAsync(x => x.Id.Equals(id));
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

        public async Task<MembershipContribution> GetMembershipByIdAsync(int id)
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
