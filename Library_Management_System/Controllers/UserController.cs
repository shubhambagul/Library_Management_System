using Library_Management_System.Modules;
using Library_Management_System.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSevice _userSevice;
        private readonly Users _user;
        public UserController(IUserSevice userSevice, Users user)
        {
            _userSevice = userSevice;
            _user = user;
        }
        [HttpPost]
        [Route("AddUser")]
        public ActionResult createUsers([FromBody] UserDto userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid user data.");
            }
            var isSuccess = _userSevice.createUser(userDTO);
            if (isSuccess)
            {
                return Ok("Login Success");
            }
            return BadRequest("Email Id Already Exist");
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult userLogin([FromBody] LoginDto loginDTO) { 
        var checkUser = _userSevice.userLogin(loginDTO);
            if (checkUser == null) { 
            return BadRequest("Invilid user");
            }
            return Ok(checkUser);
        }
    }
}
