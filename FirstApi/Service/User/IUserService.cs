using FirstApi.Models;
using FirstApi.Tables;

namespace FirstApi.Service.User
{
    public interface IUserService
    {
        Task<Tables.AppUser> AddUser(UserRequestModel m);
        Task<AppUser?> GetUser(AuthModel m);
        string CreateToken(string userName);
        string GetCurrentLoggedIn();

    }
}
