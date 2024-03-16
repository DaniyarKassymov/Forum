using Forum.Models;

namespace Forum.ViewModels.UserVms;

public class ProfileVm
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Avatar { get; set; }

    public List<Theme> Themes { get; set; } = new();
    public List<Answer> Answers { get; set; } = new();
}