using AutoMapper;
using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.AutoMapper
{
    public static class MapperSettings
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            //source --> destination
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.CreateMap<Member, MemberRead>()
               .ForMember(des => des.Memberships, act => act.MapFrom(src => src.Memberships))
               .ForMember(des => des.Claims, act => act.MapFrom(src => src.Claims));

                x.CreateMap<MemberRead, Member>()
               .ForMember(des => des.Memberships, act => act.MapFrom(src => src.Memberships))
               .ForMember(des => des.Claims, act => act.MapFrom(src => src.Claims));


                x.CreateMap<MemberToAdd, Member>()
                .ForMember(des => des.Memberships, act => act.MapFrom(src => src.Memberships))
                .ForMember(des => des.Claims, act => act.MapFrom(src => src.Claims));

                x.CreateMap<Member, MemberToAdd>()
               .ForMember(des => des.Memberships, act => act.MapFrom(src => src.Memberships))
               .ForMember(des => des.Claims, act => act.MapFrom(src => src.Claims));
            
                x.CreateMap<MembershipContribution, MembershipContributionToAdd>()
                  .ForMember(des => des.ContributionName, act => act.MapFrom(src => src.ContributionName));

                x.CreateMap<MembershipContributionToAdd, MembershipContribution>()
                 .ForMember(des => des.ContributionName, act => act.MapFrom(src => src.ContributionName));

                x.CreateMap<MembershipContribution, MembershipContributionReadDto>()
                 .ForMember(des => des.ContributionName, act => act.MapFrom(src => src.ContributionName));

                x.CreateMap<MembershipContributionReadDto, MembershipContribution>()
                .ForMember(des => des.ContributionName, act => act.MapFrom(src => src.ContributionName));
            });

            return mapperConfig;
        }
    }
}
