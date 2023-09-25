using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class JwtSwaggerMiddleware
{
    private readonly RequestDelegate _next;

    public JwtSwaggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Check if the request is for the Swagger endpoint
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            // Check if the user is authenticated
            if (!context.User.Identity.IsAuthenticated)
            {
                // Return a 401 Unauthorized response if not authenticated
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        // Continue processing the request
        await _next(context);
    }
}
