using PrivateSchoolExe.Data;
using PrivateSchoolExe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchoolExe.Helpers
{
    public class Authentication
    {

        public User Register(User user, string password)
        {
            byte[] passwordHash, PasswordSalt;
            CreatePasswordHash(password, out passwordHash, out PasswordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = PasswordSalt;
            using (DataContext db = new DataContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return user;
        }

        public User Login(string email, string password)
        {
            User user = new User();
            using (DataContext db = new DataContext())
            {
                user = db.Users.Include("Level").Include("TrainerSchedules").FirstOrDefault(x => x.Email == email);
            }
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

    }
}
