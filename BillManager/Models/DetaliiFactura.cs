using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class DetaliiFactura
    {

        public int IdDetaliiFactura { get; set; }
        public int IdLocatie { get; set; }
        public string NumeProdus { get; set; }
        public decimal Cantitate { get; set; }
        public decimal PretUnitar { get; set; }
        public decimal Valoare { get; set; }
        public int IdFactura { get; set; }
        public Factura Factura { get; set; }
    }
}
