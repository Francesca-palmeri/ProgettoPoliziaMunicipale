namespace ProgettoPoliziaMunicipale.ViewModels
{
    public class AnagraficaViewModel
    {
        public int Idanagrafica { get; set; }
        public string Cognome { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string? Indirizzo { get; set; }
        public string? Città { get; set; }
        public string? Cap { get; set; }
        public string CodFisc { get; set; } = string.Empty;
    }
}
