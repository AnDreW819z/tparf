using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using tparf.client.Interfaces;
using tparf.dto.Auth;

namespace tparf.client.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationStateProvider _authStateProvider;
		private readonly string baseUrl;
		public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
			_authStateProvider = authStateProvider;
			baseUrl = "https://tparf-api.ru:443/api/Authorization" /*"http://localhost:5149/api/Authorization"*/;
		}
		public async Task<LoginResponse> Login(LoginModel model)
		{

			var loginResult = await _httpClient.PostAsJsonAsync($"{baseUrl}/login", model);
			Console.WriteLine(loginResult.ToString());
			if (!loginResult.IsSuccessStatusCode)
				return new LoginResponse { StatusCode = 0, Message = "Server error" };
			var loginResponseContent = await loginResult.Content.ReadFromJsonAsync<LoginResponse>();
			if (loginResponseContent != null && loginResponseContent.Token != "")
			{
				_localStorage.SetItemAsync("accessToken", loginResponseContent.Token);
				((AuthProvider)_authStateProvider).NotifyUserAuthentication(loginResponseContent.Token);
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponseContent.Token);

				await _localStorage.SetItemAsync<long>("userId", loginResponseContent.Id);
				await _localStorage.SetItemAsync<long>("cartId", loginResponseContent.CartId);
			}
			return loginResponseContent;

		}

		public async Task<LoginResponse> Registration(RegistrationModel model)
		{
			var registerResult = await _httpClient.PostAsJsonAsync($"{baseUrl}/registration", model);
			var loginResponseContent = await registerResult.Content.ReadFromJsonAsync<LoginResponse>();
			if (loginResponseContent != null)
			{
				_localStorage.SetItemAsync("accessToken", loginResponseContent.Token);
				((AuthProvider)_authStateProvider).NotifyUserAuthentication(loginResponseContent.Token);
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponseContent.Token);

				await _localStorage.SetItemAsync<long>("userId", loginResponseContent.Id);
				await _localStorage.SetItemAsync<long>("cartId", loginResponseContent.CartId);
			}
			return loginResponseContent;
		}
		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("accessToken");
			await _localStorage.RemoveItemAsync("userId");
			await _localStorage.RemoveItemAsync("cartId");
			((AuthProvider)_authStateProvider).NotifyUserLogout();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}
