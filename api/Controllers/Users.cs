using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
//using System.Net.Mail;
using BCrypt.Net;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

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







/////////////////////////////////////////////////
/// <summary>
/// عدلت كومه
/// </summary>
/// <param name="length"></param>
/// <returns></returns>




 
    private string GenerateRandomPassword(int length = 12)
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        var randomBytes = new byte[length];
        var password = new StringBuilder(length);

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        for (int i = 0; i < length; i++)
        {
            password.Append(validChars[randomBytes[i] % validChars.Length]);
        }

        return password.ToString();
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }




private async Task SendPasswordEmail(string recipientEmail, string emailBody)
{
    var emailMessage = new MimeMessage();
    emailMessage.From.Add(new MailboxAddress("Hometecs Company", "ghaith.kh.audi@gmail.com"));
    emailMessage.To.Add(new MailboxAddress("", recipientEmail));
    emailMessage.Subject = "كلمة المرور الجديدة الخاصة بك";

    var bodyBuilder = new BodyBuilder
    {
        HtmlBody = emailBody,
        TextBody = "لقد تلقينا طلبًا لتعيين كلمة المرور الخاصة بك."
    };
    emailMessage.Body = bodyBuilder.ToMessageBody();

    using (var client = new SmtpClient())
    {
        try
        {
            await client.ConnectAsync("smtp.gmail.com", 465, true); // استخدم SSL على المنفذ 465
            await client.AuthenticateAsync("ghaith.kh.audi@gmail.com", "oucr ryjk mzfu iqcv");

            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            // سجل الأخطاء هنا
        }
    }

}

// دالة إرسال البريد الإلكتروني باستخدام SmtpClient
/*
 public void SendEmail(string toEmail, string subject, string body)
{
    var fromEmail = "ghaith.kh.audi@gmail.com";
    var password = "oucr ryjk mzfu iqcv"; // استخدم كلمة المرور للتطبيق التي أنشأتها سابقاً

    var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential(fromEmail, password),
        EnableSsl = true,
        
    };

    var mailMessage = new MailMessage
    {
        From = new MailAddress(fromEmail ,"Hometecs Company"),
        Subject = subject,
        Body = body,
        IsBodyHtml = true,
    };

    mailMessage.To.Add(toEmail);

    smtpClient.Send(mailMessage);
}


*/

 [HttpPost]
[Route("RequestPassworde")]
public async Task<ActionResult<LoggedUserDTO>> RequestPassworde([FromBody] LoginDTO loginDTO)
{
    if (loginDTO == null || string.IsNullOrEmpty(loginDTO.EngineerNumber) || string.IsNullOrEmpty(loginDTO.Email))
    {
        return BadRequest(new LoggedUserDTO
        {
            ErrorMessage = "Invalid login request."
        });
    }

    if (loginDTO.LoginType == "Mobile")
    {
        var engineer = await _dataContext.Engineeres
            .Include(e => e.Person)
            .FirstOrDefaultAsync(e => e.EngNumber == loginDTO.EngineerNumber);

        if (engineer == null)
        {
            return Unauthorized(new LoggedUserDTO
            {
                ErrorMessage = "Engineer not found."
            });
        }

        if (engineer.Person == null || string.IsNullOrEmpty(engineer.Person.Email))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new LoggedUserDTO
            {
                ErrorMessage = "Person data or email not found."
            });
        }

        // التحقق من تطابق البريد الإلكتروني
        if (!engineer.Person.Email.Equals(loginDTO.Email, StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(new LoggedUserDTO
            {
                ErrorMessage = "Email does not match our records."
            });
        }

        var generatedPassword = GenerateRandomPassword();
        var hashedPassword = HashPassword(generatedPassword);

        var passwordEntry = new PasswordEng
        {
            UserName = loginDTO.Username,
            EngineerNumber = loginDTO.EngineerNumber,
            Password = hashedPassword,
            CreatedAt = DateTime.UtcNow
        };

        _dataContext.passwordEngs.Add(passwordEntry);
        await _dataContext.SaveChangesAsync();

        string emailBody = $@"
        <!DOCTYPE html>
        <html lang='ar'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title> تعيين كلمة المرور</title>
        </head>
        <body style='font-family: Arial, sans-serif; color: #333; text-align: right;'>
            <p>عزيزي المهندس {engineer.Person.FirstName}،</p>

            <p>لقد تلقينا طلبًا لتعيين كلمة المرور الخاصة بك. كلمة المرور الجديدة الخاصة بك هي:</p>

            <p style='font-weight: bold; font-size: 16px;'>{generatedPassword}</p>

            <p>يرجى الحفاظ على هذه الكلمة السرية وعدم مشاركتها مع أي شخص. إذا لم تطلب تعيين كلمة المرور، يرجى الاتصال بنا فورًا.</p>

            <p>شكرًا لك،<br/>
            فريق دعم Hometecs</p>

            <hr/>

            <p style='font-size: 12px; color: #666;'>
                تم إرسال هذا البريد الإلكتروني إليك لأنك طلبت  تعيين كلمة المرور لحسابك في Hometecs. إذا لم تقم بهذا الطلب، يرجى الاتصال بفريق الدعم الخاص بنا على <a href='mailto:support@hometecs.com'>support@hometecs.com</a>.
            </p>

            <p style='font-size: 12px; color: #666;'>
                شركة Hometecs، حمص-باب هود-الشارع الرئسيي
            </p>
        </body>
        </html>";

        await SendPasswordEmail(engineer.Person.Email, emailBody);

        return Ok(new LoggedUserDTO
        {
            EnsuranceNumber = engineer.Person.EnsuranceNumber
        });
    }

    return Unauthorized(new LoggedUserDTO
    {
        ErrorMessage = "Invalid login attempt."
    });
}







