using System.Text.RegularExpressions;

namespace Roomies.Application.Helpers;

public static class ValidationHelper
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        var emailRegex = new Regex(
            "^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}(?<!\\d+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return emailRegex.IsMatch(email);
    }

    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var passwordRegex = new Regex(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", RegexOptions.Compiled);
        return passwordRegex.IsMatch(password);
    }
}
