using CodeCool.SeasonalProductDiscounter.Model.Users;

namespace CodeCool.SeasonalProductDiscounter.Service.Users;

public interface IAuthenticationService
{
    bool Authenticate(User user);
}
