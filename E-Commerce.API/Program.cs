using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.DataSeeding;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			// Redit is used to store objects in the "RAM" of the server "the server that the application is deployed on , not the client RAM"
			// in repository layer , search for "" and install it from nuget packages

			#region Services

			var builder = WebApplication.CreateBuilder(args);


			// Add services to the container.

			builder.Services
				.AddDbContext<DataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
			// Don't forget to add the project reference in the Api project (references Repository project) 

			builder.Services.AddControllers();        // Allowa the dependancy injection for the controllers
													  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			
			
			// builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

			builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
			builder.Services.AddScoped<PictureUrlResolver>();


			// This is for changing the response of the "Bad Request" Response , (when sending a string instead of a int in the "GetById" method)
			builder.Services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState.Where(e => e.Value.Errors.Any()).SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();
					
					return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors});
				};
			});



			// For Redit 
			builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
			{
				var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RadisConnection"));
				return ConnectionMultiplexer.Connect(config);
			});


			builder.Services.AddScoped<IBasketService , BasketServices>();
			builder.Services.AddScoped<IBasketRepository , BasketRepository>();
			builder.Services.AddScoped<ICashService , CashService>();
			#endregion

			var app = builder.Build();
			await InitializeDBAsync(app);
			#region Pipelines / Middlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();
			app.UseMiddleware<CustomExceptionHandler>();     // for the internal server exception

			app.Run();

			#endregion
		}

		private static async Task InitializeDBAsync(WebApplication app)              // takes app (which is a WebApplication)
		{
			// Steps : 
			// Create the DB if not exists
			// Apply Seeding


			// new way for making dependancy injection 
			using (var scope = app.Services.CreateScope())
			{
				var service = scope.ServiceProvider;
				var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

				try
				{
					var context = service.GetRequiredService<DataContext>();

					// Create the DB if not exists
					if ((await context.Database.GetPendingMigrationsAsync()).Any())
						await context.Database.MigrateAsync();

					// Apply Seeding
					await DataContextSeed.SeedDataAsync(context);

				}
				catch (Exception ex)
				{
					//Console.WriteLine(ex);     --- Not allowed here !
					//  How to log the error ?
					
					var logger = LoggerFactory.CreateLogger<Program>();
					logger.LogError(ex.Message);
				}
			}


		}
	}
}
