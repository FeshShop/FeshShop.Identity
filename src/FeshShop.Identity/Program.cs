using System.Reflection;
using FeshShop.Common;
using FeshShop.Common.Authentication;
using FeshShop.Common.Mediator;
using FeshShop.Common.Mongo;
using FeshShop.Common.Mongo.Contracts;
using FeshShop.Common.Mvc;
using FeshShop.Common.Swagger;
using FeshShop.Identity;
using FeshShop.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

const string corsPolicy = nameof(corsPolicy);
string[] headers = [ "X-Operation", "X-Resource", "X-Total-Count" ];

var builder = WebApplication.CreateBuilder(args);
builder
    .Services
    .AddInitializers(typeof(IMongoDbInitializer))
    .AddMongoDatabase(builder.Configuration)
    .AddHealthChecker(builder.Configuration)
    .AddMongoRepositories()
    .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
    .AddServices(Assembly.GetExecutingAssembly())
    .AddMediator()
    .AddJwt(builder.Configuration)
    .AddCors(options =>
    {
        options.AddPolicy(corsPolicy, cors =>
            cors.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders(headers));
    })
    .AddSwagger(builder.Configuration)
    .AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app
    .UseCors(corsPolicy)
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthorization()
    .UseSwagger()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapHealthCheckPath();
        endpoints.MapControllers();
    });

var startupInitializer = app.Services.GetRequiredService<IStartupInitializer>();
await startupInitializer.InitializeAsync();

await app.RunAsync();