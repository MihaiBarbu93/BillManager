using BillManager.DTOs;
using BillManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Interfaces
{
    public interface IFacturiRepository
    {
        Task<List<Factura>> GetFacturiAsync();
        
        Task<bool> SaveAllAsync();
    }
}
