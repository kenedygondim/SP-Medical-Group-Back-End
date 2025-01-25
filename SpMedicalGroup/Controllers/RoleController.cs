using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService RoleService;

        public RoleController(IRoleService RoleService)
        {
            this.RoleService = RoleService;
        }

        [HttpGet("GetAllRoles")]
        [Authorize(Roles="3")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                List<Role> todasRoles = await RoleService.GetAllRoles();
                return StatusCode(200, todasRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AdicionarRole")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> AdicionarrRole(Role novoRole)
        {
            try
            {
                Role roleCriada = await RoleService.AdicionarrRole(novoRole);
                return StatusCode(201, roleCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
