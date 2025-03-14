using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ProgettoPoliziaMunicipale.Data;
using ProgettoPoliziaMunicipale.Models;
using ProgettoPoliziaMunicipale.ViewModels;
using Microsoft.EntityFrameworkCore;

public class VerbaleController : Controller
{
    private readonly ApplicationDbContext _context;

    public VerbaleController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var verbali = await _context.Verbali.Include(v => v.IdanagraficaNavigation).Include(v => v.IdviolazioneNavigation).ToListAsync();
        return View(verbali);
    }

    // GET: Verbale/Create
    public IActionResult Create()
    {
        ViewData["Anagraficas"] = new SelectList(_context.Anagrafiche, "Idanagrafica", "Nome");
        ViewData["TipoViolaziones"] = new SelectList(_context.TipoViolazioni, "Idviolazione", "Descrizione");
        return View();
    }

    // POST: Verbale/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VerbaleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var verbale = new Verbale
            {
                DataViolazione = model.DataViolazione,
                IndirizzoViolazione = model.IndirizzoViolazione,
                NominativoAgente = model.NominativoAgente,
                DataTrascrizioneVerbale = model.DataTrascrizioneVerbale,
                Importo = model.Importo,
                DecurtamentoPunti = model.DecurtamentoPunti,
                Idanagrafica = model.Idanagrafica,
                Idviolazione = model.Idviolazione
            };

            _context.Add(verbale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    
}
