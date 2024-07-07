using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class SimpleRelation
    {
         public int Id { get; set; }
    public string Name { get; set; }

    public int RelationTypeId { get; set; }
    public SimplePerson Person { get; set; }
    }
}