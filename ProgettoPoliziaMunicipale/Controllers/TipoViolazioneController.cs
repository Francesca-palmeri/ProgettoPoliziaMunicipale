using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoPoliziaMunicipale.Data;

public class TipoViolazioneController : Controller
{
    private readonly ApplicationDbContext _context;

    public TipoViolazioneController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TipoViolazione
    public async Task<IActionResult> Index()
    {
        var tipoViolazioni = await _context.TipoViolazioni.ToListAsync();
        return View(tipoViolazioni);
    }
}
