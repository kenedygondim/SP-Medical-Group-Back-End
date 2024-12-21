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
    public class MedicoController : ControllerBase
    {
        private readonly MedicoModel medicoModel = new();


        [HttpGet("InformacoesMedicoEspecifico")]
        public async Task<IActionResult> InformacoesMedicoEspecifico([FromQuery] string cpfMedico)
        {
            InformacoesMedicoPopUp medico = await medicoModel.InformacoesMedicoEspecifico(cpfMedico);

            return Ok(medico);
        }



        [HttpGet("Acessar")]
        [Authorize(Roles = "2")]
        public IActionResult Acessar()
        {
            return Ok();
        }


        [HttpGet("ListarInformacoesBasicasMedico")]
        public IActionResult ListarInformacoesBasicasMedico([FromQuery] string? especialidade, [FromQuery] string? nomeMedico, [FromQuery] string? numCrm)
        {
            List<MedicoInformacoesCardDto> lista = medicoModel.ListarInformacoesBasicasMedico(especialidade, nomeMedico, numCrm);

            return Ok(lista);
        }

        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Medico> lista = medicoModel.ListarTodos();

            return Ok(lista);
        }
    }
}
