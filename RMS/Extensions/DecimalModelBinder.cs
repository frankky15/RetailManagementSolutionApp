using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Extensions.ModelBinders;

public class DecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var decimalString = valueProviderResult.FirstValue;

        var cultureInfo = CultureInfo.InvariantCulture;
        // if the first regex matches, the number string is in us culture
        if (Regex.IsMatch(decimalString, @"^(:?[\d,]+\.)*\d+$"))
            cultureInfo = new CultureInfo("en-US");

        // if the second regex matches, the number string is in de culture
        else if (Regex.IsMatch(decimalString, @"^(:?[\d.]+,)*\d+$"))
            cultureInfo = new CultureInfo("de-DE");

        if (!Decimal.TryParse(decimalString, cultureInfo, out Decimal num))
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Decimal numbers should use ' . ' and not ' , ' for decimal divisor.");
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(num);
        return Task.CompletedTask;
    }
}
