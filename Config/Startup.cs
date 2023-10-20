/***
 * Startup of the host and its services
 * - this is where we map the folders
 * - kick of our repositories
 * - and bring up the SignalR Hubs
 */
namespace net.applicationperformance.ChatApp.Config;
using Repositories;

/***
 * Startup class is given to the Web Host Builder
 */
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration; ;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Register services here
        services.AddControllers();
        services.AddSignalR();
        services.AddMvc();
        services.AddSingleton(new UserRepository());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure middleware and request handling here

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        // Map paths to static content
        app.mapStaticFiles(Path.Combine(env.ContentRootPath, "Content/css"), "/css");
        app.mapStaticFiles(Path.Combine(env.ContentRootPath,"Content/js"), "/js");
        
        // Define routing and endpoints
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<Hubs.ChatHub>("/chathub"); // Map the SignalR hub
            //endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllers();
        });
    }

}
