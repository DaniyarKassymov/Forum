using Forum.Models;
using Forum.ViewModels.AnswerVms;

namespace Forum.Util.Mappers;

public static class AnswersMapper
{
    public static ThemeAnswerVm AnswerThemeAnswerVm(Answer answer)
    {
        return new ThemeAnswerVm
        {
            Id = answer.Id,
            Text = answer.Text,
            CreationDate = answer.CreationDate,
            UserId = answer.UserId,
            ThemeId = answer.ThemeId,
            User = answer.User,
            Theme = answer.Theme
        };

    }
}