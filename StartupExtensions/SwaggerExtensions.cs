using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace CSharp.AspNetCore.Spa.Vuejs.StartupExtensions
{
	/// <summary>
	/// For more information:
	/// - https://github.com/domaindrivendev/Swashbuckle
	/// - https://github.com/Microsoft/aspnet-api-versioning/wiki/Swashbuckle-Integration
	/// </summary>
	public static class SwaggerExtensions
	{
		public static IServiceCollection ConfigureAndAddSwaggerGen(this IServiceCollection services) =>
			services.AddSwaggerGen(options =>
			{
				var apiVerDescProvider = services
					.BuildServiceProvider()
					.GetRequiredService<IApiVersionDescriptionProvider>();

				foreach (var apiVerDesc in apiVerDescProvider.ApiVersionDescriptions)
				{
					options.SwaggerDoc(apiVerDesc.GroupName, CreateInfoForApiVersion(apiVerDesc));
				}

				var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
				var path = System.IO.Path.Combine(AppContext.BaseDirectory, fileName);
				options.IncludeXmlComments(path);
			});

		public static IApplicationBuilder ConfigureAndUseSwaggerUi(this IApplicationBuilder app) =>
			app.UseSwaggerUI(options =>
			{
				var apiVerDescProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
				foreach (var apiVerDesc in apiVerDescProvider.ApiVersionDescriptions)
				{
					var url = $"/swagger/{apiVerDesc.GroupName}/swagger.json";
					var name = apiVerDesc.GroupName.ToUpperInvariant();
					options.SwaggerEndpoint(url, name);
				}
			});
		private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description) =>
			new OpenApiInfo
			{
				Title = $"ASP.NET Core Vue Starter {description.ApiVersion}",
				Version = description.ApiVersion.ToString(),
			};
	}
}
