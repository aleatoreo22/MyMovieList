using Microsoft.AspNetCore.Components;

namespace MyMovieList.Controller;

public class CoverModel : ComponentBase
{
    [Parameter] public string? ImageSource { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public int Id { get; set; }
}