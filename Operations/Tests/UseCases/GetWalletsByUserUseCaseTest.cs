using Application.UseCases;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Tests.Utils.Factories;
using Xunit;

namespace Tests.Application
{

    public class GetWalletsByUserUseCaseTest
    {
        [Fact(DisplayName = "Should be get wallets by user successfully")]
        public async void ShouldGetUserByEmailSuccessfully()
        {
            var fakeWallets = WalletFactory.GenerateWallets();
            var fakeUser = fakeWallets[0].User;

            var walletRepositoryMock = new Mock<IWalletRepository>();
            walletRepositoryMock.Setup(repository => repository.GetByUser(It.IsAny<User>())).ReturnsAsync(fakeWallets);
            GetWalletsByUserUseCase sut = new GetWalletsByUserUseCase(walletRepositoryMock.Object);

            var result = await sut.Execute(fakeUser);

            Assert.NotNull(result);
            Assert.Equal(fakeWallets, result);
            walletRepositoryMock.Verify(repository => repository.GetByUser(fakeUser), Times.Once);

        }
    }
}
