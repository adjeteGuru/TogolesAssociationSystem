using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private static List<Member> members = new List<Member>()
        {
            new Member
            {
                Id = 1,
                FirstName ="John",
                LastName ="Doe",
                DateOfBirth = new DateTime(2000,01,31),
                IsActive=true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = null                
            },
            new Member
            {
                Id = 2,
                FirstName ="Brenda",
                LastName ="Love",
                DateOfBirth = new DateTime(1980,11,20),
                IsActive=true,
                IsChair = true,
                MembershipDate = DateTime.Today,
                PhotoUrl = null
            },
            new Member
            {
                Id = 3,
                FirstName ="Smith",
                LastName ="Joe",
                DateOfBirth = new DateTime(1970,07,30),
                IsActive=true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = null
            },

        };
        public MemberRepository()
        {

        }
        public Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetMemberByIdAsync(int id)
        {
            throw new NotImplementedException();
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
            var filteredMembers = members.Where(member => member.LastName.ToLower().Contains(filter.ToLower())).ToList();
            return filteredMembers;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
