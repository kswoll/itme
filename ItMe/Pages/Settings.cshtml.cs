using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Transfer;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly ItMeDb db;

        public SettingsModel(ItMeDb db)
        {
            this.db = db;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(IFormFile file)
        {
            var id = User.GetId();
            var person = await db.Persons.SingleAsync(x => x.Id == id);
            var key = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

            using (var client = Aws.CreateClient())
            using (var stream = file.OpenReadStream())
            {
                var transfer = new TransferUtility(client);
                await transfer.UploadAsync(stream, "laddler", key);

                if (person.FavIconS3Key != null)
                {
                    await client.DeleteObjectAsync("laddler", person.FavIconS3Key);
                }
            }

            person.FavIconS3Key = key;
            await db.SaveChangesAsync();

            return RedirectToPage("Settings");
        }
    }
}