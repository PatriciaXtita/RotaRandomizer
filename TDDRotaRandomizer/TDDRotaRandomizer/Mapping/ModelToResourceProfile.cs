using AutoMapper;
using RotaRandomizer.Models;
using RotaRandomizer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Employee, EmployeeResource>();
            CreateMap<Rota, RotaResource>();
            CreateMap<Shift, ShiftResource>().ForMember(se => se.ShiftEmployee, opt => opt.MapFrom(src => src.ShiftEmployee.Name));
        }
    }
}
