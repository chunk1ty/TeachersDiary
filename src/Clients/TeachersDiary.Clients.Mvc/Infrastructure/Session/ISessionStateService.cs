using System.Threading.Tasks;
using System.Web;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Session
{
    public interface ISessionStateService
    {
        Task<SessionState> GetAsync(HttpContextBase httpContext);

        Task SyncAsync(HttpContextBase httpContext);

        void Abandon(HttpContextBase httpContext);
    }
}