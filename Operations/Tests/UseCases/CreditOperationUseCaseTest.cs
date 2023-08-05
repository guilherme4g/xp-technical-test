using Application.UseCases;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Tests.Utils.Factories;
using Xunit;

namespace Tests.UseCases
{
    public class CreditOperationUseCaseTest
    {
        [Fact(DisplayName = "Should fail when operation already exists")]
        public async Task ShouldFailWhenOperationAlreadyExists()
        {
            var fakeOperation = OperationFactory.GenerateOperation();

            var operationRepositoryMock = new Mock<IOperationRepository>();
            operationRepositoryMock.Setup(repository => repository.GetById(fakeOperation.Id)).ReturnsAsync(fakeOperation);

            var sut = new CreditOperationUseCase(new Mock<IWalletRepository>().Object, operationRepositoryMock.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Execute(fakeOperation));

            operationRepositoryMock.Verify(repository => repository.GetById(fakeOperation.Id), Times.Once);
        }

        [Fact(DisplayName = "Should fail when wallet not found")]
        public async Task ShouldFailWhenWalletNotFound()
        {
            var fakeOperation = OperationFactory.GenerateOperation();

            var walletRepositoryMock = new Mock<IWalletRepository>();
            var operationRepositoryMock = new Mock<IOperationRepository>();

            operationRepositoryMock.Setup(repository => repository.GetById(fakeOperation.Id)).ReturnsAsync(null as Operation);
            walletRepositoryMock.Setup(repository => repository.GetByUserAndCurrency(fakeOperation.Owner.User, fakeOperation.Currency)).ReturnsAsync(null as Wallet);

            var sut = new CreditOperationUseCase(walletRepositoryMock.Object, operationRepositoryMock.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Execute(fakeOperation));

            operationRepositoryMock.Verify(repository => repository.GetById(fakeOperation.Id), Times.Once);
            walletRepositoryMock.Verify(repository => repository.GetByUserAndCurrency(fakeOperation.Owner.User, fakeOperation.Currency), Times.Once);
        }

        [Fact(DisplayName = "Should execute credit operation successfully")]
        public async Task ShouldExecuteCreditOperationSuccessfully()
        {
            var fakeOperation = OperationFactory.GenerateOperation();
            var fakeWallet = WalletFactory.GenerateWallet();

            var walletRepositoryMock = new Mock<IWalletRepository>();
            var operationRepositoryMock = new Mock<IOperationRepository>();

            operationRepositoryMock.Setup(repository => repository.GetById(fakeOperation.Id)).ReturnsAsync(null as Operation);
            walletRepositoryMock.Setup(repository => repository.GetByUserAndCurrency(fakeOperation.Owner.User, fakeOperation.Currency)).ReturnsAsync(fakeWallet);
            operationRepositoryMock.Setup(repository => repository.Create(fakeOperation)).Returns(Task.CompletedTask);
            walletRepositoryMock.Setup(repository => repository.UpdateBalance(fakeWallet.Id, fakeOperation.Value)).Returns(Task.CompletedTask);

            var sut = new CreditOperationUseCase(walletRepositoryMock.Object, operationRepositoryMock.Object);

            await sut.Execute(fakeOperation);

            operationRepositoryMock.Verify(repository => repository.GetById(fakeOperation.Id), Times.Once);
            walletRepositoryMock.Verify(repository => repository.GetByUserAndCurrency(fakeOperation.Owner.User, fakeOperation.Currency), Times.Once);
            operationRepositoryMock.Verify(repository => repository.Create(fakeOperation), Times.Once);
            walletRepositoryMock.Verify(repository => repository.UpdateBalance(fakeWallet.Id, fakeOperation.Value), Times.Once);
        }
    }
}
