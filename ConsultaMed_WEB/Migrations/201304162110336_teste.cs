namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame");
            DropForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica");
            DropIndex("dbo.Agendamento", new[] { "MedicoUserId" });
            DropIndex("dbo.Agendamento", new[] { "PacienteUserId" });
            DropIndex("dbo.Usuario", new[] { "EspecialidadeId" });
            DropIndex("dbo.Usuario", new[] { "EnderecoId" });
            DropIndex("dbo.Usuario", new[] { "ConvenioId" });
            DropIndex("dbo.Horario", new[] { "MedicoUserId" });
            DropIndex("dbo.HorarioTemp", new[] { "MedicoUserId" });
            DropIndex("dbo.Clinica", new[] { "EnderecoId" });
            DropIndex("dbo.Prontuario", new[] { "ExameId" });
            DropIndex("dbo.Prontuario", new[] { "UserId" });
            DropIndex("dbo.ConvenioClinica", new[] { "Convenio_ConvenioId" });
            DropIndex("dbo.ConvenioClinica", new[] { "Clinica_ClinicaId" });
            AddForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade", "EspecialidadeId");
            AddForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco", "EnderecoId");
            AddForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio", "ConvenioId");
            AddForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco", "EnderecoId");
            AddForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame", "ExameId");
            AddForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio", "ConvenioId");
            AddForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica", "ClinicaId");
            CreateIndex("dbo.Agendamento", "MedicoUserId");
            CreateIndex("dbo.Agendamento", "PacienteUserId");
            CreateIndex("dbo.Usuario", "EspecialidadeId");
            CreateIndex("dbo.Usuario", "EnderecoId");
            CreateIndex("dbo.Usuario", "ConvenioId");
            CreateIndex("dbo.Horario", "MedicoUserId");
            CreateIndex("dbo.HorarioTemp", "MedicoUserId");
            CreateIndex("dbo.Clinica", "EnderecoId");
            CreateIndex("dbo.Prontuario", "ExameId");
            CreateIndex("dbo.Prontuario", "UserId");
            CreateIndex("dbo.ConvenioClinica", "Convenio_ConvenioId");
            CreateIndex("dbo.ConvenioClinica", "Clinica_ClinicaId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ConvenioClinica", new[] { "Clinica_ClinicaId" });
            DropIndex("dbo.ConvenioClinica", new[] { "Convenio_ConvenioId" });
            DropIndex("dbo.Prontuario", new[] { "UserId" });
            DropIndex("dbo.Prontuario", new[] { "ExameId" });
            DropIndex("dbo.Clinica", new[] { "EnderecoId" });
            DropIndex("dbo.HorarioTemp", new[] { "MedicoUserId" });
            DropIndex("dbo.Horario", new[] { "MedicoUserId" });
            DropIndex("dbo.Usuario", new[] { "ConvenioId" });
            DropIndex("dbo.Usuario", new[] { "EnderecoId" });
            DropIndex("dbo.Usuario", new[] { "EspecialidadeId" });
            DropIndex("dbo.Agendamento", new[] { "PacienteUserId" });
            DropIndex("dbo.Agendamento", new[] { "MedicoUserId" });
            DropForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame");
            DropForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario");
            DropForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario");
            CreateIndex("dbo.ConvenioClinica", "Clinica_ClinicaId");
            CreateIndex("dbo.ConvenioClinica", "Convenio_ConvenioId");
            CreateIndex("dbo.Prontuario", "UserId");
            CreateIndex("dbo.Prontuario", "ExameId");
            CreateIndex("dbo.Clinica", "EnderecoId");
            CreateIndex("dbo.HorarioTemp", "MedicoUserId");
            CreateIndex("dbo.Horario", "MedicoUserId");
            CreateIndex("dbo.Usuario", "ConvenioId");
            CreateIndex("dbo.Usuario", "EnderecoId");
            CreateIndex("dbo.Usuario", "EspecialidadeId");
            CreateIndex("dbo.Agendamento", "PacienteUserId");
            CreateIndex("dbo.Agendamento", "MedicoUserId");
            AddForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica", "ClinicaId", cascadeDelete: true);
            AddForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio", "ConvenioId", cascadeDelete: true);
            AddForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame", "ExameId", cascadeDelete: true);
            AddForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco", "EnderecoId", cascadeDelete: true);
            AddForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio", "ConvenioId", cascadeDelete: true);
            AddForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco", "EnderecoId", cascadeDelete: true);
            AddForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade", "EspecialidadeId", cascadeDelete: true);
            AddForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario", "UserId", cascadeDelete: true);
        }
    }
}
