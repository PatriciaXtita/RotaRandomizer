using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> ListAsync();
    }
}
