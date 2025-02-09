using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.API.Extensions
{
    public static class MemberExtensions
    {
        public static List<MemberRead> ConvertToDto(this IEnumerable<Member> members)
        {
            return (
                from member in members
                select new MemberRead
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Telephone = member.Telephone,
                    Address = member.Address,
                    Postcode = member.Postcode,
                    City = member.City,
                    NextOfKin = member.NextOfKin,
                    NextOfKinContact = member.NextOfKinContact,
                    Relationship = member.Relationship,
                    IsActive = member.IsActive,
                    DateOfBirth = member.DateOfBirth,
                    IsEligibleToClaim = member.IsEligibleToClaim,
                    MembershipDate = member.MembershipDate,
                    //PhotoUrl = member.PhotoUrl,
                    Title = member.Title,
                    Memberships = null,
                    Claims = null
                }).ToList();
        }

        public static List<MemberRead> ConvertToDto(this IEnumerable<Member> members, IEnumerable<MembershipContributionReadDto> membershipsDto, IEnumerable<ClaimReadDto> claimReadDtos)
        {
            return (
                from member in members
                select new MemberRead
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Telephone = member.Telephone,
                    Address = member.Address,
                    Postcode = member.Postcode,
                    City = member.City,
                    NextOfKin = member.NextOfKin,
                    NextOfKinContact = member.NextOfKinContact,
                    Relationship = member.Relationship,
                    IsActive = member.IsActive,
                    DateOfBirth = member.DateOfBirth,
                    IsEligibleToClaim = member.IsEligibleToClaim,
                    MembershipDate = member.MembershipDate,
                    //PhotoUrl = member.PhotoUrl,
                    Title = member.Title,
                    Memberships = membershipsDto.ToList(),
                    Claims = claimReadDtos.ToList()
                }).ToList();
        }

        public static List<MemberRead> ConvertToDto(this IEnumerable<Member> members, IEnumerable<ClaimReadDto> claimReadDtos)
        {
            return (
                from member in members
                select new MemberRead
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Telephone = member.Telephone,
                    Address = member.Address,
                    Postcode = member.Postcode,
                    City = member.City,
                    NextOfKin = member.NextOfKin,
                    NextOfKinContact = member.NextOfKinContact,
                    Relationship = member.Relationship,
                    IsActive = member.IsActive,
                    DateOfBirth = member.DateOfBirth,
                    IsEligibleToClaim = member.IsEligibleToClaim,
                    MembershipDate = member.MembershipDate,
                    //PhotoUrl = member.PhotoUrl,
                    Title = member.Title,
                    Memberships = null,
                    Claims = claimReadDtos.ToList()
                }).ToList();
        }

        public static List<MembershipContributionReadDto> ConvertToDto(this List<MembershipContribution> membership)
        {
            return new List<MembershipContributionReadDto>
            {
                new MembershipContributionReadDto
                {
                    Id = membership[0].Id,
                    ContributionName = membership[0].ContributionName,
                    Amount = membership[0].Amount,
                    DateOfContribution = membership[0].DateOfContribution,
                    MemberId = membership[0].MemberId
                }
            };
        }

        public static List<ClaimReadDto> ConvertToDto(this List<Claim> claims)
        {
            return new List<ClaimReadDto>
            {
                new ClaimReadDto
                {
                    Id = claims[0].Id,
                    Name = claims[0].Name,
                    Description = claims[0].Description,
                    ClaimType = claims[0].ClaimType,
                    ClaimRemain = claims[0].ClaimRemain,
                    MemberId = claims[0].MemberId,
                   NextOfKinName = claims[0].NextOfKinName,
                   NextOfKinContact = claims[0].NextOfKinContact
                }
            };
        }

        public static Member ConvertToDto(this MembershipContributionToAdd membership)
        {
            return new Member
            {
                FirstName = membership.MemberFirstName,
                LastName = membership.MemberLastName
            };
        }

        public static Member ConvertToDto(this ClaimToAdd claimToAdd)
        {
            return new Member
            {
                FirstName = claimToAdd.MemberFirstName,
                LastName = claimToAdd.MemberLastName
            };
        }

        public static MembershipContribution ConvertToDto(this MembershipContributionToAdd membership, Member member)
        {
            return new MembershipContribution
            {
                ContributionName = membership.ContributionName,
                Amount = membership.Amount,
                DateOfContribution = membership.DateOfContribution,
                MemberId = member.Id
            };
        }

        public static Claim ConvertToDto(this ClaimToAdd claim, Member member)
        {
            return new Claim
            {
                Name = claim.Name,
                Description = claim.Description,
                TotalClaimPerMember = claim.TotalClaimPerMember,
                ClaimRemain = claim.ClaimRemain,
                MemberId = member.Id,
                ClaimType = claim.ClaimType
            };
        }
    }
}
