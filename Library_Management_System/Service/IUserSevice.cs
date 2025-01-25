using Library_Management_System.Modules;

namespace Library_Management_System.Service
{
    public interface IUserSevice
    {
        public bool createUser(UserDto userDTO);

        public Users userLogin(LoginDto loginDTO);
    }
}
