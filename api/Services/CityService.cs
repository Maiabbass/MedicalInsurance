using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
               
              }
                 }
                   catch(TransactionAbortedException ex)
                 {
                   response.ErrorMessage = ex.Message;
                 }
                 catch(Exception exx){  response.ErrorMessage = exx.Message;}
                response.InsertedId  =insertedId;
             return response;
        }

        

     
        public bool Update(int Id, CityEditDTO city){
           return _unitOfWork.CityRepository.Update(Id, city);
        }

        
         public async Task<City?>Get(int Id)
     {
      return await _unitOfWork.CityRepository.Get(Id);
     }

     public async Task<IEnumerable<City>>GetAll()
        {
            return  await _unitOfWork.CityRepository.GetAll() ;
        }




        public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        _unitOfWork.CityRepository.DeleteByCityId(Id);
      
        _unitOfWork.CityRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}
    }
} 
