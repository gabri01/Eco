using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
	public interface IBusiness
	{		
        public bool VerificaUtenteEsistente(string Email);
        //public bool AutenticazioneUtente(Utente Utente);
        public string Login(Login Utente);
        public bool EsisteUtente(Login Utente);
        public long Registrazione(Registrazione NuovoUtente);
        public bool ModificaUtente(string Nome, string Cognome, string Email);
        public List<Prodotto> GetAllProdotti();
        public List<OrdiniUtente> GetOrdini(string Email);
        public bool InsertOrdine(NuovoOrdine nuovoOrdine, string Email);
        //public bool InsertOrdine(List<Prodotto> Prodotti, string Email, string MetodoPagamento, string Corriere, string IndirizzoSpedizione, string Commento);
        public long InsertProdotto(Prodotto prodotto);
        public int DeleteOrderByUser(int idOrdine, string email);
        public int UpdateProduct(Prodotto prodotto);
        public int DeleteProduct(int idProdotto);
        public int UpdateOrder(int idOrdine, int idStato);
        public List<Corriere> GetAllCorrieri();
        public List<Pagamento> GetAllPagamenti();
        public List<Categoria> GetAllCategorie();
        public Utente GetUtenteByID(string Email);
        public List<Prodotto> SearchProductsByName(string nomeProdotto);
        public List<Prodotto> SearchProductByCategory(string nomeCategoria);
        public int DeleteOrder(int idOrdine, string Email);
    }
}

