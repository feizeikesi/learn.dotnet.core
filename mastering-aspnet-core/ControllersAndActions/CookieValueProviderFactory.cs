using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControllersAndActions
{
    /// <summary>
    /// 自定义 Action 参数
    /// </summary>
    public class CookieValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Add(new CookieValueProvider(context.ActionContext));
            return Task.CompletedTask;
        }
    }
}