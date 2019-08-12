using System;
using System.Collections.Generic;
using System.Linq;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public interface IUserRepository
    {
        User Login(string userName,
                                   string password);
    }

    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>
                                    {
                                        new User
                                        {
                                            Id = 1,
                                            UserName = "bob",
                                            Password = "bob",
                                            DisplayName = "Bob Smith"
                                        },
                                        new User{
                                                    Id          = 2,
                                                    UserName    = "suzie",
                                                    Password    = "suzie",
                                                    DisplayName = "Suzie Woo"
                                                }
                                    };

        public User Login(string userName,
                          string password)
        {
            //Dont really do this.  in a DB store the password encrypted.
            return _users.SingleOrDefault(x => x.UserName == userName && x.Password == password) ?? throw new UnauthorizedAccessException();
        }
    }
}
