using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public class NuovoOrdine
    {
        public string Pagamento { get; set; }
        public string Corriere { get; set; }
        public string IndirizzoSpedizione { get; set; }
        public string Commento { get; set; }
        public List<Prodotto> Prodotti { get; set; }
    }
}
