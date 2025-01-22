using System;

namespace WypozyczalniaSprzetu
{
    [Serializable]
    public class Karnet
    {
        private static int liczbaGenerowana = 1; // Licznik numerów w danym dniu
        private static string dzisiejszaData = DateTime.Now.ToString("dd/MM/yyyy"); // Bieżąca data

        public string ID { get; }
        public string Rodzaj { get; set; }
        public double Cena { get; set; }
        public DateTime DataZakupu { get; set; }
        public int CzasTrwania { get; set; } // Liczba dni/godzin

        public Karnet(string rodzaj, double cena, DateTime dataZakupu, int czasTrwania)
        {
            // Jeśli data się zmieniła, resetujemy licznik
            string nowaData = DateTime.Now.ToString("dd/MM/yyyy");
            if (nowaData != dzisiejszaData)
            {
                dzisiejszaData = nowaData;
                liczbaGenerowana = 1;
            }

            ID = $"{dzisiejszaData}/{liczbaGenerowana++}"; // Generowanie ID

            Rodzaj = rodzaj;
            Cena = cena;
            DataZakupu = dataZakupu;
            CzasTrwania = czasTrwania;
        }

        public double ObliczKoszt()
        {
            return Cena * CzasTrwania;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Rodzaj: {Rodzaj}, Data: {DataZakupu.ToShortDateString()}, Czas trwania: {CzasTrwania} dni";
        }
    }
}
