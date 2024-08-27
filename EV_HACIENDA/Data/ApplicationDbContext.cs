using EV_HACIENDA.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<FacturaElectronica> FacturasElectronicas { get; set; }
    public DbSet<Emisor> Emisores { get; set; }
    public DbSet<Receptor> Receptores { get; set; }
    public DbSet<Impuesto> Impuestos { get; set; }
    public DbSet<LineaDetalle> LineasDetalles { get; set; }
    public DbSet<ResumenFactura> ResumenFacturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // relación uno a muchos entre FacturaElectronica y LineaDetalle
        modelBuilder.Entity<FacturaElectronica>()
            .HasMany(f => f.LineasDetalles)
            .WithOne(ld => ld.FacturaElectronica)
            .HasForeignKey(ld => ld.FacturaElectronicaId)
            .OnDelete(DeleteBehavior.Cascade);

        // relación uno a muchos entre Emisor y FacturaElectronica
        modelBuilder.Entity<Emisor>()
            .HasMany(e => e.FacturasElectronicas)
            .WithOne(f => f.Emisor)
            .HasForeignKey(f => f.EmisorId);

        //  relación uno a muchos entre Receptor y FacturaElectronica
        modelBuilder.Entity<Receptor>()
            .HasMany(r => r.FacturasElectronicas)
            .WithOne(f => f.Receptor)
            .HasForeignKey(f => f.ReceptorId);

        // relación uno a muchos entre ResumenFactura y FacturaElectronica
        modelBuilder.Entity<ResumenFactura>()
            .HasMany(rf => rf.FacturasElectronicas)
            .WithOne(f => f.ResumenFactura)
            .HasForeignKey(f => f.ResumenFacturaId);

        base.OnModelCreating(modelBuilder);
    }

}
