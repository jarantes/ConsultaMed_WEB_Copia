namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReformulacaoDataAnotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Horario", "PerInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.Horario", "PerFim", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clinica", "EnderecoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Especialidade", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.Clinica", "Site", c => c.String());
            AlterColumn("dbo.Convenio", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.Prontuario", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.Exame", "Descricao", c => c.String(nullable: false));
            AddForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco", "EnderecoId");
            CreateIndex("dbo.Clinica", "EnderecoId");
            DropColumn("dbo.Horario", "Semana");
            DropColumn("dbo.Clinica", "Email");
            DropColumn("dbo.Clinica", "Bairro");
            DropColumn("dbo.Clinica", "Cidade");
            DropColumn("dbo.Clinica", "Telefone1");
            DropColumn("dbo.Clinica", "Telefone2");
            DropColumn("dbo.Clinica", "Rua");
            DropColumn("dbo.Clinica", "Numero");
            DropColumn("dbo.Prontuario", "DataCriacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prontuario", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clinica", "Numero", c => c.Int(nullable: false));
            AddColumn("dbo.Clinica", "Rua", c => c.String(nullable: false));
            AddColumn("dbo.Clinica", "Telefone2", c => c.String());
            AddColumn("dbo.Clinica", "Telefone1", c => c.String(nullable: false));
            AddColumn("dbo.Clinica", "Cidade", c => c.String(nullable: false));
            AddColumn("dbo.Clinica", "Bairro", c => c.String(nullable: false));
            AddColumn("dbo.Clinica", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Horario", "Semana", c => c.String());
            DropIndex("dbo.Clinica", new[] { "EnderecoId" });
            DropForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco");
            AlterColumn("dbo.Exame", "Descricao", c => c.String());
            AlterColumn("dbo.Prontuario", "Descricao", c => c.String());
            AlterColumn("dbo.Convenio", "Descricao", c => c.String());
            AlterColumn("dbo.Clinica", "Site", c => c.String(nullable: false));
            AlterColumn("dbo.Especialidade", "Descricao", c => c.String());
            DropColumn("dbo.Clinica", "EnderecoId");
            DropColumn("dbo.Horario", "PerFim");
            DropColumn("dbo.Horario", "PerInicio");
        }
    }
}
