using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Extensions
{
    public static class MemberExtension
    {
        public static List<MemberRead> ConverToDto(this List<Member> members, MemberRead memberRead)
        {
            return (from member in members
                    select new MemberRead
                    {
                        Id = member.Id,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        DateOfBirth = member.DateOfBirth,
                        IsActive = member.IsActive,
                        MembershipDate = member.MembershipDate,
                        IsChair = member.IsChair,
                        Title = member.Title,
                        PhotoUrl = member.PhotoUrl,
                    }).ToList();
        }
    }
}
