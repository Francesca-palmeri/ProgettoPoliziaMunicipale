namespace ProgettoPoliziaMunicipale.ViewModels
{
    public class ContestazioneViewModel
    {
        public int IdVerbale { get; set; }
        public string Trasgressore { get; set; }
        public string Violazione { get; set; }
        public DateTime DataViolazione { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }

        public string MotivoContestazione { get; set; }
    }
}
