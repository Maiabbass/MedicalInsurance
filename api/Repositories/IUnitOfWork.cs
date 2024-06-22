using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

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
        IEngineeringeDeparRepository EngineeringeDeparRepository {get;}
        ISurgicalProceduresRepository SurgicalProceduresRepository {get;}

        IClimsRepository ClimsRepository {get ;}

        ISearchRepository SearchRepository{get ;}

        ISubscriberRepository SubscriberRepository{get ;}
       

        Task<bool> Complete();
       
    }
}