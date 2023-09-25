using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface IUserTypeStatusController
{
    Task<ActionResult<TUserTypeStatus>> PostTUserTypeStatus(TUserTypeStatusHelper tUserTypeStatusHelper);
    Task<ActionResult<IEnumerable<TUserTypeStatus>>> GetTUserTypeStatuses();
    Task<ActionResult<TUserTypeStatus>> GetTUserTypeStatus(int id);
    Task<IActionResult> PutTUserTypeStatus(int id, TUserTypeStatusHelper tUserTypeStatus);
    Task<IActionResult> DeleteTUserTypeStatus(int id);
}
