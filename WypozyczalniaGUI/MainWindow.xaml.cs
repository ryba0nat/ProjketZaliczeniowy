using System.Windows;

namespace WypozyczalniaGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Ustaw rozmiar na 80% szerokości i wysokości
            this.Width = screenWidth ;
            this.Height = screenHeight ;

            // Opcjonalnie ustaw okno na środku ekranu
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;

            
        }

        // Otwieranie okna Wypożyczalnia
        private void Wypozyczalnia_Click(object sender, RoutedEventArgs e)
        {
            WypozyczalniaWindow wypozyczalniaOkno = new WypozyczalniaWindow();
            wypozyczalniaOkno.Owner = this;
            wypozyczalniaOkno.Show();
            
        }

        // Otwieranie okna Karnety
        private void Karnety_Click(object sender, RoutedEventArgs e)
        {
            KarnetyWindow karnetyOkno = new KarnetyWindow();
            karnetyOkno.Owner = this;
            karnetyOkno.Show();
            
        }

        // Zamknięcie aplikacji
        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
