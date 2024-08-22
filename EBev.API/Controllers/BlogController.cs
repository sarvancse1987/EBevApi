namespace EBev.API.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<BlogController> _logger;
        public BlogController(IBlogService blogService
            , ILogger<BlogController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(BlogVm request)
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _blogService.Add(request)
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

        [HttpPut("{blogId}/update")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int blogId, BlogVm request)
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _blogService.Update(blogId, request)
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

        [HttpGet("{blogId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int blogId)
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _blogService.Get(blogId)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while get Blog");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Error = new ApiErrorResponse($"Exception while get Blog details. Message: {ex.Message}")
                });
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new ApiResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Result = await _blogService.GetAll()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while get all Blog");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Error = new ApiErrorResponse($"Exception while get all Blog details. Message: {ex.Message}")
                });
            }
        }
    }
}
