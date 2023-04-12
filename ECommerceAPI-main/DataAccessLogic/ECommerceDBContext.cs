﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models;
using Interfaces;

//EF
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

//JWT
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

//Mail
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace DataAccessLogic
{
    public partial class ECommerceDBContext : DbContext, IDAL
    {
        private readonly string connString;

        public ECommerceDBContext(string connString)
        {
            this.connString = connString;
        }

        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options) {}

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Corriere> Corrieres { get; set; }
        public virtual DbSet<Ordine> Ordines { get; set; }
        public virtual DbSet<Pagamento> Pagamentos { get; set; }
        public virtual DbSet<Prodotto> Prodottos { get; set; }
        public virtual DbSet<Ruolo> Ruolos { get; set; }
        public virtual DbSet<Utente> Utentes { get; set; }
        public virtual DbSet<Ordiniprodotti> Ordiniprodottis { get; set; }
        public virtual DbSet<StatoOrdine> StatoOrdines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordine>(entity =>
            {
                entity.HasOne(d => d.IDStatoNavigation)
                   .WithMany(p => p.Ordines)
                   .HasForeignKey(d => d.IdStato)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Ordine_StatoOrdine");

                entity.HasOne(d => d.IDCorriereNavigation)
                    .WithMany(p => p.Ordines)
                    .HasForeignKey(d => d.IDCorriere)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordine_Corriere");

                entity.HasOne(d => d.IDUtenteNavigation)
                    .WithMany(p => p.Ordines)
                    .HasForeignKey(d => d.IDUtente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordine_Utente");

                entity.HasOne(d => d.IdPagamentoNavigation)
                    .WithMany(p => p.Ordines)
                    .HasForeignKey(d => d.IdPagamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordine_Pagamento");
            });

            modelBuilder.Entity<Ordiniprodotti>(entity =>
            {
                entity.HasOne(d => d.IDOrdineNavigation)
                    .WithMany(p => p.Ordiniprodottis)
                    .HasForeignKey(d => d.IDOrdine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordiniprodotti_Ordine");

                entity.HasOne(d => d.IDProdottoNavigation)
                    .WithMany(p => p.Ordiniprodottis)
                    .HasForeignKey(d => d.IDProdotto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ordiniprodotti_Prodotto1");
            });

            modelBuilder.Entity<Prodotto>(entity =>
            {
                entity.HasOne(d => d.IDCategoriaNavigation)
                    .WithMany(p => p.Prodottos)
                    .HasForeignKey(d => d.IDCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prodotto_Categoria");
            });


            modelBuilder.Entity<Utente>(entity =>
            {

                //entity.HasOne(d => d.IDRuoloNavigation)
                //    .WithMany(p => p.Utentes)
                //    .HasForeignKey(d => d.IDRuolo)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Utente_Ruolo");

                entity.Property(e => e.Email).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        public bool EsisteUtente(Login Utente)
        {
            try
            {
                Utente EsisteUtente = (from u in Utentes
                                       where u.Email == Utente.Email
                                       && u.Password == Utente.Password
                                       select u).SingleOrDefault();
                if (EsisteUtente == null)
                    throw new InvalidOperationException();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public string Login(Login Utente)
        {
            string Key = "MNU66iBl3T5rh6H52i69";
            var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var Credentials = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256);
            string Duration = "60";

            //var utente = this.Utentes.SingleOrDefault(u => u.Email == Utente.Email);
            //if (utente != null)
            //{
            //    var role = this.Ruolos.SingleOrDefault(r => r.ID == utente.IDRuolo);
            //    if (role != null)
            //    {
            //        var roleName = (role.ID == 1) ? "Utente" : "Amministratore";

            //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            //        {
            //            new Claim(ClaimTypes.Email, Utente.Email),
            //            new Claim(ClaimTypes.Role, role.Nome)
            //        });


            Claim[] Claims = new[]
            {
                 new Claim("Email", Utente.Email)
            };

            var JwtToken = new JwtSecurityToken
            (
                issuer: "localhost",
                audience: "localhost",
                claims: Claims,
                expires: DateTime.Now.AddMinutes(Int32.Parse(Duration)),
                signingCredentials: Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(JwtToken);
        }

        public bool VerificaUtenteEsistente(string Email)
        {
            try
            {
                Utente UtenteEsistente = this.Utentes.SingleOrDefault(Em => Em.Email == Email);
            }
            catch (InvalidOperationException)
            {
                return true;
            }
            return false;
        }

        public long Registrazione(Registrazione NuovoUtente)
        {
            Utente Utente = new Utente()
            {
                Cognome = NuovoUtente.Cognome,
                Nome = NuovoUtente.Nome,
                Email = NuovoUtente.Email,
                Password = NuovoUtente.Password,
                IDRuolo = 2
            };
            var InsertUtente = this.Utentes.Add(Utente);
            this.SaveChanges();  
            Utente UtenteInserito = this.Utentes.SingleOrDefault(Ema => Ema.Email.Equals(NuovoUtente.Email));
            return UtenteInserito.ID;
        }

        public bool ModificaUtente(string Nome, string Cognome, string Email)
        {
            using (IDbContextTransaction transaction = this.Database.BeginTransaction())
            {
                try
                {
                    var EsisteUtente = (from u in Utentes
                                           where
                                           u.Email == Email
                                        select u).SingleOrDefault();

                    if (EsisteUtente != null)
                    {
                        EsisteUtente.Nome = Nome;
                        EsisteUtente.Cognome = Cognome;
                        EsisteUtente.Email = Email;
                        this.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                }
                catch (InvalidOperationException)
                {
                    transaction.Rollback();  
                }
            }
            return false;
        }

        public List<Prodotto> GetAllProdotti()
        {
            return this.Prodottos.ToList();
        }

        public long InsertProdotto(Prodotto prodotto)
        {
            Prodotto product = new Prodotto()
            {
                Nome = prodotto.Nome,
                Quantita = prodotto.Quantita,
                Prezzo = prodotto.Prezzo,
                IDCategoria = prodotto.IDCategoria
            };
            var InsertProdotto = this.Prodottos.Add(product);
            this.SaveChanges();
            return prodotto.ID;
        }

     //Vecchia funzionante   
//public bool InsertOrdine(NuovoOrdine nuovoOrdine, string Email)
//{
//    using (IDbContextTransaction transaction = this.Database.BeginTransaction())
//    {
//        try
//        {
//            var Utente = (
//                from u in this.Utentes
//                where u.Email == Email
//                select u
//            ).SingleOrDefault();

//            Pagamento pagamentoEsiste = (
//                from mp in Pagamentos
//                where mp.Nome == MetodoPagamento
//                select mp
//            ).SingleOrDefault();

//            if (pagamentoEsiste == null)
//                throw new Exception("Il metodo di pagamento selezionato non esiste");

//            Corriere corriereEsiste = (
//                from c in Corrieres
//                where c.Nome == Corriere
//                select c
//            ).SingleOrDefault();

//            if (corriereEsiste == null)
//                throw new Exception("Il corriere selezionato non esiste");

//            Ordine NuovoOrdine = new Ordine()
//            {
//                IDUtente = Utente.ID,
//                Data = DateTime.Now,
//                IdStato = 1,
//                Commento = Commento,
//                IdPagamento = pagamentoEsiste.ID,
//                IDCorriere = corriereEsiste.ID,
//                IndirizzoSpedizione = IndirizzoSpedizione
//            };

//            this.Ordines.Add(NuovoOrdine);

//            this.SaveChanges();

//            foreach (Prodotto Prodotto in Prodotti)
//            {
//                Prodotto ProdottoEsiste = (
//                    from p in Prodottos
//                    where p.ID == Prodotto.ID
//                    select p
//                ).SingleOrDefault();

//                if (ProdottoEsiste == null)
//                    throw new Exception("Il prodotto selezionato non esiste");

//                if (ProdottoEsiste.Quantita < Prodotto.Quantita)
//                    throw new Exception("La quantità selezionata è maggiore di quella disponibile in magazzino");

//                ProdottoEsiste.Quantita -= Prodotto.Quantita;

//                Ordiniprodotti DettaglioOrdine = new Ordiniprodotti()
//                {
//                    IDOrdine = NuovoOrdine.ID,
//                    IDProdotto = ProdottoEsiste.ID,
//                    Quantita = Prodotto.Quantita,
//                    //Prezzo = ProdottoEsiste.Prezzo
//                };

//                this.Ordiniprodottis.Add(DettaglioOrdine);
//            }

//            this.SaveChanges();

//            transaction.Commit();

//            return true;
//        }
//        catch (Exception ex)
//        {
//            transaction.Rollback();

//            throw new Exception(ex.Message);
//        }
//    }
//}

        //Per recuperare la lista dei prodotti presenti in un ordine, si può utilizzare una query simile a questa:

        //
        //from op in Ordiniprodottis
        //join p in Prodottos on op.IDProdotto equals p.ID
        //where op.IDOrdine == IDOrdine
        //select new Prodotto()
        //{
        //    ID = p.ID,
        //    Nome = p.Nome,
        //    Descrizione = p.Descrizione,
        //    Categoria = p.Categoria.Nome,
        //    Prezzo = op.Prezzo,
        //    Quantita = op.Quantita
        //}

public bool InsertOrdine(NuovoOrdine nuovoOrdine, string Email)
        {
            using (IDbContextTransaction transaction = this.Database.BeginTransaction())
            {
                try
                {
                    var Utente = (
                        from u in this.Utentes
                        where u.Email == Email
                        select u
                    ).SingleOrDefault();

                    Pagamento pagamentoEsiste = (
                        from mp in Pagamentos
                        where mp.Nome == nuovoOrdine.Pagamento
                        select mp
                    ).SingleOrDefault();

                    if (pagamentoEsiste == null)
                        throw new Exception("Il metodo di pagamento selezionato non esiste");

                    Corriere corriereEsiste = (
                        from c in Corrieres
                        where c.Nome == nuovoOrdine.Corriere
                        select c
                    ).SingleOrDefault();

                    if (corriereEsiste == null)
                        throw new Exception("Il corriere selezionato non esiste");

                    Ordine NuovoOrdine = new Ordine()
                    {
                        IDUtente = Utente.ID,
                        Data = DateTime.Now,
                        IdStato = 1,
                        Commento = nuovoOrdine.Commento,
                        IdPagamento = pagamentoEsiste.ID,
                        IDCorriere = corriereEsiste.ID,
                        IndirizzoSpedizione = nuovoOrdine.IndirizzoSpedizione
                    };

                    this.Ordines.Add(NuovoOrdine);

                    this.SaveChanges();

                    foreach (Prodotto Prodotto in nuovoOrdine.Prodotti)
                    {
                        Prodotto ProdottoEsiste = (
                            from p in Prodottos
                            where p.ID == Prodotto.ID
                            select p
                        ).SingleOrDefault();

                        if (ProdottoEsiste == null)
                            throw new Exception("Il prodotto selezionato non esiste");

                        if (ProdottoEsiste.Quantita < Prodotto.Quantita)
                            throw new Exception("La quantità selezionata è maggiore di quella disponibile in magazzino");

                        ProdottoEsiste.Quantita -= Prodotto.Quantita;

                        Ordiniprodotti DettaglioOrdine = new Ordiniprodotti()
                        {
                            IDOrdine = NuovoOrdine.ID,
                            IDProdotto = ProdottoEsiste.ID,
                            Quantita = Prodotto.Quantita,
                        };

                        this.Ordiniprodottis.Add(DettaglioOrdine);
                    }

                    this.SaveChanges();

                    //Email
                    try
                    {
                        //Autenticazione email
                        var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();
                        var emailConfig = config.GetSection("EmailConfiguration");
                        var from = emailConfig["From"];
                        var password = emailConfig["Password"];
                        var host = emailConfig["Host"];
                        var port = int.Parse(emailConfig["Port"]);
                        var enablessl = bool.Parse(emailConfig["EnableSssl"]);

                        var body = "Dettagli dell'ordine:\n\n";
                        body += $"Utente: {Utente.Nome} {Utente.Cognome}\n";
                        body += $"Indirizzo di spedizione: {nuovoOrdine.IndirizzoSpedizione}\n\n";
                        body += "Prodotti:\n";
                        foreach (Prodotto prodotto in nuovoOrdine.Prodotti)
                        {
                            body += $"{prodotto.Quantita} {prodotto.Nome} {prodotto.Prezzo}€\n";
                        }
                        body += $"Pagamento: {pagamentoEsiste.Nome}\n";
                        body += $"Corriere: {corriereEsiste.Nome}\n";

                        MailMessage mailMessage = new MailMessage();
                        SmtpClient smtpClient = new SmtpClient();
                        mailMessage.From = new MailAddress(from);
                        mailMessage.To.Add(new MailAddress(Email));
                        mailMessage.Subject = "Conferma ordine";
                        mailMessage.Body = body;
                        smtpClient.Port = port;
                        smtpClient.Host = host;
                        smtpClient.EnableSsl = enablessl;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(from, password);
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Timeout = 10000; //impostiamo un timeout di 10 secondi

                        //Autenticazione email
                        smtpClient.SendCompleted += (s, e) =>
                        {
                            if (e.UserState != null)
                            {
                                ((MailMessage)e.UserState).Dispose();
                            }
                        };
                        smtpClient.SendAsync(mailMessage, mailMessage);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("Email non inviata" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new Exception(ex.Message);
                }
            }
        }

        public List<OrdiniUtente> GetOrdini(string Email)
        {
            var StEmail = (
            from u in this.Utentes
            where u.Email == Email
            select u).SingleOrDefault();

            var query = (from o in this.Ordines
                         join u in this.Utentes on o.IDUtente equals u.ID
                         join pa in this.Pagamentos on o.IdPagamento equals pa.ID
                         join c in this.Corrieres on o.IDCorriere equals c.ID
                         join s in this.StatoOrdines on o.IdStato equals s.ID
                         join op in this.Ordiniprodottis on o.ID equals op.IDOrdine
                         join p in this.Prodottos on op.IDProdotto equals p.ID
                         where u.ID == StEmail.ID
                         select new OrdiniUtente()
                         {
                             ID = o.ID,
                             Data = o.Data,
                             IndirizzoSpedizione = o.IndirizzoSpedizione,
                             Quantita = op.Quantita,
                             NomePagamento = pa.Nome,
                             NomeCorriere = c.Nome,
                             NomeStato = s.Descrizione,
                             Commento = o.Commento,
                             Prodotti = (from op in Ordiniprodottis
                                         join p in this.Prodottos on op.IDProdotto equals p.ID
                                         where op.IDOrdine == o.ID
                                         select new Prodotto
                                         {
                                             ID = p.ID,
                                             Nome = p.Nome,
                                             Quantita = p.Quantita,
                                             Prezzo = p.Prezzo,
                                             Rating = p.Rating,
                                             IDCategoria = p.IDCategoria,
                                         }).ToList(),
                         })
                         .GroupBy(o => o.ID)
                         .Select(g => new OrdiniUtente
                         {
                             ID = g.Key,
                             Data = g.First().Data,
                             IndirizzoSpedizione = g.First().IndirizzoSpedizione,
                             Quantita = g.Sum(o => o.Quantita),
                             NomePagamento = g.First().NomePagamento,
                             NomeCorriere = g.First().NomeCorriere,
                             NomeStato = g.First().NomeStato,
                             Commento = g.First().Commento,
                             Prodotti = g.SelectMany(o => o.Prodotti)
                                         .GroupBy(p => p.ID)
                                         .Select(pg => new Prodotto
                                         {
                                             ID = pg.Key,
                                             Nome = pg.First().Nome,
                                             Quantita = pg.Sum(p => p.Quantita),
                                             Prezzo = pg.First().Prezzo,
                                             Rating = pg.First().Rating,
                                             IDCategoria = pg.First().IDCategoria,
                                         }).ToList()
                         }).ToList();
            return query;
        }

    public int DeleteOrderByUser(int idOrdine, string email)
        {
            // Verifica utente
            var utente = this.Utentes.SingleOrDefault(u => u.Email == email);
            if (utente == null)
            {
                throw new Exception("Utente non trovato, credenziali errate");
            }
            else
            {
                // Verifica ordine non esistente
                var ordine = this.Ordines.SingleOrDefault(o => o.ID == idOrdine);
                if (ordine == null)
                {
                    throw new Exception("Ordine da rimuovere indicato non esistente");
                }
                else
                {
                    //Rimozione ordine
                    this.Ordines.Remove(ordine);
                    this.SaveChanges();
                    return idOrdine;
                }
            }
        }

        public int DeleteOrder(int idOrdine, string Email)
        {
            var StEmail = (
               from u in this.Utentes
               where u.Email == Email
               select u).SingleOrDefault();

                // Verifica ordine non esistente
                var ordine = this.Ordines.SingleOrDefault(o => o.ID == idOrdine);
                if (ordine == null)
                {
                    throw new Exception("Ordine da rimuovere indicato non esistente");
                }
                else if (ordine.IdStato != 1)
                {
                    throw new Exception("L'ordine può essere cancellato solo se non è stato spedito");
                }
                {
                    //Rimozione ordine
                    this.Ordines.Remove(ordine);
                    this.SaveChanges();
                    return idOrdine;
                }
        }

        public int UpdateProduct(Prodotto prodotto)
        { 

            // Verifica prodotto non esistente
            var productToUpdate = this.Prodottos.SingleOrDefault(p => p.ID == prodotto.ID);
            if (prodotto == null)
            {
                throw new Exception("Il prodotto da apportare modifiche selezionato non esiste");
            }

            // Modifica prodotto
            productToUpdate.Quantita = prodotto.Quantita;
            productToUpdate.Nome = prodotto.Nome;
            productToUpdate.Prezzo = prodotto.Prezzo;
            productToUpdate.Rating = prodotto.Rating;

            this.SaveChanges();
            return prodotto.ID;
        }


        public int DeleteProduct(int idProdotto)
        {
            // Verifica prodotto non esistente
            var prodotto = this.Prodottos.SingleOrDefault(p => p.ID == idProdotto);
            if (prodotto == null)
            {
                throw new Exception("Il prodotto da rimuovere indicato non trovato, riprova");
            }
            else
            {
                // Rimozione prodotto
                this.Prodottos.Remove(prodotto);
                this.SaveChanges();
                return idProdotto;
            }
        }

        public int UpdateOrder(int idOrdine, int idStato)
        {
            // Verifica ordine non esistente
            var ordine = this.Ordines.SingleOrDefault(o => o.ID == idOrdine);
            if (ordine == null)
            {
                throw new Exception("L'ordine indicato da apportare modifiche non trovato, riprova");
            }
            else
            {
                // Verifica stato 
                var stato = this.StatoOrdines.SingleOrDefault(s => s.ID == idStato);
                if (stato == null)
                {
                    throw new Exception("Lo stato indicato non esiste");
                }
                else
                {
                    // Modifica ordine
                    ordine.IdStato = stato.ID;
                    this.SaveChanges();
                    return ordine.ID;
                }
            }
        }

        public List<Corriere> GetAllCorrieri()
        {
            return this.Corrieres.ToList();
        }

        public List<Pagamento> GetAllPagamenti()
        {
            return this.Pagamentos.ToList();
        }

        public List<Categoria> GetAllCategorie()
        {
            return this.Categorias.ToList();
        }

        public Utente GetUtenteByID(string Email)
        {
            using (var dbContext = new ECommerceDBContext(connString))
            {
                var StEmail = (
               from u in this.Utentes
               where u.Email == Email
               select u).SingleOrDefault();

                var utente = dbContext.Utentes
                    .Include(u => u.Ordines)
                    .FirstOrDefault(u => u.ID == StEmail.ID);

                return utente;
            }
        }

        public List<Prodotto> SearchProductsByName(string nomeProdotto)
        {
            if (string.IsNullOrEmpty(nomeProdotto))
            {
                throw new ArgumentException("Il campo non può essere vuoto");
            }

            var query = this.Prodottos.AsQueryable();

            // Ricerca prodotto per nome
            query = query.Where(p => p.Nome.Contains(nomeProdotto));

            // Verifica prodotto non esistente
            if (!query.Any())
            {
                throw new ArgumentException("Nome del prodotto cercato non è valido");
            }
            return query.ToList();
        }

        public List<Prodotto> SearchProductByCategory(string nomeCategoria)
        {
            if (string.IsNullOrEmpty(nomeCategoria))
            {
                throw new ArgumentException("Il campo non può essere vuoto");
            }

            var query = this.Prodottos
                .Where(p => p.IDCategoriaNavigation.Nome.Equals(nomeCategoria))
                .ToList();

            // Verifica categoria non esistente
            if (!query.Any())
            {
                throw new ArgumentException("Categoria cercata non è valida");
            }

            return query.ToList();
        }
    }
}
