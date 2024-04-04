using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class PayMethod
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PayMethod(int id, string nameMethod)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.NameMethod = nameMethod;
   
   
        }
         public int Id { get; set; }

        public string NameMethod { get; set; }
        
        public ICollection<AnnualData> AnnualDatas{get; set;}
    }
}