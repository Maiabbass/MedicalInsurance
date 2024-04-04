using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Relation
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Relation(int id, string name, int engineereId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.Name = name;
    
    this.EngineereId = engineereId;
   
        }
         public int Id { get; set; }

        public string Name { get; set; }

            
        public ICollection<Person> Persons{get; set;}

        public int EngineereId { get; set; }    
        public Engineere Engineere { get; set; }
    }
}