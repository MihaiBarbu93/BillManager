
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
using System.Web.Helpers;

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
            Factura factura = new Factura() { IdFactura = 0 };
            return View(factura);
        }
        [HttpGet]
        public async Task<IActionResult> EditFactura(int id)
        {
            Factura factura = await _facturiRepository.GetFacturaAsync(id);
            return View("Index", factura);
         
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
        public async Task<IActionResult> ModificareFactura()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                bool status = false;

                if (ModelState.IsValid)
                {
                    var content = reader.ReadToEndAsync().Result;
                    Factura facturaModificata = JsonConvert.DeserializeObject<Factura>(content);
                    Factura facturaOriginala = await _facturiRepository.GetFacturaRawAsync(facturaModificata.IdFactura);


                    facturaOriginala.IdLocatie = facturaModificata.IdLocatie;
                    facturaOriginala.NumarFactura = facturaModificata.NumarFactura;
                    facturaOriginala.DataFactura = facturaModificata.DataFactura;
                    facturaOriginala.NumeClient = facturaModificata.NumeClient;
                    facturaOriginala.DetaliiFactura = new List<DetaliiFactura>();
            
                    foreach (var produs in facturaModificata.Produse)
                    {
                        facturaOriginala.DetaliiFactura.Add(new DetaliiFactura
                        {
                            IdDetaliiFactura= produs.IdDetaliiFactura,
                            IdLocatie = facturaModificata.IdLocatie,
                            NumeProdus = produs.NumeProdus,
                            Cantitate = produs.Cantitate,
                            PretUnitar = produs.PretUnitar,
                            Valoare = produs.Valoare,
                            IdFactura = facturaModificata.IdFactura
                        });
                        
        
                    }
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
