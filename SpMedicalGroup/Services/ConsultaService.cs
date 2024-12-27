using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Models;
using SpMedicalGroup.Dto.Consulta;

namespace SpMedicalGroup.Services
{
    public class ConsultaService
    {

        private readonly SpMedicalGroupContext ctx = new();

        public async Task<List<ConsultaDetalhadaDto>> ListarTodosConsultasMedico(string emailMedico)
        {
            var cpfMedico = await MedicoService.BuscaCpfMedicoPorEmail(emailMedico) ?? throw new Exception();

            var consultasMedico = await 
                (from pac in ctx.Pacientes
                join con in ctx.Consulta on pac.Cpf equals con.CpfPaciente
                join dis in ctx.Disponibilidades on con.DisponibilidadeId equals dis.DisponibilidadeId
                join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
                join esp in ctx.Especialidades on con.EspecialidadeId equals esp.EspecialidadeId
                join medEsp in ctx.MedicosEspecialidades
                    on new { CpfMedico = med.Cpf, esp.EspecialidadeId }
                    equals new { medEsp.CpfMedico, medEsp.EspecialidadeId }
                where med.Cpf == cpfMedico
                select new ConsultaDetalhadaDto
                {
                    NomePaciente = pac.NomeCompleto,
                    CpfPaciente = pac.Cpf,
                    NomeMedico = med.NomeCompleto,
                    Especialidade = esp.Nome,
                    DataConsulta = dis.DataDisp,
                    HoraInicio = dis.HoraInicio,
                    HoraFim = dis.HoraFim,
                    Preco = medEsp.ValorProcedimento,
                    Situacao = con.Situacao
                }).ToListAsync();

            return consultasMedico;
        }

        public async Task<ConfirmarConsultaDetalhesDto> ConfirmarConsultaDetalhes(string cpf, string nomeEspecialidade) 
        {
            var consultaDetalhes = await 
                (from medEsp in ctx.MedicosEspecialidades
                join esp in ctx.Especialidades on medEsp.EspecialidadeId equals esp.EspecialidadeId
                join med in ctx.Medicos on medEsp.CpfMedico equals med.Cpf
                join emp in ctx.Empresas on med.cnpj_empresa equals emp.Cnpj
                join ende in ctx.Enderecos on emp.EnderecoId equals ende.EnderecoId
                where med.Cpf == cpf && esp.Nome == nomeEspecialidade
                select new ConfirmarConsultaDetalhesDto
                {
                    ValorConsulta = medEsp.ValorProcedimento,
                    NomeEmpresa = emp.NomeFantasia,
                    Municipio = ende.Municipio,
                    Bairro = ende.Bairro,
                    Logradouro = ende.Logradouro,
                    Numero = ende.Numero,
                    Complemento = ende.Complemento ?? ""
                }).FirstOrDefaultAsync() ?? throw new Exception();

            return consultaDetalhes;
        }

        public async Task<Consulta> Agendar(AgendarConsultaDto novaConsulta)
        {
            bool disponibilidadeUtilizavel = await VerificaDisponibilidadeJaPreenchida(novaConsulta.DisponibilidadeId);

            if (!disponibilidadeUtilizavel)
                throw new Exception("Disponibilidade já preenchida");

            string cpfPaciente = await PacienteService.BuscaCpfPacientePorEmail(novaConsulta.EmailPaciente) ?? throw new Exception();

            Consulta consultaCriada = new()
            {
                EspecialidadeId = novaConsulta.EspecialidadeId,
                DisponibilidadeId = novaConsulta.DisponibilidadeId,
                CpfPaciente = cpfPaciente,
                Descricao = novaConsulta.Descricao,
                Situacao = "Agendada",
                IsTelemedicina = novaConsulta.IsTelemedicina
            };

            await ctx.Consulta.AddAsync(consultaCriada);
            await ctx.SaveChangesAsync();

            return consultaCriada;
        }

        public async Task<bool> VerificaDisponibilidadeJaPreenchida (int disponibilidadeId)
        {
            return await ctx.Consulta
                .Where(c => c.DisponibilidadeId == disponibilidadeId)
                .FirstOrDefaultAsync() == null;
        }

        public async Task<List<ConsultaDetalhadaDto>> ListarTodasConsultasPaciente(string email)
        {
            string cpfPaciente = await PacienteService.BuscaCpfPacientePorEmail(email) ?? throw new Exception();
            
            var consultasPaciente = await
                (from pac in ctx.Pacientes
                 join con in ctx.Consulta on pac.Cpf equals con.CpfPaciente
                 join dis in ctx.Disponibilidades on con.DisponibilidadeId equals dis.DisponibilidadeId
                 join med in ctx.Medicos on dis.CpfMedico equals med.Cpf
                 join esp in ctx.Especialidades on con.EspecialidadeId equals esp.EspecialidadeId
                 join medEsp in ctx.MedicosEspecialidades
                     on new { CpfMedico = med.Cpf, esp.EspecialidadeId }
                     equals new { medEsp.CpfMedico, medEsp.EspecialidadeId }
                 where pac.Cpf == cpfPaciente
                 select new ConsultaDetalhadaDto
                 {
                     NomePaciente = pac.NomeCompleto,
                     CpfPaciente = pac.Cpf,
                     NomeMedico = med.NomeCompleto,
                     Especialidade = esp.Nome,
                     DataConsulta = dis.DataDisp,
                     HoraInicio = dis.HoraInicio,
                     HoraFim = dis.HoraFim,
                     Preco = medEsp.ValorProcedimento,
                     //Adicionar Descrição e Endereço
                     Situacao = con.Situacao
                 }).ToListAsync();
                

            return consultasPaciente;
        }
    }
}
