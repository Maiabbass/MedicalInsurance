using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class PasswordEng
{
    public int Id { get; set; }
    public string EngineerNumber { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }

    public string UserName { get ; set;}
        
    }
}