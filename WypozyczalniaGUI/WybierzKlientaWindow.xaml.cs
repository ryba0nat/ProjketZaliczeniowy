using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WypozyczalniaSprzetu;

namespace WypozyczalniaGUI
{
    public partial class WybierzKlientaWindow : Window
    {
        private KlientManager klientManager;
        public Klient WybranyKlient { get; private set; }

        public WybierzKlientaWindow(KlientManager klientManager)
        {
            InitializeComponent();
            this.klientManager = klientManager;
            OdswiezListe();
        }

        private void OdswiezListe()
        {
            ListaKlientow.Items.Clear();
            foreach (var klient in klientManager.GetKlienci())
            {
                ListaKlientow.Items.Add($"{klient.Imie} {klient.Nazwisko} - {klient.PESEL}");
            }
        }

        private void WybierzKlienta_Click(object sender, RoutedEventArgs e)
        {
            if (ListaKlientow.SelectedItem == null)
            {
                MessageBox.Show("Wybierz klienta z listy!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int index = ListaKlientow.SelectedIndex;
            WybranyKlient = klientManager.GetKlienci()[index];

            DialogResult = true;
            Close();
        }

        private void DodajKlienta_Click(object sender, RoutedEventArgs e)
        {
            var oknoDodaj = new DodajKlientaWindow(klientManager);
            if (oknoDodaj.ShowDialog() == true)
            {
                OdswiezListe();
            }
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
