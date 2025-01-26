using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Domain.Tests
{
    public class GivenClaimIsCalled
    {
        [Fact]
        public void WhenInitializeOnFirstClaim_ShouldCalculateRemainingClaim()
        {
            var claim = new Claim()
            {
                Id = Guid.NewGuid(),
                Name = "Claim 1",
                Description = "Claim 1 Description",
                CurrentClaim = 1,
                TotalClaimPerMember = 1             
            };

            //claim.ClaimRemainCalculation();

            Assert.Equal(0, claim.ClaimRemain);
        }

        [Fact]
        public void WhenInitializeOnSecondClaim_ShouldCalculateRemainingClaim()
        {
            var claim = new Claim()
            {
                Id = Guid.NewGuid(),
                Name = "Claim 2",
                Description = "Claim 2 Description",
                CurrentClaim = 2,
                TotalClaimPerMember = 3
            };

            //claim.ClaimRemainCalculation();

            Assert.Equal(1, claim.ClaimRemain);
        }

        [Fact]
        public void WhenInitializeOnThirdClaim_ShouldCalculateRemainingClaim()
        {
            var claim = new Claim()
            {
                Id = Guid.NewGuid(),
                Name = "Claim 3",
                Description = "Claim 3 Description",
                CurrentClaim = 3,
                TotalClaimPerMember = 3
            };

            //claim.ClaimRemainCalculation();

            Assert.Equal(0, claim.ClaimRemain);
        }

        [Fact]
        public void WhenInitializeOnFourthClaim_ShouldCalculateRemainingClaim()
        {
            var claim = new Claim()
            {
                Id = Guid.NewGuid(),
                Name = "Claim 4",
                Description = "Claim 4 Description",
                CurrentClaim = 4,
                TotalClaimPerMember = 3
            };

            //claim.ClaimRemainCalculation();

            Assert.Equal(0, claim.ClaimRemain);
        }
    }
}