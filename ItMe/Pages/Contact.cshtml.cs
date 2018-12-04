using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItMe.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty(Name = "g-recaptcha-response")]
        public string GoogleRecaptchaResponse { get; set; }

        private readonly IRazorViewToStringRenderer renderer;

        public ContactModel(IRazorViewToStringRenderer renderer)
        {
            this.renderer = renderer;
        }

        public void OnGet()
        {
        }

/*
        public async Task<IActionResult> OnGetAsync()
        {
            var text = await renderer.RenderViewToStringAsync("ContactEmail", new ContactEmailModel());
            return Content(text, "text/plain");
        }
*/

        public async Task<IActionResult> OnPostAsync()
        {
            if (!await RecaptchaUtils.ValidateResponse(GoogleRecaptchaResponse))
            {
                return Unauthorized();
            }


            var text = await renderer.RenderViewToStringAsync("ContactEmail", new ContactEmailModel
            {
                Name = Name
            });

            using (var client = Aws.CreateEmailClient())
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = "me@kirkwoll.com",
                    Destination = new Destination
                    {
                        ToAddresses = new List<string>
                        {
                            "kirk.woll@gmail.com"
                        }
                    },
                    Message = new Message
                    {
                        Subject = new Content(Name),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = text
                            }
                        }
                    }
                };

                await client.SendEmailAsync(sendRequest);
            }

            return RedirectToPage("Index");

        }
    }
}