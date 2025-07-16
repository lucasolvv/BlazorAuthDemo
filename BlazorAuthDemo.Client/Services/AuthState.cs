using BlazorAuthDemo.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
namespace BlazorAuthDemo.Client.Services
{
    public class AuthState : AuthenticationStateProvider
    {
        private User? _currentUser;

        public async Task Login(User user)
        {
            _currentUser = user;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_currentUser != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, _currentUser.Name),
                new Claim(ClaimTypes.Email, _currentUser.Email)
            }, "FakeAuth");

                var user = new ClaimsPrincipal(identity);
                return Task.FromResult(new AuthenticationState(user));
            }

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }

    }
}
