using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Engineere
    {

        #nullable disable
        public Engineere()
 
        {
             
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
         public int Id{ get; set; }

        public string EngNumber { get; set; }

        public string SubNumber { get; set; }

    
      //  public int PersonId { get; set; }    
        public Person Person { get; set; }

        public ICollection<Relation>Relations{get ; set ;}

        public int statusId{ get; set;}
        public Status Status { get; set;}

        public int  SpecializationId{ get; set;}
        public Specialization Specialization{ get; set;}

        public int? WorkPlaceId{ get; set;}
        public WorkPlace WorkPlace { get; set;}

        public ICollection< AnnualData> AnnualDatas{ get; set;}
        public ICollection<Claims> Claims { get ; set;}
    }
}