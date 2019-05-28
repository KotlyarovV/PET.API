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
        private readonly UserAppService userAppService;

        public MustOwnAnimalHandler(AnimalAppService animalAppService, UserAppService userAppService)
        {
            this.animalAppService = animalAppService;
            this.userAppService = userAppService;
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
            var user = await userAppService.Get(userEmail);

            //todo: сделать роли и проверять админскую роль
            if (animal == null || animal.OwnerId != user.Id || user.Name != "admin")
            {
                context.Fail();

                return;
            }

            context.Succeed(requirement);
        }
    }
}