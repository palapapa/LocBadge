using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace LocBadge.ModelBinders;

public class LocModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        return Task.CompletedTask;
    }
}
