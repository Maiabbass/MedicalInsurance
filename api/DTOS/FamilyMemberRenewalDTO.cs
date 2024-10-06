using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{  
    
    
    public class FamilyMemberRenewalDTO
{
    public int PersonId { get; set; }
    public bool Waiting { get; set; }
    public bool CardStatus { get; set; }
}
}