using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SimpleShop.Infrastructure.Database;

namespace SimpleShop.Web.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void SetDefaultCulture(this WebApplication app)
        {
            var defaultCulture = new CultureInfo("pl-PL");
            defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
            defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";

            var supportedCultures = new[]
            {
                defaultCulture
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }

        public static void SeedDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                ApplicationDbContextInitializer.Initialize(context);
            }
        }
    }
}
