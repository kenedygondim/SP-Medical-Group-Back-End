using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Services;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService pacienteService = new();

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

        [HttpGet("ListarPacientesMedico")]
        public async Task<IActionResult> ListarPacientesMedico([FromQuery] string emailUsuario)
        {
            try
            {
                List<NomeECpfDoPacienteDto> pacientesMedico = await pacienteService.ListarPacientesMedico(emailUsuario);
                return Ok(pacientesMedico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastroPacienteDto novoPaciente)
        {
            try
            {
                Paciente pacienteCriado = await pacienteService.CadastrarPaciente(novoPaciente);
                return StatusCode(201, pacienteCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(new {mensagem = "Não foi possível realizar o cadastro. Tente novamente!", detalhes = ex.Message});
            }
        }
    }
}
