using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class SurgicalProcedures
    {

        #nullable disable
         public  int  Id { get; set; }

         public string Name { get; set; }

         public int Year { get ; set;}

         public string Pathological_specialization { get; set; }
         
         [Column(TypeName = "decimal(18,2)")]

         public decimal Price { get; set; }

         public ICollection<Claims> Claims{get ; set; }

         ///public  DateTime? Date { get ; set ;}
         ///
         public ICollection<Note>? Notes { get; set;}

    }
}