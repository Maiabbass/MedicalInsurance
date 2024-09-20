using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class RelationTypeDTO
    {
         public int Id { get; set; } 
        public string Name { get; set; }
         public int Year { get; set; }
         public string NoteContent { get; set; } 
    }
}