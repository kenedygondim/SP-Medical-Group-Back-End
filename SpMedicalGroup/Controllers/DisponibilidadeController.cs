using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Dto.Disponibilidade;
using SpMedicalGroup.Repositories;
using SpMedicalGroup.Services;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadeController : ControllerBase
    {
        private readonly IDisponibilidadeService disponibilidadeService;

        public DisponibilidadeController(IDisponibilidadeService disponibilidadeService)
        {
            this.disponibilidadeService = disponibilidadeService;
        }

        [HttpPost("adicionar")]
        [Authorize(Roles="2")]
        public async Task<IActionResult> Adicionar(CriarDisponibilidadeDto novaDisponibilidade)
        {
            try
            {
                var disponibilidadeCriada = await disponibilidadeService.AdicionarDisponibilidade(novaDisponibilidade);
                return StatusCode(201, disponibilidadeCriada);
            }
            catch (Exception ex)
            {
                return BadRequest( new { mensagem = "Não foi possível adicionar disponibilidade. Tente Novamente!", detalhes = ex.Message });
            }
        }

        [HttpGet("listarDisponibilidadesMedicoPorData")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> ListarDisponibilidadesMedicoPorData([FromQuery] string cpf, [FromQuery] string data)
        {
            try
            {
                var disponibilidades = await disponibilidadeService.ListarDisponibilidadesMedicoPorData(cpf, data, true);
                return StatusCode(200, disponibilidades);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
