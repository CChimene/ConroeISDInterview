using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using System.Text;
using ConroeISDInterview.Models;
using System.Data;
using System.Xml;
using Microsoft.AspNetCore.Session;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ConroeISDInterview.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public string Message {get; set;} = "";
    public readonly long emptyFileSize = 48;

    public readonly int numColumns = 4;

    public DataTable outputTable = new DataTable();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
    /*Event handler for the submit button on the main page, grabs the submitted file and checks for errors before sending it
    as a string to be formatted into a table on DisplayDataTable*/
    public void OnPostUpload(IFormFile fileName){
        try{
            //check if the file has data
            if(fileName.Length <= emptyFileSize){
                Message ="File does not contain any records";
            }
            //ensure that the extension on the file is .csv
            else if(!fileName.ContentType.Equals("text/csv")){
                Message = "File is not in the correct format (.csv)";
            }
            else{
                var output = new StringBuilder();
                string[] row;
                using(var reader = new StreamReader(fileName.OpenReadStream())){
                    row = reader.ReadLine().Split(',');
                    //Read in the first line and verify the file has the correct number of columns
                    if(row.Length != numColumns){
                        Message = "File contains the wrong number of columns";
                    }
                    else{
                       foreach(string header in row){
                            outputTable.Columns.Add(header);
                       }
                       while(!reader.EndOfStream){
                            output.Append(reader.ReadLine()+ "\n");
                       }
                       //Pass the information scraped from the file as a string to a page that will format it into a table to be displayed
                       HttpContext.Session.SetString("data", output.ToString());
                       Response.Redirect("/displayDataTable");
                    }
                }
            }

        }
        catch {
            Message = "There was an error in processing your request";
        }
    }

}
