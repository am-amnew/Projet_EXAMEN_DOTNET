using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.dal;
using WebApplication2.Models;

public class AbonneController : Controller
{
    private readonly LivreDbContext _context;

    public AbonneController(LivreDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var abonnes = _context.Abonnes.ToList();
        return View(abonnes);
    }
    public IActionResult LivresEmpruntes(int abonneId)
    {
        var abonne = _context.Abonnes
            .Include(a => a.Emprunts)
            .ThenInclude(e => e.Livre)
            .FirstOrDefault(a => a.Id == abonneId);

        return View(abonne);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Abonne abonne)
    {
        if (ModelState.IsValid)
        {
            _context.Abonnes.Add(abonne);
            await _context.SaveChangesAsync();
            return RedirectToAction("redirect");
        }

        return View(abonne);
    }
    public IActionResult redirect()
    {

        return View("redirect");
    }





    public IActionResult Edit(int id)
    {
        var abonne = _context.Abonnes.Find(id);

        if (abonne == null)
        {
            return NotFound();
        }

        return View(abonne);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Abonne abonne)
    {
        if (id != abonne.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(abonne);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonneExists(abonne.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        return View(abonne);
    }

    public IActionResult Delete(int id)
    {
        var abonne = _context.Abonnes.Find(id);

        if (abonne == null)
        {
            return NotFound();
        }

        return View(abonne);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var abonne = await _context.Abonnes.FindAsync(id);

        if (abonne == null)
        {
            return NotFound();
        }

        _context.Abonnes.Remove(abonne);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private bool AbonneExists(int id)
    {
        return _context.Abonnes.Any(e => e.Id == id);
    }

}
