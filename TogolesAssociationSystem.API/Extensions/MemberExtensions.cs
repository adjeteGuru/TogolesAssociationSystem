using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.API.Extensions
{
    public static class MemberExtensions
    {
        public static IEnumerable<MemberRead> ConvertToDto(this IEnumerable<Member> members)
        {
            return (
                from member in members
            select new MemberRead
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                IsActive = member.IsActive,
                DateOfBirth = member.DateOfBirth,
                IsChair = member.IsChair,
                MembershipDate = member.MembershipDate,
                PhotoUrl = member.PhotoUrl,
                Title = member.Title,
                Memberships = null
            }).ToList();           
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
            //convert to return mbship where memberId = member
            return new MembershipContribution
            {
                ContributionName = membership.ContributionName,
                Amount = membership.Amount,
                DateOfContribution = membership.DateOfContribution,
                IsAnnualContribution = membership.IsAnnualContribution,
                MemberId = member.Id
            };
        }

        //public static IEnumerable<MembershipContributionRead> ConvertToDto(this IEnumerable<Member> members, IEnumerable<MembershipContribution> memberships)
        //{
        //    return (from contribution in memberships
        //            join member in members
        //            on contribution.MemberId equals member.Id
        //            select new MembershipContributionRead
        //            {
        //                Id = contribution.Id, 
        //                ContributionName = contribution.ContributionName,
        //                Amount = contribution.Amount,
        //                IsAnnualContribution = contribution.IsAnnualContribution,
        //                DateOfContribution = contribution.DateOfContribution,
        //                MemberId = member.Id,
        //                MemberName = member.FirstName + " " + member.LastName
        //            }).ToList();           
        //}
    }
}
