using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoPoliziaMunicipale.Data;
using ProgettoPoliziaMunicipale.Models;
using ProgettoPoliziaMunicipale.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TipoViolazioneController : Controller
{
    private readonly ApplicationDbContext _context;

    public TipoViolazioneController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tipoViolazioni = await _context.TipoViolazioni
            .Select(v => new TipoViolazioneViewModel
            {
                Idviolazione = v.Idviolazione, 
                Descrizione = v.Descrizione
            })
            .ToListAsync();


        return View(tipoViolazioni);
    }
}
