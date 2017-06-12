using Darts.Models.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Darts.Filters
{
    public class CustomerFilter : ActionFilterAttribute
    {
        private readonly ISpelerRepository _customerRepository;

        public CustomerFilter(ISpelerRepository customerRespoitory)
        {
            _customerRepository = customerRespoitory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["customer"] =
                context.HttpContext.User.Identity.IsAuthenticated ?
                _customerRepository.GetBy(context.HttpContext.User.Identity.Name)
                : null;
            base.OnActionExecuting(context);
        }
    }
}
