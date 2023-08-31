using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using System.Text;
using ConroeISDInterview.Models;
using System.Data;
using System.Xml;

namespace ConroeISDInterview.Pages;

public class IndexModel : PageModel
{
    public string Message {get; set;} = "Initial Value";
    public readonly long emptyFileSize = 48;

    public readonly int numColumns = 4;

    public DataTable outputTable = new DataTable();

    public DBcntxt Context;

    public IndexModel(DBcntxt _context)
    {
        Context = _context;
    }

    public void OnGet()
    {

    }

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
                        Message = row[0];
                    }
                    else{
                       while(!reader.EndOfStream){
                            row = reader.ReadLine().Split(',');
                            Context.Add(new PayrollRecord{
                                EmployeeID = Int32.Parse(row[0]),
                                FirstName = row[1],
                                LastName = row[2],
                                PayrollError = Convert.ToBoolean(row[3])
                            });
                       } 
                       Context.SaveChanges();
                       Redirect("/DisplayTable");
                    }
                }
            }

        }
        catch {
            Message = "There was an error in processing your request";
        }
    }

}
