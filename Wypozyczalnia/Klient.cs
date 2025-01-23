using System.Collections.Generic;

namespace WypozyczalniaSprzetu
{

    public class NieprawidlowyPeselException : Exception
    {
        public NieprawidlowyPeselException(string message)
            : base(message) { }
    }
        public class Klient
    {
        private string pesel;
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PESEL
        {
            get => pesel;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 11)
                {
                    throw new NieprawidlowyPeselException($"PESEL musi mieć dokładnie 11 znaków. Podano: {value}");
                }
                pesel = value;
            }
        }
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
