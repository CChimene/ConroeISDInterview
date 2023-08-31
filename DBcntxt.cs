using ConroeISDInterview.Models;
using Microsoft.EntityFrameworkCore;

namespace ConroeISDInterview
{
    public class DBcntxt : DbContext {
        public DBcntxt(DbContextOptions<DBcntxt> options) : base(options)
        {
           
        }
       
        public DbSet<PayrollRecord> PayrollRecords { get; set; }
    }

}
