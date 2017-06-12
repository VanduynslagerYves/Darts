using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Darts.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Darts.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Brewer> Brewers { get; set; }
        //public DbSet<Beer> Beers { get; set; }
        public DbSet<Resultaat> Resultaten { get; set; }
        public DbSet<Speler> Spelers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Brewer>(MapBrewer);
            //modelBuilder.Entity<Beer>(MapBeer);
            modelBuilder.Entity<Speler>(MapSpeler);
            modelBuilder.Entity<Resultaat>(MapResultaat);
            //modelBuilder.Entity<Order>(MapOrder);
            //modelBuilder.Entity<OrderLine>(MapOrderLine);
        }

        //public static void MapOrder(EntityTypeBuilder<Order> o) {
        //    o.ToTable("Order");
        //    o.Property(t => t.Street).IsRequired().HasMaxLength(100);
        //    o.HasMany(t => t.OrderLines).WithOne().HasForeignKey(t => t.OrderId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        //    o.HasOne(c => c.Location).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        //}

        public static void MapSpeler(EntityTypeBuilder<Speler> c)
        {
            c.ToTable("Speler");
            c.Property(t => t.Naam)
                .IsRequired()
                .HasMaxLength(100);
            c.Property(t => t.Voornaam)
                .IsRequired()
                .HasMaxLength(100);
            c.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            c.HasMany(t => t.Resultaten)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        //public static void MapOrderLine(EntityTypeBuilder<OrderLine> ol)
        //{
        //    ol.ToTable("OrderLine");
        //    ol.HasKey(t => new
        //    {
        //        t.OrderId,
        //        t.ProductId
        //    });
        //    ol.HasOne(o => o.Product).WithMany().IsRequired().HasForeignKey(o => o.ProductId).OnDelete(DeleteBehavior.Restrict);
        //}

        private static void MapResultaat(EntityTypeBuilder<Resultaat> m)
        {
            //Table name
            m.ToTable("Resultaat");

            //Primary Key
            m.HasKey(t => t.ResultaatId);

            //Properties
            m.Property(t => t.Punten)
                .IsRequired()
                .HasMaxLength(50);
            m.Property(t => t.SpeelDatum)
                .IsRequired();
        }


        //private void MapBeer(EntityTypeBuilder<Beer> b)
        //{
        //    //Table name
        //    b.ToTable("Beer");
        //    // Properties
        //    b.Property(t => t.Name).IsRequired().HasMaxLength(100);
        //}

        //private static void MapBrewer(EntityTypeBuilder<Brewer> b)
        //{
        //    //Table name
        //    b.ToTable("Brewer");

        //    //Primary Key
        //    b.HasKey(t => t.BrewerId);

        //    //Properties
        //    b.Property(t => t.Name)
        //        .HasColumnName("BrewerName")
        //        .IsRequired()
        //        .HasMaxLength(100);

        //    b.Property(t => t.ContactEmail)
        //        .HasMaxLength(100);

        //    b.Property(t => t.Street)
        //        .HasMaxLength(100);

        //    b.Property(t => t.BrewerId)
        //        .ValueGeneratedOnAdd();

        //    //Associations
        //    b.HasMany(t => t.Beers)
        //        .WithOne()
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Cascade);

        //    b.HasOne(t => t.Location)
        //       .WithMany()
        //       .IsRequired(false)
        //       .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
