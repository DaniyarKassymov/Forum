using System.ComponentModel.DataAnnotations;
using Forum.database;
using Forum.Models;
using Forum.Util.Mappers;
using Forum.ViewModels.AccountVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ForumDbContext _db;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ForumDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {   
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Display(Name = "Register")]
    public async Task<IActionResult> RegisterAsync(RegisterVm vm)
    {
        if (ModelState.IsValid)
        {
            var user = AccountMapper.RegisterVmUser(vm);
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Profile", "User");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(vm);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        var vm = new LoginVm
        {
            EmailOrUserName = null,
            Password = null
        };

        return View(vm);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Display(Name = "Login")]
    public async Task<IActionResult> LoginAsync(LoginVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var user = await _userManager.FindByEmailAsync(vm.EmailOrUserName) 
                   ?? await _db.Users.FirstOrDefaultAsync(u => u.UserName == vm.EmailOrUserName);

        var result = await _signInManager.PasswordSignInAsync(
            user!,
            vm.Password,
            false,
            false);

        if (result.Succeeded)
            return RedirectToAction("Profile", "User");
            
        ModelState.AddModelError(string.Empty, "Неверно введены данные");

        return View(vm);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Display(Name = "LogOut")]
    public async Task<IActionResult> LogOutAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}