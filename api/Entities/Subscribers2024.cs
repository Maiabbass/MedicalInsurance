using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Subscribers2024
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id { get; set; }

         public string? EnsuranceNumber { get ; set; }
         public  string? FullName { get ; set;}

         public  string?  NationalId { get ; set ;}

         public DateTime? BirthDate { get; set; }


    }
}