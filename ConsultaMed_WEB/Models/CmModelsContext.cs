using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConsultaMed_WEB.Models
{
    public class CmContext : DbContext
    {
        public CmContext()
            : base("name=ConsultaMed")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<HorarioTemp> HorarioTemps { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
    }
}
