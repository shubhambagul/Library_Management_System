using Library_Management_System.Modules;
using Microsoft.AspNetCore.Http.HttpResults;
using Library_Management_System.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Library_Management_System.Service
{
    public class UserService:IUserSevice
    {
        private readonly LibraryManagementContext _context;
        private readonly Users _user;
        public UserService(LibraryManagementContext context, Users user)
        {
            _context = context;
            _user = user;
        }
        public bool createUser(UserDto userDTO)
        {
            try
            {
                var checkEmailId = _context.users.FirstOrDefault(x => x.Email == userDTO.Email);
                if (checkEmailId != null)
                {
                    return false;
                }
                _user.Name = userDTO.Name;
                _user.Email = userDTO.Email;
                _user.Password = userDTO.Password;
                _user.Role = userDTO.Role;
                _context.users.Add(_user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { 
            throw new Exception(ex.Message);
            }
        }
        public Users userLogin(LoginDto loginDTO)
        {
            try
            {
                return _context.users.FirstOrDefault(n => n.Email == loginDTO.Email && n.Password == loginDTO.Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
