using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Construction;
using TheBlogProject.Data;
using TheBlogProject.Enums;

namespace TheBlogProject.Services
{
    public class DataService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        
        // Does 3 things
        public async Task ManageDataAsync()
        {
            // Task 1: Seed a few Roles into the system
            await SeedRolesAsync();

            // Task 2: Seed a few Users into the System
            await SeedUsersAsync();
        
        }

        private async Task SeedRolesAsync()
        {
            // if there are already Roles in the System, do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }

            // Otherwise we want to create a few Roles
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                // Use the Role Manager to create Roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {

        }

        
        
        
        
        // Task 3: 

    }
}
