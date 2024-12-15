using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadeController : ControllerBase
    {
        private readonly DisponibilidadeModel disponibilidadeModel = new();

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(CriarDisponibilidadeDto novaDisponibilidade)
        {
            await disponibilidadeModel.adicionarDisponibilidade(novaDisponibilidade);

            return StatusCode(201);
        }

    }
}
