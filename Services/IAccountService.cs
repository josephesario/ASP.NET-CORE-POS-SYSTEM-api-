using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Services
{
    public interface IAccountService
    {
        IActionResult AddAccounts([FromBody] TAccountHelper account);
        IActionResult GetAllAccounts();
        Task<IActionResult> GetAccountsById(int id);
        IActionResult UpdateAccounts(int id, [FromBody] TAccountHelper updatedAccount);
    }
}
