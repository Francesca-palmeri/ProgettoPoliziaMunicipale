using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoPoliziaMunicipale.Data;
using ProgettoPoliziaMunicipale.Models;
using ProgettoPoliziaMunicipale.ViewModels;

public class AnagraficaController : Controller
{
    private readonly ApplicationDbContext _context;

    public AnagraficaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Anagrafica
    public async Task<IActionResult> Index()
    {
        var anagraficas = await _context.Anagrafiche.ToListAsync();
        return View(anagraficas);
    }

    // GET: Anagrafica/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Anagrafica/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AnagraficaViewModel model)
    {
        if (ModelState.IsValid)
        {
            var anagrafica = new Anagrafica
            {
                Cognome = model.Cognome,
                Nome = model.Nome,
                Indirizzo = model.Indirizzo,
                Città = model.Città,
                Cap = model.Cap,
                CodFisc = model.CodFisc
            };

            _context.Add(anagrafica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
