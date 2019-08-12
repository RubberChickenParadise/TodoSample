using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPI.Managers
{
    public interface ISessionManager
    {
        User GetUserForRequest(HttpRequest request);
    }

    public class SessionManager : ISessionManager
    {
        private ISessionRepository _sessionRepo;

        public SessionManager(ISessionRepository sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        public User GetUserForRequest(HttpRequest request)
        {
            var sessionId = request.Headers["sessionid"].First();

            return _sessionRepo.ValidateSession(sessionId);
        }
    }
}
