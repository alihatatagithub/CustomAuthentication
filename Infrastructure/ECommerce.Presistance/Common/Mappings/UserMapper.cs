using AutoMapper;
using ECommerce.Contract.Mappings;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Common.Mappings
{
    public class UserMapper : IUserMapper
    {
        private readonly IMapper _mapper;

        public UserMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public User ToEntity(RegisterDTO dto)
        {
            return _mapper.Map<User>(dto);
        }

        //public TDestination Map<TDestination, TSource>(TSource source)
        //{
        //    return _mapper.Map<TDestination>(source);
        //}

    }
}
