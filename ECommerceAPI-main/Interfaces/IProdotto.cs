using System;
using Models;
using Models.Models;

namespace Interfaces
{
	public interface IProdotto
	{
        //public List<Prodotto> GetAllProdotti();
        public List<ProdottoCategoria> GetAllProdotti();
        public long InsertProdotto(Prodotto prodotto);
        public int UpdateProduct(Prodotto prodotto);
        public int DeleteProduct(int idProdotto);
        public List<Prodotto> SearchProductsByName(string nomeProdotto);
        public List<Prodotto> SearchProductByCategory(string nomeCategoria);

    }
}
