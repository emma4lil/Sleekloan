using LoanApp.Shared.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApp.Application.DTOs
{
    public class LoanApplicationRequest
    {
        public string? ApplicantName { get; set; }
        public int LoanAmount { get; set; }
        public int LoanTerm { get; set; }
    }

    public class LoanApplicationResponse
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public DateTime ApplicationDate { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}
