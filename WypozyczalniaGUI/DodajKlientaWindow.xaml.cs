using System;
using System.Windows;
using WypozyczalniaSprzetu;

namespace WypozyczalniaGUI
{
    public partial class DodajKlientaWindow : Window
    {
        private KlientManager klientManager;

        public DodajKlientaWindow(KlientManager klientManager)
        {
            InitializeComponent();
            this.klientManager = klientManager;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ImieTextBox.Text) ||
                string.IsNullOrWhiteSpace(NazwiskoTextBox.Text) ||
                string.IsNullOrWhiteSpace(PeselTextBox.Text))
            {
                MessageBox.Show("Wypełnij wszystkie pola!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nowyKlient = new Klient(ImieTextBox.Text, NazwiskoTextBox.Text, PeselTextBox.Text);
            klientManager.DodajKlienta(nowyKlient);

            DialogResult = true;
            Close();
        }
    }
}
