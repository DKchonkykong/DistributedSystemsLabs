using DistSysAcwServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DistSysAcwServer.DataAccess
{
    public class UserDatabaseAccess
    {
        private readonly UserContext _context;

        public UserDatabaseAccess(UserContext context)
        {
            _context = context;
        }

        public bool UserNameExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public bool ApiKeyExists(string apiKey)
        {
            return _context.Users.Any(u => u.ApiKey == apiKey);
        }

        public bool ApiKeyAndUserNameMatch(string apiKey, string username)
        {
            return _context.Users.Any(u => u.ApiKey == apiKey && u.UserName == username);
        }

        public User? GetUserByApiKey(string apiKey)
        {
            return _context.Users.FirstOrDefault(u => u.ApiKey == apiKey);
        }

        public User? GetUserByUserName(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public User CreateUser(string username)
        {
            bool isFirstUser = !_context.Users.Any();

            User user = new User
            {
                ApiKey = Guid.NewGuid().ToString(),
                UserName = username,
                Role = isFirstUser ? "Admin" : "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        //part of task 13 now adds user logs to database
        public void AddLog(string apiKey , string logString)
        {
            User? user = GetUserByApiKey(apiKey);
            
            if (user == null)
            { 
                return;
            }

            Log log = new Log(logString, apiKey);
            
                user.Logs.Add(log);
                _context.SaveChanges();
            
        }
        public bool DeleteUserByUserName(string username)
        {
            User? user = GetUserByUserName(username);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public bool ChangeUserRole(string username, string role)
        {
            User? user = GetUserByUserName(username);

            if (user == null)
            {
                return false;
            }

            user.Role = role;
            _context.SaveChanges();

            return true;
        }
    }
}