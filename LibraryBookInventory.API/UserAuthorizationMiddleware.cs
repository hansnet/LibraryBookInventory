namespace LibraryBookInventory.API
{
    public class UserAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Path.StartsWithSegments("/api/Authentication/login") || context.Request.Path.StartsWithSegments("/api/Authentication/register"))
            {
                await _next(context);
                return;
            }

            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("You are not authenticated");
                return;
            }

            if (!context.User.IsInRole("Admin"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("You do not have permission to access this resource");
                return;
            }

            await _next(context);
        }
    }
}
