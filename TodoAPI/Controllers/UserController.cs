using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.ComplexResponses;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepo;
        private ISessionRepository _sessionRepo;

        public UserController(IUserRepository userRepo,
                              ISessionRepository sessionRepo)
        {
            _userRepo = userRepo;
            _sessionRepo = sessionRepo;
        }

        [HttpPost]
        [Route("Login")]
        public LoginResponse Login(string userName,
                          string password)
        {
            try
            { 
            var resp = new LoginResponse();

            resp.User = _userRepo.Login(userName,
                                       password);

            resp.SessionToken = _sessionRepo.GenerateSession(resp.User);

            return resp;
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = 401;
                return null;
            }
        }
    }
}