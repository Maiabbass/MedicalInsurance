using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class PersonEditDTO
    {
        #pragma warning disable CS8618

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

        public string  Mobile { get; set; }

        public string Email { get; set; }


        public bool Subscrib { get; set; }

        public bool Affiliate  { get; set; }

        public bool Beneficiary { get; set; }

            public int GenderId {get; set;}


        
           
           // relation feilds...
          public int EngineereId{ get ; set ;}
           public int PersonId{ get ; set ;}
            public int RelationTypeId{ get ; set ;}
       
        

       
       
    }
}