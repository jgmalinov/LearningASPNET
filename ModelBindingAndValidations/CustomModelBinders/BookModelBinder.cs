using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBinding.Models;

namespace ModelBinding.CustomModelBinders
{
    public class BookModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Book book = new Book();
            if (bindingContext.ValueProvider.GetValue("FirstName").Length != 0)
            {
                book.Author = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
            }
            if (bindingContext.ValueProvider.GetValue("LastName").Length != 0)
            {
                book.Author += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
            }
            if (bindingContext.ValueProvider.GetValue("BookName").Length != 0)
            {
                book.Name = bindingContext.ValueProvider.GetValue("BookName").FirstValue;
            }
            if (bindingContext.ValueProvider.GetValue("Description").Length != 0)
            {
                book.Description = bindingContext.ValueProvider.GetValue("Description").FirstValue;
            }
            bindingContext.Result = ModelBindingResult.Success(book);
            return Task.CompletedTask;
        }
    }
}
