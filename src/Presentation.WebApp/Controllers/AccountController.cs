
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        if (email == null)
            return RedirectToAction(nameof(Register));

        TempData.Keep("Email");

        return View(new RegisterPasswordViewModel { Email = email });
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
            await _userManager.AddToRoleAsync(user, "Member");

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

    [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult ExternalLogin(string provider)
{
    var redirectUrl = Url.Action(
        nameof(ExternalLoginCallback),
        "Account");

    var properties = _signInManager.ConfigureExternalAuthenticationProperties(
        provider,
        redirectUrl);

    return Challenge(properties, provider);
}

[HttpGet]
public async Task<IActionResult> ExternalLoginCallback()
{
    var info = await _signInManager.GetExternalLoginInfoAsync();

    if (info is null)
    {
        TempData["Error"] = "External login failed.";
        return RedirectToAction(nameof(Login));
    }

    var signInResult = await _signInManager.ExternalLoginSignInAsync(
        info.LoginProvider,
        info.ProviderKey,
        isPersistent: false,
        bypassTwoFactor: true);

    if (signInResult.Succeeded)
        return RedirectToAction("Index", "Home");

    var email = info.Principal.FindFirstValue(ClaimTypes.Email);

    if (string.IsNullOrWhiteSpace(email))
    {
        TempData["Error"] = "GitHub did not provide an email address.";
        return RedirectToAction(nameof(Login));
    }

    var user = await _userManager.FindByEmailAsync(email);

    if (user is null)
    {
        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = "",
            LastName = ""
        };

        var createResult = await _userManager.CreateAsync(user);

        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
                ModelState.AddModelError("", error.Description);

            return View(nameof(Login), new LoginViewModel());
        }

        await _userManager.AddToRoleAsync(user, "Member");
    }

    var addLoginResult = await _userManager.AddLoginAsync(user, info);

    if (!addLoginResult.Succeeded)
    {
        TempData["Error"] = "Could not link GitHub login.";
        return RedirectToAction(nameof(Login));
    }

    await _signInManager.SignInAsync(user, isPersistent: false);

    return RedirectToAction("Index", "Home");
}
}