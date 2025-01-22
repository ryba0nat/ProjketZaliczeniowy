using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WypozyczalniaSprzetu;

namespace WypozyczalniaGUI
{
    public partial class WypozyczalniaWindow : Window
    {
        private SprzetManager sprzetManager = new SprzetManager();
        private WypozyczenieManager wypozyczenieManager;
        private KlientManager klientManager = new KlientManager();

        private List<SprzetNarciarski> listaSprzetu; // 🔹 Poprawna deklaracja listy sprzętu

        public WypozyczalniaWindow()
        {
            InitializeComponent();
            listaSprzetu = DataManager.WczytajSprzet(); //  Wczytujemy sprzęt z XML
            sprzetManager = new SprzetManager(); // 🔹 Inicjalizacja SprzetManager
            wypozyczenieManager = new WypozyczenieManager(sprzetManager); // 🔹 Przekazujemy SprzetManager
            OdswiezListe();
        }


        private void DodajSprzet_Click(object sender, RoutedEventArgs e)
        {
            var oknoDodajSprzet = new DodajSprzetWindow(sprzetManager);
            if (oknoDodajSprzet.ShowDialog() == true)
            {
                OdswiezListe(); // ✔ Po dodaniu sprzętu odświeżamy GUI
            }
        }

        private void UsunSprzet_Click(object sender, RoutedEventArgs e)
        {
            if (ListaSprzetu.SelectedItem == null)
            {
                MessageBox.Show("Nie wybrano sprzętu do usunięcia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int indeks = ListaSprzetu.SelectedIndex;
            var listaSprzetu = sprzetManager.GetSprzet();
            if (indeks < 0 || indeks >= listaSprzetu.Count)
            {
                MessageBox.Show("Błąd: Nieprawidłowy wybór sprzętu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SprzetNarciarski sprzetDoUsuniecia = listaSprzetu[indeks];

            MessageBoxResult result = MessageBox.Show($"Czy na pewno chcesz usunąć {sprzetDoUsuniecia.Opis()}?",
                                             "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
                return;
            sprzetManager.UsunSprzet(indeks); // ✔ Usuwamy sprzęt z menedżera
            OdswiezListe(); // ✔ Odświeżamy widok
            DataManager.ZapiszSprzet(sprzetManager.GetSprzet());
            //listaSprzetu.RemoveAt(indeks);
            //OdswiezListe();
            //DataManager.ZapiszSprzet(listaSprzetu); // 🔹 Aktualizacja XML po usunięciu
        }



        private void WypozyczSprzet_Click(object sender, RoutedEventArgs e)
        {
            if (ListaSprzetu.SelectedItem == null)
            {
                MessageBox.Show("Wybierz sprzęt do wypożyczenia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int indeks = ListaSprzetu.SelectedIndex;
            var listaSprzetu = sprzetManager.GetSprzet();
           


            if (indeks < 0 || indeks >= listaSprzetu.Count)
                {
                    MessageBox.Show("Błąd: Nieprawidłowy wybór sprzętu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                SprzetNarciarski sprzet = listaSprzetu[indeks]; // ✔ Teraz indeks jest bezpieczny

                var okno = new WybierzKlientaWindow(klientManager);
                if (okno.ShowDialog() == true)
                {
                    wypozyczenieManager.WypozyczSprzet(sprzet.ID, okno.WybranyKlient);
                    MessageBox.Show($"Sprzęt {sprzet.ID} został wypożyczony przez {okno.WybranyKlient.Imie} {okno.WybranyKlient.Nazwisko}.",
                                    "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                    OdswiezListe();
                    DataManager.ZapiszSprzet(sprzetManager.GetSprzet()); // Zapisujemy zmiany do XML
                }
 
        }





        private void OdswiezListe()
        {
            ListaSprzetu.Items.Clear();
            var listaSprzetu = sprzetManager.GetSprzet(); // Pobranie sprzętu

            if (listaSprzetu.Count == 0) // 🔹 Jeśli lista jest pusta, wyświetlamy komunikat
            {
                ListaSprzetu.Items.Add("Brak dostępnego sprzętu.");
                return;
            }

            foreach (var sprzet in listaSprzetu)
            {
                string statusWypozyczenia = sprzet.CzyWypozyczony ? "Tak" : "Nie";
                string klientInfo = sprzet.CzyWypozyczony ? sprzet.Wypozyczajacy : "-";

                ListaSprzetu.Items.Add($"ID: {sprzet.ID}, Rodzaj: {sprzet.Rodzaj}, Rozmiar: {sprzet.Rozmiar}, " +
                                       $"Stan: {sprzet.StanTechniczny}, Wypożyczony: {statusWypozyczenia}, Klient: {klientInfo}, " +
                                       $"Typ: {(sprzet is Narty ? ((Narty)sprzet).TypNart : ((Snowboard)sprzet).TypSnowboardu)}");
            }
        }
    }
}
