using Microsoft.AspNetCore.Identity;

namespace Forum.Models;

public class User : IdentityUser
{
    public required string Avatar { get; set; }

    public List<Theme> Themes { get; set; } = new();
    public List<Answer> Answers { get; set; } = new();
}