using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Models;

namespace SpMedicalGroup.Services
{
    public class EspecialidadeService
    {
        private readonly SpMedicalGroupContext ctx = new();

        public async Task<List<IdNomeEspecialidadeDto>> ObterEspecialidadesMedico(string cpf)
        {
            var especialidadesMedico = await 
                (from esp in ctx.Especialidades
                join medEsp in ctx.MedicosEspecialidades on esp.EspecialidadeId equals medEsp.EspecialidadeId
                join med in ctx.Medicos on medEsp.CpfMedico equals med.Cpf
                where medEsp.CpfMedico == cpf
                select new IdNomeEspecialidadeDto
                {
                    EspecialidadeId = esp.EspecialidadeId,
                    Nome = esp.Nome
                }).ToListAsync();

            return especialidadesMedico;
        }

        public async Task<List<PaginaEspecialidadesDto>> ListarInfoPaginaEspecialidades()
        {
            var especialidadesInformacoes = await 
                (from esp in ctx.Especialidades
                join medEsp in ctx.MedicosEspecialidades on esp.EspecialidadeId equals medEsp.EspecialidadeId
                join med in ctx.Medicos on medEsp.CpfMedico equals med.Cpf
                group new { med, medEsp } by new { esp.Nome, esp.Descricao } into grouped
                select new PaginaEspecialidadesDto
                {
                    Especialidade = grouped.Key.Nome,
                    Descricao = grouped.Key.Descricao,
                    NumeroMedicos = grouped.Count(),
                    PrecoMinimo = grouped.Min(g => g.medEsp.ValorProcedimento)
                }).ToListAsync();


            return especialidadesInformacoes;
        }


        public async Task<List<Especialidade>> ListarTodas()
        {
            return await ctx.Especialidades.ToListAsync();
        }
    }
}
