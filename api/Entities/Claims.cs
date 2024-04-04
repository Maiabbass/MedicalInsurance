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
        public Claims(int id, string fullName, string surgicalPro, int hospitalId, DateTime loginDate, DateTime exitDate, decimal totalPrice, decimal additionalPrice, decimal approvedPrice, string enduranceRatio, bool trust)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.Id = id;
    this.FullName = fullName;
    this.SurgicalPro = surgicalPro;
    this.HospitalId = hospitalId;
    this.LoginDate = loginDate;
    this.ExitDate = exitDate;
    this.TotalPrice = totalPrice;
    this.AdditionalPrice = additionalPrice;
    this.ApprovedPrice = approvedPrice;
    this.EnduranceRatio = enduranceRatio;
    this.Trust= trust;
   
        }
        public int Id { get; set; }

        public string FullName { get; set; }

        public string SurgicalPro { get; set; }


        public DateTime LoginDate { get; set; }

        public DateTime ExitDate { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        
        public decimal AdditionalPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        
        public decimal ApprovedPrice { get; set; }
        
        public string EnduranceRatio { get; set; }

        public bool Trust { get; set;}

        public int HospitalId{get ; set;}
        public Hospital Hospital{get; set;}


        
    }
}