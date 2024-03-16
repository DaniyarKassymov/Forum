using System.ComponentModel.DataAnnotations;
using Forum.database;
using Forum.Models;
using Forum.Util.Mappers;
using Forum.ViewModels.PaginationVms;
using Forum.ViewModels.ThemeVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers;

public class ThemeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ForumDbContext _db;

    public ThemeController(UserManager<User> userManager, ForumDbContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Forum()
    {
        var themes = _db.Themes
            .Include(t => t.User)
            .ToList();

        var forumVms = themes
            .Select(ThemeMapper.ThemeForumVm)
            .OrderByDescending(t => t.CreationDate);

        return View(forumVms);
    }

    [HttpGet]
    [Authorize]
    public IActionResult CreateTheme()
    {
        var vm = new CreateThemeVm
        {
            Title = null,
            Description = null,
            CreationDate = DateTime.UtcNow,
            UserId = _userManager.GetUserId(User)!
        };
        
        return View(vm);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    [Display(Name = "CreateTheme")]
    public async Task<IActionResult> CreateTheme(CreateThemeVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        
        var post = await _db.Themes.AddAsync(ThemeMapper.CreateThemeVmTheme(vm));
        await _db.SaveChangesAsync();
            
        return RedirectToAction("Forum", "Theme");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Details(long id, int page = 1)
    {
        const int pageSize = 4;
        
        var theme = _db.Themes
            .Include(t => t.User)
            .Include(t => t.Answers)
            .FirstOrDefault(t => t.Id == id);

        _db.Answers
            .Include(a => a.User)
            .Include(a => a.Theme)
            .ToList();   
        
        var count = theme.Answers.Count();
        var items = theme.Answers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        var paginationVm = new PaginationVm(count, page, pageSize);
        
        var vm = new PageVm()
        {
            PaginationVm = paginationVm,
            Answers = items,
            ForumVm = ThemeMapper.ThemeForumVm(theme)
        };

        return View(vm);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> AddAnswer(string answer, long themeId)
    {
        if (answer != null)
        {
            var answerInDb = new Answer
            {
                Text = answer,
                CreationDate = DateTime.UtcNow,
                UserId = _userManager.GetUserId(User),
                ThemeId = themeId
            };

            await _db.Answers.AddAsync(answerInDb);
            await _db.SaveChangesAsync();

            var lastAnswer = await _db.Answers
                .Include(a => a.User)
                .Include(a => a.Theme)
                .OrderBy(a => a.Id).LastOrDefaultAsync();
            

            var vm = AnswersMapper.AnswerThemeAnswerVm(lastAnswer);

            return PartialView(vm);
        }

        return NotFound();
    }
}