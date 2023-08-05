using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api_Users.Controllers.Operations
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Operations")]
    [Route("wallets")]
    public class GetWalletsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public GetWalletsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Execute()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                HttpResponseMessage response = await client.GetAsync("http://localhost:5200/wallets/85F80757-E64E-4E9D-956C-442137AF0311");
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();

                // Aqui você pode transformar a resposta em um objeto e retorná-lo, se necessário.
                // Por enquanto, estamos apenas retornando Ok.
                return Ok(body);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}