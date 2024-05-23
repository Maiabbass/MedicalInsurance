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
      public class HospitalService : IHospitalService
    {
        private  readonly IUnitOfWork _unitOfWork;

        public HospitalService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(HospitalEditDTO hospitalEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                       
                        Hospital hospital =new Hospital()
           {
          
             Name = hospitalEditDTO.Name,
             Enabled=hospitalEditDTO.Enabled,
             Inside=hospitalEditDTO.Inside,
             CityId=hospitalEditDTO.CityId,
             
            
           };

           
         insertedId=  await _unitOfWork.HospitalRepository.Add(hospital);

             

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

        

     

         public async Task<IEnumerable<Hospital>>GetAll()
        {
            return  await _unitOfWork.HospitalRepository.GetAll() ;
        }

              public async Task<Hospital?>Get(int Id)
     {
      return await _unitOfWork.HospitalRepository.Get(Id);
     }
      public bool Update(int Id, HospitalEditDTO hospital){
           return _unitOfWork.HospitalRepository.Update(Id, hospital);
        }

        public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        _unitOfWork.HospitalRepository.DeleteByHospitalId(Id);
      
        _unitOfWork.HospitalRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}



    }
}