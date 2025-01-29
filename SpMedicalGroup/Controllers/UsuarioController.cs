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

        //[HttpGet(CriptografarSenhas")]
        //public async Task<IActionResult> CriptografarSenhas()
        //{
        //    try
        //    {
        //        await usuarioService.updateAllPassword();
        //        return StatusCode(200, "Senhas alteradas com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet("GetAllUsuarios")]
        [Authorize(Roles="3")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                List<Usuario> todosUsuarios = await usuarioService.GetAllUsuarios();
                return StatusCode(200, todosUsuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        [Authorize(Roles = "1,2,3")]
        public async Task<Usuario> AlterarSenha ([FromBody] AlterarSenhaDto alterarSenhaDto)
        {
            Usuario usuario = await usuarioService.AlterarSenha(alterarSenhaDto);

            return usuario;
        }

        [HttpDelete("ExcluirUsuario")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> ExcluirUsuario([FromQuery] int idUsuario)
        {
            try
            {
                await usuarioService.ExcluirUsuario(idUsuario);
                return StatusCode(204, "Especialidade excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível excluir usuário.", detalhes = ex.Message });
            }
        }

    }
}
