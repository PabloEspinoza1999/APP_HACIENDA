﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EV_HACIENDA.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240731015204_DBMO")]
    partial class DBMO
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EV_HACIENDA.Models.Codigo", b =>
                {
                    b.Property<int>("CodigoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoId"));

                    b.Property<string>("CodigoValor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodigoId");

                    b.ToTable("Codigo");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.Emisor", b =>
                {
                    b.Property<int>("EmisorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmisorId"));

                    b.Property<string>("Barrio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Canton")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoPais")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Distrito")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtrasSenas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmisorId");

                    b.ToTable("Emisores");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.FacturaElectronica", b =>
                {
                    b.Property<int>("FacturaElectronicaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacturaElectronicaId"));

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmisorId")
                        .HasColumnType("int");

                    b.Property<string>("EstadoEnvio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<int>("LineaDetalleId")
                        .HasColumnType("int");

                    b.Property<string>("NumeroConsecutivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("ReceptorId")
                        .HasColumnType("int");

                    b.Property<int>("ResumenFacturaId")
                        .HasColumnType("int");

                    b.HasKey("FacturaElectronicaId");

                    b.HasIndex("EmisorId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("ReceptorId");

                    b.HasIndex("ResumenFacturaId");

                    b.ToTable("FacturasElectronicas");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.Impuesto", b =>
                {
                    b.Property<int>("ImpuestoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImpuestoId"));

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tarifa")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ImpuestoId");

                    b.ToTable("Impuestos");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.LineaDetalle", b =>
                {
                    b.Property<int>("LineaDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineaDetalleId"));

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CodigoId")
                        .HasColumnType("int");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacturaElectronicaId")
                        .HasColumnType("int");

                    b.Property<int>("ImpuestoId")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoDescuento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MontoTotalLinea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NaturalezaDescuento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroLinea")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LineaDetalleId");

                    b.HasIndex("CodigoId");

                    b.HasIndex("FacturaElectronicaId")
                        .IsUnique();

                    b.HasIndex("ImpuestoId");

                    b.ToTable("LineasDetalles");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductoId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.Receptor", b =>
                {
                    b.Property<int>("ReceptorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceptorId"));

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReceptorId");

                    b.ToTable("Receptores");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.ResumenFactura", b =>
                {
                    b.Property<int>("ResumenFacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResumenFacturaId"));

                    b.Property<decimal>("TotalComprobante")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalGravado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalImpuesto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ResumenFacturaId");

                    b.ToTable("ResumenFacturas");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.FacturaElectronica", b =>
                {
                    b.HasOne("EV_HACIENDA.Models.Emisor", "Emisor")
                        .WithMany()
                        .HasForeignKey("EmisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EV_HACIENDA.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EV_HACIENDA.Models.Receptor", "Receptor")
                        .WithMany()
                        .HasForeignKey("ReceptorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EV_HACIENDA.Models.ResumenFactura", "ResumenFactura")
                        .WithMany()
                        .HasForeignKey("ResumenFacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emisor");

                    b.Navigation("Producto");

                    b.Navigation("Receptor");

                    b.Navigation("ResumenFactura");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.LineaDetalle", b =>
                {
                    b.HasOne("EV_HACIENDA.Models.Codigo", "Codigo")
                        .WithMany()
                        .HasForeignKey("CodigoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EV_HACIENDA.Models.FacturaElectronica", "FacturaElectronica")
                        .WithOne("LineaDetalle")
                        .HasForeignKey("EV_HACIENDA.Models.LineaDetalle", "FacturaElectronicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EV_HACIENDA.Models.Impuesto", "Impuesto")
                        .WithMany()
                        .HasForeignKey("ImpuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Codigo");

                    b.Navigation("FacturaElectronica");

                    b.Navigation("Impuesto");
                });

            modelBuilder.Entity("EV_HACIENDA.Models.FacturaElectronica", b =>
                {
                    b.Navigation("LineaDetalle")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
