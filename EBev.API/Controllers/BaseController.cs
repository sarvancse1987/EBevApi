namespace EBev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EBevPolicy")]
    //[Authorize(Roles = "EBevUser")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
    }
}
