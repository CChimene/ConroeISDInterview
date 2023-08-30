using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using System.Text;

namespace ConroeISDInterview.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public string Message {get; set;} = "Initial Value";
    public readonly long emptyFileSize = 48;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public void OnPostUpload(IFormFile fileName){
        try{
            if(fileName.Length <= emptyFileSize){
                Message ="File does not contain any records";
            }
            else if(!fileName.ContentType.Equals("text/csv")){
                Message = "File is not in the correct format (.csv)";
            }
            else{
                var output = new StringBuilder();
                string[] headers;
                using(var reader = new StreamReader(fileName.OpenReadStream())){
                    headers = reader.ReadLine().Split(',');
                    if(headers.Length != 4){
                        Message = "File does not contain the correct number of columns";
                    }
                    else{
                        while(!reader.EndOfStream){
                        output.Append(reader.ReadLine());
                        }
                        Message = output.ToString();
                    }
                    
                }
            }

        }
        catch {

        }
    }

}
