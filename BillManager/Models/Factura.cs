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
        }

        public int IdFactura { get; set; }
        public int IdLocatie { get; set; }
        public string NumarFactura { get; set; }
        public DateTime DataFactura { get; set; }
        public string NumeClient { get; set; }

        public virtual ICollection<DetaliiFactura> DetaliiFactura { get; set; }
    }
}
