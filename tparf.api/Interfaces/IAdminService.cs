using tparf.dto.Auth;

namespace tparf.api.Interfaces
{
    public interface IAdminService
    {
        Task<Status> SeedRolesAsync();
        Task<Status> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto);
        Task<Status> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
    }
}
