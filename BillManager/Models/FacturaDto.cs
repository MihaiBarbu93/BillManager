using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class FacturaDto
    {
        public int IdLocatie { get; set; }
        public string NumeClient { get; set; }
        public string NumarFactura { get; set; }
        public DateTime DataFactura { get; set; }
        public List<ProduseDto> Produse { get; set; }

        public FacturaDto()
        {
            Produse = new List<ProduseDto>();
        }
    }

    public class ProduseDto
    {
        public string NumeProdus { get; set; }
        public decimal Cantitate { get; set; }
        public decimal PretUnitar { get; set; }
        public decimal Valoare { get; set; }
    }
}
