using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository {get;}

        IEngineerRepository EngineerRepository{get;}


        IRelationRepository RelationRepository{get;}

        IAnnualDataRepository AnnualDataRepository{get;}

        IAgeSegmentsRepository AgeSegmentsRepository{get;}

        Task<bool> Complete();
    }
}