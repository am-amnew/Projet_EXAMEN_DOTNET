using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.dal;
using WebApplication2.Models;

public class LivreController : Controller
{
    private readonly LivreDbContext _context;
    private int abonneId;

    public LivreController(LivreDbContext context)
    {
        _context = context;
    }



    public IActionResult css()
    {

        return View("css");
    }

    public IActionResult css2()
    {

        return View("css2");
    }


    public IActionResult Index()
    {
        var livres = _context.Livres.Include(l => l.Emprunts).ToList();
        return View(livres);
    }

    public IActionResult Index2()
    {
        var abonnes = _context.Abonnes.ToList();
        return View(abonnes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Livre livre)
    {
        if (ModelState.IsValid)
        {
            _context.Livres.Add(livre);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        return View(livre);
    }

    [HttpGet]
    public IActionResult Emprunter(int livreId)
    {
        ViewBag.LivreId = livreId;
        return View();
    }

    [HttpPost]
    public IActionResult EmprunterConfirmed(int livreId, [Bind("AbonneId, DateEmprunt, DateRetour")] Emprunt emprunt)
    {
        if (ModelState.IsValid)
        {

            var activeLoansCount = _context.Emprunts.Count(e => e.AbonneId == emprunt.AbonneId);

            if (activeLoansCount >= 2)
            {
                ModelState.AddModelError("", "L'abonné ne peut emprunter plus de 2 livres à la fois.");
                return View("Emprunter", emprunt);
            }

            var livre = _context.Livres.Find(livreId);

            if (livre != null && !livre.EstEmprunte)
            {
                if ((emprunt.DateRetour - emprunt.DateEmprunt).TotalDays > 14)
                {
                    ModelState.AddModelError("", "La durée de l'emprunt ne doit pas dépasser 2 semaines.");
                    return View("Emprunter", emprunt);
                }

                livre.EstEmprunte = true;
                emprunt.LivreId = livre.Id;

                _context.Emprunts.Add(emprunt);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Le livre est déjà emprunté ou n'est pas disponible.");
            }
        }

        return View("Emprunter", emprunt);
    }


    [HttpGet]
    public IActionResult Restituer(int livreId)
    {
        ViewBag.LivreId = livreId;
        return View();
    }
    [HttpPost]
    public IActionResult RestituerConfirmed(int livreId)
    {
        var livre = _context.Livres.Find(livreId);

        if (livre != null && livre.EstEmprunte)
        {
            var emprunt = _context.Emprunts.FirstOrDefault(e => e.LivreId == livre.Id);

            if (emprunt != null)
            {

                _context.Emprunts.Remove(emprunt);


                livre.EstEmprunte = false;


                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Aucun emprunt en cours pour ce livre.");
            }
        }
        else
        {
            ModelState.AddModelError("", "Le livre n'est pas emprunté ou n'est pas valide.");
        }

        return View("Restituer");
    }



}
