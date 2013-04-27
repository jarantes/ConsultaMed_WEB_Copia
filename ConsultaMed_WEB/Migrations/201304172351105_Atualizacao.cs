namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizacao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame");
            DropIndex("dbo.Prontuario", new[] { "ExameId" });
            AddColumn("dbo.Prontuario", "Exame", c => c.String());
            AddColumn("dbo.Prontuario", "Exame_ExameId", c => c.Int());
            AddForeignKey("dbo.Prontuario", "Exame_ExameId", "dbo.Exame", "ExameId");
            CreateIndex("dbo.Prontuario", "Exame_ExameId");
            DropColumn("dbo.Prontuario", "ExameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prontuario", "ExameId", c => c.Int(nullable: false));
            DropIndex("dbo.Prontuario", new[] { "Exame_ExameId" });
            DropForeignKey("dbo.Prontuario", "Exame_ExameId", "dbo.Exame");
            DropColumn("dbo.Prontuario", "Exame_ExameId");
            DropColumn("dbo.Prontuario", "Exame");
            CreateIndex("dbo.Prontuario", "ExameId");
            AddForeignKey("dbo.Prontuario", "ExameId", "dbo.Exame", "ExameId", cascadeDelete: true);
        }
    }
}
