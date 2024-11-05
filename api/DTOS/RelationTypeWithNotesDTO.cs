using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class RelationTypeWithNotesDTO
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public List<NoteCreateDTO>? Notes { get; set; }
    }
}