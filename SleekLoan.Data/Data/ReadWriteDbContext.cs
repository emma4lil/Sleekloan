using LoadApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekLoan.Infrastructure.Data
{
    public class ReadWriteDbContext: DbContext
    {
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public ReadWriteDbContext(DbContextOptions<ReadWriteDbContext> options) : base(options)
        {
        }
    }
}
