﻿//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultaMed_ModelBD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesCM : DbContext
    {
        public EntitiesCM()
            : base("name=EntitiesCM")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
        public DbSet<Convenio> Convenio { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Exame> Exame { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<HorarioTemp> HorarioTemp { get; set; }
        public DbSet<Prontuario> Prontuario { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
    }
}
