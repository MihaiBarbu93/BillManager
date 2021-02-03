
using BillManager.Interfaces;
using BillManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManager.Controllers
{
    public class BillController : Controller
    {

        private readonly IFacturiRepository _facturiRepository;

        public BillController(IFacturiRepository facturiRepository)
        {
            _facturiRepository = facturiRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        // POST: BillController/Create
        [HttpPost]
        public async Task<IActionResult> SalvareFactura()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = reader.ReadToEndAsync().Result;
                FacturaDto facturaDto = JsonConvert.DeserializeObject<FacturaDto>(content);
                bool status = false;
                if (ModelState.IsValid)
                {
                    Factura fc = new Factura
                    {
                        IdLocatie = facturaDto.IdLocatie,
                        NumarFactura = facturaDto.NumarFactura,
                        DataFactura = facturaDto.DataFactura,
                        NumeClient = facturaDto.NumeClient
                    };
                    foreach (var i in facturaDto.Produse)
                    {
                        fc.DetaliiFactura.Add(new DetaliiFactura
                        {
                            IdLocatie = fc.IdLocatie,
                            NumeProdus = i.NumeProdus,
                            Cantitate = i.Cantitate,
                            PretUnitar = i.PretUnitar,
                            Valoare = i.Valoare,
                            Factura = fc
                        });
                    }
                    _facturiRepository.AdaugaFactura(fc);
                    await _facturiRepository.SaveAllAsync();
                    status = true;

                }
                else
                {
                    status = false;
                }

                return Json(new { success = status });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
