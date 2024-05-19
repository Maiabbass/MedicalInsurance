using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class PersonForView
    {
        #nullable disable
               public int Id {get;set;}
        public string FirstName { get; set; }
        public string FatherName { get; set; }

        public string LastName { get; set; }

        public string  MotherName { get; set; }

        public DateTime? BirthDate { get; set; }

       
        public string NationalId { get; set; }

        public string EnsuranceNumber { get; set; }

        public string  Address { get; set; }

        public string Phone { get; set; }

        public bool Subscrib { get; set; }

        public bool Affiliate  { get; set; }

        public bool Beneficiary { get; set; }

        
      

        public int GenderId {get; set;}
       

       

    }
}