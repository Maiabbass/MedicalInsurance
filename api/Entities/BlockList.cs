using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class BlockList
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime? DateTime { get ; set;}

        public string? Note { get; set; }
        
        public  int PersonId{ get ;set;}
        public Person person{get ; set;}

        
    }
}