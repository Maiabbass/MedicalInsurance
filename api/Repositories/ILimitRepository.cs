using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface ILimitRepository
    {
        Task<bool> Add_Limits(List<Limits> limits);
        Task Update_Limits(List<Limits> limits);

         public void Delete_Limite(int year);
    }
}