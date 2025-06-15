using LoanApp.Shared.enums;
using System.ComponentModel.DataAnnotations;

namespace LoadApp.Data.Entities
{
    public class LoanApplication
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Applicant name is required")]
        public string ApplicantName { get; set; } = string.Empty;

        [Range(1, 1000000)]
        public decimal LoanAmount { get; set; }

        [Range(1, 360)]
        public int LoanTerm { get; set; } // number of days

        [Range(0.01, 100.00)]
        public decimal InterestRate { get; set; }

        public LoanStatus LoanStatus { get; set; } = LoanStatus.Pending;

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

    }
}
