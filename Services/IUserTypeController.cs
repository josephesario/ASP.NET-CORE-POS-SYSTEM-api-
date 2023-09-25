using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface IUserTypeController
{
    Task<ActionResult<TUserTypeHelper>> PostTUserType(TUserTypeHelper tUserType);
    Task<ActionResult<IEnumerable<TUserType>>> GetTUserTypes();
    Task<ActionResult<TUserType>> GetTUserType(int id);
    Task<IActionResult> PutTUserType(int id, TUserTypeHelper tUserType);
    Task<IActionResult> DeleteTUserType(int id);
}
