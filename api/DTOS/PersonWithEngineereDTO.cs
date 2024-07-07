using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    
   public class PersonWithEngineereDTO
{
    // خصائص جدول Person
    public int PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? FatherName { get; set; }
    public string? LastName { get; set; }
    public string? MotherName { get; set; }
    public string? NationalId { get; set; }
    public string? EnsuranceNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public int? StatusId { get; set; }

    public int? GenderId { get; set; }

    // خصائص جدول Engineere
    public string? EngNumber { get; set; }
    public string? SubNumber { get; set; }
    public int? SpecializationId { get; set; }

   
    public int? WorkPlaceId { get; set; }

    // يمكنك إضافة المزيد من الخصائص حسب الحاجة
}

}