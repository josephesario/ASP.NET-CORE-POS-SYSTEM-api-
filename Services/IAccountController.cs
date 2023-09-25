using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;

namespace POSApp
{
    public interface IAccountController
    {
        IActionResult AddAccount([FromBody] TAccountHelper account);

        IActionResult GetAllAccounts();

        Task<IActionResult> GetAccountById(int id);

        IActionResult UpdateAccount(int id, [FromBody] TAccountHelper updatedAccount);

        IActionResult AccountDeposit(int id, [FromBody] AccountUpdateHelperDeposit updateDto);

        IActionResult AccountWithDrawal(int id, [FromBody] AccountUpdateHelperWithdra updateDto);

        IActionResult actionResult(int id);
    }
}
