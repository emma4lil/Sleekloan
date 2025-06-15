using LoanApp.Shared.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekLoan.Domain.DTOs
{
    public class UpdateLoanApplication
    {
        public object?[]? Id { get; set; }
        public string ApplicantName { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanTerm { get; set; }
        public decimal InterestRate { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}
