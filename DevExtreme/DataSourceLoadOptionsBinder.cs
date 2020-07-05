using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CSharp.AspNetCore.Spa.Vuejs.DevExtreme
{
	public class DataSourceLoadOptionsBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var result = DataSourceLoadOptionsParser.Parse(bindingContext);
			result.Match(
				loadOptions =>
				{
					bindingContext.Result = ModelBindingResult.Success(loadOptions);
				}, failures =>
				{
					bindingContext.Result = ModelBindingResult.Failed();
					foreach (var failure in failures)
					{
						bindingContext.ModelState.AddModelError(
							failure.Name,
							$"'{failure.Value}' is not a valid value.");
					}
				});
			return Task.CompletedTask;
		}
	}
}
