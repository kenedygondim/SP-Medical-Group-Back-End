using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;
using SpMedicalGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly EspecialidadeModel especialidadeModel = new();

        [HttpGet("obterEspecialidadesMedico")]
        public async Task<IActionResult> obterEspecialidadesMedico([FromQuery] string cpf)
        {
            List<NomeEspecialidadeDto> lista = await especialidadeModel.obterEspecialidadesMedico(cpf);

            return Ok(lista);
        }


        [HttpGet("/paginaEspecialidades")]
        public async Task<IActionResult> PaginaEspecialidades()
        {
            List<PaginaEspecialidadesDto> lista = await especialidadeModel.ListarInfoPaginaEspecialidades();

            return Ok(lista);
        }


        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ListarTodos()
        {
            List<Especialidade> especialidades = await especialidadeModel.ListarTodas();

            return Ok(especialidades);
        }
    }
}
