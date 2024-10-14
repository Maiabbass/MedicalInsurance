using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class SurgicalProceduresEditDTO
    {
        #nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
      

        public string? Pathological_specialization { get; set; }

        public decimal Price { get ; set;}
        public  decimal Ceiling{ get ; set ;}
        public  decimal IN{ get ; set;}
        public  decimal OUT { get ; set ;}

       public string? NoteContent { get; set; } 

       public int? Year { get ; set ;}


    }
    }
