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
                    Relationship = member.Relationship,
                    IsActive = member.IsActive,
                    DateOfBirth = member.DateOfBirth,
                    IsChair = member.IsChair,
                    MembershipDate = member.MembershipDate,
                    PhotoUrl = member.PhotoUrl,
                    Title = member.Title,
                    Memberships = null,
                }).ToList();
        }

        public static List<MemberRead> ConvertToDto(this IEnumerable<Member> members, IEnumerable<MembershipContributionReadDto> membershipsDto)
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
                    Relationship = member.Relationship,
                    IsActive = member.IsActive,
                    DateOfBirth = member.DateOfBirth,
                    IsChair = member.IsChair,
                    MembershipDate = member.MembershipDate,
                    PhotoUrl = member.PhotoUrl,
                    Title = member.Title,
                    Memberships = membershipsDto.ToList(),
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
                    IsAnnualContribution = membership[0].IsAnnualContribution,
                    MemberId = membership[0].MemberId
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

        public static MembershipContribution ConvertToDto(this MembershipContributionToAdd membership, Member member)
        {
            return new MembershipContribution
            {
                ContributionName = membership.ContributionName,
                Amount = membership.Amount,
                DateOfContribution = membership.DateOfContribution,
                IsAnnualContribution = membership.IsAnnualContribution,
                MemberId = member.Id
            };
        }
    }
}
