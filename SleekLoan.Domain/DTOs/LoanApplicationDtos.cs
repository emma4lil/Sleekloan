using LoanApp.Shared.enums;
using System.ComponentModel.DataAnnotations;

namespace LoanApp.Application.DTOs
{
    public class LoanApplicationRequest
    {
        [Required(ErrorMessage = "Applicant name is required")]
        public string? ApplicantName { get; set; }
        [Range(1, 1000000)]
        public int LoanAmount { get; set; } = 20;
        [Range(1, 360)]
        public int LoanTerm { get; set; } = 1;
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
