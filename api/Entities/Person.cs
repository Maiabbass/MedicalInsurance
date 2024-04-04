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


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Person(int id, string fatherName, string lastName, string nationalId, string ensuranceNumber, string phone, bool subscrib, bool beneficiary, int genderId , int relationId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.FatherName = fatherName;
    this.LastName = lastName;
    this.NationalId = nationalId;
    this.EnsuranceNumber = ensuranceNumber;
    this.Phone = phone;
    this.Subscrib = subscrib;
    this.Beneficiary = beneficiary;
    this.GenderId = genderId;
    this.RelationId=relationId;
   
        }
                public int Id {get;set;}
        public string FirstName { get; set; }
        public string FatherName { get; set; }

        public string LastName { get; set; }

        public string  MotherName { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(10,MinimumLength =10,ErrorMessage ="Invalid National ID Length")]
        public string NationalId { get; set; }

        public string EnsuranceNumber { get; set; }

        public string  Address { get; set; }

        public string Phone { get; set; }

        public bool Subscrib { get; set; }

        public bool Affiliate  { get; set; }

        public bool Beneficiary { get; set; }

        
        public Engineere Engineere { get; set; }

        public int RelationId{ get ; set ;}
        public Relation Relation{get; set; }

        public int GenderId {get; set;}
        public Gender Gender{ get; set;}

    }
}