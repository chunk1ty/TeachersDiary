using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.Identity;

using TeachersDiary.Clients.Mvc.Infrastructure.Constants;
using TeachersDiary.Data.Services.Contracts;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Session
{
    public class SessionStateService : ISessionStateService
    {
        private readonly IUserService _userService;

        public SessionStateService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<SessionState> GetAsync(HttpContextBase httpContext)
        {
            var sessionState = (SessionState)httpContext.Session[MvcConstants.SessionKey];

            if (sessionState == null)
            {
                sessionState = new SessionState();
            }

            // if user is log in but session is exparied 
            if (sessionState.User == null && httpContext.User.Identity.IsAuthenticated)
            {
                sessionState = await RetrieveSessionStateAsync(httpContext);
                httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }

            return sessionState;
        }

        public async Task SyncAsync(HttpContextBase httpContext)
        {
            var sessionState = (SessionState)httpContext.Session[MvcConstants.SessionKey];

            if (sessionState == null)
            {
                sessionState = new SessionState();
            }

            // if user is log in but session is exparied 
            if (sessionState.User == null && httpContext.User.Identity.IsAuthenticated)
            {
                sessionState = await RetrieveSessionStateAsync(httpContext);
                httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }
        }

        public void Abandon(HttpContextBase httpContext)
        {
            httpContext.Session.Abandon();
        }

        private async Task<SessionState> RetrieveSessionStateAsync(HttpContextBase httpContext)
        {
            var user = await _userService.GetUserByIdAsync(httpContext.User.Identity.GetUserId());

            return new SessionState() { User = user };
        }
    }
}