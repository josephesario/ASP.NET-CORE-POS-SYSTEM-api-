using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using POS.Models;
using Microsoft.AspNetCore.Authorization;

namespace POSApp
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase, IAccountController
    {
        private readonly posDbContext _context;

        public AccountController(posDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"> This is the model for the account</param>
        /// <returns>Account</returns>
        [HttpPost("AddAccount")]
        public IActionResult AddAccount([FromBody] TAccountHelper account)
        {
            try
            {

                if (account == null) 
                {
                    return BadRequest("Invalid input data.");
                }

                _context.Add(account);
                _context.SaveChanges();
                return Ok("Account added successfully.");

            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _context.TAccounts.ToList();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpGet("GetAccountById/{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                // Retrieve and return an account by its ID from the database
                var account = await _context.TAccounts.FirstOrDefaultAsync(temp => temp.AccountId == id);

                if (account == null)
                {
                    return NotFound("Account not found.");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("UpdateAccount/{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] TAccountHelper updatedAccount)
        {
            try
            {
                if (updatedAccount == null)
                {
                    return BadRequest("Invalid input data.");
                }

                // Retrieve the existing account by its ID from the database
                var existingAccount = _context.TAccounts.FirstOrDefault(a => a.AccountId == id);

                if (existingAccount == null)
                {
                    return NotFound("Account not found.");
                }


                // Update all properties of the existing account with the values from updatedAccount
                existingAccount.AccountName = updatedAccount.AccountName;
                existingAccount.Deposit = updatedAccount.Deposit;
                existingAccount.AllTimeBalance = updatedAccount.AllTimeBalance;
                existingAccount.Withdrawal = updatedAccount.Withdrawal;
                existingAccount.Description = updatedAccount.Description;
                existingAccount.ClientId = updatedAccount.ClientId;
                existingAccount.UserDetailsId = updatedAccount.UserDetailsId;
                existingAccount.DayAdded = updatedAccount.DayAdded;

                // Save the changes to the database
                _context.SaveChanges();

                return Ok("Account updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    
        [HttpPatch("AccountDeposit/{id}")]
        public IActionResult AccountDeposit(int id, [FromBody] AccountUpdateHelperDeposit updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return BadRequest("Invalid input data.");
                }

                // Retrieve the existing account by its ID from the database
                var existingAccount = _context.TAccounts.FirstOrDefault(a => a.AccountId == id);

                if (existingAccount == null)
                {
                    return NotFound("Account not found.");
                }

                // Update only the specified fields
                existingAccount.Deposit = updateDto.Deposit;

                // Save the changes to the database
                _context.SaveChanges();

                return Ok("Account updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPatch("AccountWithDrawal/{id}")]
        public IActionResult AccountWithDrawal(int id, [FromBody] AccountUpdateHelperWithdra updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return BadRequest("Invalid input data.");
                }

                // Retrieve the existing account by its ID from the database
                var existingAccount = _context.TAccounts.FirstOrDefault(a => a.AccountId == id);

                if (existingAccount == null)
                {
                    return NotFound("Account not found.");
                }

                // Update only the specified fields
                existingAccount.Withdrawal = updateDto.Withdrawal;

                // Save the changes to the database
                _context.SaveChanges();

                return Ok("Account updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("DeleteAccount/{id}")]
        public IActionResult actionResult(int id)
        {

            var deleteAccount = _context.TAccounts.FirstOrDefault(a => a.AccountId == id);
            if (deleteAccount != null) { 

                _context.Remove(deleteAccount);
                return Ok("Account deleted Successfully");

            }
            else
            {
                return NotFound();
            }


        }


    }
}

      