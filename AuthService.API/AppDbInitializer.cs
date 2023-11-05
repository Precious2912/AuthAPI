using AuthService.Core.Entities;
using AuthService.Entities;

namespace AuthService.API
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder application)
        {
            using (var serviceScope = application.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    //seed data
                    var user1 = new User { Email = "test@gmail.com", FirstName = "Test", LastName = "User", Gender = "F", PhoneNumber = "080XXXXXXXX", Username = "test21", PasswordHash = "erftyhuytr23456gfdfghj=" };

                    context.Users.Add(user1);
                    context.SaveChanges();
                }
            }
        }
    }
}
