
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Database
{
    public class DbInitializer
    {
        private static Guid id1;
        private static Guid id2;
        private static Guid id3;
        private static Guid id4;
        private static Guid id5;
        private static Guid id6;
     
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();

            if (context.Members != null && context.Members.Any() && context.MembershipContributions != null && context.MembershipContributions.Any())
                return;          

            //var members = GetMembers().ToArray();
            //context.Members.AddRange(members);
            //context.SaveChanges();

            //var memberships = GetMembershipContributions().ToArray();
            //context.MembershipContributions.AddRange(memberships);
            //context.SaveChanges();
        }
        public static List<MembershipContribution> GetMembershipContributions()
        {
            var contributions = new List<MembershipContribution>();
          
            MembershipContribution membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 01),
                MemberId = Guid.Parse("f186b238-7507-4559-c386-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 10),
                MemberId = Guid.Parse("f186b238-7507-4559-c386-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 02, 12),
                MemberId = Guid.Parse("57785add-9633-4b09-c387-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 01, 01),
                MemberId = Guid.Parse("57785add-9633-4b09-c387-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 02),
                MemberId = Guid.Parse("d18ba51d-61a2-4250-c388-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 11),
                MemberId = Guid.Parse("d18ba51d-61a2-4250-c388-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 12, 03),
                MemberId = Guid.Parse("7318ea82-c866-4a93-c389-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 10, 10),
                MemberId = Guid.Parse("7318ea82-c866-4a93-c389-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 02, 02),
                MemberId = Guid.Parse("481431b2-91f3-4d1d-c38a-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 9, 9),
                MemberId = Guid.Parse("481431b2-91f3-4d1d-c38a-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Funerals",
                IsAnnualContribution = false,
                Amount = 50,
                DateOfContribution = new DateTime(2023, 4, 4),
                MemberId = Guid.Parse("9429ef04-a76f-4c0a-c38b-08dcb7e85cc4")
            };
            contributions.Add(membership);
            membership = new MembershipContribution
            {
                Id = Guid.NewGuid(),
                ContributionName = "Annuals",
                IsAnnualContribution = true,
                Amount = 60,
                DateOfContribution = new DateTime(2023, 05, 09),
                MemberId = Guid.Parse("9429ef04-a76f-4c0a-c38b-08dcb7e85cc4")
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
                    Id = id1,
                    Title ="Mr",
                    FirstName ="John",
                    LastName ="Doe",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>(),
                    NextOfKin = "Brenda",
                    Relationship = "sister",
                    Address = "34 Bentley road",
                    Postcode = "BR3 1AS",
                    City ="Birmingham",
                    Telephone ="07458893212"
                },
                new Member
                {
                    Id = id2,
                    Title ="Miss",
                    FirstName ="Brenda",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>(),
                     NextOfKin = "John",
                    Relationship = "brother",
                    Address = "34 Bentley road",
                    Postcode = "BR3 1AS",
                    City ="Birmingham",
                    Telephone ="07126678342"
                },
                new Member
                {
                    Id = id3,
                     Title ="Mr",
                    FirstName ="Smith",
                    LastName ="Joe",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>(),
                     NextOfKin = "Jenny",
                    Relationship = "wife",
                    Address = "5 Batman garden",
                    Postcode = "NG5 9AQ",
                    City ="Nottingham",
                    Telephone ="07894432123"
                },
                 new Member
                {
                    Id = id4,
                    Title ="Mrs",
                    FirstName ="Jenny",
                    LastName ="Ralph",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>(),
                     NextOfKin = "Smith",
                    Relationship = "husband",
                    Address = "5 Batman garden",
                    Postcode = "NG5 9AQ",
                    City ="Nottingham",
                    Telephone ="01156676543"
                },
                new Member
                {
                    Id = id5,
                    Title ="Mr",
                    FirstName ="Brandy",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>(),
                     NextOfKin = "Jacky",
                    Relationship = "sister",
                    Address = "12A Grandy street",
                    Postcode = "NG7 4GG",
                    City ="Nottingham",
                    Telephone ="07567789543"
                },
                new Member
                {
                    Id = id6,
                    Title ="Miss",
                    FirstName ="Jacky",
                    LastName ="Jone",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>(),
                     NextOfKin = "Brandy",
                    Relationship = "brother",
                    Address = "12A Grandy street",
                    Postcode = "NG7 4GG",
                    City ="Nottingham",
                    Telephone ="07124456765"
                }
             };

            return members;
        }
    }
}
