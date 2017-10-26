using System.Linq;
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
        private readonly ISchoolService _schoolService;

        public SessionStateService(IUserService userService, ISchoolService schoolService)
        {
            _userService = userService;
            _schoolService = schoolService;
        }

        public async Task<SessionState> GetAsync(HttpContextBase httpContext)
        {
            var sessionState = (SessionState)httpContext.Session[MvcConstants.SessionKey];

            if (sessionState == null)
            {
                sessionState = new SessionState();
                //httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }

            // if user is log in but session is exparied 
            if (sessionState.User == null && httpContext.User.Identity.IsAuthenticated)
            {
                sessionState = await RetrieveSessionStateAsync(httpContext);
                httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }

            return sessionState;
        }

        public async Task SetAsync(HttpContextBase httpContext)
        {
            var sessionState = await RetrieveSessionStateAsync(httpContext);
            httpContext.Session[MvcConstants.SessionKey] = sessionState;
        }

        public async Task SyncSessionAsync(HttpContextBase httpContext)
        {
            var sessionState = (SessionState)httpContext.Session[MvcConstants.SessionKey];

            if (sessionState == null)
            {
                sessionState = new SessionState();
                //httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }

            // if user is log in but session is exparied 
            if (sessionState.User == null && httpContext.User.Identity.IsAuthenticated)
            {
                sessionState = await RetrieveSessionStateAsync(httpContext);
                httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }
        }

        public void SyncSession(HttpContextBase httpContext)
        {
            var sessionState = (SessionState)httpContext.Session[MvcConstants.SessionKey];

            if (sessionState == null)
            {
                sessionState = new SessionState();
                //httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }

            // if user is log in but session is exparied 
            if (sessionState.User == null && httpContext.User.Identity.IsAuthenticated)
            {
                sessionState = RetrieveSessionState(httpContext);
                httpContext.Session[MvcConstants.SessionKey] = sessionState;
            }
        }

        public async Task RefreshSessionState(HttpContextBase httpContext)
        {
            httpContext.Session.Abandon();

            var sessionState = await RetrieveSessionStateAsync(httpContext);
            httpContext.Session[MvcConstants.SessionKey] = sessionState;
        }

        public void Abandon(HttpContextBase httpContext)
        {
            httpContext.Session.Abandon();
        }

        private async Task<SessionState> RetrieveSessionStateAsync(HttpContextBase httpContext)
        {
            var user = await _userService.GetUserByIdAsync(httpContext.User.Identity.GetUserId());

            return new SessionState() {User = user };
        }

        private SessionState RetrieveSessionState(HttpContextBase httpContext)
        {
            var user = _userService.GetUserById(httpContext.User.Identity.GetUserId());

            //var school = Task.Run(async () => await _schoolService.GetAllAsync()).Result;

            return new SessionState() { User = user };
        }
    }

    public interface ISessionStateService
    {
        Task<SessionState> GetAsync(HttpContextBase httpContext);

        Task SetAsync(HttpContextBase httpContext);

        Task SyncSessionAsync(HttpContextBase httpContext);

        void SyncSession(HttpContextBase httpContext);

        void Abandon(HttpContextBase httpContext);
    }
}