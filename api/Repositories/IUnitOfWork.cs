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
        ICityRepository CityRepository { get; }
        IHospitalRepository HospitalRepository { get; }

        IWorkplaceRepository WorkplaceRepository{get;}

        IEngineeringUnitsRepository EngineeringUnitsRepository{get;}
        IEngineeringeDeparRepositry EngineeringeDeparRepositry {get;}
        ISurgicalProceduresRepository SurgicalProceduresRepository {get;}
       

        Task<bool> Complete();
    }
}