using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpMedicalGroup.Domains;
using SpMedicalGroup.Models;
using SpMedicalGroup.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Serilog;

namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioModel usuarioModel = new();

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Log.Information("Iniciando login...");
                Usuario usuarioBuscado = usuarioModel.Login(login.Email, login.Senha);

                if (usuarioBuscado == null)
                {
                    Log.Information("E-mail ou senha inválidos");
                    return NotFound("E-mail ou senha inválidos");
                }

                Log.Information("Usuário encontrado");

                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.UsuarioId.ToString()),
                    new Claim("role", usuarioBuscado.UsuarioId.ToString())
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

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
