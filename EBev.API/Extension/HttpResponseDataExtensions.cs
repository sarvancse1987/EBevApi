namespace EBev.API.Extension
{
    public static class HttpResponseDataExtensions
    {
        public static IActionResult CreateBadRequestResult(string errorMessage)
        {
            var apiResponse = new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Success = false,
                Error = new ApiErrorResponse(errorMessage)
            };

            return new BadRequestObjectResult(apiResponse);
        }
    }
}
