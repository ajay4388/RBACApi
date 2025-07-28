using System.Security.Claims;

namespace RBAC.API.CustomMiddleware
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _requiredRole;

        public CustomAuthorizationMiddleware(RequestDelegate next, string requiredRole)
        {
            _next = next;
            _requiredRole = requiredRole;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null || !string.Equals(roleClaim.Value, _requiredRole, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"Access denied. Requires role: {_requiredRole}");
                return;
            }

            await _next(context);
        }
    }


    
}
