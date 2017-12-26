using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Route
{
    /**
     * 
     * 自定义路由约束
     * 
     */
    public class EvenIntRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if ((values.ContainsKey(routeKey) == false) || (values[routeKey] == null))
            {
                return false;
            }

            var value = values[routeKey].ToString();
            int intValue;

            if (int.TryParse(value, out intValue) == false)
            {
                return false;
            }

            return (intValue % 2) == 0;
        }
    }

    public class IsAuthenticatedRouteConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            return httpContext.Request.Cookies.ContainsKey("auth");
        }
    }
}