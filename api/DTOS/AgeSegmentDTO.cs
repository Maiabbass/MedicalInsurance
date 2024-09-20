using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class AgeSegmentDTO
{    

    public int Id { get; set; }
    public int Year { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

       
        public decimal TheAmount { get; set; }

   
       public decimal? EnduranceRatio { get; set; }
    
    
    public string NoteContent { get; set; }  // ملاحظة خاصة بكل قطاع عمري
}

}