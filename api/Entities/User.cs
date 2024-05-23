using System;
using System.Collections.Generic;
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
    }
}