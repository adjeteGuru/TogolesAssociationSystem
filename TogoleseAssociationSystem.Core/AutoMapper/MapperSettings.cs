﻿using AutoMapper;
using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Core.AutoMapper
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
            });

            return mapperConfig;
        }
    }
}