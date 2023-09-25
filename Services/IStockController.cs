using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Models;

namespace POSApp.Controllers
{
    public interface IStockController
    {
        Task<ActionResult<TStockHelper>> PostTStock(TStockHelper tStock);

        Task<ActionResult<IEnumerable<TStock>>> GetTStocks();

        Task<ActionResult<TStock>> GetTStock(int id);

        Task<IActionResult> PutTStock(int id, TStockHelper tStock);

        Task<IActionResult> DeleteTStock(int id);
    }
}
