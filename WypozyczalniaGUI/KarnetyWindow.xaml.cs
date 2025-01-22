using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WypozyczalniaSprzetu; // Importujemy klasę Karnet i KarnetManager

namespace WypozyczalniaGUI
{
    public partial class KarnetyWindow : Window
    {
        private KarnetManager karnetManager = new KarnetManager();

        public KarnetyWindow()
        {
            InitializeComponent();
            OdswiezListe();
        }

        // Kupowanie karnetu
        private void KupKarnet_Click(object sender, RoutedEventArgs e)
        {
            KupKarnetWindow okno = new KupKarnetWindow();
            if (okno.ShowDialog() == true && okno.CzyKupiono)
            {
                double cenaCalkowita = okno.CenaJednostkowa * okno.CzasTrwania;
                // Tworzymy nowy karnet na podstawie wyboru użytkownika
                Karnet nowyKarnet = new Karnet(okno.Rodzaj, okno.CenaJednostkowa, DateTime.Now, okno.CzasTrwania);
                karnetManager.DodajKarnet(nowyKarnet);
                OdswiezListe();
            }
        }

        // Usuwanie karnetu
        private void UsunKarnet_Click(object sender, RoutedEventArgs e)
        {
            if (ListaKarnetow.SelectedItem == null)
            {
                MessageBox.Show("Wybierz karnet do usunięcia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string wybranyOpis = ListaKarnetow.SelectedItem.ToString();
            Karnet wybranyKarnet = karnetManager.GetKarnety().FirstOrDefault(k => k.ToString() == wybranyOpis);

            if (wybranyKarnet == null)
            {
                MessageBox.Show("Nie znaleziono karnetu na liście!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            karnetManager.GetKarnety().Remove(wybranyKarnet);
            MessageBox.Show($"Karnet {wybranyKarnet.ID} został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            OdswiezListe();
        }

        // Odświeżenie listy karnetów
        private void OdswiezListe()
        {
            ListaKarnetow.Items.Clear();
            foreach (var karnet in karnetManager.GetKarnety())
            {
                ListaKarnetow.Items.Add(karnet.ToString());
            }
        }
    }
}
