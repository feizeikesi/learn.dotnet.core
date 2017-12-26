using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Models;

namespace Route.Controllers
{
    public class HomeController : Controller
    {
        [CustomRouteValue("foo")]
        public IActionResult Index()
        {
            var foo = ControllerContext.RouteData.Values["foo"];
            return this.View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


        //默认未匹配到的页面路由 
        [HttpGet("{*url}", Order = int.MaxValue)]
        public IActionResult CatchAll()
        {
            this.Response.StatusCode = StatusCodes.Status404NotFound;
            return this.View();
        }

        //配合app.UseStatusCodePagesWithReExecute 使用
        [Route("error/404")]
        public IActionResult Error404()
        {
            this.Response.StatusCode = StatusCodes.Status404NotFound;
            return this.View();
        }
    }
}