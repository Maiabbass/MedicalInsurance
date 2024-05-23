using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class WorkPlace
    {
        public WorkPlace()
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public WorkPlace(int id, string name, string location,  int engineeringUnitsId, string phone)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.Name = name;
    this.Location = location;
    this.EngineeringUnitsId = engineeringUnitsId;
    this.Phone = phone;
   
   
        }
                public int Id { get; set; }

        public string Name { get; set; }

        public string? Location { get; set; }

        public string  Phone { get; set; }


        public ICollection<Engineere>Engineeres{get; set;}

        public int EngineeringUnitsId{ get; set;}
        public EngineeringUnits EngineeringUnits { get; set;} 

        public ICollection<AnnualData> AnnualDatas{ get; set;}
        }
    }
