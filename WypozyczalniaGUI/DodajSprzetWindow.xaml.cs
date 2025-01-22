using System;
using System.Windows;
using System.Windows.Controls;
using WypozyczalniaSprzetu; // Importujemy klasy sprzętu

namespace WypozyczalniaGUI
{
    public partial class DodajSprzetWindow : Window
    {
        public string Rodzaj { get; private set; }
        public string TypSprzetu { get; private set; }
        public string Rozmiar { get; private set; }
        public string StanTechniczny { get; private set; }
        public bool CzyDodano { get; private set; } = false;
        private SprzetManager sprzetManager;

        public DodajSprzetWindow(SprzetManager manager)
        {
            InitializeComponent();
            sprzetManager = manager;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzenie wyboru rodzaju sprzętu
            if (RodzajComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Rodzaj = selectedItem.Content.ToString();
            }
            else
            {
                MessageBox.Show("Wybierz rodzaj sprzętu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Sprawdzenie czy użytkownik podał typ sprzętu
            if (string.IsNullOrWhiteSpace(TypSprzetuTextBox.Text))
            {
                MessageBox.Show("Podaj typ sprzętu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(RozmiarTextBox.Text, out int rozmiar))
            {
                MessageBox.Show("Podaj poprawny rozmiar (liczba)!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TypSprzetu = TypSprzetuTextBox.Text;
            //Rozmiar = RozmiarTextBox.Text;
            StanTechniczny = StanTextBox.Text;

            SprzetNarciarski nowySprzet;
            if (Rodzaj == "Narty")
            {
                nowySprzet = new Narty(TypSprzetu, rozmiar, StanTechniczny, false);
            }
            else if (Rodzaj == "Snowboard")
            {
                nowySprzet = new Snowboard(TypSprzetu, rozmiar, StanTechniczny, false);
            }
            else
            {
                MessageBox.Show("Nieznany rodzaj sprzętu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Dodaj sprzęt do SprzetManager
            sprzetManager.DodajSprzet(nowySprzet);
            DataManager.ZapiszSprzet(sprzetManager.GetSprzet()); // ✔ Zapisujemy sprzęt do XML


            CzyDodano = true;

            DialogResult = true;
            Close();
        }
    }
}
