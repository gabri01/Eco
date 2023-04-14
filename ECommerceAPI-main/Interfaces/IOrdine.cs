using System;
using Models;

namespace Interfaces
{
	public interface IOrdine
	{
        //GetAllOrdini
        public List<OrdiniUtente> GetOrdini(string Email);
        public bool InsertOrdine(NuovoOrdine nuovoOrdine, string Email);
        //public bool InsertOrdine(List<Prodotto> Prodotti, string Email, string MetodoPagamento, string Corriere, string IndirizzoSpedizione, string Commento);
        public int DeleteOrderByUser(int idOrdine, string email);
        public int UpdateOrder(int idOrdine, int idStato, string Email);
        public int DeleteOrder(int idOrdine, string Email);
    }
}

