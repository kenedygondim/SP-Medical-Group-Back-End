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

        //[HttpGet("ListarPacientesMedico")]
        //[Authorize(Roles = "2")]
        //public async Task<IActionResult> ListarPacientesMedico([FromQuery] string emailUsuario)
        //{
        //    try
        //    {
        //        List<InfoBasicasPaciente> pacientesMedico = await pacienteService.ListarPacientesMedico(emailUsuario);
        //        return Ok(pacientesMedico);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromForm] CadastroPacienteDto novoPaciente)
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

        [HttpGet("InfoBasicasUsuario")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> RetornarInfoBasicasUsuario([FromQuery] string email)
        {
            try
            {
                InfoBasicasUsuario paciente = await pacienteService.InfoBasicasUsuario(email);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PerfilCompletoPaciente")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> PerfilCompletoPaciente([FromQuery] string email)
        {
            try
            {
                PerfilPacienteDto paciente = await pacienteService.PerfilCompletoPaciente(email);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("InformacoesBasicasPaciente")]
        public async Task<IActionResult> InformacoesBasicasPaciente([FromQuery] string emailMedico, [FromQuery] string? especialidade, [FromQuery] string? nomePaciente, [FromQuery] string? dataAtendimento)
        {
            try
            {
                List<InfoBasicasUsuario> pacientes = await pacienteService.ListarInformacoesBasicasPaciente(emailMedico, especialidade, nomePaciente, dataAtendimento);
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}