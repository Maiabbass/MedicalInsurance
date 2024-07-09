using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IPdfService
    {
        public byte[] GeneratePdfEngUnits(IEnumerable<EngineeringUnits> units);
       public byte[] GeneratePdfWorkPlace(IEnumerable<WorkPlace> workPlaces, IEnumerable<EngineeringUnits> engineeringUnits);
        public byte[] GeneratePdfEngineerReport(IEnumerable<SimpleEngineer> engineers);
        
    }
}