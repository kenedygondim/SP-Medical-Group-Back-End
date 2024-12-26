using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Services;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly EspecialidadeService especialidadeService = new();

        [HttpGet("obterEspecialidadesMedico")]
        public async Task<IActionResult> ObterEspecialidadesMedico([FromQuery] string cpf)
        {
            try
            {
                List<IdNomeEspecialidadeDto> especialidadesMedico = await especialidadeService.ObterEspecialidadesMedico(cpf);
                return StatusCode(200, especialidadesMedico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/paginaEspecialidades")]
        public async Task<IActionResult> PaginaEspecialidades()
        {
            try
            {
                List<PaginaEspecialidadesDto> infoPaginaEspecialidades = await especialidadeService.ListarInfoPaginaEspecialidades();
                return StatusCode(200, infoPaginaEspecialidades);
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
                List<Especialidade> especialidades = await especialidadeService.ListarTodas();
                return StatusCode(200, especialidades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
