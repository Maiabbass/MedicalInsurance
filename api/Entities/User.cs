using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Entities
{
    public class User :IdentityUser
    {

        #nullable disable
     //   public string FullName { get; set; }
     public string FullName { get; set; }

   public int? EngineerId { get; set; } 

    [ForeignKey("EngineerId")]
    public virtual Engineere Engineer { get; set; }


    public int? PersonId { get; set; } 

    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }
    }
}