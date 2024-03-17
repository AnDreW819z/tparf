using tparf.dto.Auth;

namespace tparf.api.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<Status> ChangePassword(ChangePasswordModel model);
        public Task<LoginResponse> Login(LoginModel model);
        public Task<Status> Registration(RegistrationModel model);
        public Task<Status> ConfirmEmail(string token, string email);
        public Task<LoginResponse> Login(string email, string pass);
    }
}
