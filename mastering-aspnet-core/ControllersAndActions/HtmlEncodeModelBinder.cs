using System;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ControllersAndActions
{
    /// <summary>
    /// 自定义 ModelBinder
    /// https://www.stevejgordon.co.uk/html-encode-string-aspnet-core-model-binding
    /// </summary>
    public class HtmlEncodeModelBinder : IModelBinder
    {
        private readonly IModelBinder _fallbackBinder;

        public HtmlEncodeModelBinder(IModelBinder fallbackBinder)
        {
            if (fallbackBinder == null)
                throw new
                    ArgumentNullException(nameof(fallbackBinder));

            _fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);

            if (valueProviderResult.Length > 0)
            {
                var valueAsString = valueProviderResult.FirstValue;

                if (string.IsNullOrEmpty(valueAsString))
                {
                    return _fallbackBinder.BindModelAsync(bindingContext);
                }

                var result = HtmlEncoder.Default.Encode(valueAsString);

                bindingContext.Result = ModelBindingResult.Success(result);
            }

            return TaskCache.CompletedTask;
        }
    }


    public class HtmlEncodeAttribute : Attribute { }

    public class HtmlEncodeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if ((context.Metadata.ModelType == typeof(string)) &&
                (context.Metadata.ModelType.GetTypeInfo().IsDefined(typeof(HtmlEncodeAttribute))))
            {
                return new HtmlEncodeModelBinder(new SimpleTypeModelBinder(context.Metadata.ModelType));
            }

            return null;
        }
    }
}