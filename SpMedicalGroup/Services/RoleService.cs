﻿using Microsoft.EntityFrameworkCore;
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
