namespace FeshShop.Identity.Controllers
{
    using FeshShop.Common.Mediator.Contracts;
    using FeshShop.Common.Mvc;
    using FeshShop.Identity.Messages.Commands;
    using FeshShop.Identity.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IMediator mediator, IIdentityService identityService) 
            :base(mediator)
            => this.identityService = identityService;

        [HttpPost]
        [Route(nameof(SignUp))]
        public async Task<IActionResult> SignUp([FromBody] SignUpInputModel model)
        {
            model.BindId(x => x.Id);
            await identityService.SignUpAsync(model.Id, model.Email, model.Password, model.Role);

            return this.NoContent();
        }

        [HttpPost]
        [Route(nameof(SignIn))]
        public async Task<IActionResult> SignIn([FromBody] SignInInputModel model)
            => this.Ok(await this.identityService.SignInAsync(model.Email, model.Password));
    }
}
