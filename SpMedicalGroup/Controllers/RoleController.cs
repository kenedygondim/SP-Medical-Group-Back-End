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
        public async Task<IActionResult> AdicionarrRole([FromQuery] string nomeRole)
        {
            try
            {
                await RoleService.AdicionarRole(nomeRole);
                return StatusCode(201, "Perfil de usuário criado!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível adicionar perfil de usuário.", detalhes = ex.Message });
            }
        }
    }
}
