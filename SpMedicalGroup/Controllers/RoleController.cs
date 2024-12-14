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
    public class RoleController : ControllerBase
    {
        private readonly RoleModel RoleModel = new();

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Role> roles = RoleModel.ListarTodas();

            return Ok(roles);
        }

        [Authorize(Roles = "3")]
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Role novoRole)
        {
            RoleModel.Cadastrar(novoRole);

            return StatusCode(201);
        }
    }
}
