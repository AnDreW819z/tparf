using Microsoft.AspNetCore.Identity;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;

namespace tparf.api.Service
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly TparfDbContext _context;

        public AdminService(TparfDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Status> MakeAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);

            if (user is null)
                return new Status()
                {
                    StatusCode = 404,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return new Status()
            {
                StatusCode = 200,
                Message = "User is now an ADMIN"
            };
        }

        public async Task<Status> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);

            if (user is null)
                return new Status()
                {
                    StatusCode = 404,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, UserRoles.Owner);

            return new Status()
            {
                StatusCode = 200,
                Message = "User is now an Moderator"
            };
        }

        public async Task<Status> SeedRolesAsync()
        {
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(UserRoles.User);
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(UserRoles.Owner);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(UserRoles.Admin);

            if (isUserRoleExists && isOwnerRoleExists && isAdminRoleExists)
                return new Status()
                {
                    StatusCode = 200,
                    Message = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));
            await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.Owner));
            await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.Admin));

            return new Status()
            {
                StatusCode = 200,
                Message = "Role Seeding Done Successfully"
            };
        }
    }
}
