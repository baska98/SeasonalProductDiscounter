using CodeCool.SeasonalProductDiscounter.Model.Users;
using CodeCool.SeasonalProductDiscounter.Service.Users;

namespace CodeCool.SeasonalProductDiscounter.Ui.Authentication;

public class UserAuthenticator
{
    private readonly IAuthenticationService _authenticationService;

    public UserAuthenticator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    private User GetUser()
    {
        string userName = GetTextInput("Please enter your username: ");
        string password = GetTextInput("Please enter your password: ");

        return new User(0,userName, password);
    }

    private static string GetTextInput(string text)
    {
        string input = string.Empty;

        while (input == string.Empty)
        {
            Console.Write(text);
            input = Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public bool Authenticate()
    {
        var user = GetUser();
        if (!_authenticationService.Authenticate(user))
        {
            Console.WriteLine("Invalid user");
            return false;
        }

        return true;
    }
}
