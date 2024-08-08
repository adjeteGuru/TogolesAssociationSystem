
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Database
{
    public class DbInitializer
    {
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();

            if (context.Members != null && context.Members.Any() && context.MembershipContributions != null && context.MembershipContributions.Any())
                return;
            //if (context.MembershipContributions != null && context.MembershipContributions.Any())
            //{
            //    return;
            //}

            var members = GetMembers().ToArray();
            context.Members.AddRange(members);
            context.SaveChanges();

            var memberships = GetMembershipContributions().ToArray();
            context.MembershipContributions.AddRange(memberships);
            context.SaveChanges();
        }
        public static List<MembershipContribution> GetMembershipContributions()
        {
            var contributions = new List<MembershipContribution>
            { };
            MembershipContribution membership = new MembershipContribution
            {
                //Id = 1,
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 01),
                //MemberId = 1
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 2,
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 10),
                //MemberId = 1
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 3,
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 02, 12),
                //MemberId = 2
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 4,
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 01, 01),
                //MemberId = 2
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 5,
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 02),
                //MemberId = 3
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 6,
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 11),
                //MemberId = 3
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 7,
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 03),
                //MemberId = 4
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 8,
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 10),
                //MemberId = 4
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 9,
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 02, 02),
                //MemberId = 5
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 10,
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 9, 9),
                //MemberId = 5
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 11,
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 4, 4),
                //MemberId = 6
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                //Id = 12,
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 05, 09),
                //MemberId = 6
            };
            contributions.Add(membership);

            return contributions;
        }

            public static List<Member> GetMembers()
        {
            var members = new List<Member>
             {
                  new Member
                {
                    Id = Guid.NewGuid(),
                    Title ="Mr",
                    FirstName ="John",
                    LastName ="Doe",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    Title ="Miss",
                    FirstName ="Brenda",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                     Title ="Mr",
                    FirstName ="Smith",
                    LastName ="Joe",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                 new Member
                {
                    Id = Guid.NewGuid(),
                    Title ="Mrs",
                    FirstName ="Jenny",
                    LastName ="Ralph",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    Title ="Mr",
                    FirstName ="Brandy",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                    Id = Guid.NewGuid(),
                    Title ="Miss",
                    FirstName ="Jacky",
                    LastName ="Jone",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                }
             };

            return members;
        }
    }
}
