using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, 
                           RoleManager<IdentityRole> roleManager, 
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        // Does 3 things: Seeds Roles, Seeds Users as Admin and Mod
        public async Task ManageDataAsync()
        {
            // Task: Create DB from Migrations
            await _dbContext.Database.MigrateAsync();

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
            // if there are already Users in the System, do nothing
            if (_dbContext.Users.Any())
            {
                return;
            }

            // Administrator Creation
            // Step 1: Creates new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "Brent.Cleary@gmail.com",
                UserName = "Brent.Cleary@gmail.com",
                FirstName = "Brent",
                LastName = "Cleary",
                DisplayName = "The Admin Professor",
                PhoneNumber = "1234567890",
                EmailConfirmed = true
            };

            // Step 2: Use the UserManager to create a new User that is defined by the AdminUser Table
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            // Step 3: Add the new User to the Administrator Role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());



            // Moderator Creation
            // Step 1 Repeat: Create the Moderator User
            var modUser = new BlogUser()
            {
                Email = "Moderator@email.com",
                UserName = "Moderator@email.com",
                FirstName = "Brent",
                LastName = "Cleary",
                DisplayName = "The Moderator Professor",
                PhoneNumber = "1234567890",
                EmailConfirmed = true
            };

            // Step 2: Use the UserManager to create a new User that is defined by the ModeratorUser Table
            await _userManager.CreateAsync(modUser, "Abc&123!");

            // Step 3: Add the new User to the Administrator Role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }

    }
}
