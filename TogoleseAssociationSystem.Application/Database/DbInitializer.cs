
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
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 01),
                MemberId = Guid.Parse("64c74be2-7f55-421c-9794-094919b07d9c")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 10),
                MemberId = Guid.Parse("64c74be2-7f55-421c-9794-094919b07d9c")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 02, 12),
                MemberId = Guid.Parse("0907dd64-571f-4726-8cd2-0cc5941ac40d")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 01, 01),
                MemberId = Guid.Parse("0907dd64-571f-4726-8cd2-0cc5941ac40d")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 02),
                MemberId = Guid.Parse("d39e3ec4-b7cc-49bb-86f4-5f299be9d4a0")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 11),
                MemberId = Guid.Parse("d39e3ec4-b7cc-49bb-86f4-5f299be9d4a0")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 03),
                MemberId = Guid.Parse("07cf1222-aae6-4c68-8689-68725add708e")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 10),
                MemberId = Guid.Parse("07cf1222-aae6-4c68-8689-68725add708e")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 02, 02),
                MemberId = Guid.Parse("deadc3a9-745e-447f-82c5-6a02a138ce13")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 9, 9),
                MemberId = Guid.Parse("deadc3a9-745e-447f-82c5-6a02a138ce13")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 4, 4),
                MemberId = Guid.Parse("5de18056-565e-4c96-9227-db4386801450")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 05, 09),
                MemberId = Guid.Parse("5de18056-565e-4c96-9227-db4386801450")
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
