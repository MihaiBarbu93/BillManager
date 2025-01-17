﻿
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

        public void StergereProdus(List<int> idProduse)
        {
            var produse = _context.DetaliiFacturi.Where(pr => idProduse.Contains(pr.IdDetaliiFactura));
            foreach (var prod in produse)
            {
                try
                {
                    _context.DetaliiFacturi.Remove(prod);
                }
                catch (Exception e) {
                    throw new Exception($"{e.Message}");
                }
                var x = _context.DetaliiFacturi;

            }

        }

        public void AdaugaFactura(Factura factura)
        {
            _context.Facturi.Add(factura);
        }

            public async Task<Factura> GetFacturaAsync(int idFactura)
        {
            var facturi = await _context.Facturi
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
            var factura = facturi.FirstOrDefault(fc => fc.IdFactura == idFactura);
            return factura;
        }

        public async Task<Factura> GetFacturaRawAsync(int idFactura)
        {
            return await _context.Facturi.Include(p=>p.DetaliiFactura).FirstOrDefaultAsync(fc => fc.IdFactura == idFactura);
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
                    .Select(detalii => new Produse
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
