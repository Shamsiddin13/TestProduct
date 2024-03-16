using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;
using TestProductWeb.DbContexts;
using TestProductWeb.Extensions;
using TestProductWeb.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TestDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCustomService();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v2", new OpenApiInfo { Title = "ProductAPI.swagger", Version = "v2" });
});

// Serialog
var logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .Enrich.FromLogContext()
   .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v2/swagger.json", "ProductAPI.swagger");

});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
	c.RoutePrefix = "area/swagger";
});
app.UseMiddleware<TokenRedirectMiddleware>();

app.UseStatusCodePages(async context =>
{
	if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
	{
		context.HttpContext.Response.Redirect("home");
	}
});


app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
