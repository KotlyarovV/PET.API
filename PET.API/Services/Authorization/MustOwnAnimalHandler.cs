using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using PET.Application.Services;

namespace PET.API.Services.Authorization
{
    public class MustOwnAnimalHandler : AuthorizationHandler<MustOwnAnimalRequirement>
    {
        private readonly AnimalAppService animalAppService;

        public MustOwnAnimalHandler(AnimalAppService animalAppService)
        {
            this.animalAppService = animalAppService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MustOwnAnimalRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;

            if (filterContext == null)
            {
                context.Fail();

                return;
            }

            var animalIdString = filterContext.RouteData.Values["id"]
                .ToString();

            if (!Guid.TryParse(animalIdString, out var animalId))
            {
                context.Fail();

                return;
            }

            var userEmail = context.User.FindFirstValue(ClaimTypes.Name);

            var animal = await animalAppService.Get(animalId);

            // todo: здесь не animal.Name, а animal.Owner что-то такое :)
            if (animal == null || animal.Name != userEmail)
            {
                context.Fail();

                return;
            }

            context.Succeed(requirement);
        }
    }
}