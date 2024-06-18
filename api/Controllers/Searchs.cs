using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Searchs : ControllerBase
    {

    private readonly ISearchService  _searchService;

    public Searchs(ISearchService  searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    [Route("find/ByEnsuranceNumber/{EnsuranceNumber}")]
    public async Task<ActionResult<Person?>>GetWithEN( string EnsuranceNumber){
    return Ok(await _searchService.GetWithEN(EnsuranceNumber));
    }



    [HttpGet]
    [Route("find/ByName/{Name}")]

    public async Task<ActionResult<Person?>>GetWithName( string Name){
    return Ok( await _searchService.GetWithName(Name));
    }

    [HttpGet]
    [Route("find/ByNationalId/{NationalId}")]
    public async Task<ActionResult<Person?>>GetWithNationalId( string NationalId){
    return Ok(await _searchService.GetWithNationalId(NationalId));
    }


    }
}