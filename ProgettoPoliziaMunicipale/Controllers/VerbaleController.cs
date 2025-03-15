using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ProgettoPoliziaMunicipale.Data;
using ProgettoPoliziaMunicipale.Models;
using ProgettoPoliziaMunicipale.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class VerbaleController : Controller
{
    private readonly ApplicationDbContext _context;

    public VerbaleController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Azione per visualizzare l'elenco dei verbali
    public async Task<IActionResult> Index()
    {
        var verbali = await _context.Verbali
            .Include(v => v.IdanagraficaNavigation) // Include la navigazione per l'anagrafica
            .Include(v => v.IdviolazioneNavigation) // Include la navigazione per la violazione
            .ToListAsync();

        return View(verbali);
    }

    // Azione per la creazione di un nuovo verbale
    public async Task<IActionResult> Create()
    {
        // Recupera tutte le anagrafiche e i tipi di violazione dal database
        var tipiViolazione = await _context.TipoViolazioni.ToListAsync();

        var anagrafiche = await _context.Anagrafiche
            .Select(a => new
            {
                a.Idanagrafica,
                NomeCompleto = a.Cognome + " " + a.Nome + " " + a.Indirizzo
            }).ToListAsync();

        var anagraficheSelectList = new SelectList(anagrafiche, "Idanagrafica", "NomeCompleto");

        // Crea la SelectList per i tipi di violazione
        var tipiViolazioneSelectList = new SelectList(tipiViolazione, "Idviolazione", "Descrizione");

        // Passa i dati alla vista tramite ViewBag o ViewModel
        var viewModel = new VerbaleViewModel
        {
            Anagrafiche = anagraficheSelectList,
            TipiViolazione = tipiViolazioneSelectList, // Passa la lista dei tipi di violazione
            IdViolazione = 0 // Imposta un valore di default per la violazione
        };

        return View(viewModel);
    }

    // POST: Verbale/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VerbaleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            // Mostra gli errori di validazione nel console
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            // Ritorna alla vista con gli errori
            return View(viewModel);
        }

        // Usa l'anagrafica selezionata dal form
        var verbale = new Verbale
        {
            DataViolazione = viewModel.DataViolazione,
            IndirizzoViolazione = viewModel.IndirizzoViolazione,
            NominativoAgente = viewModel.NominativoAgente,
            DataTrascrizioneVerbale = viewModel.DataTrascrizioneVerbale,
            Importo = viewModel.Importo,
            DecurtamentoPunti = viewModel.DecurtamentoPunti,
            Idanagrafica = viewModel.IdAnagrafica, // Associa l'anagrafica selezionata
            Idviolazione = viewModel.IdViolazione, // Associa la violazione selezionata
        };

        _context.Add(verbale);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // Azione per visualizzare i dettagli del verbale e la possibilità di contestarlo
    [HttpGet]
    public IActionResult Contestazione(int id)
    {
        var verbale = _context.Verbali
            .Where(v => v.Idverbale == id)
            .Select(v => new ContestazioneViewModel
            {
                IdVerbale = v.Idverbale,
                Trasgressore = v.IdanagraficaNavigation.Cognome + " " + v.IdanagraficaNavigation.Nome,
                Violazione = v.IdviolazioneNavigation.Descrizione,
                DataViolazione = v.DataViolazione,
                Importo = v.Importo,
                DecurtamentoPunti = v.DecurtamentoPunti
            })
            .FirstOrDefault();

        if (verbale == null)
        {
            return NotFound(); // Se il verbale non è trovato, restituisci un errore
        }

        return View(verbale);
    }

    // POST: Salva la contestazione
    [HttpPost]
    public IActionResult SalvaContestazione(ContestazioneViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Qui puoi implementare la logica per salvare la contestazione nel database
            // ad esempio in una tabella "Contestazioni", se necessario.

            TempData["Successo"] = "Contestazione inviata con successo!";
            return RedirectToAction("Index");
        }

        return View("Contestazione", model);
    }
}
