using Microsoft.EntityFrameworkCore;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipContribution> MembershipContributions { get; set; }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Member>().HasData(new Member
            {
                Id = Guid.NewGuid(),
                Title = "Mr",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>(),
                NextOfKin = "Brenda",
                Relationship = "sister",
                Address = "34 Bentley road",
                Postcode = "BR3 1AS",
                City = "Birmingham",
                Telephone = "07458893212"
            });

            builder.Entity<Member>().HasData(new Member
            {
                Id = Guid.NewGuid(),
                Title = "Miss",
                FirstName = "Brenda",
                LastName = "Love",
                DateOfBirth = new DateTime(1980, 11, 20),
                IsActive = true,
                IsChair = true,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>(),
                NextOfKin = "John",
                Relationship = "brother",
                Address = "34 Bentley road",
                Postcode = "BR3 1AS",
                City = "Birmingham",
                Telephone = "07126678342"
            });

            builder.Entity<Member>().HasData(new Member
            {
                Id = Guid.NewGuid(),
                Title = "Mr",
                FirstName = "Smith",
                LastName = "Joe",
                DateOfBirth = new DateTime(1970, 07, 30),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>(),
                NextOfKin = "Jenny",
                Relationship = "wife",
                Address = "5 Batman garden",
                Postcode = "NG5 9AQ",
                City = "Nottingham",
                Telephone = "07894432123"
            });
        }
    }
}
