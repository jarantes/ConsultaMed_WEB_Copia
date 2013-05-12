namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retiradaForgeinKey2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario");
            DropIndex("dbo.HorarioTemp", new[] { "MedicoUserId" });
            AddColumn("dbo.HorarioTemp", "UsuarioMedico_UserId", c => c.Int());
            AddForeignKey("dbo.HorarioTemp", "UsuarioMedico_UserId", "dbo.Usuario", "UserId");
            CreateIndex("dbo.HorarioTemp", "UsuarioMedico_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HorarioTemp", new[] { "UsuarioMedico_UserId" });
            DropForeignKey("dbo.HorarioTemp", "UsuarioMedico_UserId", "dbo.Usuario");
            DropColumn("dbo.HorarioTemp", "UsuarioMedico_UserId");
            CreateIndex("dbo.HorarioTemp", "MedicoUserId");
            AddForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario", "UserId", cascadeDelete: true);
        }
    }
}
