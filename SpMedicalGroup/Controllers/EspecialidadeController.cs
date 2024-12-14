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

        [HttpGet("/paginaEspecialidades")]
        public IActionResult PaginaEspecialidades()
        {
            List<PaginaEspecialidadesDto> lista = especialidadeModel.ListarInfoPaginaEspecialidades();

            return Ok(lista);
        }


        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Especialidade> especialidades = especialidadeModel.ListarTodas();

            return Ok(especialidades);
        }
    }
}
