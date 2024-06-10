using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Claims
    {
        internal object valueObject;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Claims()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            /*
            this.Id = id;
    this.HospitalId = hospitalId;
    this.Company_fees=company_fees ;
    this.EnsuranceNumber=ensuranceNumber;
    this.LoginDate = loginDate;
    this.ExitDate = exitDate;
    this.TotalPrice = totalPrice;
    this.non_Add = non_Add;
    this.non_AddForPerson=non_AddForPerson;
    this.ApprovedPrice = approvedPrice;
    this.EnduranceRatio = enduranceRatio;
    this.Trust= trust;
    */
   
        }
        public int Id { get; set; }


        public string EnsuranceNumber { get; set; }

         
        public  string FullName{ get; set; }

 
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        
        public decimal Company_fees { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        
        public decimal ApprovedPrice { get; set; }

         [Column(TypeName = "decimal(18,2)")]
        
        public decimal non_Add { get; set; }

           [Column(TypeName = "decimal(18,2)")]
        
        public decimal non_AddForPerson { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        
        public decimal EnduranceRatio { get; set; }

        public int HospitalId{get ; set;}
        public Hospital Hospital{get; set;}

        public bool Trust { get; set;}
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


        
    }
}