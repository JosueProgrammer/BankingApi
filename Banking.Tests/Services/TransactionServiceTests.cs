using Banking.Application.DTOs.Transaction;
using Banking.Application.Services;
using Banking.Domain.Entities;
using Banking.Domain.Enums;
using Banking.Domain.Exceptions;
using Banking.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Banking.Tests.Services;

public class TransactionServiceTests
{
    private readonly Mock<IBankAccountRepository> _accountRepositoryMock;
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly TransactionService _sut;

    public TransactionServiceTests()
    {
        _accountRepositoryMock = new Mock<IBankAccountRepository>();
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _sut = new TransactionService(
            _accountRepositoryMock.Object,
            _transactionRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Deposit_ShouldIncreaseAccountBalance()
    {
        // Arrange
        var accountNumber = "ACC-20260715-1234";
        var idempotencyKey = Guid.NewGuid().ToString();
        var initialBalance = 1000m;
        var depositAmount = 500m;
        
        var account = new BankAccount(Guid.NewGuid(), accountNumber, initialBalance);
        
        var depositDto = new DepositDto { Amount = depositAmount };

        _transactionRepositoryMock.Setup(x => x.GetByIdempotencyKeyAsync(idempotencyKey))
            .ReturnsAsync((Transaction?)null);

        _accountRepositoryMock.Setup(x => x.GetByAccountNumberAsync(accountNumber))
            .ReturnsAsync(account);

        _transactionRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Transaction>()))
            .ReturnsAsync((Transaction t) => t);

        // Act
        var result = await _sut.DepositAsync(accountNumber, idempotencyKey, depositDto);

        // Assert
        account.Balance.Should().Be(1500m);
        
        result.Should().NotBeNull();
        result.Type.Should().Be(TransactionType.Deposit.ToString());
        result.Amount.Should().Be(depositAmount);
        result.BalanceAfterTransaction.Should().Be(1500m);

        _transactionRepositoryMock.Verify(x => x.CreateAsync(It.Is<Transaction>(t => 
            t.Type == TransactionType.Deposit && 
            t.Amount == depositAmount && 
            t.BalanceAfterTransaction == 1500m)), Times.Once);
            
        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Withdraw_ShouldDecreaseAccountBalance_WhenEnoughFunds()
    {
        // Arrange
        var accountNumber = "ACC-20260715-1234";
        var idempotencyKey = Guid.NewGuid().ToString();
        var initialBalance = 1000m;
        var withdrawAmount = 300m;
        
        var account = new BankAccount(Guid.NewGuid(), accountNumber, initialBalance);
        
        var withdrawDto = new WithdrawDto { Amount = withdrawAmount };

        _transactionRepositoryMock.Setup(x => x.GetByIdempotencyKeyAsync(idempotencyKey))
            .ReturnsAsync((Transaction?)null);

        _accountRepositoryMock.Setup(x => x.GetByAccountNumberAsync(accountNumber))
            .ReturnsAsync(account);

        _transactionRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Transaction>()))
            .ReturnsAsync((Transaction t) => t);

        // Act
        var result = await _sut.WithdrawAsync(accountNumber, idempotencyKey, withdrawDto);

        // Assert
        account.Balance.Should().Be(700m);
        
        result.Should().NotBeNull();
        result.Type.Should().Be(TransactionType.Withdrawal.ToString());
        result.Amount.Should().Be(withdrawAmount);
        result.BalanceAfterTransaction.Should().Be(700m);

        _transactionRepositoryMock.Verify(x => x.CreateAsync(It.Is<Transaction>(t => 
            t.Type == TransactionType.Withdrawal && 
            t.Amount == withdrawAmount && 
            t.BalanceAfterTransaction == 700m)), Times.Once);
            
        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Withdraw_ShouldThrowBusinessException_WhenInsufficientFunds()
    {
        // Arrange
        var accountNumber = "ACC-20260715-1234";
        var idempotencyKey = Guid.NewGuid().ToString();
        var initialBalance = 500m;
        var withdrawAmount = 1000m;
        
        var account = new BankAccount(Guid.NewGuid(), accountNumber, initialBalance);
        
        var withdrawDto = new WithdrawDto { Amount = withdrawAmount };

        _transactionRepositoryMock.Setup(x => x.GetByIdempotencyKeyAsync(idempotencyKey))
            .ReturnsAsync((Transaction?)null);

        _accountRepositoryMock.Setup(x => x.GetByAccountNumberAsync(accountNumber))
            .ReturnsAsync(account);

        // Act
        Func<Task> act = async () => await _sut.WithdrawAsync(accountNumber, idempotencyKey, withdrawDto);

        // Assert
        await act.Should().ThrowAsync<InsufficientFundsException>();

        _transactionRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Transaction>()), Times.Never);
            
        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(x => x.RollbackAsync(), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
    }
}
