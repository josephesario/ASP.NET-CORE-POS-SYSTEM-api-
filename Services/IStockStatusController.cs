using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POSApp.Controllers
{
    public interface IStockStatusController
    {
        Task<ActionResult<TStockStatusHelper>> PostTStockStatus(TStockStatusHelper tStockStatus);
        Task<ActionResult<IEnumerable<TStockStatus>>> GetTStockStatuses();
        Task<ActionResult<TStockStatus>> GetTStockStatus(int id);
        Task<IActionResult> PutTStockStatus(int id, TStockStatusHelper tStockStatus);
        Task<IActionResult> DeleteTStockStatus(int id);
    }
}
