namespace E_Commerce.API.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string ErrorMessage { get; set; }

		public ApiResponse(int statusCode, string? errorMessage = null)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage ?? GetErrorMessageForStatusCode(StatusCode);
		}
		private string GetErrorMessageForStatusCode(int statusCode)
			=> statusCode switch
			{
				400 => "Bad Request",
				401 => "Un Authorized",
				404 => "Not Found",
				500 => "Internal Server Error",
			};
	}
}
