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


        [HttpGet("GetDetalhesMedico")]
        public async Task<IActionResult> GetDetalhesMedico([FromQuery] string cpfMedico)
        {
            try
            {
                InformacoesMedicoPopUp infoMedicoPopUp = await medicoService.GetDetalhesMedico(cpfMedico);
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

        [HttpGet("GetInfoBasicasMedico")]
        public async Task<IActionResult> GetInfoBasicasMedico([FromQuery] string? especialidade, [FromQuery] string? nomeMedico, [FromQuery] string? numCrm)
        {
            try
            {
                List<MedicoInformacoesCardDto> informacoesBasicasMedicos = await medicoService.GetInfoBasicasMedico(especialidade, nomeMedico, numCrm);
                return StatusCode(200, informacoesBasicasMedicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllMedicos")]
        public async Task<IActionResult> GetAllMedicos()
        {
            try
            {
                List<Medico> todosMedicos = await medicoService.GetAllMedicos();
                return StatusCode(200, todosMedicos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInfoBasicasUsuarioMedico")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetInfoBasicasUsuarioMedico([FromQuery] string email)
        {
            try
            {
                InfoBasicasUsuario paciente = await medicoService.GetInfoBasicasUsuarioMedico(email);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPerfilCompletoMedico")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetPerfilCompletoMedico([FromQuery] string email)
        {
            try
            {
                PerfilCompletoMedicoDto medico = await medicoService.GetPerfilCompletoMedico(email);
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
