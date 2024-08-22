namespace EBev.API.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonService personService
            , ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PersonVm request)
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _personService.Add(request)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while add new Blog");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Error = new ApiErrorResponse($"Exception while add new Blog details. Message: {ex.Message}")
                });
            }
        }

        [HttpPut("{personId}/update")]
        public async Task<IActionResult> Update([FromRoute] int personId, PersonVm request)
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _personService.Update(personId, request)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while update Blog");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Error = new ApiErrorResponse($"Exception while update Blog details. Message: {ex.Message}")
                });
            }
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> Get([FromRoute] int personId)
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _personService.Get(personId)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while get person");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Error = new ApiErrorResponse($"Exception while get person details. Message: {ex.Message}")
                });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _personService.GetAll()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while get all person");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Error = new ApiErrorResponse($"Exception while get all person details. Message: {ex.Message}")
                });
            }
        }
    }
}
