using Microsoft.EntityFrameworkCore;
using TogoleseSolidarity.Domain.Models;

namespace TogoleseSolidarity.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
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
            IsEligibleToClaim = true,
            MembershipDate = DateTime.Today,
            NextOfKin = "Brenda",
            NextOfKinContact = "07459999999",
            Relationship = "sister",
            TotalClaimRemain = 2,
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
            IsEligibleToClaim = true,
            MembershipDate = DateTime.Today,
            NextOfKin = "John",
            NextOfKinContact = "07459999999",
            TotalClaimRemain = 2,
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
            IsEligibleToClaim = true,
            MembershipDate = DateTime.Today,
            NextOfKin = "Jenny",
            NextOfKinContact = "07459999999",
            Relationship = "wife",
            TotalClaimRemain = 2,
            Address = "5 Batman garden",
            Postcode = "NG5 9AQ",
            City = "Nottingham",
            Telephone = "07894432123"
        });
    }
}
