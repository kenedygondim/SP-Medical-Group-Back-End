using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService consultaService = new();

        [HttpGet("ListarTodosConsultasMedico")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> ListarTodosConsultasMedico(string email)
        {
            try
            {
                List<ConsultaDetalhadaDto> result = await consultaService.ListarTodosConsultasMedico(email);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConfirmarConsultaDetalhes")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ConfirmarConsultaDetalhes([FromQuery] string cpf, [FromQuery] string nomeEspecialidade)
        {
            try
            {
                ConfirmarConsultaDetalhesDto result = await consultaService.ConfirmarConsultaDetalhes(cpf, nomeEspecialidade);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Acessar")]
        [Authorize(Roles = "1")]
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

        [HttpPost("Agendar")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Agendar(AgendarConsultaDto novaConsulta)
        {
            try
            {
                Consulta consultaCriada = await consultaService.Agendar(novaConsulta);
                return StatusCode(201, consultaCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível agendar consulta. Tente novamente!", detalhes = ex.Message});
            }
        }

        [HttpGet("ListarTodasConsultasPaciente")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ListarTodasConsultasPaciente([FromQuery] string email)
        {
            try
            {
                List<ConsultaDetalhadaDto> result = await consultaService.ListarTodasConsultasPaciente(email);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
