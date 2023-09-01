using ConroeISDInterview.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text.Json;
using System.Web;

namespace ConroeISDInterview.Pages;

public class DisplayDataTableModel : PageModel
{

    public DataTable outputTable = new DataTable();
    public string Message = "";
    public string[] entries;
    public List<PayrollRecord> formatted;
    public bool idSort = true;
    public bool firstNameSort = true;
    public bool lastNameSort = true;
    public bool payrollErrorSort = true;
    private bool IsSortedAscending;
    private string CurrentSortColumn;


    public DisplayDataTableModel()
    {
    }

    public void OnGet()
    {
        try{
            string data = HttpContext.Session.GetString("data");
            entries = data.Split("\n");
            formatted = this.GetAllRecords();
        }
        catch{
            Message = "An error has occured while displaying the data";
        }
        
    }

    private List<PayrollRecord> GetAllRecords(){
        List<PayrollRecord> payrollRecords = new List<PayrollRecord>();
        for(int i = 0; i < entries.Length - 1;i++){
            string[] entry = entries[i].Split(",");
            payrollRecords.Add(new PayrollRecord{
                EmployeeID=Convert.ToInt32(entry[0]),
                FirstName=entry[1],
                LastName=entry[2],
                PayrollError=Convert.ToBoolean(entry[3])
            });
        }
        return payrollRecords;
    }

    public void SortById(){
        formatted = formatted.OrderBy(x => x.EmployeeID).ToList();
        idSort = !idSort;
        Message = "SORT";
    }

    public void SortByFirstName(){
        formatted.Sort((x,y) => x.FirstName.CompareTo(y.FirstName));
        if(!firstNameSort){
            formatted.Reverse();
        }
        firstNameSort = !firstNameSort;
    }

    public void SortByLastName(){
        formatted.Sort((x,y) => x.LastName.CompareTo(y.LastName));
        if(!lastNameSort){
            formatted.Reverse();
        }
        lastNameSort = !lastNameSort;
    }
    
    public void SortByPayrollError(){
        formatted.Sort((x,y) => x.PayrollError.CompareTo(y.PayrollError));
        if(!payrollErrorSort){
            formatted.Reverse();
        }
        payrollErrorSort = !payrollErrorSort;
    }

    public string GetSortStyle(string columnName)
    {
        if (CurrentSortColumn != columnName)
        {
            return string.Empty;
        }
        if (IsSortedAscending)
        {
            return "▲";
        }
        else
        {
            return "▼";
        }
    }

    public void SortTable(string columnName)
    {
        Console.WriteLine(columnName);
        if (columnName != CurrentSortColumn)
        {
            //We need to force order by descending on the new column
            formatted = formatted.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
            CurrentSortColumn = columnName;
            IsSortedAscending = true;

        }
        else //Sorting against same column but in different direction
        {
            if (IsSortedAscending)
            {
                formatted = formatted.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
            }
            else
            {
                formatted= formatted.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
            }

            IsSortedAscending = !IsSortedAscending;
        }
    }
}