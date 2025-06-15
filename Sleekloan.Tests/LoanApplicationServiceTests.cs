using LoadApp.Data.Entities;
using LoanApp.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SleekLoan.Application.Services;
using SleekLoan.Infrastructure.Data;

namespace LoanApp.Tests.Services
{
    public class LoanApplicationServiceTests
    {
        private readonly Mock<ReadWriteDbContext> _mockContext;
        private readonly Mock<ILogger<LoanApplicationService>> _mockLogger;
        private readonly LoanApplicationService _service;

        public LoanApplicationServiceTests()
        {
            _mockContext = new Mock<ReadWriteDbContext>(new DbContextOptions<ReadWriteDbContext>());
            _mockLogger = new Mock<ILogger<LoanApplicationService>>();
            _service = new LoanApplicationService(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateLoanApplicationRequest_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var request = new CreateLoanApplicationRequest
            {
                ApplicantName = "John Doe",
                LoanAmount = 50000,
                LoanTerm = 12
            };

            var mockSet = new Mock<DbSet<LoanApplication>>();
            _mockContext.Setup(c => c.LoanApplications).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.CreateLoanApplicationRequest(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Loan application created successfully.", result.Message);
            mockSet.Verify(m => m.Add(It.IsAny<LoanApplication>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }
    }
}