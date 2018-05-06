using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Done.Data
{
    public class DbInitializer
    {
        private const string AdministratorsRole = "Administrators";
        private const string UsersRole = "Users";
        private const string AdministratorUser = "Administrator";
        private const string AdministratorUserPassword = "123asdQ";

        private readonly DoneContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer( 
            DoneContext context, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ILogger<DbInitializer> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task Initialize()
        {
            await TryAddRole(AdministratorsRole);
            await TryAddRole(UsersRole);
            await TryAddUser(AdministratorUser, AdministratorUserPassword, AdministratorsRole);
        }

        private async Task TryAddRole(string role)
        {   
            if (await _roleManager.FindByNameAsync(role) != null)
            {
                _logger.LogInformation($"Role `{role}` already exists.");
                return;
            }

            _logger.LogInformation($"Creating the role `{role}`.");

            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
            {
                var exception = new ApplicationException($"Role `{role}` cannot be created");
                _logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
                throw exception;
            }

            _logger.LogDebug($"Created the role `{role}` successfully");
        }

        private async Task TryAddUser(string name, string password, string role)
        {
            if (await _userManager.FindByNameAsync(name) != null)
            {
                _logger.LogInformation($"User `{name}` already exists.");
                return;
            }

            _logger.LogInformation($"Creating user `{name}`.");
            var user = new IdentityUser(name);       
    
            var result = await _userManager.CreateAsync(user, password);        
        
            if (!result.Succeeded)
            {
                var exception = new ApplicationException($"User `{name}` cannot be created");
                _logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
                throw exception;
            }

            _logger.LogDebug($"Created user `{name}` successfully");

            result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                var exception = new ApplicationException($"User `{name}` cannot be added to role `{role}`");
                _logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
                throw exception;
            }

            _logger.LogDebug($"Added user `{name}` to role `{role}`.");
        }
        
        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}