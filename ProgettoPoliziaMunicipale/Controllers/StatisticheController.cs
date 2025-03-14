using Microsoft.AspNetCore.Mvc;
using ProgettoPoliziaMunicipale.Data;


[Route("Statistiche/[action]")]
public class StatisticheController : Controller
{
    private readonly ApplicationDbContext _context;

    public StatisticheController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VerbaliPerTrasgressore()
    {
        var verbali = _context.Verbali
            .GroupBy(v => v.Idanagrafica)
            .Select(g => new { Trasgressore = g.Key, TotaleVerbali = g.Count() })
            .ToList();

        return View(verbali);
    }

    [HttpGet]
    public IActionResult PuntiDecurtatiPerTrasgressore()
    {
        var punti = _context.Verbali
            .GroupBy(v => v.Idanagrafica)
            .Select(g => new { Trasgressore = g.Key, TotalePunti = g.Sum(v => v.DecurtamentoPunti) })
            .ToList();

        return View(punti);
    }

    [HttpGet]
    public IActionResult ViolazioniPiuDiDieciPunti()
    {
        var violazioni = _context.Verbali
            .Where(v => v.DecurtamentoPunti > 10)
            .Select(v => new { v.DataViolazione, v.NominativoAgente, v.Importo, v.DecurtamentoPunti })
            .ToList();

        return View(violazioni);
    }

    [HttpGet]
    public IActionResult ViolazioniMaggioriDiQuattrocentoEuro()
    {
        var violazioni = _context.Verbali
          .Where(v => v.Importo >= 400)
          .Join(
              _context.Anagrafiche,
              v => v.Idanagrafica,  
              a => a.Idanagrafica, 
              (v, a) => new
              {
                  a.Cognome,
                  a.Nome,
                  a.Indirizzo,
                  v.DataViolazione,
                  v.Importo,
                  v.DecurtamentoPunti
              }) .ToList();  
        return View(violazioni);
    }
}
