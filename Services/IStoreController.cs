using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface IStoreController
{
    Task<ActionResult<TStoreHelper>> PostTStore(TStoreHelper tStore);
    Task<ActionResult<IEnumerable<TStore>>> GetTStores();
    Task<ActionResult<TStore>> GetTStore(int id);
    Task<IActionResult> PutTStore(int id, TStoreHelper tStore);
    Task<IActionResult> DeleteTStore(int id);
}
