using System.Threading.Tasks;
using Uxios.Core.Models;

namespace Uxios.Core.Plugins
{
    public interface IUxiosPlugin
    {
        Task OnBeforeRequest(UxiosRequest request);
        Task OnAfterResponse<T>(UxiosResponse<T> response);
    }
}