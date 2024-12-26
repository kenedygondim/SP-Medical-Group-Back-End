using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService RoleService = new();

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet("ListarTodos")]
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

        [Authorize(Roles = "3")]
        [HttpPost("cadastrar")]
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
