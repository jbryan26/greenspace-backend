using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Auth;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Authentication and authorisation controller.
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]/[action]")]

    public class AuthController : Controller
    {
        private const string passSalt = "mysuperrandomsalt";

        private readonly ReservationsDbContext _dbContext;
        //  private readonly MailJetService _mailService;

        public AuthController(ReservationsDbContext dbContext /*, MailJetService mailService*/)
        {
            _dbContext = dbContext;
            // _mailService = mailService;
        }

        /// <summary>
        /// Login and getting auth token that should be used with any request
        /// </summary>
        /// <param name="logAndPass">login and password struct</param>
        /// <returns>token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoginResult), 200)]
        [ProducesResponseType(404)]
        public IActionResult Login([FromBody] LogAndPass logAndPass)
        {
            var tokenProvider = new TokenProvider();

            var user = _dbContext.Users.FirstOrDefault(user => user.Email == logAndPass.Login);
            if (user == null) return NotFound("User not found");

            var token = tokenProvider.LoginUser(logAndPass.Login, logAndPass.Password, user);
            if (token == null) return NotFound("Login or password is incorrect");
            else
            {
                HttpContext.Session.SetString("JWToken", token);
                user.PasswordHash = "";

                return Ok(new LoginResult()
                {
                    Token = token,
                    UserInfo = user
                });
            }
        }

        /// <summary>
        /// Logout a user and invalidates his token
        /// </summary>
        /// <param name="logAndPass">login and password struct</param>
        /// <returns>token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult Logout()
        {
            /*var tokenProvider = new TokenProvider();

            var user = _dbContext.Users.FirstOrDefault(user => user.Email == logAndPass.Login);
            if (user == null) return NotFound("User not found");

            var token = tokenProvider.LoginUser(logAndPass.Login, logAndPass.Password, user);*/
            var token = HttpContext.Session.GetString("JWToken");
            if (token == null) return NotFound("Token not presented");
            else
            {
                HttpContext.Session.Clear();
                return Ok();
            }
        }

        /// <summary>
        /// Creation of new user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateAccount(string email, string pass, UserRoles userRole)
        {
            if (_dbContext.Users.Any(user => user.Email.ToLower() == email.ToLower()))
                return BadRequest("User with such email already exists");

            //if requested creation of admins then checking rights
            if (userRole == UserRoles.Admin)
            {
                if (User.FindFirst("AccessLevel")?.Value != UserRoles.SuperAdmin.ToString())
                    return Unauthorized("you have to be  SuperAdmin to create other admins");
            }

            if (userRole == UserRoles.SiteAdmin)
            {
                if (User.FindFirst("AccessLevel")?.Value != UserRoles.Admin.ToString() &&
                    User.FindFirst("AccessLevel")?.Value != UserRoles.SuperAdmin.ToString())
                    return Unauthorized("you have to be Admin or superAdmin to create site admins");
            }

            //generate pass
            //todo: in prod make pass stronger
            // var pass = Guid.NewGuid().ToString("n").Substring(0, 3);
            var computedHash = CryptographyProcessor.Hash(pass);

            //send email
            //  var res = await _mailService.SendRegistrationMail(email, pass);

            var usr = new User()
            {
                Email = email,
                PasswordHash = computedHash,
                UserRole = userRole
            };
            _dbContext.Users.Add(usr);
            _dbContext.SaveChanges();

            return Ok(usr);

        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetHash(string password)
        {
            return Ok(CryptographyProcessor.Hash(password));
        }

        private string GenerateHash(string pass)
        {
            var md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(passSalt + pass));
            string computedHash = BitConverter.ToString(bytes).Replace("-", "");
            return computedHash;
        }

        public class LogAndPass
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public class LoginResult
        {
            public string Token { get; set; }

            public User UserInfo { get; set; }
        }
}
}