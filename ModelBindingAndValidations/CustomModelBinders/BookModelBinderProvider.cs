using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ModelBinding.Models;

namespace ModelBinding.CustomModelBinders
{
    public class BookModelBinderProvider: IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(Book))
            {
                return new BinderTypeModelBinder(typeof(BookModelBinder));
            }
            return null;
        }
    }
}
