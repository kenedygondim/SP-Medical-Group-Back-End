using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services;
using SpMedicalGroup.Repositories;
using SpMedicalGroup.Dto.Usuario;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet("ListarTodos")]
        [Authorize(Roles="3")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                List<Usuario> todosUsuarios = await usuarioService.ListarTodos();
                return StatusCode(200, todosUsuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //provisório
        [HttpPut("AlterarSenha")]
        [Authorize(Roles = "1,2,3")]
        public async Task<Usuario> AlterarSenha ([FromBody] AlterarSenhaDto alterarSenhaDto)
        {
            Usuario usuario = await usuarioService.AlterarSenha(alterarSenhaDto);

            return usuario;
        }
    }
}
