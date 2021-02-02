using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.DTOs
{
    public class FacturaDto
    {
        public int IdFactura { get; set; }
        public int IdLocatie { get; set; }
        public string NumeClient { get; set; }
        public string NumarFactura { get; set; }
        public DateTime DataFactura { get; set; }
        public List<Produse> Produse { get; set; }

        public FacturaDto()
        {
            Produse = new List<Produse>();
        }
    }

    public class Produse
    {
        public int IdDetaliiFactura { get; set; }
        public string NumeProdus { get; set; }
        public decimal Cantitate { get; set; }
        public decimal PretUnitar { get; set; }
        public decimal Valoare { get; set; }
    }
}
