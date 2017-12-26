using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControllersAndActions
{
    public class CookieValueProvider : IValueProvider
    {
        private readonly ActionContext _actionContext;

        public CookieValueProvider(ActionContext actionContext)
        {
            this._actionContext = actionContext;
        }

        public bool ContainsPrefix(string prefix)
        {
            return this._actionContext.HttpContext.Request.Cookies.ContainsKey(prefix);
        }

        public ValueProviderResult GetValue(string key)
        {
            return new ValueProviderResult(this._actionContext.HttpContext.Request.Cookies[key]);
        }
    }
}
