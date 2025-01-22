using System.Collections.Generic;

namespace WypozyczalniaSprzetu
{
    public class Klient
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PESEL { get; set; }
        public List<Wypozyczenie> HistoriaWypozyczen { get; } = new List<Wypozyczenie>();
        public List<Karnet> HistoriaKarnetow { get; } = new List<Karnet>();

        public Klient(string imie, string nazwisko, string pesel)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            PESEL = pesel;
        }

        public void DodajWypozyczenie(Wypozyczenie wypozyczenie)
        {
            HistoriaWypozyczen.Add(wypozyczenie);
        }

        public void KupKarnet(Karnet karnet)
        {
            HistoriaKarnetow.Add(karnet);
        }
    }
}
