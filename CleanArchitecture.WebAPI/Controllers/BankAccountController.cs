using CleanArchitecture.UseCases.Dtos.BankDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpPost]
        public async Task<IActionResult> OpenAccount([FromBody] BankAccountDto bankAccountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _bankAccountService.OpenAccountAsync(bankAccountDto.AccountHolderName, bankAccountDto.AccountType, bankAccountDto.InitialDeposit);
            return CreatedAtAction(nameof(GetAccountByNumber), new { accountNumber = account.AccountNumber }, account);
        }

        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(string accountNumber)
        {
            var account = await _bankAccountService.GetAccountByNumberAsync(accountNumber);
            if (account == null)
                return NotFound($"Account with number {accountNumber} not found.");

            return Ok(account);
        }

        [HttpGet("holder/{accountHolderName}")]
        public async Task<IActionResult> GetAccountsByHolderName(string accountHolderName)
        {
            var accounts = await _bankAccountService.GetAccountsByHolderNameAsync(accountHolderName);
            return Ok(accounts);
        }
    }
}
