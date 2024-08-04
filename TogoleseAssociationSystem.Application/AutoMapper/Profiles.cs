using AutoMapper;
using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Member, MemberRead>();
            CreateMap<MemberRead, Member>();
            //CreateMap<List<Member>, List<MemberRead>>();
            CreateMap<MembershipContribution, MembershipContributionRead>();
            CreateMap<MembershipContributionRead, MembershipContribution>();
        }
    }
}
