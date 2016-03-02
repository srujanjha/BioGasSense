using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BioGasSenseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public async void refresh()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri("https://biogassense.azure-mobile.net/tables/biogassensor?$top=1&$orderby=__createdAt%20desc"));
                var jstring = await response.Content.ReadAsStringAsync();
                for (int i = 0; i < 5; i++)
                {
                    int ix = jstring.IndexOf("sensor" + (i + 1));
                    int lx = jstring.IndexOf("\"", ix +8);
                    int rx = jstring.IndexOf("\"", lx + 2);
                    string s = jstring.Substring(lx+1,rx-lx-1);
                    TextBlock txt = (TextBlock)Stack.Children[i];
                    txt.Text = "Sensor #" + (i + 1) + ": " + s;
                }
            }
            catch (Exception) { }
        }
        
        private async Task DoPeriodicWorkAsync(TimeSpan dueTime,TimeSpan interval,CancellationToken token)
        {
            // Initial wait time before we begin the periodic loop.
            try
            {
                if (dueTime > TimeSpan.Zero)
                    await Task.Delay(dueTime, token);

                // Repeat this loop until cancelled.
                while (!token.IsCancellationRequested)
                {
                    refresh();
                    // Wait to repeat again.
                    if (interval > TimeSpan.Zero)
                        await Task.Delay(interval, token);
                }
            }
            catch (Exception) { }
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                refresh();
                var dueTime = TimeSpan.FromSeconds(2);
                var interval = TimeSpan.FromSeconds(2);
                await DoPeriodicWorkAsync(dueTime, interval, CancellationToken.None);
            }
            catch (Exception) { }
        }
    }
}
