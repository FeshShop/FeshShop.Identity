namespace FeshShop.Identity.Controllers;

using Common.Mediator.Contracts;
using Common.Mvc;
using Messages.Commands;
using Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class IdentityController(IMediator mediator, IIdentityService identityService) : ApiController(mediator)
{
    [HttpPost]
    [Route(nameof(SignUp))]
    public async Task<IActionResult> SignUp([FromBody] SignUpInputModel model)
    {
        model.BindId(x => x.Id);
        await identityService.SignUpAsync(model.Id, model.Email, model.Password, model.Role);

        return NoContent();
    }

    [HttpPost]
    [Route(nameof(SignIn))]
    public async Task<IActionResult> SignIn([FromBody] SignInInputModel model)
        => Ok(await identityService.SignInAsync(model.Email, model.Password));
}