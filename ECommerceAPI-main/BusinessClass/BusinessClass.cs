using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Interfaces;
using Models.Models;

namespace BusinessLogic
{
    public class BusinessClass : IBusiness
    {
        IDAL dal;
        public BusinessClass(IDAL dal)
        {
            this.dal = dal;
        }

        public bool VerificaUtenteEsistente(string Email)
        {
            return dal.VerificaUtenteEsistente(Email);
        }
        public long Registrazione(Registrazione NuovoUtente)
        {
            return dal.Registrazione(NuovoUtente);
        }
        public string Login(Login Utente)
        {
            return dal.Login(Utente);
        }
        public bool EsisteUtente(Login Utente)
        {
            return dal.EsisteUtente(Utente);
        }
        public bool ModificaUtente(string Nome, string Cognome, string Email)
        {
            return dal.ModificaUtente(Nome, Cognome, Email);
        }
        //public List<Prodotto> GetAllProdotti()
        //{
        //    return dal.GetAllProdotti();
        //}
        public List<ProdottoCategoria> GetAllProdotti()
        {
            return dal.GetAllProdotti();
        }
        public List<OrdiniUtente> GetOrdini(string Email)
        {
            return dal.GetOrdini(Email);
        }
        public bool InsertOrdine(NuovoOrdine nuovoOrdine, string Email)
        {
            return dal.InsertOrdine(nuovoOrdine, Email);
        }
        //public bool InsertOrdine(List<Prodotto> Prodotti, string Email, string MetodoPagamento, string Corriere, string IndirizzoSpedizione, string Commento)
        //{
        //    return dal.InsertOrdine(Prodotti, Email, MetodoPagamento, Corriere, IndirizzoSpedizione, Commento);
        //}
        public long InsertProdotto(Prodotto prodotto)
        {
            return dal.InsertProdotto(prodotto);
        }
        public List<Corriere> GetAllCorrieri()
        {
            return dal.GetAllCorrieri();
        }
        public List<Pagamento> GetAllPagamenti()
        {
            return dal.GetAllPagamenti();
        }
        public List<Categoria> GetAllCategorie()
        {
            return dal.GetAllCategorie();
        }
        public Utente GetUtenteByID(string Email)
        {
            return dal.GetUtenteByID(Email);
        }
        public int DeleteOrder(int idOrdine, string Email) 
        {
            return dal.DeleteOrder(idOrdine, Email);
        } 

        //GABRIELE
        public int DeleteOrderByUser(int idOrdine, string email)
        {
            return dal.DeleteOrderByUser(idOrdine, email);
        }
        public int UpdateProduct(Prodotto prodotto)
        {
            return dal.UpdateProduct(prodotto);
        }
        public int DeleteProduct(int idProdotto)
        {
            return dal.DeleteProduct(idProdotto);
        }
        public int UpdateOrder(int idOrdine, int idStato, string Email)
        {
            return dal.UpdateOrder(idOrdine, idStato, Email);
        }
        public List<Prodotto> SearchProductsByName(string nomeProdotto)
        {
            return dal.SearchProductsByName(nomeProdotto);
        }
        public List<Prodotto> SearchProductByCategory(string nomeCategoria)
        {
            return dal.SearchProductByCategory(nomeCategoria);
        }
    }
}



