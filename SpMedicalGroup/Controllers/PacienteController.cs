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
    public class PacienteController : ControllerBase
    {
        private readonly PacienteModel pacienteModel = new();

        [HttpGet("Acessar")]
        [Authorize(Roles = "1")]
        public IActionResult Acessar()
        {
            return Ok();
        }


        [HttpGet("ListarPacientesMedico")]
        public async Task<IActionResult> ListarPacientesMedico([FromQuery] string emailUsuario)
        {
            List<NomeECpfDoPacienteDto> lista =  await pacienteModel.ListarPacientesMedico(emailUsuario);
            return Ok(lista);
        }

        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Paciente> lista = pacienteModel.ListarTodos();
            return Ok(lista);
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastroPacienteDto novoPaciente)
        {
        
            return Ok(await pacienteModel.CadastrarPaciente(novoPaciente));
        }
    }
}
