using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NHIS_Portal.Data;
using NHIS_Portal.Models.AddNHIS_Provider;
using NHIS_Portal.Models.Authentication;
using NHIS_Portal.Models.Authentication.SignUp;
using NHIS_Portal.Models.AuthorizationCode;
using NHIS_Portal.Models.Complains;
using NHIS_Portal.Models.Entities;
using NHIS_Portal.Models.Report;
using NHIS_Portal.Service.Models;
using NHIS_Portal.Service.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NHIS_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _db;//to get access to d Database
        
        private readonly UserManager<IdentityUser> _userManager;//get access to userManagement in C#
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
       
        public AuthenticationController(UserManager<IdentityUser> userManager, ApplicationDbContext db,
           RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _db = db;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            //Check if User Exist
            var userExist = await _userManager.FindByEmailAsync(register.Email);
            if(userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!"});
            }
            //Add the user to DB
            IdentityUser user = new()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Email,
                TwoFactorEnabled = true,
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            var user1 = new User

            {
               // AuthId = user.Id,
                BranchId = register.BranchId,
                DepartmentId = register.DepartmentId,
                FirstName = register.FirstName,
                LastName = register.LastName,
                StaffId = register.StaffId
            };
            _db.Users.Add(user1);
            _db.SaveChanges();
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "User Created!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User failed to create!" });
            }

        }
        [HttpGet]
        [Route("testEmail")]
        public IActionResult TestEmail()
        {
            var message = new Message(new string[]
               {  "olaoluwaesan.dev@gmail.com" }, "Test", "Now! I can send email using C# code");

            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status200OK, 
                new Response { Status = "Success", Message = "Email sent successfully" });
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            //Check if the userExists
            var userExist = await _userManager.FindByEmailAsync(login.Email);
            if(userExist != null && await _userManager.CheckPasswordAsync(userExist, login.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                //Add role to user list
                var userRoles = await _userManager.GetRolesAsync(userExist);
                foreach(var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                //call the below method 'GetToken'
                var jwtToken = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                }
                 );
            }
            return Unauthorized();
        }


        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordlink = Url.Action("ResetPassword", "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot password link", forgotPasswordlink!);
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Password Changed request is sent on Email {user.Email}. Please open your email & click the link" }
                    );
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Could not send link to the email, please try again" });


        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new { model });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach(var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"Password has been changed" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Could not send link to the email, please try again" });


        }

        //Generate token with the claim
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        [HttpPost("createProvider")]
        public async Task<IActionResult> CreateProvider([FromBody] CreateProvider createProvider)
        {
            //Check if provider exist
            var providerExist = _db.ProviderDetails.Where(x => x.ProviderName == createProvider.ProviderName).FirstOrDefault();
            if (providerExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "Provider already exists!" });
            }
            //Add the provider to DB
            
            var AddProvider = new ProviderDetails
            {
                ProviderName = createProvider.ProviderName,
                ProviderCode = createProvider.NHIScode,
                ProviderAddress = createProvider.Address,
                ProviderEmail = createProvider.Email,
                ProviderPhoneNumber = createProvider.PhoneNumber
            };
            _db.ProviderDetails.Add(AddProvider);
            var result = _db.SaveChanges();
            if (result > 0)
            {
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "Provider details Created!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Provider details failed to create!" });
            }
            
        }
        [HttpGet("ProviderDetails")]//Search button in API
        //Search for ProviderDetails by using "ProviderName"
        public async Task<IActionResult> ProviderDetails(string providerName)
        {
            var ProviderList = _db.ProviderDetails.Where(x => x.ProviderName.Contains(providerName)).ToList();
            return Ok(ProviderList);
        }
        [HttpPost("createComplain")]
        public async Task<IActionResult> CreateComplain([FromBody] CreateComplain createComplain)
        {
            //check if enrolee complain Exist
            var complainExist = _db.EnroleeComplain.Where(a => a.Complaint == createComplain.Complaint).FirstOrDefault();
            if (complainExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "You have already created this complain" });
            }
            //Add the complain to Db
            var addComplain = new EnroleeComplain
            {
                ProviderId = createComplain.ProviderId,
                TreatmentId = createComplain.TreatmentId,
                LocationId = createComplain.LocationId,
                AuthorizationCode = createComplain.AuthorizationCode,
                EnroleeName = createComplain.EnroleeName,
                EnroleeNumber = createComplain.EnroleeNumber,
                Diagnosis = createComplain.Diagnosis,
                Complaint = createComplain.Complaint,
            };
            _db.EnroleeComplain.Add(addComplain);
            var result = _db.SaveChanges();
            if(result > 0)
            {
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "Complain created successfully" }
                    );
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Complain failed to create" }
                    );
            }
           
        }
        [HttpPost("createAuthorizationCode")]
        [Authorize]
        public async Task<IActionResult> CreateAuthorizationCode([FromBody] CreateAuthorizationCode createAuthorizationCode)
        {
            //Check if authorizationCodeExist
            var authorizationCodeExist = _db.AuthorizationCodes.Where(c => c.EnroleeName == createAuthorizationCode.EnroleeName).FirstOrDefault();
            if(authorizationCodeExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "You have already created this authorizationCode" });
            }
            
            object tokenUser;

            var items = this.HttpContext.Items.TryGetValue("User", out tokenUser);
            
            UserToken userToken = (UserToken)tokenUser;
            var userExist = await _userManager.FindByEmailAsync(userToken.email);
           var user = _db.Users.Where(x => x.Id/*AuthId*/ == userExist.Id).FirstOrDefault();
           
            //int month = rnd.Next(1, 13);  // creates a number between 1 and 12 
            var authCode = Guid.NewGuid().ToString().Substring(0, 16).Replace("-", "").ToUpper();
            var addAuthorizationCode = new AuthorizationCode
            {
                EnroleeName = createAuthorizationCode.EnroleeName,
                EnroleeNumber = createAuthorizationCode.EnroleeNumber,
                EnroleePhoneNumber = createAuthorizationCode.EnroleePhoneNumber,
                Provider = createAuthorizationCode.Provider,
                Diagnosis = createAuthorizationCode.Diagnosis,
                Code = authCode,
                IssuedDate = DateTime.Now,
                IssuedBy = user.FirstName

            };
            _db.AuthorizationCodes.Add(addAuthorizationCode);
            var myCode = _db.SaveChanges();
            if(myCode > 0)
            {
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "Authorization code generated successfully",Data = addAuthorizationCode }
                    );
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Authorization code failed to generate" }
                    );
            }
            
        }
        [HttpGet("VerifyByAuthCode")]
        //I use <IActionResult> if i want to return Ok
        public async Task<IActionResult> VerifyAuthCode(string EnterAuthorisationCode)
        {
            var authCode = _db.AuthorizationCodes.Where(b => b.Code.Contains(EnterAuthorisationCode)).ToList();
            return Ok(authCode);
        }
        [HttpGet("VerifyByEnroleeNumber")]
        public async Task<IActionResult> VerifyByEnroleeNumber(string EnroleeNumber)
        {
            var enrolee = _db.AuthorizationCodes.Where(g => g.EnroleeNumber.Contains(EnroleeNumber)).ToList();
            return Ok(enrolee);
    }
        [HttpPost("VerifyByDate")]
        public async Task<IActionResult> VerifyDate(UserReport CreatedDateToEndDate)
        {
            var report = _db.AuthorizationCodes.Where(j => j.IssuedDate >= CreatedDateToEndDate.CreatedDate && j.IssuedDate <= CreatedDateToEndDate.EndDate).ToList();
            return Ok(report);
                
        }
        [HttpPost("IssuedBy")]
        [Authorize]
        public async Task<IActionResult> IssuedBy(UserReport CreatedDateToEndDate)
        {
            //object tokenUser;

            //var items = this.HttpContext.Items.TryGetValue("User", out tokenUser);

            //UserToken userToken = (UserToken)tokenUser;
            //var userExist = await _userManager.FindByEmailAsync(userToken.email);

            object tokenUser;

            var items = this.HttpContext.Items.TryGetValue("User", out tokenUser);

            //var userToken = (UserToken)tokenUser;
           var userToken = JsonConvert.DeserializeObject<UserToken>(tokenUser.ToString());
            var userExist = await _userManager.FindByEmailAsync(userToken.email);
            //var user = _db.Users.Where(x => x.Id == userExist.Id).FirstOrDefault();

            var report = _db.AuthorizationCodes.Where(b => b.IssuedDate >= CreatedDateToEndDate.CreatedDate 
            && b.IssuedDate <= CreatedDateToEndDate.EndDate && b.IssuedBy == user.FirstName).ToList();
            return Ok(report);


        }
        
    }

}

