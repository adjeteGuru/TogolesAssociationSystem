using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TogoleseAssociationSystem.Domain.Models;
using TogoleseAssociationSystem.Infrastructure.Database;

namespace TogoleseAssociationSystem.IntegrationTests.Helpers
{
    public class DatabaseHelper
    {
        private static SqlConnection dbConnection = 
            new SqlConnection(ConfigurationHelper.GetSetting("DefaultConnectionString"));
        
        public DatabaseHelper()
        {
            dbConnection.Open();
        }        

        public static async Task<string> GetTheMemberIdByPartialName(string name)
        {
            const string query = "SELECT Members.Id FROM Members WHERE Members.[LastName] LIKE @name";
            var result = await dbConnection.QueryAsync<Guid>(query, new { name = $"%{name}%" });

            return result.Single().ToString();
        }
           
        public static async Task RemoveTheMemberDataByPartialName(string name)
        {
            var memberId = await GetTheMemberIdByPartialName(name);

            const string query = @"DELETE FROM MembershipContributions WHERE MemberId = @memberId;
                                  DELETE FROM Members WHERE Id = @memberId;";
            await dbConnection.ExecuteAsync(query, new { memberId });
        }
 
        public static void InsertMember(Member member, Guid id, ScenarioContext scenarioContext)
        {
            InsertMemberRow(member, id, scenarioContext);
            if (member.Memberships != null && member.Memberships.Any())
            {
                InsertMembershipRows(member, id);
            }           
        }

        public static void EnsureMembershipContributionExistInDatabase(params string[] ContributionNames)
        {
            foreach (var contributionName in ContributionNames)
            {
                var membershipId = dbConnection.Query<int>("SELECT Id FROM MembershipContributions WHERE [ContributionName] = @contributionName", new { contributionName });
                if (!membershipId.Any())
                {
                    dbConnection.Query<int>("INSERT INTO MembershipContributions ([ContributionName]) OUTPUT Inserted.Id VALUES (@contributionName)", new { contributionName });
                }
            }
        }

        public static void CloseConnection()
        {
            dbConnection.Close();
            dbConnection.Dispose();
            dbConnection = null;
        }

        public static AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(ConfigurationHelper.GetSetting("DefaultConnectionString"))
                .Options;

            return new AppDbContext(options);
        }

        private static void InsertMemberRow(Member member, Guid id, SpecFlowContext scenarioContext)
        {
            const string query = @"INSERT INTO Members (Id, FirstName, Surname, Address, Postcode, City, Telephone, IsActive, IsChair, DateOfBirth, NextOfKin, Title, Relationship, MembershipDate, PhotoUrl)
                    Values (@Id, @FirstName, @Surname, @Address, @Postcode, @City, @Telephone, @IsActive, @IsChair, @DateOfBirth, @NextOfKin, @Title, @Relationship, @MembershipDate, @PhotoUrl)";

            dbConnection.Query(query, new
            {
                Id = id,
                Firstname = member.FirstName,
                LastName = member.LastName,
                Postcode = member.Postcode,
                City = member.City,
                Telephone = member.Telephone,
                IsActive = member.IsActive,
                IsChair = member.IsChair,
                DateOfBirth = member.DateOfBirth,
                NextOfKin = member.NextOfKin,
                Title = member.Title,
                Relationship = member.Relationship,
                MembershipDate = member.DateOfBirth,
                PhotoUrl = member.PhotoUrl
            });

            scenarioContext.Get<List<string>>("allCreatedMembers").Add(member.LastName);
        }

        private static void InsertMembershipRows(Member member, Guid memberId)
        {
            foreach (var membership in member.Memberships)
            {
                var membershipId = dbConnection
                    .Query<int>("SELECT Id FROM MembershipContributions WHERE [ContributionName] = @membershipName", new { membership })
                    .Single();

                dbConnection.Query("INSERT INTO MembershipContributions (Id, ContributionName, Amount, DateOfContribution, IsAnnualContribution, MemberId)" +
                    "VALUES (@Id, @ContributionName, @Amount, @DateOfContribution, @IsAnnualContribution, @MemberId)", new
                    {
                        Id = membershipId, 
                        MembershipName = membership.ContributionName,
                        Amount = membership.Amount,
                        DateOfContribution = membership.DateOfContribution,
                        IsAnnualContribution = membership.IsAnnualContribution,
                        MemberId = memberId
                    });
            }
        }      
    }
}
