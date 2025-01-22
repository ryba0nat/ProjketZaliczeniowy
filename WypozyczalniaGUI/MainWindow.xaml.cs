using System.Windows;

namespace WypozyczalniaGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Otwieranie okna Wypożyczalnia
        private void Wypozyczalnia_Click(object sender, RoutedEventArgs e)
        {
            WypozyczalniaWindow wypozyczalniaOkno = new WypozyczalniaWindow();
            wypozyczalniaOkno.Show();
        }

        // Otwieranie okna Karnety
        private void Karnety_Click(object sender, RoutedEventArgs e)
        {
            KarnetyWindow karnetyOkno = new KarnetyWindow();
            karnetyOkno.Show();
        }

        // Zamknięcie aplikacji
        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
