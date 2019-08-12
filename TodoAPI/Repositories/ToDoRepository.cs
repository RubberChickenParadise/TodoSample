using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using TodoAPI.Models;

namespace TodoAPI
{
    public interface IToDoRepository
    {
        void DeleteTodo(int    userId,
                        int item);

        TodoItem UpdateTodo(int      userId,
                            TodoItem item);

        TodoItem CreateTodo(int      userId,
                            TodoItem item);

        TodoItem GetSingleTodoForUser(int userId,
                                      int id);

        List<TodoItem> GetTodoForUser(int userId);
    }

    public class ToDoRepository : IToDoRepository
    {
        private static ConcurrentDictionary<int,TodoItem> _items = new ConcurrentDictionary<int,TodoItem>();
        private static int maxId = 0;


        static ToDoRepository()
        {
            for (var i = 0; i < 20; i++)
            {
                var id = ++maxId;
                _items.TryAdd(id,
                              new TodoItem
                              {
                                  Id          = id,
                                  OwnerId     = i % 2 + 1,
                                  Completed   = false,
                                  Title       = $"Sample Todo {id}",
                                  Description = "This is a sample TODO item.  If this was a real todo item it would have meaning"
                              });
            }
        }

        public void DeleteTodo(int userId,
                               int item)
        {
            try
            {
                var i = _items[item];

                if (i.OwnerId == userId)
                {
                    _items.Remove(item,
                                  out var woot);
                }
            }
            catch (KeyNotFoundException)
            {
                //Dont care since its not there to delete.
                return;
            }
        }

        public TodoItem UpdateTodo(int      userId,
                                   TodoItem item)
        {
            try
            {
                var i = _items[item.Id];

                if (i.OwnerId == userId)
                {
                    _items[item.Id] = item;
                }

                return _items[item.Id];
            }
            catch (KeyNotFoundException)
            {
                return CreateTodo(userId,
                                  item);
            }
        }

        public TodoItem CreateTodo(int      userId,
                                   TodoItem item)
        {
            item.Id = ++maxId;
            item.OwnerId = userId;

            _items[item.Id] = item;

            return _items[item.Id];
        }

        public TodoItem GetSingleTodoForUser(int userId,
                                             int id)
        {
            var i = _items[id];

            if (i.OwnerId != userId)
            {
                throw new UnauthorizedAccessException();
            }

            return i;
        }

        public List<TodoItem> GetTodoForUser(int userId)
        {
            return _items.Where(x => x.Value.OwnerId == userId)
                         .Select(x=>x.Value)
                         .ToList();
        }
    }
}
