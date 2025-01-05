using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Services;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FotoPerfilController : ControllerBase
    {
        private readonly FotoPerfilService fotoPerfilService = new();

        [HttpPut("AlterarFotoPerfil")]
        public async Task<IActionResult> AlterarFotoPerfil([FromForm] AlterarFotoPerfilDto novaFotoPerfil)
        {
            try
            {
                var fotoPerfilAlterada = await fotoPerfilService.AlterarFotoPerfil(novaFotoPerfil);
                return StatusCode(200, fotoPerfilAlterada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível alterar foto de perfil. Tente Novamente!", detalhes = ex.Message });
            }
        }
    }
}
