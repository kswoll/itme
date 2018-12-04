using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItMe.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IRazorViewToStringRenderer renderer;

        public ContactModel(IRazorViewToStringRenderer renderer)
        {
            this.renderer = renderer;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var text = await renderer.RenderViewToStringAsync("/Pages/ContactEmail.cshtml", new ContactEmailModel());
            return Content(text, "text/plain");
        }
    }
}