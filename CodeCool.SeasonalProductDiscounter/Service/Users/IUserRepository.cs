using CodeCool.SeasonalProductDiscounter.Model.Users;

namespace CodeCool.SeasonalProductDiscounter.Service.Users;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    bool Add(User user);
    User Get(string name);
}
