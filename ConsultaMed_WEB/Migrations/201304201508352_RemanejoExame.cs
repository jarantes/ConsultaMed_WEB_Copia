namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemanejoExame : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exame", "MedicoUserId", "dbo.Usuario");
            DropIndex("dbo.Exame", new[] { "MedicoUserId" });
            DropColumn("dbo.Exame", "MedicoUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exame", "MedicoUserId", c => c.Int());
            CreateIndex("dbo.Exame", "MedicoUserId");
            AddForeignKey("dbo.Exame", "MedicoUserId", "dbo.Usuario", "UserId");
        }
    }
}
