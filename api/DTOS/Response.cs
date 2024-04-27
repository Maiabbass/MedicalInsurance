using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class Response
    {
      #nullable enable

        public int? InsertedId { get; set; }
        public string? ErrorMessage { get; set; }

    }
}