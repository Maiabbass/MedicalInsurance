using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class FullPersonDataDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string BirthDate { get; set; }
        public string NationalId { get; set; }
        public string EnsuranceNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string WorkPlace { get; set; }
        public decimal Amount { get; set; }
        public decimal ExAmount { get; set; }
        public decimal TotalAmount { get; set; }
        
        // خصائص للصور والملفات
        public List<byte[]> Images { get; set; } = new List<byte[]>();
        public List<byte[]> Words { get; set; } = new List<byte[]>();
    }
}
