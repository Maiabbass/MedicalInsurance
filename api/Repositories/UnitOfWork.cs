using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Services;

namespace api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _dataContext;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DataContext dataContext , IServiceProvider serviceProvider)
        {
            _dataContext = dataContext;
            _serviceProvider = serviceProvider;
            
        }

        private IAnnualDataService annualDataService => _serviceProvider.GetRequiredService<IAnnualDataService>();

        



        public IPersonRepository PersonRepository => new PersonRepository(_dataContext,annualDataService);
        public IEngineerRepository EngineerRepository=> new EngineerRepository(_dataContext, annualDataService);

        public IRelationRepository RelationRepository=>new RelationRepository(_dataContext);

        public IAnnualDataRepository AnnualDataRepository=>new AnnualDataRepository(_dataContext);

        public IAgeSegmentsRepository AgeSegmentsRepository=>new AgeSegmentsRepository(_dataContext);
        
        public ICityRepository CityRepository=>new CityRepository(_dataContext);
        public IHospitalRepository HospitalRepository=>new HospitalRepository(_dataContext);
        public IWorkplaceRepository WorkplaceRepository=>new WorkplaceRepository(_dataContext);
        public IEngineeringUnitsRepository EngineeringUnitsRepository=>new EngineeringUnitsRepository(_dataContext);
        public IEngineeringeDeparRepository EngineeringeDeparRepository => new EngineeringeDeparRepository(_dataContext);

        public ISurgicalProceduresRepository SurgicalProceduresRepository => new SurgicalProceduresRepository(_dataContext);
 
        public IClimsRepository ClimsRepository => new ClimsRepository(_dataContext);

        public ISearchRepository  SearchRepository => new SearchRepository(_dataContext);

        public IUploadRepository UploadRepository => new UploadRepository(_dataContext);
        public ISpecializationRepository SpecializationRepository=> new SpecializationRepository(_dataContext);

        public IQuiriesRepositories QuiriesRepositories => new QuiriesRepositories(_dataContext);

        public IRecoveredRepository RecoveredRepository => new RecoveredRepository(_dataContext);

        

        public Task<bool> Complete()
        {
            throw new NotImplementedException();
        }
    }
}