using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProgettoPoliziaMunicipale.Models;

[Table("TipoViolazione")]
public partial class TipoViolazione
{
    [Key]
    [Column("idviolazione")]
    public int Idviolazione { get; set; }

    [Column("descrizione")]
    [StringLength(200)]
    [Unicode(false)]
    public string Descrizione { get; set; } = null!;

    [InverseProperty("IdviolazioneNavigation")]
    public virtual ICollection<Verbale> Verbales { get; set; } = new List<Verbale>();
}
