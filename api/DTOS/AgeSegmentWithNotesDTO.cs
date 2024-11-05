using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class AgeSegmentWithNotesDTO
    {
        public int FromYear { get; set; }
    public int ToYear { get; set; }
    public decimal TheAmount { get; set; }
    public float EnduranceRatio { get; set; }
    public int Year { get; set; }
    public List<NoteCreateDTO>? Notes { get; set; }
    }
}