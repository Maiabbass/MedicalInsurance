using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class SimpleEngineer
    {
        public int Id { get; set; }
    public string EngNumber { get; set; }
    public string SubNumber { get; set; }
    public SimplePerson Person { get; set; }

    public List<SimpleRelation> Relations { get; set; }

    
        public SimpleWorkPlace WorkPlace { get; set; }
        public SimpleSpecialization Specialization { get; set; }
        public SimplePayMethod PayMethod { get; set; }
        public SimpleEngineeringUnit EngineeringUnit { get; set; }
        public decimal TotalRelationsAmount { get; internal set; }
    }
    }
