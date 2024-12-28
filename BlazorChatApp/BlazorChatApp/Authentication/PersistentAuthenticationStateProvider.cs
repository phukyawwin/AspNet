using System.Security.Claims;
using BlazorChatApp.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlazorChatApp.Authentication
{
    public class PersistentAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
    {
        private readonly PersistentComponentState _state;
        private readonly IdentityOptions _identityOptions;
        private readonly PersistingComponentStateSubscription _stateSubscription;
        private Task<AuthenticationState> _stateTask;

        public PersistentAuthenticationStateProvider(PersistentComponentState state, IOptions<IdentityOptions> options)
        {
            _state=state;
            _identityOptions = options.Value;
            AuthenticationStateChanged += OnAuthenticationStateChanged;
            _stateSubscription = _state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
        }

       

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            _stateTask = task;
        }
        private async Task OnPersistingAsync()
        {
            var authenticationState=await _stateTask;
            var principal = authenticationState.User;
            if (principal.Identity?.IsAuthenticated ==true)
            {
                var userId=principal.FindFirst(_identityOptions.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(_identityOptions.ClaimsIdentity.EmailClaimType)?.Value;
                var fullName = principal.Claims.Where(f => f.Type == ClaimTypes.Name).Last().Value;
                if (userId != null && email != null && fullName != null)
                {
                    _state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        Id = userId,
                        FullName=fullName,
                        Email=email
                    });
                }
            }
           
        }
        public void Dispose()
        {
            _stateSubscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
