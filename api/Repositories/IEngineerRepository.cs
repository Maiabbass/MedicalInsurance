using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IEngineerRepository
    {
          Task <IEnumerable<Engineere>> GetAll();

        Task <Engineere?> Get(int Id);

        Task <int> Add (Engineere person);
    }
}