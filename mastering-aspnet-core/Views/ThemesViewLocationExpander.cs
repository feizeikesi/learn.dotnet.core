using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Views
{
    /// <summary>
    /// 自定义ViewLocationExpander
    /// 
    /// </summary>
    public class ThemesViewLocationExpander : IViewLocationExpander
    {
        public ThemesViewLocationExpander(string theme)
        {
            this.Theme = theme;
        }

        public string Theme { get; }

        /// <summary>
        /// 这将被调用来检索所需的视图位置
        /// </summary>
        /// <param name="context">通过context参数，可以访问所有的请求参数（HttpContext，RouteData，等）</param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            var theme = context.Values["theme"];

            return viewLocations
                .Select(x => x.Replace("/Views/", "/Views/" + theme + "/"))
                .Concat(viewLocations);
        }

        /// <summary>
        /// 用于初始化视图位置扩展器; 在这个例子中，我用它在上下文中传递了一些值
        /// </summary>
        /// <param name="context"></param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["theme"] = this.Theme;
        }
    }


}