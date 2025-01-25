using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;
using SpMedicalGroup.Services;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            this.consultaService = consultaService;
        }

        [HttpGet("GetAllConsultasMedico")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetAllConsultasMedico(string email)
        {
            try
            {
                List<ConsultaDetalhadaDto> result = await consultaService.GetAllConsultasMedico(email);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDetalhesConsulta")]
        public async Task<IActionResult> GetDetalhesConsulta([FromQuery] string cpf, [FromQuery] string nomeEspecialidade)
        {
            try
            {
                ConfirmarConsultaDetalhesDto result = await consultaService.GetDetalhesConsulta(cpf, nomeEspecialidade);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Acessar")]
        [Authorize(Roles = "1,2")]
        public IActionResult Acessar()
        {
            try
            {
                return StatusCode(200, "Acesso liberado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AgendarConsulta")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> AgendarConsulta(AgendarConsultaDto novaConsulta)
        {
            try
            {
                Consulta consultaCriada = await consultaService.AgendarConsulta(novaConsulta);
                return StatusCode(201, consultaCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível agendar consulta. Tente novamente!", detalhes = ex.Message });
            }
        }

        [HttpGet("GetAllConsultasPaciente")]
        [Authorize(Roles ="1")]
        public async Task<IActionResult> GetAllConsultasPaciente([FromQuery] string email)
        {
            try
            {
                List<ConsultaDetalhadaDto> result = await consultaService.GetAllConsultasPaciente(email);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("CancelarConsulta")]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> CancelarConsulta([FromQuery] int consultaId)
        {
            try
            {
                await consultaService.CancelarConsulta(consultaId);
                return StatusCode(204, "Consulta cancelada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("MarcarConsultaComoConcluida")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> MarcarConsultaComoConcluida([FromQuery] int consultaId)
        {
            try
            {
                await consultaService.MarcarConsultaComoConcluida(consultaId);
                return StatusCode(204, "Consulta marcada como concluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
