using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProgettoPoliziaMunicipale.ViewModels
{
    public class VerbaleViewModel
    {
        public int Idverbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; } = string.Empty;
        public string NominativoAgente { get; set; } = string.Empty;
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }

        public int? Idanagrafica { get; set; }
        public int? Idviolazione { get; set; }

        public List<SelectListItem> Trasgressori { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Violazioni { get; set; } = new List<SelectListItem>();
    }
}
