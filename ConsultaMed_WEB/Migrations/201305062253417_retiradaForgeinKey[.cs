namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retiradaForgeinKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario");
            DropIndex("dbo.Horario", new[] { "MedicoUserId" });
            AddColumn("dbo.Horario", "UsuarioMedico_UserId", c => c.Int());
            AddForeignKey("dbo.Horario", "UsuarioMedico_UserId", "dbo.Usuario", "UserId");
            CreateIndex("dbo.Horario", "UsuarioMedico_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Horario", new[] { "UsuarioMedico_UserId" });
            DropForeignKey("dbo.Horario", "UsuarioMedico_UserId", "dbo.Usuario");
            DropColumn("dbo.Horario", "UsuarioMedico_UserId");
            CreateIndex("dbo.Horario", "MedicoUserId");
            AddForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario", "UserId", cascadeDelete: true);
        }
    }
}
