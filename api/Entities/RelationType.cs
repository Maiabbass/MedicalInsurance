using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class RelationType
    {
        #nullable disable

        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
         public int Year { get; set; }

        public  ICollection<Relation> Relations { get; set; }
        public  ICollection<Note>? Notes { get ; set ;}
    }
}