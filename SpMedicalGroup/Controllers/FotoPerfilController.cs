using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Services;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FotoPerfilController : ControllerBase
    {
        private readonly IFotoPerfilService fotoPerfilService;

        public FotoPerfilController(IFotoPerfilService fotoPerfilService)
        {
            this.fotoPerfilService = fotoPerfilService;
        }

        [HttpPut("AlterarFotoPerfil")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> AlterarFotoPerfil([FromQuery] string email, [FromForm] IFormFile novaFotoPerfil)
        {
            try
            {
                if (novaFotoPerfil == null)
                    return BadRequest("DTO chegou nulo");

                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email está vazio");

                if (novaFotoPerfil == null)
                    return BadRequest("FotoPerfil está nula");

                var fotoPerfilAlterada = await fotoPerfilService.AlterarFotoPerfil(email, novaFotoPerfil);
                return StatusCode(200, fotoPerfilAlterada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro inesperado", detalhes = ex.Message });
            }
        }

    }
}