private async Task<ActionResult<LoggedUserDTO>> GenerateTokenForUser(User userResult, bool isMobileLogin = false)
{
    try
    {
        var userRoles = await _userManager.GetRolesAsync(userResult);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userResult.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims);

        return Ok(new LoggedUserDTO
        {
            Id = userResult.Id,
            Username = userResult.UserName,
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            Roles = userRoles.ToList(),
            EnsuranceNumber = isMobileLogin ? userResult.Person.EnsuranceNumber : null // إرجاع الرقم التأميني فقط في حالة الدخول عبر التطبيق
        });
    }

   catch (DbUpdateException ex)
{
    var sqlException = ex.InnerException as SqlException;
    if (sqlException != null)
    {
        return BadRequest($"SQL Error: {sqlException.Message} | {sqlException.StackTrace}");
    }

    throw;
}
}





[HttpPost]
[Route("login")]
public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // البحث في جدول PasswordEng بناءً على الرقم الهندسي
    var passwordEngRecord = await _dataContext.passwordEngs
        .Where(p => p.EngineerNumber == loginDto.EngineerNumber)
        .FirstOrDefaultAsync();

    if (passwordEngRecord == null)
    {
        return Unauthorized("المستخدم غير موجود.");
    }

    // التحقق من كلمة المرور
    var passwordValid = VerifyPassword(loginDto.Password, passwordEngRecord.Password);

    if (!passwordValid)
    {
        return Unauthorized("كلمة المرور غير صحيحة.");
    }

    // البحث في جدول Engineere باستخدام EngNumber للحصول على Id
    var engineerRecord = await _dataContext.Engineeres
        .Where(e => e.EngNumber == loginDto.EngineerNumber)
        .FirstOrDefaultAsync();

    if (engineerRecord == null)
    {
        return Unauthorized("لا يوجد مهندس مرتبط بهذا الرقم الهندسي.");
    }

    // استخدام Id من جدول Engineere للبحث في جدول Person للحصول على EnsuranceNumber
    var personRecord = await _dataContext.Persons
        .Where(p => p.Id == engineerRecord.Id)
        .FirstOrDefaultAsync();

    if (personRecord == null)
    {
        return Unauthorized("لا يوجد سجل لشخص مرتبط بهذا المهندس.");
    }

    // إرجاع الرقم التأميني عند نجاح العملية
    return Ok(new LoggedUserDTO
    {
        EnsuranceNumber = personRecord.EnsuranceNumber
    });
}

private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
{
    // هنا يتم التحقق من كلمة المرور المدخلة بعد تشفيرها ومقارنتها مع كلمة المرور المشفرة المخزنة
    return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
}



