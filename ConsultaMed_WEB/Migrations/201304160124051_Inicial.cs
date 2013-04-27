namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco", "EnderecoId");
            CreateIndex("dbo.Clinica", "EnderecoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clinica", new[] { "EnderecoId" });
            DropForeignKey("dbo.Clinica", "EnderecoId", "dbo.Endereco");
        }
    }
}
