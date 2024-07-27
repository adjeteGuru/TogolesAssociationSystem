using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private static readonly List<Member> MemberList = new List<Member>()
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
                }
        };    

        public Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return MemberList.FirstOrDefault(m => m.Id == id);
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
                var filteredMembers = new List<Member>();
                filteredMembers = MemberList
                .Where(member => member.LastName.ToLower()
                .Contains(filter.ToLower())).ToList();

                return filteredMembers;
            }

            return MemberList.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
