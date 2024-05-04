using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;

namespace api.Services
{
    public class CityService : ICityService
    {
        private  readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(CityEditDTO cityEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                        // add city data 
                        City city =new City()
           {
          
             Name = cityEditDTO.Name,
             
            
           };

           
         insertedId=  await _unitOfWork.CityRepository.Add(city);

             

                scope.Complete();
                } // using 
               } // try 
              catch(TransactionAbortedException ex)
            {

                response.ErrorMessage = ex.Message;
                 
            }

            response.InsertedId  =insertedId;
             return response;
            
        }

        public Task<IEnumerable<City>> GetAll()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<City>> ICityService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
} 
