using CodeCool.SeasonalProductDiscounter.Model.Users;
using CodeCool.SeasonalProductDiscounter.Service.Users;

namespace CodeCool.SeasonalProductDiscounter.Service.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public bool Authenticate(User user)
    {
        User userDB = _userRepository.Get(user.UserName);
        if (userDB == null)
        {
            return false;
        }
        if (userDB.UserName == user.UserName && userDB.Password == user.Password)
        {
            return true;
        }

        return false;
    }
}
