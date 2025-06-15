using LoanApp.Application.DTOs;

namespace LoanApp.Shared.common
{
    public interface ILoanApplicationService
    {
        Task<LoanApplicationResponse> CreateLoanApplicationRequest(LoanApplicationRequest request);
        Task<LoanApplicationResponse> GetLoanApplicationById(int id);
        Task<List<LoanApplicationResponse>> GetLoanApplicationsBy(int page = 1, int pageSize = 20);
        Task<LoanApplicationResponse> UpdateLoanApplicationRequest(int id, LoanApplicationRequest request);
        Task<bool> DeleteLoanApplicationRequest(int id);
    }
}
