using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using net.applicationperformance.ChatApp.Repositories;


namespace ChatApp.Tests.Integration;

public class ChatAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    public static Mock<IUserRepository> mockRepo = null;
    protected override IHostBuilder CreateHostBuilder()
    {
        return net.applicationperformance.ChatApp.ChatApp.CreateHostBuilder(Array.Empty<string>())
            .UseEnvironment("Development")
            .ConfigureWebHostDefaults(webBuilder =>
            {
                
            })
            .ConfigureServices(services =>
            {
                var userRepo = services.SingleOrDefault(svc => svc.ServiceType == typeof(IUserRepository));
                if (mockRepo == null || userRepo == null) return;
                
                services.Remove(userRepo);
                services.AddSingleton(mockRepo.Object);
            });
    }
}