using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Services;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            this.pacienteService = pacienteService;
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

        [HttpPost("CadastrarPaciente")]
        public async Task<IActionResult> CadastrarPaciente([FromForm] CadastroPacienteDto novoPaciente)
        {
            try
            {
                Paciente pacienteCriado = await pacienteService.CadastrarPaciente(novoPaciente);
                return StatusCode(201, pacienteCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível realizar o cadastro. Tente novamente!", detalhes = ex.Message });
            }
        }

        [HttpGet("GetInfoBasicasUsuarioPaciente")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetInfoBasicasUsuarioPaciente([FromQuery] string email)
        {
            try
            {
                InfoBasicasUsuario paciente = await pacienteService.GetInfoBasicasUsuarioPaciente(email);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPerfilCompletoPaciente")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetPerfilCompletoPaciente([FromQuery] string email)
        {
            try
            {
                PerfilPacienteDto paciente = await pacienteService.GetPerfilCompletoPaciente(email);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInfoBasicasPaciente")]
        public async Task<IActionResult> GetInfoBasicasPaciente([FromQuery] string emailMedico, [FromQuery] string? especialidade, [FromQuery] string? nomePaciente, [FromQuery] string? dataAtendimento)
        {
            try
            {
                List<InfoBasicasUsuario> pacientes = await pacienteService.GetInfoBasicasPaciente(emailMedico, especialidade, nomePaciente, dataAtendimento);
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}