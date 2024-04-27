using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Relation
    {


       #nullable disable

         
         public int Id { get; set; }

        public string Name { get; set; }

            
      //  public ICollection<Person> Persons{get; set;}

        public int EngineereId { get; set; }    
        public Engineere Engineere { get; set; }


         public int PersonId { get; set; }    
        public Person Person { get; set; }


        public int RelationTypeId { get; set; }

        public RelationType RelationType { get; set; }

    }
}