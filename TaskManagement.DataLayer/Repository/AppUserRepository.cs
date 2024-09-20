using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.IRepository;
using System.Security.Cryptography;

namespace TaskManagement.DataLayer.Repository
{
    public class AppUserRepository:GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(TaskManagementDbContext taskManagementDbContext) : base(taskManagementDbContext) 
        {
                
        }
        
        public AppUser Login()
        {
            Console.Write("Kullanıcı adınızı girin: ");
            string userName = Console.ReadLine();

            Console.Write("Şifrenizi girin: ");
            string password = Console.ReadLine();
            string passwordHash = HashPassword(password);

            var user =GetQuery(u => u.UserName == userName && u.Password == passwordHash);

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
            //userDbContext.AppUser.Add(newUser);
            //userDbContext.SaveChanges();
            Add(newUser);

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
