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

        [HttpDelete("Excluir")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Excluir([FromQuery] int disponibilidadeId)
        {
            try
            {
                var disponibilidadeCriada = await disponibilidadeService.ExcluirDisponibilidade(disponibilidadeId);
                return StatusCode(204, "Disponibilidade excluída com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível excluir disponibilidade. Tente Novamente!", detalhes = ex.Message });
            }
        }

        // TO-DO: Alterar nome do endpoint e método
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

        // TO-DO: Alterar nome do endpoint e método
        [HttpGet("listarTodasDisponibilidadesMedicoPorData")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> ListarTodasDisponibilidadesMedicoPorData([FromQuery] string cpf, [FromQuery] string data)
        {
            try
            {
                var disponibilidades = await disponibilidadeService.ListarDisponibilidadesMedicoPorData(cpf, data, false);
                return StatusCode(200, disponibilidades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
