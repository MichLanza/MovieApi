using Microsoft.EntityFrameworkCore;
using MovieApi.Db;
using MovieApi.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Registro de servicios

var configuration = builder.Configuration;

//Config EF
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MovieDb")));
builder.Services.AddAutoMapper(typeof(Program));


//builder.Services.AddTransient<IFileStorage, AzureStorageService>(); //para azure
builder.Services.AddTransient<IFileStorage, LocalStorage>();
builder.Services.AddHttpContextAccessor();


#endregion






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
