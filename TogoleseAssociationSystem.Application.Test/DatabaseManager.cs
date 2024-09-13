using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Diagnostics;
using TogoleseAssociationSystem.Application.Database;

namespace TogoleseAssociationSystem.Application.Test
{
    [SetUpFixture]
    public static class DatabaseManager
    {
        private const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TogoAssDBTesting;Trusted_Connection=True;";
        private static DbContextOptions<AppDbContext> dbContextOptions;


        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            using var dbContext = new AppDbContext(dbContextOptions);
            dbContext.Database.Migrate();
            DeleteAllMembers(dbContext);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            using var process = Process.Start("sqllocaldb", "stop MSSQLLocalDB");
            process.WaitForExit();
        }

        public static AppDbContext GetDbContext()
        {
            return new AppDbContext(dbContextOptions);
        }


        public static void DeleteAllMembers(AppDbContext dbContext)
        {
            dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [dbo].[HasRoles]");
            dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [dbo].[MembershipContributions]");
            dbContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].[Members]");

            dbContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].[HasRoles]");
            dbContext.Database.ExecuteSqlRaw("DELETE FROM [dbo].[MembershipContributions]");
        }
    }
}
