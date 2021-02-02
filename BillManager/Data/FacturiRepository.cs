using BillManager.DTOs;
using BillManager.Interfaces;
using BillManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Data
{
    public class FacturiRepository : IFacturiRepository
    {
        private readonly AppDbContext _context;
        public FacturiRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<List<Factura>> GetFacturiAsync()
        {
            return await _context.Facturi
                .Select(factura => new Factura
                {
                    DataFactura = factura.DataFactura,
                    NumeClient = factura.NumeClient,
                    NumarFactura = factura.NumarFactura,
                    IdLocatie = factura.IdLocatie,
                    IdFactura = factura.IdFactura,
                    Produse = factura.DetaliiFactura
                    .Select(detalii => new Models.Produse
                    {
                        Cantitate = detalii.Cantitate,
                        NumeProdus = detalii.NumeProdus,
                        PretUnitar = detalii.PretUnitar,
                        Valoare = detalii.Valoare,
                        IdDetaliiFactura = detalii.IdDetaliiFactura
                    })
                    .ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
