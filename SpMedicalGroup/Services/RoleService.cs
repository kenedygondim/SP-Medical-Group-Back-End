using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Repositories;

namespace SpMedicalGroup.Services
{
    public class RoleService  : IRoleService
    {
        private readonly SpMedicalGroupContext ctx;

        public RoleService(SpMedicalGroupContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<string> AdicionarRole(string nomeRole)
        {
            bool perfilExistente = await ctx.Roles.FirstOrDefaultAsync(r => r.Nome == nomeRole) != null;

            if (perfilExistente)
            {
                throw new Exception("Perfil de usuário já existe.");
            }

            await ctx.Roles.AddAsync(new Role() { Nome = nomeRole });
            await ctx.SaveChangesAsync();
            return "Perfil de usuário criado!";
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await ctx.Roles.ToListAsync();
        }
    }
}
