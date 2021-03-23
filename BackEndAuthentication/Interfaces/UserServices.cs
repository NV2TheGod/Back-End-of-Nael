using backend_shop.Data;
using backend_shop.Models;
using backend_shop.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace backend_shop.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string currentPassword, string password, string confirmPassword);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private Context _context;
        public UserService(Context context)
        {
            _context = context;
        }

        static string GenerateRandomKey(int keyLength)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            provider.GetBytes(randomBytes);
            string hashString = "";
            foreach (var hashbyte in randomBytes)
            {
                hashString += hashbyte.ToString("x2");
            }
            return hashString;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.Users.FirstOrDefault(x => x.Username.Equals(username)) ?? null;

            if (user == null)
            {
                return null;
            }

            if (!computeHash(password).Equals(user.Password_Hash))
            {
                return null;
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ExceptionRaiser("Missing password");
            }

            if (_context.Users.Any(x => x.Username.Equals(user.Username)))
            {
                throw new ExceptionRaiser("Username already taken: " + user.Username);
            }

            user.Password_Hash = computeHash(password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userSettings, string currentPassword, string password, string confirmPassword)
        {
            var user = _context.Users.Find(userSettings.Id);

            if (user == null)
            {
                throw new ExceptionRaiser("User not found");
            }

            //Check if the name changed
            if (!string.IsNullOrWhiteSpace(userSettings.Username) && (!userSettings.Username.Equals(user.Username)))
            {
                //Check if the username is already taken
                if (_context.Users.Any(x => x.Username.Equals(userSettings.Username)))
                {
                    throw new ExceptionRaiser("Username already taken: " + user.Username);
                }
                else
                {
                    user.Username = userSettings.Username;
                }
            }

            //Check if the firstname changed
            if (!string.IsNullOrWhiteSpace(userSettings.First_Name) && (!userSettings.First_Name.Equals(user.First_Name)))
            {
                user.First_Name = userSettings.First_Name;
            }

            //Check if the lastname changed
            if (!string.IsNullOrWhiteSpace(userSettings.Last_Name) && (!userSettings.Last_Name.Equals(user.Last_Name)))
            {
                user.Last_Name = userSettings.Last_Name;
            }

            if (!string.IsNullOrWhiteSpace(currentPassword))
            {
                if (!computeHash(currentPassword).Equals(user.Password_Hash))
                {
                    throw new ExceptionRaiser("Invalid password !");
                }

                if (currentPassword.Equals(password))
                {
                    throw new ExceptionRaiser("You need to change your password");
                }

                if (!password.Equals(confirmPassword))
                {
                    throw new ExceptionRaiser("Password doesn't match !");
                }
                user.Password_Hash = computeHash(password);
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static string computeHash(string password)
        {
            MD5 digest = new MD5CryptoServiceProvider();
            var input = digest.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedString = "";
            foreach (var hashbyte in input)
            {
                hashedString += hashbyte.ToString("x2");
            }
            return hashedString;
        }
    }
}
