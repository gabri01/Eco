using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ProdottoCategoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        public int Quantita { get; set; }
        public int Rating { get; set; }
        public string NomeCategoria { get; set; }  
    }
}
