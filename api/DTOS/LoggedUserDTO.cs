using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class LoggedUserDTO
    {

        #nullable disable
         public string Id{get;set;}
        
          public string Username { get; set; }

          public  string token { get;set;}

          public DateTime expiration {get;set;}

          public List<string> Roles {get;set;}
    }
}