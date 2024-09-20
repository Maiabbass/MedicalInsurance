using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;


namespace api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class Blocks : ControllerBase
    {


    private readonly IBlockService _blockService;
          
        
    public Blocks(IBlockService blockService)
      {
      _blockService= blockService;
         }
        




    [HttpPost("block-person")]
    public async Task<ActionResult> BlockPerson([FromQuery] string ensuranceNumber, [FromQuery] int year, [FromBody] string not)
    {
        var result = await _blockService.BlockPerson(ensuranceNumber, year, not);

        if (result)
        {
            return Ok($"Person has been successfully blocked for the year {year}.");
        }

        return BadRequest("Person could not be found or already blocked.");
    }



        [HttpDelete("unblock-person")]
        public async Task<IActionResult> UnblockPerson([FromQuery] string insuranceNumber)
        {
            var result = await _blockService.UnblockPerson(insuranceNumber);

            if (result)
                return Ok("Person has been successfully unblocked.");

            return BadRequest("Person is not found in the block list.");
        }




            [HttpGet("blocked-persons")]
            public async Task<IActionResult> GetBlockedPersons()
            {
                var blockedPersons = await _blockService.GetAllBlockedPersons();
                
                if (blockedPersons == null || !blockedPersons.Any())
                    return NotFound("No persons are blocked.");

                return Ok(blockedPersons);
            }




            [HttpGet("is-person-blocked")]
            public async Task<IActionResult> IsPersonBlocked([FromQuery] string insuranceNumber)
            {
                var isBlocked = await _blockService.IsPersonBlocked(insuranceNumber);

                if (isBlocked)
                    return Ok($"The engineer with insurance number {insuranceNumber} is blocked.");

                return NotFound($"The engineer with insurance number {insuranceNumber} is not blocked.");
            }




    }
}