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


        private readonly INoteRepository _noteRepository;

        public HospitalService(IUnitOfWork unitOfWork, INoteRepository noteRepository)
         {
            _unitOfWork=unitOfWork;
            _noteRepository=noteRepository;
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
             Address=hospitalEditDTO.Address,
             Email=hospitalEditDTO.Email,
             Phone=hospitalEditDTO.Phone,
             Longitude=hospitalEditDTO.Longitude,
             latitude=hospitalEditDTO.Latitude,
             Year=hospitalEditDTO.Year,
             
            
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



        

       public async Task<bool> DeleteAsync(int Id)
{
    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // Await the DeleteNotesByHospitalId
            await _unitOfWork.NoteRepository.DeleteNotesByHospitalId(Id);
            
            // Await the Delete method in HospitalRepository
            await _unitOfWork.HospitalRepository.Delete(Id);
            
            scope.Complete();
            return true;
        }
    }
    catch (Exception ex)
    {
        // Handle the exception
        return false;
    }
}




    public async Task<IEnumerable<Hospital>> GetHospitalsByCityIdAsync(int cityId){
      return await _unitOfWork.HospitalRepository.GetHospitalsByCityIdAsync(cityId);
    }


   public async Task<IEnumerable<Hospital>> GetHospitalsByYear(int year){
    return await _unitOfWork.HospitalRepository.GetHospitalsByYear(year);
   }
    }
}