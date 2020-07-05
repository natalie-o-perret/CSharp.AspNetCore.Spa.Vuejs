using Microsoft.Extensions.DependencyInjection;

namespace CSharp.AspNetCore.Spa.Vuejs.StartupExtensions
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddApiExplorerAndVersioning(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options => options.ReportApiVersions = true);

            return services;
        }
    }
}
