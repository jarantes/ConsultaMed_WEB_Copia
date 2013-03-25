namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TratarFk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendamento",
                c => new
                    {
                        AgendamentoId = c.Int(nullable: false, identity: true),
                        MarcadorId = c.Int(nullable: false),
                        Horario = c.DateTime(nullable: false),
                        Data = c.DateTime(nullable: false),
                        PacienteUserId = c.Int(nullable: false),
                        MedicoUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgendamentoId)
                .ForeignKey("dbo.Usuario", t => t.MedicoUserId)
                .ForeignKey("dbo.Usuario", t => t.PacienteUserId)
                .Index(t => t.MedicoUserId)
                .Index(t => t.PacienteUserId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Sobrenome = c.String(nullable: false),
                        UserName = c.String(),
                        Email = c.String(nullable: false),
                        Cpf = c.String(nullable: false),
                        Rg = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                        Celular = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        ClinicaId = c.Int(nullable: false),
                        ConvenioId = c.Int(nullable: false),
                        EnderecoId = c.Int(nullable: false),
                        Crm = c.String(),
                        EspecialidadeId = c.Int(),
                        NumeroConvenio = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Especialidade", t => t.EspecialidadeId, cascadeDelete: true)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId, cascadeDelete: true)
                .ForeignKey("dbo.Convenio", t => t.ConvenioId, cascadeDelete: true)
                .ForeignKey("dbo.Clinica", t => t.ClinicaId, cascadeDelete: true)
                .Index(t => t.EspecialidadeId)
                .Index(t => t.EnderecoId)
                .Index(t => t.ConvenioId)
                .Index(t => t.ClinicaId);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        EspecialidadeId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadeId);
            
            CreateTable(
                "dbo.Horario",
                c => new
                    {
                        HorarioId = c.Int(nullable: false, identity: true),
                        HorarioIni = c.DateTime(nullable: false),
                        HorarioFim = c.DateTime(nullable: false),
                        TempoConsulta = c.DateTime(nullable: false),
                        Semana = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HorarioId)
                .ForeignKey("dbo.Usuario", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.HorarioTemp",
                c => new
                    {
                        HorarioTempId = c.Int(nullable: false, identity: true),
                        HorarioIni = c.DateTime(nullable: false),
                        HorarioFim = c.DateTime(nullable: false),
                        TempoConsulta = c.DateTime(nullable: false),
                        Data = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HorarioTempId)
                .ForeignKey("dbo.Usuario", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoId = c.Int(nullable: false, identity: true),
                        Rua = c.String(nullable: false),
                        Estado = c.String(nullable: false),
                        Cidade = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Cep = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Convenio",
                c => new
                    {
                        ConvenioId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.ConvenioId);
            
            CreateTable(
                "dbo.Clinica",
                c => new
                    {
                        ClinicaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                        Site = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        HorarioAtendimento = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        Cidade = c.String(nullable: false),
                        Telefone1 = c.String(nullable: false),
                        Telefone2 = c.String(),
                        Rua = c.String(nullable: false),
                        Numero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClinicaId);
            
            CreateTable(
                "dbo.Prontuario",
                c => new
                    {
                        ProntuarioId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        ExameId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProntuarioId)
                .ForeignKey("dbo.Exame", t => t.ExameId, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ExameId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Exame",
                c => new
                    {
                        ExameId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.ExameId);
            
            CreateTable(
                "dbo.ClinicaConvenio",
                c => new
                    {
                        Clinica_ClinicaId = c.Int(nullable: false),
                        Convenio_ConvenioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clinica_ClinicaId, t.Convenio_ConvenioId })
                .ForeignKey("dbo.Clinica", t => t.Clinica_ClinicaId, cascadeDelete: true)
                .ForeignKey("dbo.Convenio", t => t.Convenio_ConvenioId, cascadeDelete: true)
                .Index(t => t.Clinica_ClinicaId)
                .Index(t => t.Convenio_ConvenioId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ClinicaConvenio", new[] { "Convenio_ConvenioId" });
            DropIndex("dbo.ClinicaConvenio", new[] { "Clinica_ClinicaId" });
            DropIndex("dbo.Prontuario", new[] { "UserId" });
            DropIndex("dbo.Prontuario", new[] { "ExameId" });
            DropIndex("dbo.HorarioTemp", new[] { "UserId" });
            DropIndex("dbo.Horario", new[] { "UserId" });
            DropIndex("dbo.Usuario", new[] { "ClinicaId" });
            DropIndex("dbo.Usuario", new[] { "ConvenioId" });
            DropIndex("dbo.Usuario", new[] { "EnderecoId" });
            DropIndex("dbo.Usuario", new[] { "EspecialidadeId" });
            DropIndex("dbo.Agendamento", new[] { "PacienteUserId" });
            DropIndex("dbo.Agendamento", new[] { "MedicoUserId" });
            DropForeignKey("dbo.ClinicaConvenio", "Convenio_ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.ClinicaConvenio", "Clinica_ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.Prontuario", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame");
            DropForeignKey("dbo.HorarioTemp", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Horario", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.Usuario", "ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.Usuario", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Usuario", "EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario");
            DropForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario");
            DropTable("dbo.ClinicaConvenio");
            DropTable("dbo.Exame");
            DropTable("dbo.Prontuario");
            DropTable("dbo.Clinica");
            DropTable("dbo.Convenio");
            DropTable("dbo.Endereco");
            DropTable("dbo.HorarioTemp");
            DropTable("dbo.Horario");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Usuario");
            DropTable("dbo.Agendamento");
        }
    }
}
