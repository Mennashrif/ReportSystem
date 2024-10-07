using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ReportSystem.Domain.Entities.UserEntity;

namespace ReportSystem.Infrastructure.Context
{
    public class SeedContext()
    {
        private static DatabaseContext _dbContext;
        private static UserManager<User> _userManager;
        private static RoleManager<AspNetRole> _roleManager;
        public static async Task Seed(DatabaseContext dbContext, UserManager<User> userManager, RoleManager<AspNetRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext.Database.EnsureCreated();

             await SeedRole();
             await SeedUser();
        }

        private static async Task SeedRole()
        {
            if (! _roleManager.Roles.Any())
            {
                string rolesAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "RoleSeed.json");
                var appRoles = JsonConvert.DeserializeObject<List<AspNetRole>>(rolesAsJson);

                if (appRoles != null)
                {

                    foreach (var role in appRoles)
                    {

                        await _roleManager.CreateAsync(role);
                    }
                    _dbContext.SaveChanges();

                }
            }

        }
        private  static async Task SeedUser()
        {
            if (!_userManager.Users.Any())
            {
                string usersAsJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "UserSeed.json");
                var users = JsonConvert.DeserializeObject<List<User>>(usersAsJson);

                if (users != null)
                {

                    foreach (var user in users)
                    {

                        await _userManager.CreateAsync(user, "P@ssw0rd");
                        

                    }

                    foreach(var user in users)
                    {
                        if (user.UserName == "SystemAdmin")
                            await _userManager.AddToRoleAsync(user, "Manager");
                        else
                            await _userManager.AddToRoleAsync(user, "User");
                    }
                    _dbContext.SaveChanges();

                }
            }

        }
    }
}
