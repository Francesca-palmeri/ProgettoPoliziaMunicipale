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
        // Recupera tutti i tipi di violazione dal database
        var tipiViolazione = await _context.TipoViolazioni.ToListAsync();

        // Passa la lista dei tipi di violazione alla vista tramite ViewBag
        ViewBag.TipiViolazione = GetTipiViolazione(tipiViolazione, "Idviolazione", "Descrizione");

        return View(new VerbaleViewModel());
    }

    private SelectList GetTipiViolazione(List<TipoViolazione> tipiViolazione, string v1, string v2)
    {
        // Crea una SelectList con i dati dei tipi di violazione
        return new SelectList(tipiViolazione, v1, v2);
    }


    // POST: Verbale/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VerbaleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            // Aggiungi il codice per loggare o visualizzare gli errori del ModelState
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Usa un metodo di logging o di debug appropriato
            }

           

            // In caso di errore, torna al form con i dati già inseriti
            return View(viewModel);
        }
        else {        
            // Creazione nuova anagrafica
            var anagrafica = new Anagrafica
            {
                Cognome = viewModel.Cognome,
                Nome = viewModel.Nome,
                Indirizzo = viewModel.Indirizzo,
                Città = viewModel.Città,
                Cap = viewModel.Cap,
                CodFisc = viewModel.CodFisc
            };

            // Aggiunta della nuova anagrafica al database
            _context.Add(anagrafica);
            await _context.SaveChangesAsync();

            // Creazione del verbale
            var verbale = new Verbale
            {
                DataViolazione = viewModel.DataViolazione,
                IndirizzoViolazione = viewModel.IndirizzoViolazione,
                NominativoAgente = viewModel.NominativoAgente,
                DataTrascrizioneVerbale = viewModel.DataTrascrizioneVerbale,
                Importo = viewModel.Importo,
                DecurtamentoPunti = viewModel.DecurtamentoPunti,
                Idanagrafica = anagrafica.Idanagrafica, // Associa l'anagrafica appena creata
                Idviolazione = viewModel.IdViolazione // Associa la violazione selezionata
            };

            // Aggiunta del verbale al database
            _context.Add(verbale);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirigi alla lista dei verbali
        }

        // Se il modello non è valido, ripopola ViewBag con i tipi di violazione
        //var tipiViolazione = await _context.TipoViolazioni.ToListAsync();
        //ViewBag.TipiViolazione = new SelectList(tipiViolazione, "Idviolazione", "Descrizione");

        // In caso di errore, torna al form con i dati già inseriti
        //return View(viewModel);
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
