using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Repositories;

namespace api.Services
{
    public class BlockService : IBlockService
    {


         private  readonly IUnitOfWork _unitOfWork;

        public BlockService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<bool> BlockPerson(string ensuranceNumber, int year , string not){
            return await _unitOfWork.BlockRepository.BlockPerson(ensuranceNumber,year , not);
        }

        public async Task<bool> UnblockPerson(string ensuranceNumber){
            return await _unitOfWork.BlockRepository.UnblockPerson(ensuranceNumber);
        }



        public async Task<List<Person>> GetAllBlockedPersons(){
            return await _unitOfWork.BlockRepository.GetAllBlockedPersons();
        }


        public async Task<bool> IsPersonBlocked(string ensuranceNumber){
            return await _unitOfWork.BlockRepository.IsPersonBlocked(ensuranceNumber);
        }
        
    }
}