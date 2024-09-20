using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
   public class NoteCreateDTO
{
    public int Id { get; set;}
 public string Content { get; set; }
    public int? PersonId { get; set; }
    public int? RelationId { get; set; }
    public int? HospitalId { get; set; }
    public int? SurgicalProcedureId { get; set; }
    public int? YearConfigId { get; set; }
}

}