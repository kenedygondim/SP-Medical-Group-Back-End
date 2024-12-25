using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Dto;
using SpMedicalGroup.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaModel consultaModel = new();

        [HttpGet("ListarTodosConsultasMedico")]
        public async Task<IActionResult> ListarTodosConsultasMedico(string email)
        {
                List<ConsultaDetalhadaDto> result = await consultaModel.ListarTodosConsultasMedico(email);
                return StatusCode(200, result);
        }

        [HttpGet("ConfirmarConsultaDetalhes")]
        public async Task<IActionResult> ConfirmarConsultaDetalhes(string cpf, string nomeEspecialidade)
        {
                ConfirmarConsultaDetalhesDto result = await consultaModel.ConfirmarConsultaDetalhes(cpf, nomeEspecialidade);
                return StatusCode(200, result);
        }

        [HttpPost("Agendar")]
        public async Task<IActionResult> Cadastrar(Consulta novaConsulta)
        {

            Consulta consultaCriada = await consultaModel.Cadastrar(novaConsulta);
            return StatusCode(201, consultaCriada);
        }

        /*[Authorize(Roles = "3")]
        [HttpPatch("atualizar/{id}")]
        public IActionResult Atualizar(byte id, Consulta consultaAtt)
        {
            consultaModel.Atualizar(id, consultaAtt);

            return StatusCode(204);

        }

        

        [Authorize(Roles = "3")]
        [HttpPost("cancelar/{id}")]
        public IActionResult CancelarConsulta(byte id)
        {
            consultaModel.CancelarConsulta(id);
            return StatusCode(204);
        }

        [Authorize(Roles = "3")]
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(byte id)
        {
            consultaModel.Deletar(id);
            return StatusCode(204);
        }

        [Authorize(Roles = "2")]
        [HttpPost("incluir/{id}/{descricao}")]
        public IActionResult IncluirDescricao(byte id, string descricao)
        {

            try
            {
                consultaModel.IncluirDescricao(id, descricao);
                return StatusCode(204);
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    mensagem = "Você não pode incluir descrição em consultas que não são suas",
                    error
                });
            }

        }

        [Authorize(Roles = "2")]
        [HttpGet("todosMedico")]
        public IActionResult LerTodasDoMedico()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                return Ok(consultaModel.LerTodasDoMedico(idUsuario));
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    mensagem = "Você não tem autorização para essa requisição",
                    error
                });
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet("todosPaciente")]
        public IActionResult LerTodasDoPaciente()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);



                return Ok(consultaModel.LerTodasDoPaciente(idUsuario));
            }
            catch (Exception error)
            {
                return BadRequest(new
                {
                    mensagem = "Você não tem autorização para essa requisição",
                    error
                });

                throw;
            }

        }

        [Authorize(Roles = "3")]
        [HttpGet("ListarTodos")]
        public IActionResult ListarTodos()
        {
            List<Consulta> lista = consultaModel.ListarTodos();

            return Ok(lista);
        }*/
    }
}
