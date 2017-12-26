using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace ControllersAndActions.Controllers
{
    /// <summary>
    /// POCO() 方式
    /// 不需要继承自ControllerBase和Controller
    /// </summary>
    public class HomeController
    {
        private readonly IUrlHelperFactory _url;

        public HomeController(IHttpContextAccessor ctx,
            IUrlHelperFactory url)
        {
            this.HttpContext = ctx.HttpContext;
            this._url = url;
        }

        //自动注入
        [ControllerContext]
        public Controller Context { get; set; }

        public HttpContext HttpContext { get; set; }

        //自动注入
        [ActionContext]
        public ActionContext ActionContext { get; set; }

        //自动注入
        [ViewDataDictionary]
        public ViewDataDictionary ViewBag { get; set; }

        public IUrlHelper Url { get; set; }

        public string Index()
        {
            this.Url = this.Url ??
                       this._url.GetUrlHelper(this.ActionContext);
            return "Hello, World!";
        }

        /// <summary>
        /// 通过服务定位器获取服务
        /// </summary>
        /// <returns></returns>
        public IActionResult Get()
        {
            var accessor = this.HttpContext.RequestServices.GetService<IHttpContextAccessor>();
            return new EmptyResult();
        }

        /// <summary>
        /// 通过 FromServicesAttribute 注入服务
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public IActionResult Get([FromServices] IHttpContextAccessor accessor)
        {
            return new EmptyResult();
        }
    }
}