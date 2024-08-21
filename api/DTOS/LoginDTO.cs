using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace api.DTOS
{
    public class LoginDTO
    {
         [Required(ErrorMessage = "User Name is required")]

         #nullable enable
        public string? Username { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

       // [Required(ErrorMessage = "Password is required")] 
        public string? EngineerNumber { get; set;}
        public string LoginType { get; set; }
        public string? Email { get;  set; }
    }
}