using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using TaskPro.Helpers;
using TaskPro.Models.Shared;

namespace TaskPro.Security
{
    public class Authorization : IAuthorizationFilter
    {
        private readonly TokenHelper tokenHelper;
        public Authorization(IConfiguration configuration)
        {
            this.tokenHelper = new TokenHelper(configuration["Jwt:Key"], "", "");
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var routesValues = context.ActionDescriptor.RouteValues.ToArray();
            var action = routesValues[0].Value;
            var controller = routesValues[1].Value;
            StringValues authorizationHeaders;

            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationHeaders))
            {
                string token = authorizationHeaders.ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token)) throw new Exception("Invalid token");
                try
                {
                    var id = tokenHelper.ValidateToken(token);
                    if (string.IsNullOrEmpty(id)) throw new Exception("Invalid token");

                    var user = (Convert.ToInt32(id)).getUser();
                    if (user is null) throw new Exception($"El usuario con el id=, no existe");

                    context.HttpContext.Items["id"] = Convert.ToInt32(id);
                }
                catch (Exception ex)
                {
                    context.Result = new UnauthorizedObjectResult(new { Mensaje = ex.Message });
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(new { Mensaje = "No estás autorizado para acceder a este recurso" });
            }
        }
    }
}
