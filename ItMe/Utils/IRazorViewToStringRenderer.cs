using System.Threading.Tasks;

namespace ItMe.Utils
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);        
    }
}