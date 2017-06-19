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

        public DbSet<Wedstrijd> Wedstrijden { get; set; }
        public DbSet<Speler> Spelers { get; set; }
        public DbSet<SpelerWedstrijd> SpelerWedstrijden { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Speler>(MapSpeler);
            modelBuilder.Entity<Wedstrijd>(MapWedstrijd);
            modelBuilder.Entity<SpelerWedstrijd>(MapSpelerWedstrijd);
        }

        public static void MapSpelerWedstrijd(EntityTypeBuilder<SpelerWedstrijd> sw)
        {
            sw.HasKey(t => new { t.SpelerId, t.WedstrijdId });

            sw.HasOne(t => t.Speler) //naar categorie (1)
                .WithMany(t => t.SpelerWedstrijd); //categorie kent producten (N)

            sw.HasOne(t => t.Wedstrijd) //naar product (1)
                .WithMany(t => t.SpelerWedstrijd); //van product naar categorieproduct (product kent categorie niet) (N)
        }
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

            c.HasMany(t => t.SpelerWedstrijd)
                .WithOne()
                .IsRequired()
                .HasForeignKey(t => t.SpelerId);

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

        private static void MapWedstrijd(EntityTypeBuilder<Wedstrijd> m)
        {
            //Table name
            m.ToTable("Wedstrijd");

            //Primary Key
            //m.HasKey(t => t.WedstrijdId);

            //Properties
            m.Property(t => t.DatumGespeeld)
                .IsRequired();

            //Relations
            m.HasMany(t => t.SpelerWedstrijd)
                .WithOne()
                .IsRequired()
                .HasForeignKey(t => t.WedstrijdId);
            //m.HasOne(t => t.Speler1)
            //    .WithMany()
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);
            //m.HasOne(t => t.Speler2)
            //    .WithMany()
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

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
