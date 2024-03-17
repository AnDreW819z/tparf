using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using tparf.api.Interfaces;
using tparf.dto.Auth;

namespace tparf.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IEmailService _emailService;

        public AdminController(IAdminService adminService, IEmailService emailService)
        {
            _adminService = adminService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seerRoles = await _adminService.SeedRolesAsync();

            return Ok(seerRoles);
        }

        [HttpPost]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _adminService.MakeAdminAsync(updatePermissionDto);

            if (operationResult.StatusCode == 200)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        [HttpPost]
        [Route("make-owner")]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _adminService.MakeOwnerAsync(updatePermissionDto);

            if (operationResult.StatusCode == 200)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

    }
}
