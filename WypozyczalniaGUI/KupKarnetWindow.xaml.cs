//using System;
//using System.Windows;
//using System.Windows.Controls;
//using WypozyczalniaSprzetu; // Importujemy klasę Karnet

//namespace WypozyczalniaGUI
//{
//    public partial class KupKarnetWindow : Window
//    {
//        public string Rodzaj { get; private set; }
//        public int CzasTrwania { get; private set; }
//        public double CenaJednostkowa { get; private set; }
//        public bool CzyKupiono { get; private set; } = false;

//        public KupKarnetWindow()
//        {
//            InitializeComponent();
//        }

//        private void Kup_Click(object sender, RoutedEventArgs e)
//        {
//            if (TypKarnetuComboBox.SelectedItem is ComboBoxItem selectedItem)
//            {
//                Rodzaj = selectedItem.Content.ToString();
//            }
//            else
//            {
//                MessageBox.Show("Wybierz rodzaj karnetu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
//                return;
//            }

//            if (!int.TryParse(IloscDniTextBox.Text, out int dni) || dni <= 0)
//            {
//                MessageBox.Show("Podaj poprawną liczbę dni!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
//                return;
//            }

//            if (!double.TryParse(CenaTextBox.Text, out double cena) || cena <= 0)
//            {
//                MessageBox.Show("Podaj poprawną cenę za dzień!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
//                return;
//            }

//            CzasTrwania = dni;
//            CenaJednostkowa = cena;
//            CzyKupiono = true;
//            DialogResult = true;
//            Close();
//        }
//    }
//}

using System;
using System.Windows;
using System.Windows.Controls;
using WypozyczalniaSprzetu; // Importujemy klasy karnetów

namespace WypozyczalniaGUI
{
    public partial class KupKarnetWindow : Window
    {
        public string Rodzaj { get; private set; }
        public int CzasTrwania { get; private set; }
        public double CenaJednostkowa { get; private set; } // 🔹 Dodana właściwość
        public bool CzyKupiono { get; private set; } = false; // 🔹 Dodana właściwość

        public KupKarnetWindow()
        {
            InitializeComponent();
        }

        // Obsługa zmiany rodzaju karnetu
        private void RodzajKarnetuComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (RodzajKarnetuComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Rodzaj = selectedItem.Content.ToString();
                CzasTrwania = Rodzaj == "Dzienny" ? 1 : Rodzaj == "Tygodniowy" ? 7 : 30;
                LiczbaDniTextBox.Text = CzasTrwania.ToString();
                ObliczCene();
            }
        }

        // Obsługa zmiany rodzaju biletu
        private void RodzajBiletuComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (RodzajBiletuComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                CenaJednostkowa = selectedItem.Content.ToString() == "Ulgowy" ? 150 : 200;
                ObliczCene();
            }
        }

        // Obliczanie ceny końcowej
        private void ObliczCene()
        {
            double cenaCalkowita = CzasTrwania * CenaJednostkowa;
            CenaTextBox.Text = cenaCalkowita.ToString("F2"); // Formatowanie do 2 miejsc po przecinku
        }

        // Kupowanie karnetu
        private void Kup_Click(object sender, RoutedEventArgs e)
        {
            if (RodzajKarnetuComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Rodzaj = selectedItem.Content.ToString();
                CzyKupiono = true; // 🔹 Zmieniamy status zakupu
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Wybierz rodzaj karnetu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

