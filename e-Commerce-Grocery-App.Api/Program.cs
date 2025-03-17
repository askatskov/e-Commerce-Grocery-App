using e_Commerce_Grocery_App.Api.Constants;
using e_Commerce_Grocery_App.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace e_Commerce_Grocery_App.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<DataContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString(DatabaseConstants.GroceryConnectionStringKey)));


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

var masterGroup = app.MapGroup("/masters").AllowAnonymous();

masterGroup.MapGet("/categories", async (DataContext context) =>
	await context.Categories
	.AsNoTracking()
	.ToArrayAsync()
);

masterGroup.MapGet("/offers", async (DataContext context) =>
	await context.Categories
	.AsNoTracking()
	.ToArrayAsync());


            app.Run("https://localhost:12345");
		}
	}
}
