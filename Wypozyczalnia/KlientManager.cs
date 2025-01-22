using System.Collections.Generic;

namespace WypozyczalniaSprzetu
{
    public class KlientManager
    {
        private List<Klient> ListaKlientow { get; } = new List<Klient>();

        public void DodajKlienta(Klient klient)
        {
            ListaKlientow.Add(klient);
        }

        public void UsunKlienta(string pesel)
        {
            Klient klient = ListaKlientow.Find(k => k.PESEL == pesel);
            if (klient != null && klient.HistoriaWypozyczen.Count == 0)
            {
                ListaKlientow.Remove(klient);
            }
        }

        public Klient ZnajdzKlientaPoPesel(string pesel)
        {
            return ListaKlientow.Find(k => k.PESEL == pesel);
        }

        public List<Klient> GetKlienci()
        {
            return ListaKlientow;
        }
    }
}
