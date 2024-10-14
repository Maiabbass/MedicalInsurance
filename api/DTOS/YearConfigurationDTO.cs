using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
   public class YearConfigurationDTO
{
    public int Id { get; set; }
    public int Year { get; set; }
    public decimal CardPrice { get; set; }
    public List<NoteCreateDTO> Notes { get; set; } 
}
}