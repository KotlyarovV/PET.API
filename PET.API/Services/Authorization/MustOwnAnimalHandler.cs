using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using PET.Application.Services;

namespace PET.API.Services.Authorization
{
    public class MustOwnAnimalHandler : AuthorizationHandler<MustOwnAnimalRequirement>
    {
        private readonly AnimalAppService animalAppService;
        private readonly UserAppService userAppService;
        private readonly IConfiguration configuration;

        public MustOwnAnimalHandler(AnimalAppService animalAppService, UserAppService userAppService,
            IConfiguration configuration)
        {
            this.animalAppService = animalAppService;
            this.userAppService = userAppService;
            this.configuration = configuration;
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
            var adminEmail = configuration.GetValue<string>("AdminEmail");

            if (animal == null || user == null || animal.OwnerId != user.Id || user.Email != adminEmail)
            {
                context.Fail();

                return;
            }

            context.Succeed(requirement);
        }
    }
}