using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class EditRolesRequest
    {
        public string UserName { get; set; }
    public string[] RoleNames { get; set; }
} 
    
}