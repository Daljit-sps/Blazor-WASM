using Shared.ViewModels;

namespace Client.AuthProviders
{
    public interface IAuthenticationService
    {
        Task<LoginResultVM> Login(LoginVM userForAuthentication);
        Task Logout();
    }
}
