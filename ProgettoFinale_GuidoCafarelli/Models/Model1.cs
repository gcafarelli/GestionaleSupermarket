using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProgettoFinale_GuidoCafarelli
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<OrdiniMagazzino> OrdiniMagazzino { get; set; }
        public virtual DbSet<OrdiniNegozi> OrdiniNegozi { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prodotti>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.OrdiniMagazzino)
                .WithRequired(e => e.Prodotti)
                .WillCascadeOnDelete(false);
        }
    }
}
