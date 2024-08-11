using Microsoft.EntityFrameworkCore;
using TogoleseAssociationSystem.Application.Database;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.IntegrationTests.Helpers
{
    public class DatabaseHelper
    {
        private readonly AppDbContext appDbContext;

        public DatabaseHelper()
        {
            var connectionString = ConfigurationHelper.GetConfigurationByName("ConnectionStrings:DefaultConnectionString");
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options;
            appDbContext = new AppDbContext(options);
        }

        public async Task<List<Member>> GetMembersAsync(Table table)
        {
            var members = new List<Member>();
            foreach (var row in table.Rows)
            {
                var member = new Member
                {
                    Id = Guid.Parse(row["id"]),
                    Address = row["address"],
                    FirstName = row["firstname"],
                    LastName = row["lastname"],
                    Postcode = row["postcode"],
                    City = row["city"],
                    Telephone = row["telephone"],
                    IsActive = bool.Parse(row["isActive"]),
                    IsChair = bool.Parse(row["isChair"]),
                    DateOfBirth = DateTime.Parse(row["dateOfBirth"]),
                    NextOfKin = row["nextofkin"],
                    Title = row["title"],
                    Relationship = row["relationship"],
                    MembershipDate = DateTime.Parse(row["membershipDate"]),
                    PhotoUrl = Array.Empty<byte>(),
                };

                members.Add(member);
            }

            await appDbContext.Members.AddRangeAsync(members);
            await appDbContext.SaveChangesAsync();
            return members;
        }

        public async Task<List<Member>> CreateMembersAsync(Table table)
        {
            var members = new List<Member>();
            foreach (var row in table.Rows)
            {
                var member = new Member
                {
                    Id = Guid.Parse(row["id"]),
                    Address = row["address"],
                    FirstName = row["firstname"],
                    LastName = row["lastname"],
                    Postcode = row["postcode"],
                    City = row["city"],
                    Telephone = row["telephone"],
                    IsActive = bool.Parse(row["isActive"]),
                    IsChair = bool.Parse(row["isChair"]),
                    DateOfBirth = DateTime.Parse(row["dateOfBirth"]),
                    NextOfKin = row["nextofkin"],
                    Title = row["title"],
                    Relationship = row["relationship"],
                    MembershipDate = DateTime.Parse(row["membershipDate"]),
                    PhotoUrl = Array.Empty<byte>(),
                };

                members.Add(member);
            }

            await appDbContext.Members.AddRangeAsync(members);
            await appDbContext.SaveChangesAsync();
            return members;
        }
    }
}
