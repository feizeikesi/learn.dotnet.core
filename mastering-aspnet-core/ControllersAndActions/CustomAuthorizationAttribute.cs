using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ControllersAndActions
{
    /// <summary>
    /// 自定义Action约束
    /// </summary>
    public class CustomAuthorizationAttribute : Attribute, IActionConstraint
    {
        public int Order { get; } = int.MaxValue;

        public bool Accept(ActionConstraintContext context)
        {
            return context.CurrentCandidate.Action.DisplayName.Contains(
                "Authorized");
        }
    }
}