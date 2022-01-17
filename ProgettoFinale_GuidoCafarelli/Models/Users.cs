namespace ProgettoFinale_GuidoCafarelli
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            OrdiniNegozi = new HashSet<OrdiniNegozi>();
        }

        [Key]
        [DisplayName("Punto Vendita")]
        public int idUser { get; set; }

        [StringLength(50)]
        [DisplayName("Punto Vendita")]
        public string User { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdiniNegozi> OrdiniNegozi { get; set; }
    }
}
