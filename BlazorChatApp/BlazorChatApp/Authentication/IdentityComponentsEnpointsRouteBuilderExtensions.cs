using System.Security.Claims;
using BlazorChatApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlazorChatApp.Authentication
{
    internal static class IdentityComponentsEnpointsRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapAddition(this IEndpointRouteBuilder builder)
        {
            var accountGroup = builder.MapGroup("/Account");
            accountGroup.MapPost("/Logout", async(ClaimsPrincipal user,SignInManager<AppUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect("/");
            });
            return accountGroup;
            
        }
    }
}
