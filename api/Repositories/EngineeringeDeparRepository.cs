using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EngineeringeDeparRepository : IEngineeringeDeparRepository
    {
          private readonly DataContext _dataContext;
         public EngineeringeDeparRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(EngineeringeDepar EngineeringeDepar)
        {

            EngineeringeDepar newEngD =new EngineeringeDepar()
             {
               Name=EngineeringeDepar.Name,
              
             };



            _dataContext.EngineeringeDepars.Add(newEngD);
              await _dataContext.SaveChangesAsync();

              return newEngD.Id;
        }
    }
}