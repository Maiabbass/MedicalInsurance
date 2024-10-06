using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class LoginResponseDTO
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
}