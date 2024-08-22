using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace EBev.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("EBevPolicy")]
    [Authorize(Roles = "EBevUser")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Nullable))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorMessage))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorMessage))]
    public abstract class BaseController : ControllerBase
    {
    }
}
