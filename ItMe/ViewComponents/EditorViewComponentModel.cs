using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ItMe.ViewComponents
{
    public class EditorViewComponentModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public bool IsRequired { get; set; }
        public ModelExpression Binding { get; set; }
    }
}