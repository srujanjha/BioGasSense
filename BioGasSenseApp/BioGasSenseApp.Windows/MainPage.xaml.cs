using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BioGasSenseApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
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
        Boolean Refresh = false;
        public async void refresh()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri("https://biogas.azure-mobile.net/tables/biogassensor?$top=1&$orderby=__createdAt%20desc"));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue ob = JsonValue.Parse(jstring);
                JsonArray ob1 = ob.GetArray();
                for (int i = 0; i < 5; i++)
                {
                    string s = ob1.GetObjectAt(0).GetNamedString("sensor" + (i + 1));
                    TextBlock txt = (TextBlock)Stack.Items[i];
                    txt.Text = "Sensor #" + (i + 1) + ": " + s;
                }
                Refresh = true;
            }
            catch (Exception) { }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Refresh)
                {
                    refresh(); Refresh = false;
                }
            }
            catch (Exception) { }
        }
        private async Task DoPeriodicWorkAsync(TimeSpan dueTime,
                                       TimeSpan interval,
                                       CancellationToken token)
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
    }
}
