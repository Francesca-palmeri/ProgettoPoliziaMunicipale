using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgettoPoliziaMunicipale.Models;

[Table("Anagrafica")]
[Index("CodFisc", Name = "UQ__Anagrafi__063721E1C7E38C87", IsUnique = true)]
public partial class Anagrafica
{
    [Key]
    [Column("idanagrafica")]
    public int Idanagrafica { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Cognome { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Indirizzo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Città { get; set; }

    [Column("CAP")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Cap { get; set; }

    [Column("Cod_Fisc")]
    [StringLength(17)]
    [Unicode(false)]
    public string CodFisc { get; set; } = null!;

    [InverseProperty("IdanagraficaNavigation")]
    public virtual ICollection<Verbale> Verbali { get; set; } = new List<Verbale>();
}
