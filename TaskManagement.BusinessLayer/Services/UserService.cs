using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Domain.Entity;
using System.Security.Cryptography;
using TaskManagement.DataLayer;

namespace TaskManagement.BusinessLayer.Services
{
    public class UserService
    {
        private readonly TaskManagementDbContext userDbContext;

        public UserService(TaskManagementDbContext context)
        {
            userDbContext = context;
        }
        public AppUser Login()
        {
            Console.Write("Kullanıcı adınızı girin: ");
            string userName = Console.ReadLine();

            Console.Write("Şifrenizi girin: ");
            string password = Console.ReadLine();
            string passwordHash = HashPassword(password);

            var user = userDbContext.AppUser.FirstOrDefault(u => u.UserName == userName && u.Password == passwordHash);

            if (user != null)
            {
                Console.WriteLine("Başarılı giriş yaptınız!");
                return user;
            }
            else
            {
                Console.WriteLine("Kullanıcı adı veya şifre yanlış!");
                return null;
            }
        }

        public void Register(string username, string password)
        {
            var passwordHash = HashPassword(password);
            var newUser = new AppUser
            {
                UserName = username,
                Password = passwordHash
            };

            // EF Core 
            userDbContext.AppUser.Add(newUser);
            userDbContext.SaveChanges();

            Console.WriteLine("Kullanıcı başarıyla kaydedildi.");
        }


        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



       

    }
}
