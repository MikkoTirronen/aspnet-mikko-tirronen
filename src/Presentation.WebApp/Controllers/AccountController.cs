
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid email or password.");

        return View(model);

    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterEmailViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        TempData["Email"] = model.Email;
        return RedirectToAction("RegisterPassword");
    }

    [HttpGet]
    public IActionResult RegisterPassword()
    {
        var email = TempData["Email"]?.ToString();

        if (email== null)
            return RedirectToAction(nameof(Register));

        TempData.Keep("Email");

        return View(new RegisterPasswordViewModel{Email = email});
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPassword(RegisterPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = TempData["Email"]?.ToString();

        if (string.IsNullOrEmpty(email))
            return RedirectToAction(nameof(Register));

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        TempData["Email"] = email;

        return View(model);


    }

     [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}