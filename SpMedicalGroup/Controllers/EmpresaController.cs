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

        [HttpGet("GetAllEmpresas")]
        public async Task<IActionResult> GetAllEmpresas()
        {
            try
            {
                List<Empresa> empresas = await empresaService.GetAllEmpresas();
                return StatusCode(200, empresas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CadastrarEmpresa")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> CadastrarEmpresa(Empresa novaEmpresa)
        {
            try
            {
                var empresaCadastrada = await empresaService.CadastrarEmpresa(novaEmpresa);
                return StatusCode(201, empresaCadastrada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível cadastrar empresa. Tente novamente!", detalhes = ex.Message });
            }
        }
    }
}
