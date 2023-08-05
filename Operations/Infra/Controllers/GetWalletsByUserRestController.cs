using Microsoft.AspNetCore.Mvc;
using Interface.Controllers;
using Infra.EntityFramework;
using Infra.EntityFramework.Repositories;

namespace Infra.Controllers
{
    [ApiController]
    [Route("/wallets")]
    public class GetWalletsByUserRestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly WalletRepository _walletRepository;
        private readonly GetWalletsByUserController _controller;


        public GetWalletsByUserRestController(ApplicationDbContext context)
        {
            _context = context;
            _walletRepository = new WalletRepository(_context);
            _controller = new GetWalletsByUserController(_walletRepository);

        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<GetWalletByUserItemResponse>>> GetWalletsByUser(string userId)
        {
            try
            {
                var request = new GetWalletsByUserRequest(Guid.Parse(userId));

                var wallets = await this._controller.Execute(request);

                return Ok(wallets);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}