using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{

   
    public class Person
    {


       
        public Person()

        {
            
        }


       
       public int Id {get;set;}
        public string FirstName { get; set; }
        public string FatherName { get; set; }

        public string LastName { get; set; }

        public string?  MotherName { get; set; }

        public DateTime? BirthDate { get; set; }

      [StringLength(11,MinimumLength =11,ErrorMessage ="Invalid National ID Length")]
        public string NationalId { get; set; }

        public string EnsuranceNumber { get; set; }

        public string?  Address { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public bool Subscrib { get; set; }

        public bool Affiliate  { get; set; }

        public bool Beneficiary { get; set; }

        
        public Engineere Engineere { get; set; }

       // public int RelationId{ get ; set ;}
      //  public Relation Relation{get; set; }

       public int? statusId{ get; set;}
        public Status? Status { get; set;}

        public int GenderId {get; set;}
         public Gender Gender{ get; set;}

         public ICollection<Relation>Relations{get ; set ;}

         public ICollection<AnnualDataDetail>  AnnualDataDetails { get; set; }
         public ICollection<Claims> Claims { get ; set;}

    }
}