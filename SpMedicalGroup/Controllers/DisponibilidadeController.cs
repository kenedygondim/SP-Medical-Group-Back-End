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

        [HttpPost("AdicionarDisponibilidade")]
        [Authorize(Roles="2")]
        public async Task<IActionResult> AdicionarDisponibilidade(CriarDisponibilidadeDto novaDisponibilidade)
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

        [HttpDelete("ExcluirDisponibilidade")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> ExcluirDisponibilidade([FromQuery] int disponibilidadeId)
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

        [HttpGet("GetDisponibilidadesMedicoNaoPreenchidas")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetDisponibilidadesMedicoNaoPreenchidas([FromQuery] string cpf, [FromQuery] string data)
        {
            try
            {
                var disponibilidades = await disponibilidadeService.GetDisponibilidadesMedicoByData(cpf, data, true);
                return StatusCode(200, disponibilidades);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllDisponibilidadesMedico")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetAllDisponibilidadesMedico([FromQuery] string cpf, [FromQuery] string data)
        {
            try
            {
                var disponibilidades = await disponibilidadeService.GetDisponibilidadesMedicoByData(cpf, data, false);
                return StatusCode(200, disponibilidades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
