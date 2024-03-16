using Forum.Models;
using Forum.ViewModels.ThemeVms;

namespace Forum.Util.Mappers;

public static class ThemeMapper
{
    public static ForumVm ThemeForumVm(Theme vm)
    {
        return new ForumVm()
        {
            Id = vm.Id,
            Title = vm.Title,
            Description = vm.Description,
            CreationDate = vm.CreationDate,
            UserId = vm.UserId,
            User = vm.User,
            Answers = vm.Answers
        };
    }

    public static Theme CreateThemeVmTheme(CreateThemeVm vm)
    {
        return new Theme()
        {
            Title = vm.Title,
            Description = vm.Description,
            CreationDate = vm.CreationDate,
            UserId = vm.UserId
        };
    }
}