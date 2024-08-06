using AutoMapper;
using TogoleseAssociationSystem.Domain.DTOs;
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
                x.CreateMap<MemberToAdd, Member>()
                .ForMember(des => des.Memberships, act => act.MapFrom(src => src.Memberships))
                .ForMember(des => des.HasRoles, act => act.MapFrom(src => src.HasRoles));

                x.CreateMap<Member, MemberToAdd>()
               .ForMember(des => des.Memberships, act => act.MapFrom(src => src.Memberships))
               .ForMember(des => des.HasRoles, act => act.MapFrom(src => src.HasRoles));

                x.CreateMap<MembershipContribution, MembershipContributionToAdd>()
                  .ForMember(des => des.ContributionName, act => act.MapFrom(src => src.ContributionName));

                x.CreateMap<MembershipContributionToAdd, MembershipContribution>()
                 .ForMember(des => des.ContributionName, act => act.MapFrom(src => src.ContributionName));
            });

            return mapperConfig;
        }
    }
}
