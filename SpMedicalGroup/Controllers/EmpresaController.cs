using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            this.empresaService = empresaService;
        }

        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ListarTodas()
        {
            try
            {
                List<Empresa> empresas = await empresaService.ListarTodas();
                return StatusCode(200, empresas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Cadastrar")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Cadastrar(Empresa novaEmpresa)
        {
            try
            {
                var empresaCadastrada = await empresaService.Cadastrar(novaEmpresa);
                return StatusCode(201, empresaCadastrada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível cadastrar empresa. Tente novamente!", detalhes = ex.Message });
            }
        }
    }
}
