using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Note
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get ; set ;}

        public int? PersonId { get; set; }
        public Person? person{ get ; set;}


       public int? AgeSegmentId { get ; set ;}
       public AgeSegments? AgeSegments{get ; set;}


       public int? RelationId { get; set;}
       public  RelationType? RelationType { get ; set ;}


       public int? HospitalId { get ; set ;}
       public  Hospital? Hospital { get ; set ;}



       public int? SurgicalProcedureId  { get ; set;}
       public SurgicalProcedures? SurgicalProcedures { get ; set; }


      public int? YearConfigId { get ; set;}
      public  YearConfiguration? YearConfiguration { get; set; }


     }
}