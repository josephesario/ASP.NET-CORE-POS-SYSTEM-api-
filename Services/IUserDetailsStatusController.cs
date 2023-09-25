using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface IUserDetailsStatusController
{
    Task<ActionResult<TUserDetailsStatusHelper>> PostTUserDetailsStatus(TUserDetailsStatusHelper tUserDetailsStatus);
    Task<ActionResult<IEnumerable<TUserDetailsStatusHelper>>> GetTUserDetailsStatuses();
    Task<ActionResult<TUserDetailsStatusHelper>> GetTUserDetailsStatus(int id);
    Task<IActionResult> PutTUserDetailsStatus(int id, TUserDetailsStatusHelper tUserDetailsStatus);
    Task<IActionResult> DeleteTUserDetailsStatus(int id);
}
