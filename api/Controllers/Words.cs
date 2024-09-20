using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Words : ControllerBase
    {
        private readonly IWordRepository _wordRepository;

    public Words(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

   
} 
    }
