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

        [HttpGet("ListarTodos")]
        [Authorize(Roles="3")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                List<Role> todasRoles = await RoleService.ListarTodas();
                return StatusCode(200, todasRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cadastrar")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Cadastrar(Role novoRole)
        {
            try
            {
                Role roleCriada = await RoleService.Cadastrar(novoRole);
                return StatusCode(201, roleCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
