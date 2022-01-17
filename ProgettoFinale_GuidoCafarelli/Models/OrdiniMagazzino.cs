namespace ProgettoFinale_GuidoCafarelli
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("OrdiniMagazzino")]
    public partial class OrdiniMagazzino
    {
        [Key]
        public int idOrdineMagazzino { get; set; }

        [Required(ErrorMessage = "Campo Obbligatorio")]
        public int idProdotto { get; set; }

        [Required(ErrorMessage = "Campo Obbligatorio")]
        [DisplayName("Quantità")]
        public int QuantitaEntrata { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Campo Obbligatorio")]
        [DisplayName("Data Consegna")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEntrata { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
