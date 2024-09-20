using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IEnduranceRatioRepository
    {
       Task<bool> Add_EnduranceRatio(List<EnduranceRatio> Ratio); 
       Task Update_EnduranceRatios(List<EnduranceRatio> enduranceRatios);
       public void Delete_Ratio(int year);
    }
}