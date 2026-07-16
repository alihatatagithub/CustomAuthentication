using AutoMapper;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<User, RegisterDTO>();

            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
