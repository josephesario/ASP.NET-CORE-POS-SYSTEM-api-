using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Models;

namespace POSApp.Controllers
{
    public interface ISaleController
    {
        Task<ActionResult<IEnumerable<TSale>>> GetTSales();

        Task<ActionResult<TSale>> GetTSale(int id);

        Task<ActionResult> AddTSale(TSaleHelper tSale);

        Task<IActionResult> PutTSale(int id, TSaleHelper tSale);

        Task<IActionResult> DeleteTSale(int id);
    }
}
