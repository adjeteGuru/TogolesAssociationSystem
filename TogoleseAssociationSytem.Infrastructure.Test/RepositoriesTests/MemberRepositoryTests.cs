using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TogoleseSolidarity.Domain.Models;
using TogoleseSolidarity.Infrastructure.Repositories;
using TogoleseSolidarity.Infrastructure.Database;

namespace TogoleseSolidarity.Infrastructure.Test.RepositoriesTests;

[TestFixture]
public class MemberRepositoryTests
{
    private AppDbContext dbContext;
    private Member expectedResult;
    private List<Member> members;

    [SetUp]
    public void Setup()
    {
        expectedResult = new Member
        {
            Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(2000, 01, 31),
            IsActive = true,
            IsEligibleToClaim = false,
            MembershipDate = DateTime.Today,
            //PhotoUrl = Array.Empty<byte>()
        };

        members = new List<Member>
        {
            new Member
            {
                Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
                FirstName ="John",
                LastName ="Doe",
                DateOfBirth = new DateTime(2000,01,31),
                IsActive=true,
                IsEligibleToClaim = false,
                MembershipDate = DateTime.Today,
                //PhotoUrl = Array.Empty<byte>()
            },
            new Member
            {
               Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
                FirstName ="Brenda",
                LastName ="Dovevi",
                DateOfBirth = new DateTime(1980,11,20),
                IsActive=true,
                IsEligibleToClaim = true,
                MembershipDate = DateTime.Today,
                //PhotoUrl = Array.Empty<byte>()
            },
            new Member
            {
                Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
                FirstName ="Smith",
                LastName ="Joe",
                DateOfBirth = new DateTime(1970,07,30),
                IsActive=true,
                IsEligibleToClaim = false,
                MembershipDate = DateTime.Today,
                //PhotoUrl = Array.Empty<byte>()
            },
        };
    }

    [TearDown]
    public void Teardown()
    {
        dbContext?.Dispose();
    }

    #region
    [Test]
    public async Task GetMembersAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
    {
        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        Func<Task> func = async () => await systemUnderTest.GetMembersAsync(null);
        await func.Should().NotThrowAsync();
    }

    [Test]
    public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedCountIsReturned()
    {
        var members = CreateTestDataList();

        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        var result = await systemUnderTest.GetMembersAsync(null);
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
    }

    [Test]
    public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedResultIsReturned()
    {
        var members = CreateTestDataList();

        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);


