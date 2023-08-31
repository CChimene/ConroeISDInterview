using ConroeISDInterview.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConroeISDInterview.Pages;

public class DisplayTableModel : PageModel
{
    public DBcntxt Context;

    public List<PayrollRecord> PayrollRecords {get; set;}

    public DisplayTableModel(DBcntxt _context)
    {
        Context = _context;
    }

    public void OnGet()
    {
        PayrollRecords = Context.PayrollRecords.ToList<PayrollRecord>();
    }

    
}