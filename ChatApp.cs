using Microsoft.AspNetCore;
namespace net.applicationperformance.ChatApp;
using Config;

public static class ChatApp
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        var host = WebHost.CreateDefaultBuilder(args);
        host.UseStartup<Startup>();
        return host;
    }

}