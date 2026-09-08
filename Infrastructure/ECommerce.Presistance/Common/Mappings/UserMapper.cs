using AutoMapper;
using ECommerce.Contract.Mappings;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Common.Mappings
{
    public class UserMapper : /*Profile, */IUserMapper
    {
        private readonly IMapper _mapper;

        public UserMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public User ToEntity(RegisterDTO dto)
        {
            //CreateMap<RegisterDTO, User>()
            //    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            return _mapper.Map<User>(dto);
        }

        //public TDestination Map<TDestination, TSource>(TSource source)
        //{
        //    return _mapper.Map<TDestination>(source);
        //}

    }
}
