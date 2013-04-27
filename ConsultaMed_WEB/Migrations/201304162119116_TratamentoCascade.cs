namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TratamentoCascade : DbMigration
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
            AddForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade", "EspecialidadeId");
            AddForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco", "EnderecoId", cascadeDelete: true);
            AddForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio", "ConvenioId");
            AddForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco", "EnderecoId", cascadeDelete: true);
            AddForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame", "ExameId", cascadeDelete: true);
            AddForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario", "UserId");
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
        }
        
        public override void Down()
        {
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
            AddForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame", "ExameId");
            AddForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco", "EnderecoId");
            AddForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio", "ConvenioId");
            AddForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco", "EnderecoId");
            AddForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade", "EspecialidadeId");
            AddForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario", "UserId");
        }
    }
}
