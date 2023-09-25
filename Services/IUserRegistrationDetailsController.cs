using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface IUserRegistrationDetailController
{
    Task<ActionResult> PostTUserRegistrationDetail(TUserRegistrationDetailHelper userRegistrationDetailHelper);

    Task<ActionResult<IEnumerable<TUserRegistrationDetail>>> GetTUserRegistrationDetails();
    Task<ActionResult<TUserRegistrationDetail>> GetTUserRegistrationDetail(int id);
    Task<IActionResult> PutTUserRegistrationDetail(int id, TUserRegistrationDetail tUserRegistrationDetail);
    Task<IActionResult> DeleteTUserRegistrationDetail(int id);
}
