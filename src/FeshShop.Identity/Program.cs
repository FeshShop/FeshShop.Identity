namespace FeshShop.Identity
{
    using FeshShop.Common.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args) 
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(wb => wb.UseStartup<Startup>().UseLogging())
                .Build()
                .Run();
    }
}
