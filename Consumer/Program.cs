using Consumer.DAL;
using Consumer.Models;
using Consumer.Orchestrators;
using Consumer.Repositories;
using Consumer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var configs = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

KafkaConsumerSetting kafkaSettings = new KafkaConsumerSetting();
configs.GetSection("kafka").Bind(kafkaSettings);

string connectionString = configs.GetConnectionString("mysql") ?? "";
ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);


ServiceCollection serviceDescriptors = new ServiceCollection();

serviceDescriptors.AddDbContext<IronGridDbContext>(options=> options.UseMySql(connectionString, serverVersion));
serviceDescriptors.AddSingleton<IIRonGridLogger, ConsoleLogger>();
serviceDescriptors.AddScoped<AssetLiveStatusRepository>();
serviceDescriptors.AddSingleton<ConsumerOrchestrators>();

ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();


ConsumerOrchestrators orchestrators = serviceProvider.GetRequiredService<ConsumerOrchestrators>();

await orchestrators.InitAsync();
