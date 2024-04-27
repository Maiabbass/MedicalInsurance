using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;

namespace api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
        public IPersonRepository PersonRepository => new PersonRepository(_dataContext);
        public IEngineerRepository EngineerRepository=> new EngineerRepository(_dataContext);

        public IRelationRepository RelationRepository=>new RelationRepository(_dataContext);

        public IAnnualDataRepository AnnualDataRepository=>new AnnualDataRepository(_dataContext);

        public IAgeSegmentsRepository AgeSegmentsRepository=>new AgeSegmentsRepository(_dataContext);
        

        public async Task<bool> Complete()
        {
            return await _dataContext.SaveChangesAsync()>0;
        }

    }
}