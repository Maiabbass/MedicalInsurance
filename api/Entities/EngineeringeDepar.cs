using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class EngineeringeDepar
    {
        public int Id { get; set; }

        public string Name { get; set; }

       public ICollection<Specialization>Specializations{get; set;}

    }
}