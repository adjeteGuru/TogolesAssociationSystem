using AutoMapper;
using TogoleseSolidarity.Application.DTOs;
using TogoleseSolidarity.Domain.Models;

namespace TogoleseSolidarity.Application.AutoMapper;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Member, MemberRead>();
        CreateMap<MemberRead, Member>();
        CreateMap<MembershipContribution, MembershipContributionReadDto>();
        CreateMap<MembershipContributionReadDto, MembershipContribution>();
        CreateMap<Claim, ClaimReadDto>();
        CreateMap<ClaimReadDto, Claim>();
        CreateMap<MemberUpdateDto, Member>();
    }
}
