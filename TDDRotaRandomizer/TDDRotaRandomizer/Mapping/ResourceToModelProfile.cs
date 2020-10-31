using AutoMapper;
using RotaRandomizer.Models;
using RotaRandomizer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CreateRotaResource, Rota>();
        }

    }
}
