using System;
using WypozyczalniaSprzetu;

class Program
{
    static void Main()
    {
        //Console.WriteLine("kotek");

        // 1️⃣ Tworzenie menedżerów
        SprzetManager sprzetManager = new SprzetManager();
        WypozyczenieManager wypozyczenieManager = new WypozyczenieManager(sprzetManager);
        KlientManager klientManager = new KlientManager();
        KarnetManager karnetManager = new KarnetManager();

        // 2️⃣ Dodawanie sprzętu
        Narty narty1 = new Narty("Freestyle", 170, "Doskonały", false);
        Snowboard snowboard1 = new Snowboard("Freeride", 160, "Dobry", false);
        sprzetManager.DodajSprzet(narty1);
        sprzetManager.DodajSprzet(snowboard1);

        Console.WriteLine("Lista sprzętu po dodaniu:");
        foreach (var sprzet in sprzetManager.GetSprzet())
        {
            Console.WriteLine(sprzet.Opis());
        }

        // 3️⃣ Dodawanie klienta
        Klient klient1 = new Klient("Jan", "Kowalski", "12345678901");
        klientManager.DodajKlienta(klient1);

        Console.WriteLine("\nLista klientów:");
        foreach (var klient in klientManager.GetKlienci())
        {
            Console.WriteLine($"{klient.Imie} {klient.Nazwisko}, PESEL: {klient.PESEL}");
        }

        // 4️⃣ Wypożyczenie sprzętu po ID
        wypozyczenieManager.WypozyczSprzet(narty1.ID, klient1);
        Console.WriteLine($"\nSprzęt o ID {narty1.ID} został wypożyczony.");

        Console.WriteLine("\nAktywne wypożyczenia:");
        foreach (var wyp in wypozyczenieManager.GetAktywneWypozyczenia())
        {
            Console.WriteLine($"Klient: {wyp.Klient.Imie} {wyp.Klient.Nazwisko} wypożyczył {wyp.Sprzet.Rodzaj} (ID: {wyp.Sprzet.ID}) w dniu {wyp.DataWypozyczenia}");
        }

        // 5️⃣ Zwrot sprzętu po ID
        wypozyczenieManager.ZwrocSprzet(narty1.ID);
        Console.WriteLine($"\nSprzęt o ID {narty1.ID} został zwrócony.");

        // 6️⃣ Sprawdzenie stanu sprzętu po zwrocie
        Console.WriteLine("\nLista sprzętu po zwrocie:");
        foreach (var sprzet in sprzetManager.GetSprzet())
        {
            Console.WriteLine(sprzet.Opis());
        }

        // 7️⃣ Kupowanie karnetów
        Karnet karnet1 = new Karnet("Dzienny", 50, DateTime.Now, 1);
        Karnet karnet2 = new Karnet("Tygodniowy", 200, DateTime.Now, 7);
        Karnet karnet3 = new Karnet("Miesięczny", 600, DateTime.Now, 30);

        klient1.KupKarnet(karnet1);
        klient1.KupKarnet(karnet2);
        klient1.KupKarnet(karnet3);

        karnetManager.DodajKarnet(karnet1);
        karnetManager.DodajKarnet(karnet2);
        karnetManager.DodajKarnet(karnet3);

        Console.WriteLine("\nLista karnetów:");
        foreach (var k in karnetManager.GetKarnety())
        {
            Console.WriteLine(k.ToString());
        }

        // 8️⃣ Usunięcie sprzętu
        sprzetManager.UsunSprzet(0);
        Console.WriteLine("\nLista sprzętu po usunięciu pierwszego elementu:");
        foreach (var sprzet in sprzetManager.GetSprzet())
        {
            Console.WriteLine(sprzet.Opis());
        }

        Console.WriteLine("\nWszystkie testy zakończone pomyślnie.");
    }
}
