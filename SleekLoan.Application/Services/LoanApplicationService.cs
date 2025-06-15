using LoadApp.Data.Entities;
using LoanApp.Application.DTOs;
using LoanApp.Shared.enums;
using Microsoft.EntityFrameworkCore;
using SleekLoan.Domain.Interfaces;
using SleekLoan.Infrastructure.Data;

namespace LoanApp.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ReadWriteDbContext context;
        public LoanApplicationService(ReadWriteDbContext context)
        {
            this.context = context;
        }
        public async Task<LoanApplicationResponse> CreateLoanApplicationRequest(LoanApplicationRequest request)
        {
            // Other logic can be added here, such as validation or business rules

            var loanApplication = new LoanApplication
            {
                ApplicantName = request.ApplicantName ?? string.Empty,
                LoanAmount = request.LoanAmount > 0 ? request.LoanAmount : 20000,
                LoanTerm = request.LoanTerm > 0 ? request.LoanTerm : 30,
                InterestRate = 5.0m, // We can call a method to calculate this based on the loan amount and term or other conditions
                ApplicationDate = DateTime.UtcNow,
                LoanStatus = LoanStatus.Pending
            };

            context.LoanApplications.Add(loanApplication);

            if (await context.SaveChangesAsync() > 0)
            {
                return await Task.FromResult(new LoanApplicationResponse
                {
                    Id = loanApplication.Id,
                });
            }

            return await Task.FromResult<LoanApplicationResponse>(null);
        }

        public Task<bool> DeleteLoanApplicationById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLoanApplicationRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LoanApplicationResponse> GetLoanApplicationById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoanApplicationResponse>> GetLoanApplicationsBy(int page = 1, int pageSize = 20)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            var skip = (page - 1) * pageSize;

            var loanApplications = await context.LoanApplications
                .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance, Todo: create read only context
                .OrderByDescending(l => l.ApplicationDate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var responses = loanApplications.Select(l => new LoanApplicationResponse
            {
                Id = l.Id,
                ApplicantName = l.ApplicantName,
                LoanAmount = l.LoanAmount,
                LoanTerm = l.LoanTerm,
                InterestRate = l.InterestRate,
                ApplicationDate = l.ApplicationDate,
                LoanStatus = l.LoanStatus
            }).ToList();

            return responses;
        }

        public Task<LoanApplicationResponse> UpdateLoanApplicationRequest(int id, LoanApplicationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
