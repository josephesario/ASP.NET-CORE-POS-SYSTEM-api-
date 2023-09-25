using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Models;

namespace POSApp.Controllers
{
    public interface ITProductCategoryController
    {
        Task<ActionResult<TProductCategoryHelper>> PostTProductCategory(TProductCategoryHelper tProductCategory);

        Task<ActionResult<IEnumerable<TProductCategory>>> GetTProductCategories();

        Task<ActionResult<TProductCategory>> GetTProductCategory(int id);

        Task<IActionResult> PutTProductCategory(int id, TProductCategoryHelper tProductCategory);

        Task<IActionResult> DeleteTProductCategory(int id);
    }
}
