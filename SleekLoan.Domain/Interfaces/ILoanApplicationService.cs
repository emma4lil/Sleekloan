using LoanApp.Application.DTOs;
using SleekLoan.Domain.DTOs;

namespace SleekLoan.Domain.Interfaces
{
    public interface ILoanApplicationService
    {
        Task<Response<bool>> CreateLoanApplicationRequest(CreateLoanApplicationRequest request);
        Task<Response<bool>> DeleteLoanApplicationById(int id);
        Task<Response<LoanApplicationModel>> GetLoanApplicationById(int id);
        Task<Response<List<LoanApplicationModel>>> GetLoanApplications(int page = 1, int pageSize = 20);
        Task<Response<bool>> UpdateLoanApplication(LoanApplicationModel request);
        Task<Response<bool>> ApproveLoanApplication(int id);
        Task<Response<bool>> RejectLoanApplication(int id);
    }

}
