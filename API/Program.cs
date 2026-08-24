using API.Repositories;
using Consumer.DAL;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string sqlConnectionString = builder.Configuration.GetConnectionString("mysql") ?? "Server=localhost;User=root;Port=3306;Password=12345;Database=iron_grid_db";
ServerVersion serverVersion = ServerVersion.AutoDetect(sqlConnectionString);
string redisConnectionString = builder.Configuration.GetConnectionString("redis") ?? "localhost";

builder.Services.AddDbContext<IronGridDbContext>(options => options.UseMySql(sqlConnectionString, serverVersion));
builder.Services.AddScoped<IAssetsRepository , AssetsRepository>();
builder.Services.AddScoped<IAssetStatusRepository, AssetStatusRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
