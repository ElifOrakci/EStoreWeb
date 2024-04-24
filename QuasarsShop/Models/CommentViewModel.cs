using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuasarShop.Models;

public class CommentViewModel
{
    public Guid ProductId { get; set; }
    public int Rating { get; set; }
    public string Text { get; set; }
    public SelectList SelectList { get; set; } = null!;
}
