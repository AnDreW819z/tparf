using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using tparf.client.Utility;

namespace tparf.client.Services
{
	public class AuthProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _http;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationState _anonymous;
		public AuthProvider(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_http = httpClient;
			_localStorage = localStorage;
			_anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _localStorage.GetItemAsync<string>("accessToken");
			Console.WriteLine(token);
			if (string.IsNullOrEmpty(token) || token == "")
			{
				return _anonymous;
			}
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			return new AuthenticationState(new ClaimsPrincipal(
				new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuth")));
		}
		public void NotifyUserAuthentication(string username)
		{
			var authUser = new ClaimsPrincipal(new ClaimsIdentity(
				new[] { new Claim(ClaimTypes.Name, username) }, "jwtAuthType"
				));
			var authState = Task.FromResult(new AuthenticationState(authUser));
			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyUserLogout()
		{
			var authState = Task.FromResult(_anonymous);
			NotifyAuthenticationStateChanged(authState);
		}
	}
}
