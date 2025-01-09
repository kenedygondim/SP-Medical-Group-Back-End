using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Services;
using SpMedicalGroup.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SpMedicalGroup.Repositories;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;
        private readonly SpMedicalGroupContext ctx;

        public LoginController(IUsuarioService usuarioService, SpMedicalGroupContext ctx)
        {
            this.usuarioService = usuarioService;
            this.ctx = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                Log.Information("\n\n\nIniciando login...\n\n\n");
                Usuario usuarioBuscado = await usuarioService.Login(login.Email, login.Senha);

                if (usuarioBuscado == null)
                {
                    Log.Information("E-mail ou senha inválidos");
                    return NotFound("E-mail ou senha inválidos");
                }

                var roleId = await ctx.Roles
                    .Where(r => r.RoleId == usuarioBuscado.RoleId)
                    .Select(r => r.RoleId)
                    .FirstOrDefaultAsync();

                if (roleId.Equals(null))
                {
                    throw new Exception("Role não encontrada para o usuário.");
                }

                var minhasClaims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, roleId.ToString()), 
                new Claim("role", roleId.ToString())          
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SpMedicalGroup-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var meuToken = new JwtSecurityToken(
                    issuer: "SpMedicalGroup",
                    audience: "SpMedicalGroup",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                return StatusCode(200, new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Não foi possível realizar o login. Tente novamente!", detalhes = ex.Message });
            }
        }
    }
}
