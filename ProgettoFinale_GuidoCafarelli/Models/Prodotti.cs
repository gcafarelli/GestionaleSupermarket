namespace ProgettoFinale_GuidoCafarelli
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            OrdiniMagazzino = new HashSet<OrdiniMagazzino>();
            OrdiniNegozi = new HashSet<OrdiniNegozi>();
        }

        [Key]
        public int idProdotto { get; set; }

        [Required(ErrorMessage = "Campo Obbligatorio")]
        [StringLength(50)]
        [DisplayName("Articolo")]
        public string Nome { get; set; }

        [StringLength(50)]
        [DisplayName("Foto")]
        public string filename { get; set; }

        public int? idCategoria { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal? Prezzo { get; set; }

        public int Stock { get; set; }

        public virtual Categorie Categorie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdiniMagazzino> OrdiniMagazzino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdiniNegozi> OrdiniNegozi { get; set; }
    }
}
