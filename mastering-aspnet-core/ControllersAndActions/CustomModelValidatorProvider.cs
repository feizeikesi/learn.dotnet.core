using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControllersAndActions
{
    public class CustomModelValidatorProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            context.Results.Add(new ValidatorItem {Validator = new CustomObjectModelValidator()});
        }
    }

    public class CustomObjectModelValidator : IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
          /*  if (context.Model is ICustomValidatable)
            {
                //supply custom validation logic here and return a collection of ModelValidationResult
            }*/

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}