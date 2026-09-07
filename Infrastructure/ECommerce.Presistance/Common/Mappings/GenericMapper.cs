using AutoMapper;
using ECommerce.Contract.Mappings;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Common.Mappings
{
    public class GenericMapper : IGenericMapper
    {
        private readonly IMapper _mapper;

        public GenericMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public TDestination Map<TDestination, TSource>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

    }
}