[HttpPost]
[Route("reset-password")]
public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // البحث في جدول PasswordEng بناءً على الرقم الهندسي
    var passwordEngRecord = await _dataContext.passwordEngs
        .Where(p => p.EngineerNumber == resetPasswordDto.EngineerNumber)
        .FirstOrDefaultAsync();

    if (passwordEngRecord == null)
    {
        return Unauthorized("المستخدم غير موجود.");
    }

    // البحث في جدول Person بناءً على الرقم الهندسي والبريد الإلكتروني
    var personRecord = await _dataContext.Persons
        .Where(p => p.Email == resetPasswordDto.Email && p.Engineere.EngNumber == resetPasswordDto.EngineerNumber)
        .FirstOrDefaultAsync();

    if (personRecord == null)
    {
        return Unauthorized("البريد الإلكتروني أو الرقم الهندسي غير صحيح.");
    }

    // توليد كلمة مرور جديدة
    var newPassword = GenerateRandomPassword();

    // تشفير كلمة المرور الجديدة
    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

    // تحديث كلمة المرور في سجل PasswordEng
    passwordEngRecord.Password = hashedPassword;
    await _dataContext.SaveChangesAsync();

    // جلب بيانات المهندس لإرسال البريد الإلكتروني
    var engineer = await _dataContext.Engineeres
        .Include(e => e.Person)
        .FirstOrDefaultAsync(e => e.EngNumber == resetPasswordDto.EngineerNumber);

    // تحضير نص البريد الإلكتروني
    string emailBody = $@"
        <!DOCTYPE html>
        <html lang='ar'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>إعادة تعيين كلمة المرور</title>
        </head>
        <body style='font-family: Arial, sans-serif; color: #333; text-align: right;'>
            <p>عزيزي المهندس {engineer.Person.FirstName}،</p>

            <p>لقد تلقينا طلبًا لاعادة تعيين كلمة المرور الخاصة بك. كلمة المرور الجديدة الخاصة بك هي:</p>

            <p style='font-weight: bold; font-size: 16px;'>{newPassword}</p>

            <p>يرجى الحفاظ على هذه الكلمة السرية وعدم مشاركتها مع أي شخص. إذا لم تطلب  اعادة تعيين كلمة المرور، يرجى الاتصال بنا فورًا.</p>

            <p>شكرًا لك،<br/>
            فريق دعم Hometecs</p>

            <hr/>

            <p style='font-size: 12px; color: #666;'>
                تم إرسال هذا البريد الإلكتروني إليك لأنك طلبت اعادة تعيين كلمة المرور لحسابك في Hometecs. إذا لم تقم بهذا الطلب، يرجى الاتصال بفريق الدعم الخاص بنا على <a href='mailto:support@hometecs.com'>support@hometecs.com</a>.
            </p>

            <p style='font-size: 12px; color: #666;'>
                شركة Hometecs، حمص-باب هود-الشارع الرئسيي
            </p>
        </body>
        </html>";

    // إرسال البريد الإلكتروني
    await SendPasswordEmail(resetPasswordDto.Email, emailBody);

    return Ok("تم إعادة تعيين كلمة المرور وإرسالها إلى بريدك الإلكتروني.");
}





[HttpPost]
[Route("RegisterManger")]
public async Task<IActionResult> StoreUserCredentials(string userName, string email, string password)
{
    // التحقق مما إذا كان المستخدم موجودًا بالفعل
    var existingUser = await _userManager.FindByNameAsync(userName);
    if (existingUser == null)
    {
        return BadRequest("User does not exist.");
    }

    // تحديث البريد الإلكتروني
    existingUser.Email = email;

    // حذف كلمة المرور القديمة (إذا كانت موجودة)
    var removePasswordResult = await _userManager.RemovePasswordAsync(existingUser);
    if (!removePasswordResult.Succeeded)
    {
        return BadRequest("Failed to remove old password.");
    }

    // تعيين كلمة المرور الجديدة (سيتم تشفيرها تلقائيًا)
    var addPasswordResult = await _userManager.AddPasswordAsync(existingUser, password);
    if (!addPasswordResult.Succeeded)
    {
        return BadRequest("Failed to add new password.");
    }

    // حفظ التغييرات في قاعدة البيانات
    var updateResult = await _userManager.UpdateAsync(existingUser);
    if (!updateResult.Succeeded)
    {
        return BadRequest("Failed to update user credentials.");
    }

    return Ok("User credentials stored successfully.");
}


[HttpPost]
[Route("LoginManger")]
public async Task<IActionResult> Login(string userName, string password)
{
    // البحث عن المستخدم باستخدام اسم المستخدم
    var user = await _userManager.FindByNameAsync(userName);
    if (user == null)
    {
        return Unauthorized("Invalid username or password.");
    }

    // التحقق من صحة كلمة المرور
    var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
    if (!passwordCheck)
    {
        return Unauthorized("Invalid username or password.");
    }

    // إذا كانت البيانات صحيحة، يمكن السماح بالدخول
    return Ok("Login successful.");
}



//////////////////////////////////////////////////////////////////
/// <summary>
/// عدلت كومه
/// </summary>
/// <param name="authClaims"></param>
/// <returns></returns>




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






          [HttpGet("getstring/{Id}")]
         
          public  ActionResult<string> Getstring(string Id)
          {
              return Ok(Id);
          }


         [HttpGet("getstring2/{Id}")]
          public  ActionResult<string> Getstring2(string Id)
          {
              return Ok(Id+Id);
          }


         
        


[HttpPost("editRoles")]
public async Task<IActionResult> EditRoles([FromBody] EditRolesRequest request) 
{
    var user = await _userManager.FindByNameAsync(request.UserName);

    if (user == null) 
    {
        return BadRequest($"{request.UserName} not found");
    }

    var userRoles = await _userManager.GetRolesAsync(user);

    var selectedRoles = request.RoleNames ?? new string[] {};

    var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

    if (!result.Succeeded) 
    {
        return BadRequest("Failed to add to roles");
    }

    result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

    if (!result.Succeeded) 
    {
        return BadRequest("Failed to remove the roles");
    }   

    return Ok(await _userManager.GetRolesAsync(user));         
}


    }

   
}