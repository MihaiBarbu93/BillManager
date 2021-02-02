using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models
{
    public class Factura
    {
        public Factura()
        {
            DetaliiFactura = new HashSet<DetaliiFactura>();
            Produse = new List<Produse>();
        }

        public int IdFactura { get; set; }
        public int IdLocatie { get; set; }
        public string NumarFactura { get; set; }
        public DateTime DataFactura { get; set; }
        public string NumeClient { get; set; }

        public virtual ICollection<DetaliiFactura> DetaliiFactura { get; set; }
        [NotMapped]
        public List<Produse> Produse { get; set; }

       
    }
    [NotMapped]
    public class Produse
    {
        public int IdDetaliiFactura { get; set; }
        public string NumeProdus { get; set; }
        public decimal Cantitate { get; set; }
        public decimal PretUnitar { get; set; }
        public decimal Valoare { get; set; }
    }
}
