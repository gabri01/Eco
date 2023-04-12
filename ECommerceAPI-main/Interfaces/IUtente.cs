using System;
using Models;

namespace Interfaces
{
	public interface IUtente
	{
        //Login
        public bool EsisteUtente(Login Utente);
        public string Login(Login Utente);

        public bool VerificaUtenteEsistente(string Email);

        //Registrazione
        public long Registrazione(Registrazione NuovoUtente);

        //public bool AutenticazioneUtente(Utente Utente);

        //Modifica Utente
        public bool ModificaUtente(string Nome, string Cognome, string Email);
        public Utente GetUtenteByID(string Email);

    }
}

