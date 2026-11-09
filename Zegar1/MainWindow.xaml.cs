using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace Zegar
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

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
    }
}