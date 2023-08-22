using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstApi.Filters
{
    public class CustomFilter : IActionFilter
    {
        private readonly AplicationDbContext _context;

        public CustomFilter(AplicationDbContext context)
        {
            _context = context;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = new UnauthorizedResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
