using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;


namespace ChatApp.Tests.Integration;

public class ChatAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    protected override IHostBuilder CreateHostBuilder()
    {
        return net.applicationperformance.ChatApp.ChatApp.CreateHostBuilder(Array.Empty<string>())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // add specific test configs here
            });
    }
}