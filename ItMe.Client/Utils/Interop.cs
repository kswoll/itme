using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.JSInterop;

namespace ItMe.Client.Utils
{
    public static class Interop
    {
        public static async Task<bool> ToggleClass(ElementRef element, string className)
        {
            return await JSRuntime.Current.InvokeAsync<bool>("interop.toggleClass", element, className);
        }

        public static async Task SetDocumentTitle(string title)
        {
            await JSRuntime.Current.InvokeAsync<object>("interop.setDocumentTitle", title);
        }

        public static async Task SetWindowLocation(string url)
        {
            await JSRuntime.Current.InvokeAsync<object>("interop.setWindowLocation", url);
        }
    }
}