using Microsoft.Build.Construction;
using TheBlogProject.Data;

namespace TheBlogProject.Services
{
    public class DataService
    {

        private readonly ApplicationDbContext _dbContext;

        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            if (_dbContext.Roles.Any() == null)
            {
                return;
            }

            // Otherwise we want to create a few Roles
        }

        private async Task SeedUsersAsync()
        {

        }

        
        
        
        
        // Task 3: 

    }
}
