using WinFormsFashionShop.Data.Entities;

namespace WinFormsFashionShop.Business.Services
{
    public interface IAuthService
    {
        bool ValidateCredentials(string username, string password);
        User? GetUserByUsername(string username);
    }
}