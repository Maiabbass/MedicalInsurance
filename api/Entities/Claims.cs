using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Claims
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Claims(int id, string firstName  ,string fatherName,string matherName, string lastName, int ensuranceNumber, int hospitalId, DateTime loginDate, DateTime exitDate,decimal company_fees , decimal totalPrice,decimal non_AddForPerson, decimal non_Add, decimal approvedPrice, string enduranceRatio, bool trust)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.Id = id;
    this.FirstName = firstName;
    this.FatherName = fatherName;
    this.MatherName = matherName;
    this.LastName = lastName;
   
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
   
        }
        public int Id { get; set; }

        public string FirstName { get; set; }
         public string FatherName { get; set; }
         public string LastName { get; set; }
        public string MatherName { get; set; }

       
         public int EnsuranceNumber { get; set; }


        public DateTime LoginDate { get; set; }

        public DateTime ExitDate { get; set; }
        
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
        
        public string EnduranceRatio { get; set; }

        public bool Trust { get; set;}

        public int HospitalId{get ; set;}
        public Hospital Hospital{get; set;}

        public int EngId {get ; set ;}
        public  Engineere Engineeree{ get ; set ;}

        public int SurgicalProceduresId { get; set; }

        public SurgicalProcedures SurgicalProcedures { get; set; }


        
    }
}