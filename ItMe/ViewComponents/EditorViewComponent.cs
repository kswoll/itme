using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ItMe.ViewComponents
{
    public class EditorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string id, object model, ModelExpression aspFor = null, string label = null, bool required = false)
        {
            return View(new EditorViewComponentModel
            {
                Id = id,
                Label = label,
                IsRequired = required,
                Binding = aspFor
            });
        }
    }
}