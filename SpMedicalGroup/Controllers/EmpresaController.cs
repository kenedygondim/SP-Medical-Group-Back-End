using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Domains;
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
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaModel empresaModel = new();

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Empresa> lista = empresaModel.ListarTodos();

            return Ok(lista);
        }

        [Authorize(Roles = "3")]
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Empresa novaEmpresa)
        {
            empresaModel.Cadastrar(novaEmpresa);

            return StatusCode(201);
        }
    }
}
