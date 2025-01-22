using System;

namespace WypozyczalniaSprzetu
{
    public class Wypozyczenie
    {
        public SprzetNarciarski Sprzet { get; set; }
        public Klient Klient { get; set; }
        public DateTime DataWypozyczenia { get; set; }
        public DateTime? DataZwrotu { get; set; }

        public Wypozyczenie(SprzetNarciarski sprzet, Klient klient, DateTime dataWypozyczenia)
        {
            Sprzet = sprzet;
            Klient = klient;
            DataWypozyczenia = dataWypozyczenia;
            sprzet.CzyWypozyczony = true;
        }

        public double ObliczKoszt()
        {
            if (DataZwrotu == null) throw new InvalidOperationException("Sprzęt jeszcze nie został zwrócony.");
            var dni = (DataZwrotu.Value - DataWypozyczenia).Days;
            return dni * Sprzet.CenaZaDzien();
        }


    }
}
