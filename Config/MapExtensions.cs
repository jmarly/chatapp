using Microsoft.Extensions.FileProviders;

namespace net.applicationperformance.ChatApp.Config;

public static class MapExtensions
{
    public static IApplicationBuilder mapStaticFiles (this IApplicationBuilder app, string appPath, string requestPath) => 
        app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(appPath),
                RequestPath = requestPath // Specify the path prefix you want
            }
        );
}