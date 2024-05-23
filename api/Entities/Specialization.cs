using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Specialization
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Specialization(int id, string name)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.Name = name;
  
   
        }
                public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Engineere>Engineeres{get; set;}

        public ICollection<EngineeringeDepar>EngineeringeDepars { get; set; }
    }
}