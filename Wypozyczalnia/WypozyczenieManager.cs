using System.Collections.Generic;


namespace WypozyczalniaSprzetu
{
    public class WypozyczenieManager
    {
        private List<Wypozyczenie> ListaWypozyczen { get; } = new List<Wypozyczenie>();
        private SprzetManager sprzetManager; // 🔹 Teraz mamy dostęp do sprzętu

        public WypozyczenieManager(SprzetManager sprzetManager)
        {
            this.sprzetManager = sprzetManager;
        }

        public void DodajWypozyczenie(Wypozyczenie wypozyczenie)
        {
            ListaWypozyczen.Add(wypozyczenie);
        }

        public void ZwrocSprzet(Wypozyczenie wypozyczenie)
        {
            wypozyczenie.DataZwrotu = System.DateTime.Now;
            wypozyczenie.Sprzet.CzyWypozyczony = false;
        }

        public List<Wypozyczenie> WyszukajWypozyczeniaPoKliencie(Klient klient)
        {
            return ListaWypozyczen.FindAll(w => w.Klient == klient);
        }

        public List<Wypozyczenie> WyszukajWypozyczeniaPoSprzecie(SprzetNarciarski sprzet)
        {
            return ListaWypozyczen.FindAll(w => w.Sprzet == sprzet);
        }

        //  Pobiera aktywne wypożyczenia (te, które nie mają ustawionej daty zwrotu)
        public List<Wypozyczenie> GetAktywneWypozyczenia()
        {
            return ListaWypozyczen.FindAll(w => w.DataZwrotu == null);
        }

        

        public void WypozyczSprzet(int idSprzetu, Klient klient)
        {
            SprzetNarciarski sprzet = sprzetManager.ZnajdzSprzetPoID(idSprzetu);

            if (sprzet != null && !sprzet.CzyWypozyczony)
            {
                sprzet.CzyWypozyczony = true;
                sprzet.Wypozyczajacy = $"{klient.Imie} {klient.Nazwisko}"; // 🔹 Zapisujemy klienta

                Wypozyczenie wypozyczenie = new Wypozyczenie(sprzet, klient, DateTime.Now);
                ListaWypozyczen.Add(wypozyczenie);
                klient.DodajWypozyczenie(wypozyczenie);

                //sprzetManager.ZapiszSprzet(); // 🔹 Zapisujemy zmiany w XML
            }
        }

        public void ZwrocSprzet(int idSprzetu)
        {
            Wypozyczenie wypozyczenie = ListaWypozyczen.Find(w => w.Sprzet.ID == idSprzetu && w.DataZwrotu == null);
            if (wypozyczenie != null)
            {
                wypozyczenie.DataZwrotu = DateTime.Now;
                wypozyczenie.Sprzet.CzyWypozyczony = false;
            }
        }

    }
}
