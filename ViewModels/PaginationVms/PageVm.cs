using Forum.Models;
using Forum.ViewModels.ThemeVms;

namespace Forum.ViewModels.PaginationVms;

public class PageVm
{
    public ForumVm? ForumVm { get; set; }
    public IEnumerable<Answer>? Answers { get; set; }
    public PaginationVm? PaginationVm { get; set; }
}