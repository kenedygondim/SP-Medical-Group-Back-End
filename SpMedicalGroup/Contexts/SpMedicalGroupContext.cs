using Microsoft.EntityFrameworkCore;
using SpMedicalGroup.Dto.Consulta;
using SpMedicalGroup.Dto.Disponibilidade;
using SpMedicalGroup.Dto.Especialidade;
using SpMedicalGroup.Dto.FotoPerfil;
using SpMedicalGroup.Dto.Medico;
using SpMedicalGroup.Dto.Paciente;
using SpMedicalGroup.Dto.Usuario;
using SpMedicalGroup.Models;


#nullable disable

namespace SpMedicalGroup.Contexts
{
    public partial class SpMedicalGroupContext : DbContext
    {
        public SpMedicalGroupContext()
        {
        }

        public SpMedicalGroupContext(DbContextOptions<SpMedicalGroupContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<FotoPerfil> FotosPerfil { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Medico> Medicos { get; set; }
        public virtual DbSet<Especialidade> Especialidades { get; set; }
        public virtual DbSet<MedicoEspecialidade> MedicosEspecialidades { get; set; }
        public virtual DbSet<Disponibilidade> Disponibilidades { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-N94GSUP;Database=SP_MEDICAL_GROUP;Trusted_Connection=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<ConsultaDetalhadaDto>().HasNoKey();
            modelBuilder.Entity<MedicoInformacoesCardDto>().HasNoKey();
            modelBuilder.Entity<PaginaEspecialidadesDto>().HasNoKey();
            modelBuilder.Entity<CriarDisponibilidadeDto>().HasNoKey();
            modelBuilder.Entity<EnvioEmailUsuario>().HasNoKey();
            modelBuilder.Entity<InfoBasicasUsuario>().HasNoKey();
            modelBuilder.Entity<PerfilPacienteDto>().HasNoKey();
            modelBuilder.Entity<InformacoesMedicoPopUp>().HasNoKey();
            modelBuilder.Entity<IdNomeEspecialidadeDto>().HasNoKey();
            modelBuilder.Entity<ConfirmarConsultaDetalhesDto>().HasNoKey();
            modelBuilder.Entity<AgendarConsultaDto>().HasNoKey();
            modelBuilder.Entity<CadastroPacienteDto>().HasNoKey();
            modelBuilder.Entity<AlterarFotoPerfilDto>().HasNoKey();


            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("tb_roles");
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tb_roles__8AFACE1A");
                entity.Property(e =>  e.RoleId)
                    .HasColumnName("role_id");
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.HasMany<Usuario>()
                    .WithOne()
                    .HasForeignKey(u => u.RoleId);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("tb_enderecos");
                entity.HasKey(e => e.EnderecoId)
                    .HasName("PK__tb_enderecos__8AFACE1A");
                entity.Property(e => e.EnderecoId)
                    .HasColumnName("endereco_id");
                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("cep");
                entity.Property(e => e.Municipio)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("municipio");
                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(105)
                    .IsUnicode(false)
                    .HasColumnName("bairro");
                entity.Property(e => e.Logradouro)
                    .IsRequired()
                    .HasMaxLength(105)
                    .IsUnicode(false)
                    .HasColumnName("logradouro");
                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero");
                entity.Property(e => e.Complemento)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("complemento");

                entity.HasOne<Medico>()
                    .WithOne(m => m.Endereco)
                    .HasForeignKey<Medico>(m => m.EnderecoId);

                entity.HasOne<Paciente>()
                     .WithOne(m => m.Endereco)
                    .HasForeignKey<Paciente>(p => p.EnderecoId);

                entity.HasOne<Empresa>()
                     .WithOne(m => m.Endereco)
                    .HasForeignKey<Empresa>(e => e.EnderecoId);

            });

            modelBuilder.Entity<FotoPerfil>(entity =>
            {
                entity.ToTable("tb_fotos_perfil");
                entity.HasKey(e => e.FotoPerfilId)
                    .HasName("PK__tb_fotos_perfil__8AFACE1A");
                entity.Property(e => e.FotoPerfilId)
                    .HasColumnName("foto_perfil_id");
                entity.Property(e => e.FotoPerfilUrl)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("foto_perfil_url");

                entity.HasOne<Medico>()
                    .WithOne(f => f.FotoPerfil)
                    .HasForeignKey<Medico>(m => m.FotoPerfilId);

                entity.HasOne<Paciente>()
                    .WithOne(f => f.FotoPerfil)
                    .HasForeignKey<Paciente>(p => p.FotoPerfilId);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("tb_usuarios");
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PK__tb_usuarios__8AFACE1A");
                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id");
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("senha");
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasColumnName("role_id");

            });

            modelBuilder.Entity<Paciente>(entity => {
                entity.ToTable("tb_pacientes");
                entity.HasKey(e => e.Cpf)
                    .HasName("PK__tb_pacientes__8AFACE1A");
                entity.Property(e => e.Cpf)
                    .HasColumnName("cpf");
                entity.Property(e => e.NomeCompleto)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nome_completo");
                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("rg");
                entity.Property(e => e.DataNascimento)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dt_nascimento");
                entity.Property(e => e.EnderecoId)
                    .IsRequired()
                    .HasColumnName("endereco_id");
                entity.Property(e => e.FotoPerfilId)
                    .HasColumnName("foto_perfil_id");
                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("usuario_id");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("tb_empresas");
                entity.HasKey(e => e.Cnpj)
                    .HasName("PK__tb_empresas__8AFACE1A");
                entity.Property(e => e.Cnpj)
                    .HasColumnName("cnpj");
                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nome_fantasia");
                entity.Property(e => e.EnderecoId)
                    .IsRequired()
                    .HasColumnName("endereco_id");

                entity.HasMany<Medico>()
                    .WithOne()
                    .HasForeignKey(m => m.cnpj_empresa);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("tb_medicos");
                entity.HasKey(e => e.Cpf)
                    .HasName("PK__tb_medicos__8AFACE1A");
                entity.Property(e => e.Cpf)
                    .HasColumnName("cpf");
                entity.Property(e => e.NomeCompleto)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nome_completo");
                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("rg");
                entity.Property(e => e.DataNascimento)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dt_nascimento");
                entity.Property(e => e.Crm)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("crm");
                entity.Property(e => e.UsuarioId)
                    .IsRequired()
                    .HasColumnName("usuario_id");
                entity.Property(e => e.EnderecoId)
                    .IsRequired()
                    .HasColumnName("endereco_id");
                entity.Property(e => e.FotoPerfilId)
                    .HasColumnName("foto_perfil_id");
                entity.Property(e => e.cnpj_empresa)
                    .HasColumnName("cnpj_empresa");
            });

            modelBuilder.Entity<Especialidade>(entity =>{
                entity.ToTable("tb_especialidades");
                entity.HasKey(e => e.EspecialidadeId)
                    .HasName("PK__tb_especialidades__8AFACE1A");
                entity.Property(e => e.EspecialidadeId)
                    .HasColumnName("especialidade_id");
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("nome");
                entity.Property(e => e.Descricao)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descricao");
            });

            modelBuilder.Entity<MedicoEspecialidade>(entity =>
            {
                entity.ToTable("tb_medico_especialidades");
                entity.HasKey(e => new { e.CpfMedico, e.EspecialidadeId })
                    .HasName("PK__tb_medico_especialidades__8AFACE1A");
                entity.Property(e => e.CpfMedico)
                    .HasColumnName("cpf_medico");
                entity.Property(e => e.EspecialidadeId)
                    .HasColumnName("especialidade_id");
                entity.Property(e => e.ValorProcedimento)
                    .IsRequired()
                    .HasColumnName("valor_procedimento");


                entity
                .HasOne(m => m.Medico)
                .WithMany(m => m.MedicoEspecialidade)
                .HasForeignKey(m => m.CpfMedico);

                entity
                .HasOne(e => e.Especialidade)
                .WithMany(e => e.MedicoEspecialidade)
                .HasForeignKey(ac => ac.EspecialidadeId);
            });

            modelBuilder.Entity<Disponibilidade>(entity => {
                entity.ToTable("tb_disponibilidades");
                entity.HasKey(e => e.DisponibilidadeId)
                    .HasName("PK__tb_disponibilidades__8AFACE1A");
                entity.Property(e => e.DisponibilidadeId)
                .HasColumnName("disponibilidade_id");
                entity.Property(e => e.CpfMedico)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cpf_medico");
                entity.Property(e => e.DataDisp)
                .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("data_disp");
                entity.Property(e => e.HoraInicio)
                .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("hora_inicio");
                entity.Property(e => e.HoraFim)
                .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("hora_fim");

                entity.HasOne<Medico>()
                .WithMany(f => f.Disponibilidades)
                .HasForeignKey(p => p.CpfMedico);
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.ToTable("tb_consultas");
                entity.HasKey(e => e.ConsultaId)
                    .HasName("PK__tb_consultas__8AFACE1A");
                entity.Property(e => e.ConsultaId)
                .HasColumnName("consulta_id");
                entity.Property(e => e.DisponibilidadeId)
                    .IsRequired()
                    .HasColumnName("disponibilidade_id");
                entity.Property(e => e.EspecialidadeId)
                .IsRequired()
                    .HasColumnName("especialidade_id");
                entity.Property(e => e.Descricao)
                .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descricao");
                entity.Property(e => e.Situacao)
                .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("situacao");
                entity.Property(e => e.CpfPaciente)
                .IsRequired()
                .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cpf_paciente");
                entity.Property(e => e.IsTelemedicina)
                .IsRequired()
                    .HasColumnName("is_telemedicina");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
