using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Users : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

         private readonly DataContext _dataContext;

         public Users(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, DataContext dataContext)
         {
             
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
            _dataContext=dataContext;
         }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForViewDTO>>> GetUsers()
        {
                var users = await _userManager.Users.ToListAsync();
                List<UserForViewDTO>usersForView=new  List<UserForViewDTO>();
                if (users!=null)
                {
                 foreach(var item in users)
                 {
                    usersForView.Add(new UserForViewDTO ()
                    {
                         Id=item.Id,
                          Email=item.Email,
                           UserName=item.UserName
                    });
                 }
                }
               
               return Ok(usersForView) ;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUser(string Id)
        {
             var _userResult = await _userManager.FindByIdAsync(Id);
             if (_userResult==null)
             {
                return NotFound();
             }
             return _userResult;
        }


             [HttpPost]
        [Route("register")]
          
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            // use transaction .. later....
           var _userResult = await _userManager.FindByNameAsync(registerDTO.Username);
            
           if (_userResult!=null)
           {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", ErrorMessage = "User already exists!"});            
           }
             bool committed=false;
           
              using (var transaction = await _dataContext.Database.BeginTransactionAsync())
              {
                try 
                {

                      User user =new ()
           {
            
                UserName=registerDTO.Username,
                Email =registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString()
           };

        

           var result = await _userManager.CreateAsync(user,registerDTO.Password);

           
          


                     await transaction.CommitAsync();
                    committed=true;
                }
                 catch(Exception )
                {
                   await transaction.RollbackAsync();
                }
              }
            
           
   
           if (!committed)
            {
                  return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", ErrorMessage = "Something Went Wrong"});         
            }
          return Ok(new Response { Status = "Success", ErrorMessage = "User register Was Successfull!" });
        
        }

               [HttpPost]
        [Route("login")]
       
        public async Task<ActionResult<LoggedUserDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            var userResult = await _userManager.FindByNameAsync(loginDTO.Username);
            if (userResult != null && await _userManager.CheckPasswordAsync(userResult, loginDTO.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(userResult);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userResult.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                List<string>loggedUserRoles=new List<string> ();
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    loggedUserRoles.Add(userRole); 
                }

               var token = GetToken(authClaims);

                
                return Ok(new LoggedUserDTO
                {
                    Id= userResult.Id,
                    Username = userResult.UserName,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                    ,Roles=loggedUserRoles,

                });
            }
            return Unauthorized(new LoggedUserDTO());
        }

          private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

              var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

         
    }
}