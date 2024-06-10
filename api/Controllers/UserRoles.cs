using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoles : ControllerBase
    {
         private readonly IUserRoleService _userRoleService;

        public UserRoles(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
       
        public async Task<IActionResult> AddUserToRole(UserRoleDTO userRoleDto)
        {
            await _userRoleService.AddUserToRoleAsync(userRoleDto.Username, userRoleDto.Role);

            return Ok(new { Message = "User added to role successfully" });
        }
    }
}
  