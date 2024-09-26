using AutoMapper;
using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Member, MemberRead>();
            CreateMap<MemberRead, Member>();           
            CreateMap<MembershipContribution, MembershipContributionReadDto>();           
            CreateMap<MembershipContributionReadDto, MembershipContribution>();
            CreateMap<MemberUpdateDto, Member>();
        }
    }
}
