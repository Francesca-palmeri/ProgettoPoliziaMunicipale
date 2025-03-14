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
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string Cap { get; set; }
        public string CodFisc { get; set; }

        // Proprietà per il tipo di violazione
        public int IdViolazione { get; set; }
        public string DescrizioneViolazione { get; set; }
    }

}
