using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public interface IAuthService
    {
        bool ValidateCredentials(string username, string password);
        UserDTO? GetUserByUsername(string username);
    }
}