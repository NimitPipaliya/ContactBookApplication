using ContactBookApplication.Models;

namespace ContactBookApplication.Data.Contract
{
    public interface IAuthRepository
    {
        bool RegisterUser(User user);

        User? ValidateUser(string username);

        bool UserExists(string loginId, string email);
    }
}
