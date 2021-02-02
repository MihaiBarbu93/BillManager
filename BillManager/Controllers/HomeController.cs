using BillManager.DTOs;
using BillManager.Interfaces;
using BillManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFacturiRepository _facturiRepository;

        public HomeController(ILogger<HomeController> logger, IFacturiRepository facturiRepository)
        {
            _facturiRepository = facturiRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Factura> facturi = await _facturiRepository.GetFacturiAsync();
            return View(facturi);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
