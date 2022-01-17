namespace ProgettoFinale_GuidoCafarelli
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdiniNegozi")]
    public partial class OrdiniNegozi
    {
        [Key]
        public int idOrdineNegozi { get; set; }

        [DisplayName("Punto Vendita")]
        public int idUser { get; set; }

        [Required(ErrorMessage = "Campo Obbligatorio")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required (ErrorMessage ="Campo Obbligatorio")]
        public int idProdotto { get; set; }

        [Required(ErrorMessage = "Campo Obbligatorio")]
        public int Quantita { get; set; }

        public virtual Prodotti Prodotti { get; set; }

        public virtual Users Users { get; set; }
    }
}
