using Microsoft.AspNetCore.Mvc;
using ProgettoPoliziaMunicipale.Data;

public class StatisticheController : Controller
{
    private readonly ApplicationDbContext _context;

    public StatisticheController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult VerbaliPerTrasgressore()
    {
        var verbali = _context.Verbali
            .GroupBy(v => v.Idanagrafica)
            .Select(g => new { Trasgressore = g.Key, TotaleVerbali = g.Count() })
            .ToList();

        return View(verbali);
    }

    public IActionResult PuntiDecurtatiPerTrasgressore()
    {
        var punti = _context.Verbali
            .GroupBy(v => v.Idanagrafica)
            .Select(g => new { Trasgressore = g.Key, TotalePunti = g.Sum(v => v.DecurtamentoPunti) })
            .ToList();

        return View(punti);
    }

    public IActionResult ViolazioniPiuDiDieciPunti()
    {
        var violazioni = _context.Verbali
            .Where(v => v.DecurtamentoPunti > 10)
            .Select(v => new { v.DataViolazione, v.NominativoAgente, v.Importo, v.DecurtamentoPunti })
            .ToList();

        return View(violazioni);
    }

    public IActionResult ViolazioniMaggioriDiQuattrocentoEuro()
    {
        var violazioni = _context.Verbali
          .Where(v => v.Importo >= 400)
          .Join(
              _context.Anagrafiche,
              v => v.Idanagrafica,  // Colonna in Verbale
              a => a.Idanagrafica,  // Colonna in Anagrafica
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
