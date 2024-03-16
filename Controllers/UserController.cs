using System.ComponentModel.DataAnnotations;
using Forum.database;
using Forum.Models;
using Forum.Util.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ForumDbContext _db;
    
    public UserController(UserManager<User> userManager, ForumDbContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    [HttpGet]
    [Authorize]
    [Display(Name = "Profile")]
    public async Task<IActionResult> ProfileAsync()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _db.Users
            .Include(u => u.Themes)
            .Include(u => u.Answers)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var vm = UserMapper.UserProfileVm(user);

        vm.Themes = vm.Themes.OrderByDescending(t => t.CreationDate).ToList();

        return View(vm);
    }
}