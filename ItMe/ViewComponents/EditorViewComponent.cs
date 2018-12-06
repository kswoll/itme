using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ItMe.ViewComponents
{
    public class EditorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string id, object model, ModelExpression aspFor, string label, bool required)
        {
            return View(new EditorViewComponentModel
            {
                Id = id,
                Label = label,
                IsRequired = required,
                Name = aspFor.Name,
                Value = aspFor.Metadata.PropertyGetter(model)
            });
        }
    }
}