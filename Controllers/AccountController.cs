using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.dal;
using WebApplication2.Models;

public class AccountController : Controller
{
    private readonly LivreDbContext _context;

    public AccountController(LivreDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (ModelState.IsValid)
        {
            if (!(userRegister.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase) ||
                  userRegister.Email.EndsWith("@admin.com", StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(UserRegister.Email), "The email must be either a Gmail or Admin address.");
                return View(userRegister);
            }

            var user = userRegister.ToUser();
            _context.UserRegisters.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login", "Account");
        }

        return View(userRegister);
    }
    public IActionResult Users()
    {
        var users = _context.UserRegisters.ToList();
        return View(users);
    }


    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.UserRegisters
                .SingleOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

            if (user != null)
            {
                if (user.Email.EndsWith("@admin.com", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("css2", "Livre");
                }
                else if (user.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("css", "Livre");
                }

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return View(loginModel);
    }


}