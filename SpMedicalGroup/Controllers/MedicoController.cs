using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Medico;
using SpMedicalGroup.Services;
using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            this.medicoService = medicoService;
        }


        [HttpGet("InformacoesMedicoEspecifico")]
        public async Task<IActionResult> InformacoesMedicoEspecifico([FromQuery] string cpfMedico)
        {
            try
            {
                InformacoesMedicoPopUp infoMedicoPopUp = await medicoService.InformacoesMedicoEspecifico(cpfMedico);
                return StatusCode(200, infoMedicoPopUp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Acessar")]
        [Authorize(Roles = "2")]
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

        [HttpGet("ListarInformacoesBasicasMedico")]
        public async Task<IActionResult> ListarInformacoesBasicasMedico([FromQuery] string? especialidade, [FromQuery] string? nomeMedico, [FromQuery] string? numCrm)
        {
            try
            {
                List<MedicoInformacoesCardDto> informacoesBasicasMedicos = await medicoService.ListarInformacoesBasicasMedico(especialidade, nomeMedico, numCrm);
                return StatusCode(200, informacoesBasicasMedicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                List<Medico> todosMedicos = await medicoService.ListarTodos();
                return StatusCode(200, todosMedicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("InfoBasicasUsuario")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> RetornarInfoBasicasUsuario([FromQuery] string email)
        {
            try
            {
                InfoBasicasUsuario paciente = await medicoService.InfoBasicasUsuario(email);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PerfilCompletoMedico")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> PerfilCompletoMedico([FromQuery] string email)
        {
            try
            {
                PerfilCompletoMedicoDto medico = await medicoService.PerfilCompletoMedico(email);
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
