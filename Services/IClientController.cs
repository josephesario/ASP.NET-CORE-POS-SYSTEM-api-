using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Models;

namespace POSApp.Controllers
{
    public interface ITClientController
    {

        Task<ActionResult> PostTClient(TClientHelper tClient);

        Task<ActionResult<TClientHelper>> GetTClients();

        Task<ActionResult<TClientHelper>> GetTClient(int id);

        Task<ActionResult<TClientHelper>> PutTClient(int id, TClientHelper tClient);

        Task<IActionResult> DeleteTClient(int id);
    }
}
