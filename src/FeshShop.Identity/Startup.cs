namespace FeshShop.Identity
{
    using FeshShop.Common;
    using FeshShop.Common.Mongo;
    using FeshShop.Common.Mongo.Contracts;
    using FeshShop.Common.Mvc;
    using FeshShop.Common.Swagger;
    using FeshShop.Identity.Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;

    public class Startup
    {
        private const string CorsPolicy = nameof(CorsPolicy);
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInitializers(typeof(IMongoDbInitializer))
                .AddScoped(typeof(IMongoRepository<User>), typeof(MongoRepository<User>))
                .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddServices(Assembly.GetExecutingAssembly())
                .AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy, cors =>
                            cors.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders(Headers));
                })
                .AddMongoDatabase(this.Configuration)
                .AddSwagger(this.Configuration)
                .AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicy)            
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseSwagger()
                .UseEndpoints(endpoints => endpoints.MapControllers());

            startupInitializer.InitializeAsync();
        }
    }
}
