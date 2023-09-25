using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.ModelHelper;
using POS.Models;

public interface IUserDetailController
{
    Task<ActionResult<TUserDetail>> PostTUserDetail(TUserDetailHelper tUserDetail);
    Task<ActionResult<IEnumerable<TUserDetail>>> GetTUserDetails();
    Task<ActionResult<TUserDetail>> GetTUserDetail(int id);
    Task<IActionResult> PutTUserDetail(int id, TUserDetailHelper tUserDetail);
    Task<IActionResult> DeleteTUserDetail(int id);
    Task<IActionResult> Login([FromHeader] string Email, string Password);
    
}
