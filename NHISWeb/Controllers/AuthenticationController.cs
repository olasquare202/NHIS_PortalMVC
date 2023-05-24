using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHIS_Portal.Service.Services;
using NHISWeb.Data;
using NHISWeb.Dto.RequestDto;
using NHISWeb.Models.Authentication;
using NHISWeb.Models.AuthorizationCode;
using NHISWeb.Models.Entities;
using NHISWeb.Views.VerifyCodeModel;
//using System;
//using System.NHIS.SessionState;

namespace NHISWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _db;//to get access to d Database

        private readonly UserManager<User> _userManager;//get access to userManagement in C#
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthenticationController(UserManager<User> userManager, ApplicationDbContext db,
           RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _db = db;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task <IActionResult> Register()
        {
            var branches = await _db.Branch.ToListAsync(); 
            

            ViewBag.Branch = branches;
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(UserRegister createUser)
        {
            //Check if User Exist
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(createUser.Email);
                if (userExist != null)

                {
                    ModelState.AddModelError("", "User already exist");
                    return View();

                }
                
                //Add the user to DB
                User user = new()
                {

                    Email = createUser.Email,
                    UserName = createUser.Email,
                    BranchId = createUser.BranchId,
                    UserRoleId = createUser.UserRoleId,
                    DepartmentId = createUser.DepartmentId,
                    FirstName = createUser.FirstName,
                    LastName = createUser.LastName,
                    StaffId = createUser.StaffId,
                    NormalizedUserName = createUser.Email,
                    NormalizedEmail = createUser.Email,
                   
                };
                
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, createUser.Password);
              
                await _db.SaveChangesAsync();
                return RedirectToAction("Login");
                return View();

            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if(user == null)
                {
                    ModelState.AddModelError("", "Email does not exist");
                    return View();
                }
                if(!await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    ModelState.AddModelError("", "Password is not correct");
                    return View();
                }
                HttpContext.Session.SetString("", login.Email);
                return RedirectToAction("AuthorizationCode");
            }
            return View();
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
            return View();
        }
        
        [HttpGet]
        public IActionResult GetResultToFrontEnd()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AuthorizationCode()
        {
            
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> AuthorizationCode(CreateAuthorizationCode createAuthorizationCode)
        {
            //Check if authorizationCodeExist
            var authorizationCodeExist = await _db.AuthorizationCodes.Where(c => c.EnroleeName == createAuthorizationCode.EnroleeName).FirstOrDefaultAsync();
            if (authorizationCodeExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "You have already created this authorizationCode" });
            }
            //Add the required data to db

            // Random rnd = new Random();
            //int month = rnd.Next(1, 13);  // creates a number between 1 and 12 
            var authCode = Guid.NewGuid().ToString().Substring(0, 16).Replace("-", "").ToUpper();
            var addAuthorizationCode = new AuthorizationCode
            {
                EnroleeName = createAuthorizationCode.EnroleeName,
                EnroleeNumber = createAuthorizationCode.EnroleeNumber,
                EnroleePhoneNumber = createAuthorizationCode.EnroleePhoneNumber,
                Provider = createAuthorizationCode.Provider,
                Diagnosis = createAuthorizationCode.Diagnosis,
                IssuedBy = createAuthorizationCode.IssuedBy,
                Code = authCode

            };
            _db.AuthorizationCodes.Add(addAuthorizationCode);
            var myCode = _db.SaveChanges();
            if (myCode > 0)
            {
                ViewBag.Status = "Success";
                ViewBag.Message = "Authorization code generated successfully";
                ViewBag.Data = authCode;
;


                return View("GetResultToFrontEnd");
                //return StatusCode(StatusCodes.Status201Created,
                //    new Response { Status = "Success", Message = "Authorization code generated successfully", Data = addAuthorizationCode }
                //    );
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Authorization code failed to generate" }
                    );
            }

        
            
        }
        [HttpGet]
        public IActionResult VerifyAuthCode()
        {

            return View();
        }
        [HttpPost]
        //I use <IActionResult> if i want to return Ok
        public async Task<IActionResult> VerifyAuthCode(VerifyAuthCodeClass verifyAuthCodeClass)
        {
            try
            {
               // var test = EnterAuthorisationCode.ToLower();
                //var all = await _db.AuthorizationCodes.Where(c => c.Code.ToLower() == test)
                var userInfo = await _db.AuthorizationCodes.Where(c => c.Code == verifyAuthCodeClass.EnterAuthorisationCode).FirstOrDefaultAsync();
                //var userInfoTest = ).FirstOrDefaultAsync();


                //return Ok(authCode);
                return View("DisplayAuthInfo", userInfo);
            }
            catch(Exception ex)
            {
                return View();
            }

            //var authCode = _db.AuthorizationCodes.Where(b => b.Code.Contains(EnterAuthorisationCode)).ToList();
            
        }

    }


}


    //"id": 1,
    //"enroleeName": "Olaoluwa Esan",
    //"enroleeNumber": "56893",
    //"enroleePhoneNumber": "08066738561",
    //"provider": "General Hospital, Somolu",
    //"diagnosis": "Head ach",
    //"issuedBy": null,
    //"issuedDate": null,
    //"code": "A4D8C3CB62B646"



