
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

            if (context.Members != null && context.Members.Any())
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
            {
                new MembershipContribution
                {
                    Id = 1,
                    ContributionName = "Funerals",
                    IsAnnualContribution = false,
                    Amount = 50,
                    DateOfContribution = new DateTime(1, 12, 2023),
                    MemberId = 1
                },
                new MembershipContribution
                {
                    Id = 2,
                    ContributionName = "Annuals",
                    IsAnnualContribution = true,
                    Amount = 60,
                    DateOfContribution = new DateTime(10, 10, 2023),
                    MemberId = 1
                },
                 new MembershipContribution
                {
                    Id = 3,
                    ContributionName = "Funerals",
                    IsAnnualContribution = false,
                    Amount = 50,
                    DateOfContribution = new DateTime(12, 02, 2023),
                    MemberId = 2
                },
                new MembershipContribution
                {
                    Id = 4,
                    ContributionName = "Annuals",
                    IsAnnualContribution = true,
                    Amount = 60,
                    DateOfContribution = new DateTime(01, 01, 2023),
                    MemberId = 2
                },
                new MembershipContribution
                {
                    Id = 5,
                    ContributionName = "Funerals",
                    IsAnnualContribution = false,
                    Amount = 50,
                    DateOfContribution = new DateTime(12, 02, 2023),
                    MemberId = 3
                },
                new MembershipContribution
                {
                    Id = 6,
                    ContributionName = "Annuals",
                    IsAnnualContribution = true,
                    Amount = 60,
                    DateOfContribution = new DateTime(10, 11, 2023),
                    MemberId = 3
                },
                 new MembershipContribution
                 {
                     Id = 7,
                     ContributionName = "Funerals",
                     IsAnnualContribution = false,
                     Amount = 50,
                     DateOfContribution = new DateTime(12, 03, 2023),
                     MemberId = 4
                 },
                 new MembershipContribution
                 {
                     Id = 8,
                     ContributionName = "Annuals",
                     IsAnnualContribution = true,
                     Amount = 60,
                     DateOfContribution = new DateTime(10, 10, 2023),
                     MemberId = 4
                 },
                new MembershipContribution
                {
                    Id = 9,
                    ContributionName = "Funerals",
                    IsAnnualContribution = false,
                    Amount = 50,
                    DateOfContribution = new DateTime(02, 02, 2023),
                    MemberId = 5
                },
                new MembershipContribution
                {
                    Id = 10,
                    ContributionName = "Annuals",
                    IsAnnualContribution = true,
                    Amount = 60,
                    DateOfContribution = new DateTime(10, 10, 2023),
                    MemberId = 5
                },
                new MembershipContribution
                {
                    Id = 11,
                    ContributionName = "Funerals",
                    IsAnnualContribution = false,
                    Amount = 50,
                    DateOfContribution = new DateTime(12, 12, 2023),
                    MemberId = 6
                },
                new MembershipContribution
                {
                    Id = 12,
                    ContributionName = "Annuals",
                    IsAnnualContribution = true,
                    Amount = 60,
                    DateOfContribution = new DateTime(05, 09, 2023),
                    MemberId = 6
                }
            };

            return contributions;
        }

            public static List<Member> GetMembers()
        {
            var members = new List<Member>
             {
                  new Member
                {
                    //Id = 1,
                    Title ="Mr",
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
                    //Id = 2,
                    Title ="Miss",
                    FirstName ="Brenda",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null,
                    //Memberships = new List<MembershipContribution>()
                    //{
                        
                    //}
                },
                new Member
                {
                    //Id = 3,
                     Title ="Mr",
                    FirstName ="Smith",
                    LastName ="Joe",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null,
                    //Memberships = new List<MembershipContribution>()
                    //{
                        
                    //}
                },
                 new Member
                {
                    //Id = 4,
                    Title ="Mrs",
                    FirstName ="Jenny",
                    LastName ="Ralph",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null,
                    //Memberships = new List<MembershipContribution>()
                    //{
                       
                    //}
                },
                new Member
                {
                    //Id = 5,
                    Title ="Mr",
                    FirstName ="Brandy",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null,
                    //Memberships = new List<MembershipContribution>()
                    //{
                        
                    //}
                },
                new Member
                {
                    //Id = 6,
                    Title ="Miss",
                    FirstName ="Jacky",
                    LastName ="Jone",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null,
                    //Memberships = new List<MembershipContribution>()
                    //{
                        
                    //}
                }
             };

            return members;
        }
    }
}
