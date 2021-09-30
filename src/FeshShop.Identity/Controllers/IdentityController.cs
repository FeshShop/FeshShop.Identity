namespace FeshShop.Identity.Controllers
{
    using FeshShop.Common.Mvc;
    using FeshShop.Identity.Messages.Commands;
    using FeshShop.Identity.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService) => this.identityService = identityService;

        [HttpPost]
        [Route(nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody]SignUpInputModel model)
        {
            model.BindId(x => x.Id);
            await identityService.SignUpAsync(model.Id, model.Email, model.Password);

            return this.NoContent();
        }
    }
}
