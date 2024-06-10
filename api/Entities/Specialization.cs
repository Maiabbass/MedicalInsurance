using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Specialization
    {


       
         public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Engineere>Engineeres{get; set;}

        public int  EngineeringeDeparId { get; set; }
        public EngineeringeDepar EngineeringeDepar{get; set; }

        

       
    }
}