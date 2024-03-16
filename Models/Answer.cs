namespace Forum.Models;

public class Answer
{
    public long Id { get; set; }
    public required string Text { get; set; }
    public required DateTime CreationDate { get; set; }

    public required string UserId { get; set; }
    public User? User { get; set; }

    public required long ThemeId { get; set; }
    public Theme? Theme { get; set; }
}