using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public interface ISessionRepository
    {
        User ValidateSession(string sessionId);
        string GenerateSession(User user);
    }

    public class SessionRepository : ISessionRepository
    {
        private static Dictionary<string, User> _sessions = new Dictionary<string, User>();

        public User ValidateSession(string sessionId)
        {
            if (!_sessions.ContainsKey(sessionId))
            {
                throw new UnauthorizedAccessException();
            }

            return _sessions[sessionId];
        }


        public string GenerateSession(User user)
        {
            if (_sessions.Any(x => x.Value.Id == user.Id))
            {
                return _sessions.Single(x => x.Value.Id == user.Id)
                                .Key;
            }

            var key = Guid.NewGuid()
                          .ToString();

            _sessions.Add(key, user);

            return key;
        }
    }
}
