using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Gender
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Gender(int id, string name )
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
           this.Id = id;
           this.Name = name;
        }


         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.None)]
         public int Id { get; set; }

        public string  Name{ get; set; }

        public ICollection<Person> Persons{get; set;}
    }
}