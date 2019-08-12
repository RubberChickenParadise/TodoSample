using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Managers;
using TodoAPI.Models;

/* -------------------------------------------------------------
     Auth should be handled in other places normally.  Doing this because its quick.
   ----------------------------------------------------------------*/


namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ISessionManager _sessionMgr;
        private IToDoRepository _todoRepo;

        public TodoController(ISessionManager sessionMgr,
                              IToDoRepository todoRepo)
        {
            _sessionMgr = sessionMgr;
            _todoRepo = todoRepo;
        }

        [HttpGet("")]
        public List<TodoItem> Get()
        {
            User user;

            try
            {
                user = _sessionMgr.GetUserForRequest(Request);
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = 401;
                return null;
            }

            return _todoRepo.GetTodoForUser(user.Id);
        }

        [HttpGet("{id:int}")]
        public TodoItem GetSingle(int id)
        {
            User user;

            try
            {
                user = _sessionMgr.GetUserForRequest(Request);
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = 401;
                return null;
            }

            return _todoRepo.GetSingleTodoForUser(user.Id,
                                                  id);
        }

        [HttpPost("")]
        public TodoItem Post(TodoItem item)
        {
            User user;

            try
            {
                user = _sessionMgr.GetUserForRequest(Request);
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = 401;
                return null;
            }

            return _todoRepo.CreateTodo(user.Id,
                                        item);
        }

        [HttpPut("{id}")]
        public TodoItem Put(int      id,
                            TodoItem item)
        {
            User user;

            try
            {
                user = _sessionMgr.GetUserForRequest(Request);
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = 401;
                return null;
            }

            return _todoRepo.UpdateTodo(user.Id,
                                        item);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user;

            try
            {
                user = _sessionMgr.GetUserForRequest(Request);
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = 401;
                return;
            }

            _todoRepo.DeleteTodo(user.Id,
                                 id);
        }
    }
}