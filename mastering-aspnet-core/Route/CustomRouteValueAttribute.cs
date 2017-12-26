using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Route
{
    public class CustomRouteValueAttribute : RouteValueAttribute
    {
        public CustomRouteValueAttribute(string routeValue) : base("custom", routeValue)
        {
        }
    }
}
