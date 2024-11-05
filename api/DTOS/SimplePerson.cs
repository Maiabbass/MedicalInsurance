using System;
using System.Collections.Generic;

namespace api.DTOS
{
    public class SimplePerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string NationalId { get; set; }
        public string EnsuranceNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool Subscrib { get; set; }
        public bool Affiliate { get; set; }
        public bool Beneficiary { get; set; }
        public int GenderId { get; set; }
        public int StatusId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? BirthDate { get; set; }

        // إضافة خاصية الصور
        public List<ImageDTO> Images { get; set; } = new List<ImageDTO>();

        // إضافة خاصية الملفات
        public List<WordDTO> Words { get; set; } = new List<WordDTO>();
    }

    
}
