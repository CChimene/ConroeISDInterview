using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using ConroeISDInterview.Models;

namespace ConroeISDInterview.Controllers{
    
    public class PayrollController : Controller{
        private string[] entries;
        private List<PayrollRecord> formatted;
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page){
            string data = HttpContext.Session.GetString("data");
            entries = data.Split("\n");
            formatted = this.GetAllRecords();
            return View(formatted);
        }

        private List<PayrollRecord> GetAllRecords(){
        List<PayrollRecord> payrollRecords = new List<PayrollRecord>();
        for(int i = 0; i < entries.Length - 1;i++){
            string[] entry = entries[i].Split(",");
            /*payrollRecords.Add(new PayrollRecord{
                EmployeeID=Convert.ToInt32(entry[0]),
                FirstName=entry[1],
                LastName=entry[2],
                PayrollError=Convert.ToBoolean(entry[3])
            });*/
        }
        return payrollRecords;
        }
    }
}
