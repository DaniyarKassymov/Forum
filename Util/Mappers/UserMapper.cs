using Forum.Models;
using Forum.ViewModels.UserVms;

namespace Forum.Util.Mappers;

public static class UserMapper
{
    public static ProfileVm UserProfileVm(User user)
    {
        return new ProfileVm()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Avatar = user.Avatar,
            Themes = user.Themes,
            Answers = user.Answers
        };
    }
}