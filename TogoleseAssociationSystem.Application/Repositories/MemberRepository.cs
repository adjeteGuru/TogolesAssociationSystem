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

        //private static readonly List<Member> MemberList = new List<Member>()
        //{
        //        new Member
        //        {
        //            Id = 1,
        //            Title ="Mr",
        //            FirstName ="John",
        //            LastName ="Doe",
        //            DateOfBirth = new DateTime(2000,01,31),
        //            IsActive=true,
        //            IsChair = false,
        //            MembershipDate = DateTime.Today,
        //            PhotoUrl = null,
        //            Memberships = new List<MembershipContribution>()
        //            {
        //                new MembershipContribution
        //                {
        //                    Id = 1,
        //                    ContributionName = "Funerals",
        //                    IsAnnualContribution = false,
        //                    Amount = 50,
        //                    DateOfContribution = new DateTime(12, 12, 2023),
        //                    MemberId = 1                            
        //                },
        //                new MembershipContribution
        //                {
        //                    Id = 2,
        //                    ContributionName = "Annuals",
        //                    IsAnnualContribution = true,
        //                    Amount = 60,
        //                    DateOfContribution = new DateTime(10, 10, 2023),
        //                    MemberId = 1
        //                }
        //            }
        //        },
        //        new Member
        //        {
        //            Id = 2,
        //            Title ="Miss",
        //            FirstName ="Brenda",
        //            LastName ="Love",
        //            DateOfBirth = new DateTime(1980,11,20),
        //            IsActive=true,
        //            IsChair = true,
        //            MembershipDate = DateTime.Today,
        //            PhotoUrl = null,
        //            Memberships = new List<MembershipContribution>()
        //            {
        //                new MembershipContribution
        //                {
        //                    Id = 1,
        //                    ContributionName = "Funerals",
        //                    IsAnnualContribution = false,
        //                    Amount = 50,
        //                    DateOfContribution = new DateTime(12, 12, 2023),
        //                    MemberId = 2
        //                },
        //                new MembershipContribution
        //                {
        //                    Id = 2,
        //                    ContributionName = "Annuals",
        //                    IsAnnualContribution = true,
        //                    Amount = 60,
        //                    DateOfContribution = new DateTime(10, 10, 2023),
        //                    MemberId = 2
        //                }
        //            }
        //        },
        //        new Member
        //        {
        //            Id = 3,
        //             Title ="Mr",
        //            FirstName ="Smith",
        //            LastName ="Joe",
        //            DateOfBirth = new DateTime(1970,07,30),
        //            IsActive=true,
        //            IsChair = false,
        //            MembershipDate = DateTime.Today,
        //            PhotoUrl = null,
        //            Memberships = new List<MembershipContribution>()
        //            {
        //                new MembershipContribution
        //                {
        //                    Id = 1,
        //                    ContributionName = "Funerals",
        //                    IsAnnualContribution = false,
        //                    Amount = 50,
        //                    DateOfContribution = new DateTime(12, 12, 2023),
        //                    MemberId = 3
        //                },
        //                new MembershipContribution
        //                {
        //                    Id = 2,
        //                    ContributionName = "Annuals",
        //                    IsAnnualContribution = true,
        //                    Amount = 60,
        //                    DateOfContribution = new DateTime(10, 10, 2023),
        //                    MemberId = 3
        //                }
        //            }
        //        },
        //         new Member
        //        {
        //            Id = 4,
        //            Title ="Mrs",
        //            FirstName ="Jenny",
        //            LastName ="Ralph",
        //            DateOfBirth = new DateTime(2000,01,31),
        //            IsActive=true,
        //            IsChair = false,
        //            MembershipDate = DateTime.Today,
        //            PhotoUrl = null,
        //            Memberships = new List<MembershipContribution>()
        //            {
        //                new MembershipContribution
        //                {
        //                    Id = 1,
        //                    ContributionName = "Funerals",
        //                    IsAnnualContribution = false,
        //                    Amount = 50,
        //                    DateOfContribution = new DateTime(12, 12, 2023),
        //                    MemberId = 4
        //                },
        //                new MembershipContribution
        //                {
        //                    Id = 2,
        //                    ContributionName = "Annuals",
        //                    IsAnnualContribution = true,
        //                    Amount = 60,
        //                    DateOfContribution = new DateTime(10, 10, 2023),
        //                    MemberId = 4
        //                }
        //            }
        //        },
        //        new Member
        //        {
        //            Id = 5,
        //            Title ="Mr",
        //            FirstName ="Brandy",
        //            LastName ="Love",
        //            DateOfBirth = new DateTime(1980,11,20),
        //            IsActive=true,
        //            IsChair = true,
        //            MembershipDate = DateTime.Today,
        //            PhotoUrl = null,
        //            Memberships = new List<MembershipContribution>()
        //            {
        //                new MembershipContribution
        //                {
        //                    Id = 1,
        //                    ContributionName = "Funerals",
        //                    IsAnnualContribution = false,
        //                    Amount = 50,
        //                    DateOfContribution = new DateTime(12, 12, 2023),
        //                    MemberId = 5
        //                },
        //                new MembershipContribution
        //                {
        //                    Id = 2,
        //                    ContributionName = "Annuals",
        //                    IsAnnualContribution = true,
        //                    Amount = 60,
        //                    DateOfContribution = new DateTime(10, 10, 2023),
        //                    MemberId = 5
        //                }
        //            }
        //        },
        //        new Member
        //        {
        //            Id = 6,
        //            Title ="Miss",
        //            FirstName ="Jacky",
        //            LastName ="Jone",
        //            DateOfBirth = new DateTime(1970,07,30),
        //            IsActive=true,
        //            IsChair = false,
        //            MembershipDate = DateTime.Today,
        //            PhotoUrl = null,
        //            Memberships = new List<MembershipContribution>()
        //            {
        //                new MembershipContribution
        //                {
        //                    Id = 1,
        //                    ContributionName = "Funerals",
        //                    IsAnnualContribution = false,
        //                    Amount = 50,
        //                    DateOfContribution = new DateTime(12, 12, 2023),
        //                    MemberId = 6
        //                },
        //                new MembershipContribution
        //                {
        //                    Id = 2,
        //                    ContributionName = "Annuals",
        //                    IsAnnualContribution = true,
        //                    Amount = 60,
        //                    DateOfContribution = new DateTime(10, 10, 2023),
        //                    MemberId = 6
        //                }
        //            }
        //        }
        //};
        public Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var member = await dbContext.Members.FindAsync(id);           
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
            //return await dbContext.Members.ToListAsync();
            return await dbContext.Members.Select(x => new Member
            {
                LastName = x.LastName,
                FirstName= x.FirstName,
                IsChair = x.IsChair,
                MembershipDate=x.MembershipDate,
                DateOfBirth=x.DateOfBirth,
                PhotoUrl=x.PhotoUrl,
                Id=x.Id,
                Title=x.Title
            }).ToListAsync();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
