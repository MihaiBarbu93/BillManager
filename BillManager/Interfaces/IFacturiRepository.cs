
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

        Task<Factura> GetFacturaRawAsync(int idFactura);
        Task<Factura> GetFacturaAsync(int idFactura);
        void AdaugaFactura(Factura factura);
        
        Task<bool> SaveAllAsync();
    }
}
