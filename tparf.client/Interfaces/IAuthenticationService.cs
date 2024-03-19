using tparf.dto.Auth;

namespace tparf.client.Interfaces
{
	public interface IAuthenticationService
	{
		public Task<LoginResponse> Login(LoginModel model);
		public Task<LoginResponse> Registration(RegistrationModel model);
		Task Logout();
	}
}
