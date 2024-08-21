using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Recovered
    {
         public int Id { get; set; }


        public string EnsuranceNumber { get; set; }

         
        public  string? FullName{ get; set; }

 
        [Column(TypeName = "decimal(18,2)")]

        public decimal? TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        
        public decimal? Company_fees { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        
        public decimal? ApprovedPrice { get; set; }

         [Column(TypeName = "decimal(18,2)")]
        
        public decimal? non_Add { get; set; }

           [Column(TypeName = "decimal(18,2)")]
        
        public decimal? non_AddForPerson { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        
        public decimal? EnduranceRatio { get; set; }

        public int? HospitalId{get ; set;}
        public Hospital Hospital{get; set;}

        public bool Status { get; set;}
        public DateTime? LoginDate { get; set; }

        public DateTime? ExitDate { get; set; }

        
        /*
        public int EngId {get ; set ;}
        public  Engineere Engineeree{ get ; set ;}
        */

         public int PersonId { get; set; }

         public Person Person { get; set; }



        public int? SurgicalProceduresId { get; set; }

        public SurgicalProcedures? SurgicalProcedures { get; set; }

        public int Number { get; set ;}
        

       public DateTime? RecoDate { get; set; }


       public DateTime? DateSurgicalProcedures { get ; set;}
 
  
        
    }
    }
