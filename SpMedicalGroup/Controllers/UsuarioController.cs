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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioModel usuarioModel = new();

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Usuario> lista = usuarioModel.ListarTodos();

            return Ok(lista);
        }
    }
}
