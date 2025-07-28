using RBAC.Data.Entities;

namespace RBAC.API.CustomMiddleware
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Viewer = "Viewer";
    }

    public static class RoleMiddlewareExtensions
    {
        public static IApplicationBuilder UseRoleProtection(this IApplicationBuilder app, string role)
        {
            return app.UseMiddleware<CustomAuthorizationMiddleware>(role);
        }
    }

}
