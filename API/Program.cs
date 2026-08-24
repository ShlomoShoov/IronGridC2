using API.Repositories;
using Consumer.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("mysql") ?? "Server=localhost;User=root;Port=3306;Password=12345;Database=iron_grid_db";
ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<IronGridDbContext>(options => options.UseMySql(connectionString, serverVersion));
builder.Services.AddScoped<IAssetsRepository , AssetsRepository>();
builder.Services.AddScoped<IAssetStatusRepository, AssetStatusRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
