using Microsoft.AspNetCore.Mvc;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Services;
using Microsoft.AspNetCore.Authorization;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService consultaService = new();

        [HttpGet("ListarTodosConsultasMedico")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> ListarTodosConsultasMedico(string email)
        {
            try
            {
                List<ConsultaDetalhadaDto> result = await consultaService.ListarTodosConsultasMedico(email);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConfirmarConsultaDetalhes")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> ConfirmarConsultaDetalhes(string cpf, string nomeEspecialidade)
        {
            try
            {
                ConfirmarConsultaDetalhesDto result = await consultaService.ConfirmarConsultaDetalhes(cpf, nomeEspecialidade);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Acessar")]
        [Authorize(Roles = "1")]
        public IActionResult Acessar()
        {
            try
            {
                return StatusCode(200, "Acesso liberado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Agendar")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Agendar(Consulta novaConsulta)
        {
            try
            {
                Consulta consultaCriada = await consultaService.Agendar(novaConsulta);
                return StatusCode(201, consultaCriada);
            }
            catch (Exception ex)
            {
                return BadRequest(new {mensagem = "Não foi possível agendar consulta. Tente novamente!", ex});
            }

        }

        /*[Authorize(Roles = "3")]
        [HttpPatch("atualizar/{id}")]
        public IActionResult Atualizar(byte id, Consulta consultaAtt)
        {
            consultaService.Atualizar(id, consultaAtt);

            return StatusCode(204);

        }
        

        [Authorize(Roles = "3")]
        [HttpPost("cancelar/{id}")]
        public IActionResult CancelarConsulta(byte id)
        {
            consultaService.CancelarConsulta(id);
            return StatusCode(204);
        }

        [Authorize(Roles = "3")]
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(byte id)
        {
            consultaService.Deletar(id);
            return StatusCode(204);
        }

        [Authorize(Roles = "2")]
        [HttpPost("incluir/{id}/{descricao}")]
        public IActionResult IncluirDescricao(byte id, string descricao)
        {

            try
            {
                consultaService.IncluirDescricao(id, descricao);
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

                return Ok(consultaService.LerTodasDoMedico(idUsuario));
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



                return Ok(consultaService.LerTodasDoPaciente(idUsuario));
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
            List<Consulta> lista = consultaService.ListarTodos();

            return Ok(lista);
        }*/
    }
}
