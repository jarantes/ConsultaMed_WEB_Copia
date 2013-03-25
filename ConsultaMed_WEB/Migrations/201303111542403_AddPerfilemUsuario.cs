namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPerfilemUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Perfil", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Perfil");
        }
    }
}
