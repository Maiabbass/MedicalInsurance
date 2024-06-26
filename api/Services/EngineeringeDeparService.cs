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
    public class EngineeringeDeparService :IEngineeringeDeparService
    {

        private  readonly IUnitOfWork _unitOfWork;

        public EngineeringeDeparService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(EngineeringeDeparEditDTO EngineeringeDeparEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                        // add city data 
                        EngineeringeDepar newD =new EngineeringeDepar()
           {
          
             Name = EngineeringeDeparEditDTO.Name,
             
            
           };

           
         insertedId=  await _unitOfWork.EngineeringeDeparRepository.Add(newD);

             

                scope.Complete();
                } // using 
               } // try 
              catch(TransactionAbortedException ex)
            {

                response.ErrorMessage = ex.Message;
                 
            }
             catch(Exception exx){  response.ErrorMessage = exx.Message;}

            response.InsertedId  =insertedId;
             return response;
            
        }

           public async Task<EngineeringeDepar?>Get(int Id)
     {
      return await _unitOfWork.EngineeringeDeparRepository.Get(Id);
     }

     public async Task<IEnumerable<EngineeringeDepar>>GetAll()
        {
            return  await _unitOfWork.EngineeringeDeparRepository.GetAll() ;
        }

      public bool Update(int Id, EngineeringeDeparEditDTO eng){
           return _unitOfWork.EngineeringeDeparRepository.Update(Id, eng);
        }

         public bool Delete(int id)
{
    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            _unitOfWork.EngineeringeDeparRepository.DeleteByEngineeringeDeparId(id);
            _unitOfWork.EngineeringeDeparRepository.Delete(id);
            scope.Complete();
            return true;
        }
    }
    catch (TransactionAbortedException ex)
    {
        // تسجيل الخطأ
        Console.WriteLine($"TransactionAbortedException: {ex.Message}");
        return false;
    }
    catch (Exception ex)
    {
        // تسجيل الخطأ
        Console.WriteLine($"Exception: {ex.Message}");
        return false;
    }
}

        
        
    }
}