using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Culture
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-Us"),
                new CultureInfo("ar-SA"),
                new CultureInfo("ur-PK"),
                new CultureInfo("hi-IN"),
                new CultureInfo("en-AU")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-Us"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html;charset=utf-8";

                var detectedCultureName = CultureInfo.CurrentCulture.DisplayName;
                var detectedUICulture = CultureInfo.CurrentUICulture.DisplayName;

                var cultureTable = "<html><body>"
                + "<table border=\"1\">"
                + $"<tr><td>Deducted Culture</td><td>{detectedCultureName}</td></tr>"
                + $"<tr><td>Deducted UI Culture</td><td>{detectedUICulture}</td></tr>"
                + $"<tr><td>Today's Date</td><td>{DateTime.Now:D}</td></tr>"
                + $"<tr><td>Culture's Formatted Number</td><td>{(1234567.89):n}</td></tr>"
                + $"<tr><td>Culture's Currency</td><td>{(42):c}</td></tr>"
                + $"</body></html>";

                await context.Response.WriteAsync(cultureTable);
            });
        }
    }
}
