using Microsoft.EntityFrameworkCore;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipContribution> MembershipContributions { get; set; }
        public DbSet<HasRole> HasRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
