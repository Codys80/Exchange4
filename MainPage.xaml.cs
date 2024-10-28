using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exchange4
{
    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        string waluta;
        public MainPage()
        {
            InitializeComponent();
            DateTime dzis = DateTime.Now;
            Date.MaximumDate = dzis;

        }

        private void EurClicked(object sender, EventArgs e)
        {
            //var picker = (Picker);
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex == -1) return;
            waluta = (string)picker.ItemsSource[selectedIndex];

            string dateToday = Date.Date.ToString("yyyy-MM-dd");
            string url = $"https://api.nbp.pl/api/exchangerates/rates/c/{waluta}/{dateToday}/?format=json";
            string json;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json);
            string s = $"Nazwa waluty: {c.currency} \n"; ;
            s += $"Kod waluty: {c.code}\n";
            s += $"Date: {c.rates[0].effectiveDate}\n";
            s += $"Cena Skupu: {c.rates[0].bid} zł\n";
            s += $"Cena sprzedaży: {c.rates[0].ask} zł\n";
            Head.Text = s;
        }

    }
}