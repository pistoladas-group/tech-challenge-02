using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechNews.Web.Models;

namespace TechNews.Web.Filters;

public class ModelStateFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Result != null)
        {
            return;
        }

        if (!context.ModelState.IsValid)
        {
            var errors = new List<string>();
            foreach (var item in context.ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            context.Result = new BadRequestObjectResult(new AppResponseModel(errors: errors));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}