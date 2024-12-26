using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Services
{
    public class RoleService
    {
        private readonly SpMedicalGroupContext ctx = new();
        public async Task<Role> Cadastrar(Role novaRole)
        {

            await ctx.Roles.AddAsync(novaRole);
            await ctx.SaveChangesAsync();
            return novaRole;
        }

        public async Task<List<Role>> ListarTodas()
        {
            return await ctx.Roles.ToListAsync();
        }
    }
}
