using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Models
{
	public class OrdiniUtente
	{
        //Tabella Ordine
        public int ID { get; set; }
        public string NomeStato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [StringLength(250)]
        public string Commento { get; set; }
        public string NomeCorriere { get; set; }
        [StringLength(50)]
        public string IndirizzoSpedizione { get; set; }
        public string NomePagamento { get; set; }
        // Tabella OrdiniProdotti
        public int Quantita { get; set; }
        // Tabella Prodotto
        public List<Prodotto> Prodotti { get; set; }
    }
}

