using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConroeISDInterview.Pages;

public class DisplayTableModel : PageModel
{
    private readonly ILogger<DisplayTableModel> _logger;

    public DisplayTableModel(ILogger<DisplayTableModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}