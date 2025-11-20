using IDB.UI.Components;
using DevExpress.Blazor;

using Microsoft.AspNetCore.Http.Features;
namespace IDB.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);
            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(IDB.Model.Cell));
            
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddScoped<AppState>();
            builder.Services.AddScoped<IDB.Business.Business>();
            builder.Services.AddDevExpressServerSideBlazorPdfViewer();
            builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);
            builder.Services.AddMvc();
            builder.Services.AddDevExpressBlazor();
            builder.Services.AddDevExpressServerSideBlazorReportViewer();
      
            builder.Services.AddServerSideBlazor()
                .AddHubOptions(options =>
                {
                    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
                    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
                    options.MaximumReceiveMessageSize = 32 * 1024 * 1024; // 32MB
                });

      
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 32 * 1024 * 1024; // 32MB
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 32 * 1024 * 1024; // 32MB
            });
            builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options =>
            {
                options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
            });

            var app = builder.Build();

          
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
               
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
