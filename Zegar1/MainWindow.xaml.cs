using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Zegar1
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        HttpClient klient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            Odswiez();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Odswiez();
        }

        private void Odswiez()
        {
            DateTime teraz = DateTime.Now;
            CultureInfo pl = new CultureInfo("pl-PL");

            GodzinaText.Text = teraz.ToString("HH:mm:ss");
            DataText.Text = teraz.ToString("dddd, d MMMM yyyy", pl);

            TydzienText.Text = "Numer tygodnia: " +
                ISOWeek.GetWeekOfYear(teraz);

        }

        private async void WarszawaButton_Click(object sender, RoutedEventArgs e)
        {
            string adres = "https://api.open-meteo.com/v1/forecast?latitude=52.23&longitude=21.01&current=temperature_2m,relative_humidity_2m";
            await PobierzPogode(adres);
        }

        private async void GdanskButton_Click(object sender, RoutedEventArgs e)
        {
            string adres = "https://api.open-meteo.com/v1/forecast?latitude=54.35&longitude=18.65&current=temperature_2m,relative_humidity_2m";
            await PobierzPogode(adres);
        }
        private async Task PobierzPogode(string adres)
        {
            WynikText.Text = "";
            StatusText.Text = "Pobieram...";

            try
            {
                string json = await klient.GetStringAsync(adres);

                WynikText.Text = json;
                StatusText.Text = "Pobrano o " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                WynikText.Text = "Błąd: " + ex.Message;
                StatusText.Text = "";
            }
        }
    }
}