using LoadApp.Data.Entities;
using LoanApp.Application.DTOs;
using LoanApp.Shared.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SleekLoan.Domain.DTOs;
using SleekLoan.Domain.Interfaces;
using SleekLoan.Infrastructure.Data;

namespace LoanApp.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ReadWriteDbContext context;
        private readonly ILogger<LoanApplicationModel> logger;

        public LoanApplicationService(ReadWriteDbContext context, ILogger<LoanApplicationModel> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<Response<bool>> ApproveLoanApplication(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreateLoanApplicationRequest(CreateLoanApplicationRequest request)
        {
            // Other logic can be added here, such as validation or business rules
            var response = new Response<bool>
            {
                IsSuccess = false,
                Message = "Failed to create loan application."
            };

            var loanApplication = new LoanApplication
            {
                ApplicantName = request.ApplicantName ?? string.Empty,
                LoanAmount = request.LoanAmount > 0 ? request.LoanAmount : 20000,
                LoanTerm = request.LoanTerm > 0 ? request.LoanTerm : 30,
                InterestRate = 5.0m, 
                ApplicationDate = DateTime.UtcNow,
                LoanStatus = LoanStatus.Pending
            };

            context.LoanApplications.Add(loanApplication);

            if (await context.SaveChangesAsync() > 0)
            {
                response.IsSuccess = true;
                response.Message = "Loan application created successfully.";
                response.Data = true;
                return await Task.FromResult(response);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteLoanApplicationById(int id)
        {
            var response = new Response<bool>
            {
                IsSuccess = false,
                Message = "Failed to delete loan application."
            };

            var loanApplication = await context.LoanApplications.FindAsync(id);
            if (loanApplication == null)
            {
                return response;
            }

            context.LoanApplications.Remove(loanApplication);
            await context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Loan application deleted successfully.";
            return response;
        }

        public async Task<Response<LoanApplicationModel>> GetLoanApplicationById(int id)
        {
            var response = new Response<LoanApplicationModel>
            {
                IsSuccess = false,
                Message = "Loan application not found."
            };

            var loanApplication = await (from loanApp in context.LoanApplications
                        .AsNoTracking()
                                         where loanApp.Id == id
                                         select new LoanApplicationModel
                                         {
                                             Id = loanApp.Id,
                                             ApplicantName = loanApp.ApplicantName,
                                             LoanAmount = loanApp.LoanAmount,
                                             LoanTerm = loanApp.LoanTerm,
                                             InterestRate = loanApp.InterestRate,
                                             ApplicationDate = loanApp.ApplicationDate,
                                             LoanStatus = loanApp.LoanStatus
                                         })
                       .FirstOrDefaultAsync();
            if (loanApplication == null)
            {
                return response;
            }

            response.IsSuccess = true;
            response.Message = "Loan application retrieved successfully.";
            response.Data = loanApplication;
            return response;
        }

        public async Task<Response<List<LoanApplicationModel>>> GetLoanApplications(int page = 1, int pageSize = 20)
        {
            var response = new Response<List<LoanApplicationModel>>
            {
                IsSuccess = false,
                Message = "Failed to retrieve loan applications."
            };

            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var skip = (page - 1) * pageSize;

            var loanApplications = await context.LoanApplications
                .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance, Todo: create read only context
                .OrderByDescending(l => l.ApplicationDate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var responses = loanApplications.Select(l => new LoanApplicationModel
            {
                Id = l.Id,
                ApplicantName = l.ApplicantName,
                LoanAmount = l.LoanAmount,
                LoanTerm = l.LoanTerm,
                InterestRate = l.InterestRate,
                ApplicationDate = l.ApplicationDate,
                LoanStatus = l.LoanStatus
            }).ToList();

            if (responses.Count == 0)
            {
                response.Message = "No loan applications found.";
                return response;
            }

            response.IsSuccess = true;
            response.Message = "Loan applications retrieved successfully.";
            response.Data = responses;
            response.PageItemCount = responses.Count;
            return response;
        }

        public Task<Response<bool>> RejectLoanApplication(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> UpdateLoanApplication(LoanApplicationModel request)
        {
            var response = new Response<bool>
            {
                IsSuccess = false,
                Message = "Failed to update loan application."
            };

            var loanApplication = await context.LoanApplications.FindAsync(request.Id);
            if (loanApplication == null)
            {
                response.Message = "Loan application not found.";
                return response;
            }

            loanApplication.ApplicantName = request.ApplicantName;
            loanApplication.LoanAmount = request.LoanAmount;
            loanApplication.LoanTerm = request.LoanTerm;
            loanApplication.InterestRate = request.InterestRate;
            loanApplication.LoanStatus = request.LoanStatus;

            context.LoanApplications.Update(loanApplication);
            await context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Loan application updated successfully.";
            response.Data = true;

            return response;
        }
    }
}