        var result = await systemUnderTest.GetMembersAsync(null);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(members);
    }

    [Test]
    public async Task GetMembersAsync_WhenIsInvokedWithFilterSupplied_ThenTheExpectedResultIsReturned()
    {
        var searchMembers = new List<Member>
        {
            expectedResult
        };

        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        var result = await systemUnderTest.GetMembersAsync("Doe");
        result.Should().BeEquivalentTo(searchMembers);

    }
    #endregion

    #region
    [Test]
    public async Task GetMemberByIdAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
    {
        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(Guid.Empty);
        await func.Should().NotThrowAsync();
    }

    [Test]
    public async Task GetMemberByIdAsync_WhenIsInvokedWithAnInvalidId_ThenTheExpectedResultIsReturned()
    {
        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        var result = await systemUnderTest.GetMemberByIdAsync(Guid.Empty);
        result.Should().BeNull();
    }

    [Test]
    public async Task GetMemberByIdAsync_WhenIsInvokedWithAValidId_ThenTheExpectedResultIsReturned()
    {
        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        var result = await systemUnderTest.GetMemberByIdAsync(Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"));
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

    }
    #endregion

    #region 
    [Test]
    public async Task CreateClaimAsync_ShouldAddClaim_WhenMemberIsValid()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 2, Claims = [] };
        var claim = new Claim { MemberId = memberId, ClaimType = ClaimType.Disability };
        
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);

        member.Claims.Should().Contain(x => x.MemberId == member.Id);
        member.Claims.Should().HaveCount(1);
    }

    [Test]
    public async Task CreateClaimAsync_ShouldNotAddClaim_WhenMemberIsInvalid()
    {
        var memberId = Guid.Empty;
        var claim = new Claim { MemberId = memberId, ClaimType = ClaimType.Disability };
        await using var context = GetContext();
        context.Members.AddRange(members);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);
        members.First().Claims.Should().HaveCount(0);
    }

    [Test]
    public async Task CreateClaimAsync_ShouldNotAddClaim_WhenMemberIsNull()
    {
        var claim = new Claim { MemberId = Guid.NewGuid(), ClaimType = ClaimType.Disability };
        await using var context = GetContext();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);
        claim.MemberId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public async Task CreateClaimAsync_WhenClaimTypeIsDisability_ThenMemberTotalClaimRemainIsDecreasedByOne()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 2, Claims = [] };
        var claim = new Claim { MemberId = memberId, ClaimType = ClaimType.Disability };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);
        member.TotalClaimRemain.Should().Be(1);
    }

    [Test]
    public async Task CreateClaimAsync_WhenClaimTypeIsDisabilityAgain_ShouldNotAddClaim()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 1, Claims = [ new Claim { ClaimType = ClaimType.Disability}] };
        var anotherSameClaimType = new Claim { MemberId = memberId, ClaimType = ClaimType.Disability };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(anotherSameClaimType);
        member.Claims.Should().HaveCount(1);
    }


    [Test]
    public async Task CreateClaimAsync_WhenClaimTypeIsDisabilityAgain_ThenMemberTotalClaimRemainIsNotDecreased()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 1, Claims = [new Claim { ClaimType = ClaimType.Disability }] };
        var anotherSameClaimType = new Claim { MemberId = memberId, ClaimType = ClaimType.Disability };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(anotherSameClaimType);
        member.TotalClaimRemain.Should().Be(1);
    }


    [Test]
    public async Task CreateClaimAsync_WhenClaimTypeIsDisabilityFirstTime_ThenMemberTotalClaimRemainIsDecreasedByOne()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 2, Claims = [] };
        var claim = new Claim { MemberId = memberId, ClaimType = ClaimType.Disability };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);
        member.TotalClaimRemain.Should().Be(1);
    }


    [Test]
    public async Task CreateClaimAsync_WhenClaimTypeIsDeath_ThenMemberIsActiveIsFalse()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 2, Claims = [] };
        var claim = new Claim { MemberId = memberId, ClaimType = ClaimType.Death };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);
        member.IsActive.Should().BeFalse();
    }

    [Test]
    public async Task CreateClaimAsync_WhenClaimTypeIsDeath_ThenMemberTotalClaimRemainIsZero()
    {
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, IsActive = true, TotalClaimRemain = 2, Claims = [] };
        var claim = new Claim { MemberId = memberId, ClaimType = ClaimType.Death };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateClaimAsync(claim);
        member.TotalClaimRemain.Should().Be(0);
    }
    #endregion

    #region
    [Test]
    public async Task CreateMember_ShouldAddMember()
    {
        var member = new Member { Id = Guid.NewGuid(), FirstName = "test" };
        await using var context = GetContext();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateMember(member);

        member.IsActive.Should().BeTrue();
        member.IsEligibleToClaim.Should().BeTrue();
    }
    #endregion

    #region 
    [Test]
    public async Task CreateMembership_ShouldAddMembership()
    {
        var memberId = Guid.NewGuid();
        var membership = new MembershipContribution { MemberId = memberId, ContributionName = "test" };
        var member = new Member { Id = memberId, IsActive = true, IsEligibleToClaim = true, Memberships = [] };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateMembership(membership);

        member.Memberships.Should().Contain(x => x.MemberId == member.Id);
        member.Memberships.Should().HaveCount(1);

    }

    [Test]
    public async Task CreateMembership_ShouldNotAddMembership_WhenMemberIsInvalid()
    {
        var memberId = Guid.Empty;
        var membership = new MembershipContribution { MemberId = memberId, ContributionName = "test" };
        var member = new Member { Id = memberId, IsActive = true, IsEligibleToClaim = true, Memberships = [] };
        await using var context = GetContext();
        context.Members.AddRange(member);
        await context.SaveChangesAsync();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateMembership(membership);

        member.Memberships.Should().HaveCount(0);
    }

    [Test]
    public async Task CreateMembership_ShouldNotAddMembership_WhenMemberIsNull()
    {
        var membership = new MembershipContribution { MemberId = Guid.NewGuid(), ContributionName = "test" };
        await using var context = GetContext();
        var systemUnderTest = new MemberRepository(context);

        await systemUnderTest.CreateMembership(membership);
        membership.MemberId.Should().NotBe(Guid.Empty);
    }
    #endregion

    //#region
    //[Test]
    //public async Task GetAllExisitingMembersAsync_ShouldReturnMembers()
    //{
    //    // Arrange
    //    var members = new List<Member> { new Member { Id = Guid.NewGuid() } };
    //    //mockDbContext.Setup(db => db.Members.ToListAsync()).ReturnsAsync(members);

    //    // Act
    //    var result = await memberRepository.GetAllExisitingMembersAsync();

    //    // Assert
    //    Assert.Equals(members, result);
    //}
    //#endregion

    //#region
    //[Test]
    //public async Task GetClaimByIdAsync_ShouldReturnClaim()
    //{
    //    // Arrange
    //    var claimId = Guid.NewGuid();
    //    var claim = new Claim { Id = claimId };
    //    mockDbContext.Setup(db => db.Claims.FindAsync(claimId)).ReturnsAsync(claim);

    //    // Act
    //    var result = await memberRepository.GetClaimByIdAsync(claimId);

    //    // Assert
    //    Assert.Equals(claim, result);
    //}

    //[Test]
    //public async Task GetClaimsAsync_ShouldReturnClaims()
    //{
    //    // Arrange
    //    var claims = new List<Claim> { new Claim { Id = Guid.NewGuid() } };
    //    mockDbContext.Setup(db => db.Claims.ToListAsync()).ReturnsAsync(claims);

    //    // Act
    //    var result = await memberRepository.GetClaimsAsync();

    //    // Assert
    //    Assert.Equals(claims, result);
    //}
    //#endregion

    //#region
    //[Test]
    //public async Task GetClaimsByMemberIdAsync_ShouldReturnClaims_WhenMemberExists()
    //{
    //    // Arrange
    //    var memberId = Guid.NewGuid();
    //    var member = new Member { Id = memberId };
    //    var claims = new List<Claim> { new Claim { MemberId = memberId } };

    //    mockDbContext.Setup(db => db.Members.FindAsync(memberId)).ReturnsAsync(member);
    //    mockDbContext.Setup(db => db.Claims.Where(c => c.MemberId == memberId).ToListAsync()).ReturnsAsync(claims);

    //    // Act
    //    var result = await memberRepository.GetClaimsByMemberIdAsync(memberId);

    //    // Assert
    //    Assert.Equals(claims, result);
    //}
    //#endregion

    //#region
    //[Test]
    //public async Task GetContributionsAsync_ShouldReturnContributions()
    //{
    //    // Arrange
    //    var contributions = new List<MembershipContribution> { new MembershipContribution { Id = Guid.NewGuid() } };
    //    mockDbContext.Setup(db => db.MembershipContributions.ToListAsync()).ReturnsAsync(contributions);

    //    // Act
    //    var result = await memberRepository.GetContributionsAsync();

    //    // Assert
    //    Assert.Equals(contributions, result);
    //}
    //#endregion

    private AppDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"WebApp_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(options);
    }

    private List<Member> CreateTestDataList()
    {
        var memberList = new List<Member>();

        Member member = new Member
        {
            Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(2000, 01, 31),
            IsActive = true,
            IsEligibleToClaim = false,
            MembershipDate = DateTime.Today,
            //PhotoUrl = Array.Empty<byte>()
        };
        memberList.Add(member);

        member = new Member
        {
            Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
            FirstName = "Brenda",
            LastName = "Love",
            DateOfBirth = new DateTime(1980, 11, 20),
            IsActive = true,
            IsEligibleToClaim = true,
            MembershipDate = DateTime.Today,
            //PhotoUrl = Array.Empty<byte>()
        };
        memberList.Add(member);

        member = new Member
        {
            Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
            FirstName = "Smith",
            LastName = "Joe",
            DateOfBirth = new DateTime(1970, 07, 30),
            IsActive = true,
            IsEligibleToClaim = false,
            MembershipDate = DateTime.Today,
            //PhotoUrl = Array.Empty<byte>()
        };
        memberList.Add(member);

        return memberList;
    }
}