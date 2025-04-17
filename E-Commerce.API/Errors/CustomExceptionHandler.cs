using System.Text.Json;

namespace E_Commerce.API.Errors
{
	public class CustomExceptionHandler
	{
		// This is a new middleware

		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandler> _logger;
		private readonly IHostEnvironment _environment;

		public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger , IHostEnvironment environment)
		{
			_next = next;
			_logger = logger;
			_environment = environment;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);


				var response = _environment.IsDevelopment() ?
					new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace) :
					new ApiExceptionResponse(StatusCodes.Status500InternalServerError) ;

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				
				var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
				                                                                                  // json ==> camelCase ..                 
				await context.Response.WriteAsync(json);

				// We can make the 4 previous lines as comments and use this only : 

				// await context.Response.WriteAsJsonAsync(response);

			}
		}
	}
}
