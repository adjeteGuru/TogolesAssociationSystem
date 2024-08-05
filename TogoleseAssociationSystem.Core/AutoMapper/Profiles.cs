using AutoMapper;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.Core.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Member, MemberRead>();
            CreateMap<MemberRead, Member>();
            CreateMap<MembershipContribution, MembershipContributionRead>();
            CreateMap<MembershipContributionRead, MembershipContribution>();
        }
    }
}
