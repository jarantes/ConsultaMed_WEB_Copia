namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicaFavorita : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClinicasFavorita",
                c => new
                    {
                        FavoritaId = c.Int(nullable: false, identity: true),
                        ClinicaId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Ordem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FavoritaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClinicasFavorita");
        }
    }
}
