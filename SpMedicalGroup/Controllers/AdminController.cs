using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SpMedicalGroup.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        [HttpGet("Acessar")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Acessar()
        {
            return StatusCode(200, "Acesso liberado!");
        }

    }
}
