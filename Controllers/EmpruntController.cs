using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.dal;
using WebApplication2.Models;


public class EmpruntController : Controller
{
    private readonly LivreDbContext _context;

    public EmpruntController(LivreDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var emprunts = _context.Emprunts.ToList();
        return View(emprunts);
    }

    

    public IActionResult EmpruntDetails(int id)
    {
        var emprunt = _context.Emprunts.FirstOrDefault(e => e.Id == id);

        if (emprunt == null)
        {
            return NotFound();
        }

        return View(emprunt);
    }

    

[HttpGet]
public IActionResult DeleteEmprunt(int empruntId)
{
    var emprunt = _context.Emprunts.Find(empruntId);

    if (emprunt == null)
    {
        return NotFound();
    }

    return View(emprunt);
}


[HttpPost, ActionName("DeleteEmpruntConfirmed")]
public IActionResult DeleteEmpruntConfirmed(int empruntId)
{
    var emprunt = _context.Emprunts.Find(empruntId);

    if (emprunt != null)
    {
        var livre = _context.Livres.Find(emprunt.LivreId);

        if (livre != null)
        {
            livre.EstEmprunte = false;
        }

        _context.Emprunts.Remove(emprunt);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    else
    {
        ModelState.AddModelError("", "Emprunt not found.");
    }

    return View("DeleteEmprunt");
}


[HttpGet("EditEmprunt/{empruntId}")]
public IActionResult EditEmprunt(int empruntId)
{
    var emprunt = _context.Emprunts.Find(empruntId);

    if (emprunt == null)
    {
        return NotFound();
    }

    return View(emprunt);
}
[HttpPost("EditEmprunt/{empruntId}")]
public IActionResult EditEmprunt(int empruntId, [Bind("Id, AbonneId, DateEmprunt, DateRetour, LivreId")] Emprunt emprunt)
{
    if (empruntId != emprunt.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        if (_context.Livres.Any(l => l.Id == emprunt.LivreId))
        {
            try
            {
                _context.Update(emprunt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpruntExists(emprunt.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            ModelState.AddModelError("LivreId", "Invalid LivreId");
        }
    }

    return View(emprunt);
}


private bool EmpruntExists(int id)
{
    return _context.Emprunts.Any(e => e.Id == id);
}

}

