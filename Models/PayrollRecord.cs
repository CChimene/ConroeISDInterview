namespace ConroeISDInterview.Models;

public class PayrollRecord
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public bool PayrollError { get; set; }

    public PayrollRecord(PayrollRecord pr){
        this.EmployeeID = pr.EmployeeID;
        this.FirstName = pr.FirstName;
        this.LastName = pr.LastName;
        this.PayrollError = pr.PayrollError;
    }

    public PayrollRecord(){}
}