using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgettoPoliziaMunicipale.Models;

[Table("Verbale")]
public partial class Verbale
{
    [Key]
    [Column("idverbale")]
    public int Idverbale { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataViolazione { get; set; }

    [Column("Indirizzo_Violazione")]
    [StringLength(100)]
    [Unicode(false)]
    public string IndirizzoViolazione { get; set; } = null!;

    [Column("Nominativo_Agente")]
    [StringLength(100)]
    [Unicode(false)]
    public string NominativoAgente { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataTrascrizioneVerbale { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Importo { get; set; }

    public int DecurtamentoPunti { get; set; }

    [Column("idanagrafica")]
    public int? Idanagrafica { get; set; }

    [Column("idviolazione")]
    public int? Idviolazione { get; set; }

    [ForeignKey("Idanagrafica")]
    [InverseProperty("Verbali")]
    public virtual Anagrafica? IdanagraficaNavigation { get; set; }

    [ForeignKey("Idviolazione")]
    [InverseProperty("Verbali")]
    public virtual TipoViolazione? IdviolazioneNavigation { get; set; }
}
