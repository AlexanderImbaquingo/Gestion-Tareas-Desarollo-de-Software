using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionTareasDesarrolloSoftware.MVC.Filters
{
    public class AuthRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("JWToken");
            
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
            
            base.OnActionExecuting(context);
        }
    }
}