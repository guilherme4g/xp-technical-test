using Domain.Entities;
using Common.Validators;
using Domain.Repositories;
using Application.UseCases;
using System.ComponentModel.DataAnnotations;

namespace Interface.Controllers
{
    public class GetWalletsByUserRequest
    {
        [Required]
        [ValidGuidAttribute]
        public Guid UserId { get; private set; }

        public GetWalletsByUserRequest(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetWalletByUserItemResponse
    {

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        public bool Standart { get; set; }

        [Required]
        public bool Active { get; set; }    

        public GetWalletByUserItemResponse(string id, string name, string currency, int balance, bool standart, bool active)
        {
            Id = id;
            Name = name;
            Currency = currency;
            Balance = balance;
            Standart = standart;
            Active = active;
        }
    }

    public class GetWalletsByUserController
    {
        private readonly GetWalletsByUserUseCase _getWalletsByUserUseCase;

        public GetWalletsByUserController(IWalletRepository walletRepository) 
        {
            _getWalletsByUserUseCase = new GetWalletsByUserUseCase(walletRepository);
        }

        public async Task<IEnumerable<GetWalletByUserItemResponse>> Execute(GetWalletsByUserRequest request)
        {
            User user = new User(request.UserId);

            IEnumerable<Wallet> wallets = await _getWalletsByUserUseCase.Execute(user);

            List<GetWalletByUserItemResponse> response = new List<GetWalletByUserItemResponse>();

            foreach (Wallet wallet in wallets)
            {
                response.Add(new GetWalletByUserItemResponse(
                        wallet.Id.ToString(),
                        wallet.Name,
                        wallet.Currency.Name,
                        wallet.Balance,
                        wallet.Standart,
                        wallet.Active
                    )
                );
            }

            return response;
        }
    }
}
