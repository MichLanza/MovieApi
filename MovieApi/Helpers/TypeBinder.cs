using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace MovieApi.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var name = bindingContext.ModelName;
            var provider = bindingContext.ValueProvider.GetValue(name);

            if (provider == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var values = JsonConvert.DeserializeObject<T>(provider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(values);
            }
            catch
            {

                bindingContext.ModelState.TryAddModelError(name, "valor invalido");
            }

            return Task.CompletedTask;

        }
    }
}
