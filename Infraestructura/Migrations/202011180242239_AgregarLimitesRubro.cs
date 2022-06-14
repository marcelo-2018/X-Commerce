namespace Infraestructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarLimitesRubro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rubro", "ActivarLimiteVenta", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rubro", "LimiteVenta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Rubro", "ActivarHoraVenta", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rubro", "HoraLimiteVentaDesde", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rubro", "HoraLimiteVentaHasta", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rubro", "HoraLimiteVentaHasta");
            DropColumn("dbo.Rubro", "HoraLimiteVentaDesde");
            DropColumn("dbo.Rubro", "ActivarHoraVenta");
            DropColumn("dbo.Rubro", "LimiteVenta");
            DropColumn("dbo.Rubro", "ActivarLimiteVenta");
        }
    }
}
