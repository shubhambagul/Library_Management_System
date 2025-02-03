using Library_Management_System.Modules;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSevice _userSevice;
        private readonly Users _user;
        private readonly IConfiguration _configuration;
        public UserController(IUserSevice userSevice, Users user, IConfiguration configuration)
        {
            _userSevice = userSevice;
            _user = user;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("AddUser")]
        public ActionResult createUsers([FromBody] UserDto userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return BadRequest("Invalid user data.");
                }
                var isSuccess = _userSevice.createUser(userDTO);
                if (isSuccess)
                {
                    return Ok("User Registered Success");
                }
                return BadRequest("Email Id Already Exist");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult userLogin([FromBody] LoginDto loginDTO)
        {
            try
            {
                var userLogin = _userSevice.userLogin(loginDTO);

                if (userLogin != null)
                {
                    var claims = new[]
                    {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", userLogin.UserId.ToString()),
            new Claim("Email", userLogin.Email),
            new Claim(ClaimTypes.Role, userLogin.Role) // Use the user's actual role
        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn
                    );

                    string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { Token = tokenValue, User = userLogin });
                }

                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }
    }
    }
