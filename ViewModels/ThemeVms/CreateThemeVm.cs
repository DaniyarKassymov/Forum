using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.ThemeVms;

public class CreateThemeVm
{
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    public required string Title { get; set; }
    [Required(ErrorMessage = "*Поле обязательно к заполнению")]
    public required string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public required string UserId { get; set; }
}