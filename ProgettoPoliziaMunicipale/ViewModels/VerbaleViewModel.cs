using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProgettoPoliziaMunicipale.ViewModels
{
    public class VerbaleViewModel
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }

        // Nuove proprietà per l'anagrafica
        [Required(ErrorMessage = "Seleziona un trasgressore.")]
        public int IdAnagrafica { get; set; }  // Deve essere obbligatorio per il verbale

        // Proprietà per il tipo di violazione
        [Required(ErrorMessage = "Seleziona un tipo di violazione.")]
        public int IdViolazione { get; set; }

        public string DescrizioneViolazione { get; set; }
        public SelectList Anagrafiche { get; set; }
        public SelectList TipiViolazione { get; set; }
    }

}
