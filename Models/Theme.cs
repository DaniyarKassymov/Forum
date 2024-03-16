namespace Forum.Models;

public class Theme
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime CreationDate { get; set; }

    public required string UserId { get; set; }
    public User? User { get; set; }

    public List<Answer> Answers { get; set; } = new();
}