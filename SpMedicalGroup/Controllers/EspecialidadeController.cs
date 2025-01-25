using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Services;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeService especialidadeService;

        public EspecialidadeController(IEspecialidadeService especialidadeService)
        {
            this.especialidadeService = especialidadeService;
        }

        [HttpGet("GetAllEspecialidadesMedico")]
        public async Task<IActionResult> GetAllEspecialidadesMedico([FromQuery] string cpf)
        {
            try
            {
                List<IdNomeEspecialidadeDto> especialidadesMedico = await especialidadeService.GetAllEspecialidadesMedico(cpf);
                return StatusCode(200, especialidadesMedico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDetalhesEspecialidades")]
        public async Task<IActionResult> GetDetalhesEspecialidades()
        {
            try
            {
                List<PaginaEspecialidadesDto> infoPaginaEspecialidades = await especialidadeService.GetDetalhesEspecialidades();
                return StatusCode(200, infoPaginaEspecialidades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllEspecialidades")]
        public async Task<IActionResult> GetAllEspecialidades()
        {
            try
            {
                List<Especialidade> especialidades = await especialidadeService.GetAllEspecialidades();
                return StatusCode(200, especialidades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
