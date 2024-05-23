using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Hospital
    {
        public Hospital()
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Hospital(int id, string name,string phone,string email, bool enabled, bool inside,int cityId, string address)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
            this.Name=name;
    this.Enabled = enabled;
    this.Inside= inside;
    this.CityId=cityId;
    this.Phone=phone;
    this.Email=email;
    this.Address= address;

   
        }
        public int Id { get; set; }
        
        public string Name  { get; set; }
        public string Address { get; set; }

        public bool Enabled { get; set; }

        public bool Inside { get ; set ;}

        public string Phone { get; set; }

        public string Email { get; set; }

        public int CityId {get ; set ;}
        public City City { get ; set ;}

        public ICollection<Claims> Claims{get ; set; }

        
    }
}