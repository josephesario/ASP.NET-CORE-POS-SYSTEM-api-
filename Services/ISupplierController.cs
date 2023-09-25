using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

public interface ISupplierController
{
    Task<ActionResult<TSupplier>> PostTSupplier(TSupplierHelper tSupplier);
    Task<ActionResult<IEnumerable<TSupplier>>> GetTSuppliers();
    Task<ActionResult<TSupplier>> GetTSupplier(int id);
    Task<IActionResult> PutTSupplier(int id, TSupplierHelper tSupplier);
    Task<IActionResult> DeleteTSupplier(int id);
}
