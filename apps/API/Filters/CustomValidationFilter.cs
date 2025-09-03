using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Validators
{
    public class CustomValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Success = 0,
                    Message = "La información recibida no es válida. Por favor, revise los datos e intente nuevamente."
                });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Success = 0,
                    Message = "La información recibida no es válida. Por favor, revise los datos e intente nuevamente."
                });
            }
        }
    }
}