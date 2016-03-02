using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAzure.MobileServices;

namespace BioGasSense
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MobileServiceClient MobileService = new MobileServiceClient(
    "https://biogassense.azure-mobile.net/",
    "GkAsiFhcjUnbBvdHTdwpQwGTAramRa77"
);
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            GetAllPorts();
            btnBrowse.IsEnabled = false;
            btnMeasure.IsEnabled = false;
        }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int ch = 0;
        private int InitializeCOMPorts()
        {
            int serial = 1;
            try
            {
                _serialPort = new SerialPort(COMPortSelected, 9600, Parity.None, 8, StopBits.One)
                {
                    Handshake = Handshake.None,
                    ReadTimeout = 1000,
                    WriteTimeout = 500
                };
                _serialPort.Open();
                _serialPort.Close();
                _serialPort.Dispose();
                return serial;
            }
            catch { }
            return -1;
        }
        private void btnMeasure_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop(); dispatcherTimer.IsEnabled = false;
            _serialPort.Close();
            _serialPort.Dispose();
            btnMeasure.IsEnabled = false;
            btnStop.IsEnabled = true;
            readings = "";
            txtValue.Text = "";
            dispatcherTimer.Interval = new TimeSpan(0, 0, 60);
            dispatcherTimer.IsEnabled = true;
            dispatcherTimer.Start();
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            int serial = InitializeCOMPorts();
            if (serial == -1)
            {
                System.Windows.MessageBox.Show("Device is not connected at " + COMPortSelected + " !!", "Error");
                btnBrowse.IsEnabled = false;
            }
            else { btnBrowse.IsEnabled = true; InitSerialPort(COMPortSelected); }
            btnMeasure.IsEnabled = false;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
           ReceiveData();
        }
        private SerialPort _serialPort;
        public void GetAllPorts()
        {
            List<String> allPorts = new List<String>();
            foreach (String portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                allPorts.Add(portName);
            }
            COMPort.ItemsSource = allPorts;
        }
        private void InitSerialPort(string serial)
        {
            Serials ob = new Serials(serial);
            _serialPort = ob.getSerialPort();
        }
        string readings = ""; string[] ar;
        private void ReceiveData()
        {
            _serialPort.Close();
            _serialPort.Dispose();
            if (!_serialPort.IsOpen)
                _serialPort.Open();
            readings = _serialPort.ReadTo("\n");
            readings=readings.Replace("\n", "").Replace("\r","");
            ar = readings.Split(' ');
            if (ar.Length != 5) return;
            new Thread(() =>
            {
                appendFile(); txtValue.Dispatcher.BeginInvoke((Action)(() => txtValue.Text = "Sensor 1:" + ar[0] + "\nSensor 2:" + ar[1] + "\nSensor 3:" + ar[2] + "\nSensor 4:" + ar[3] + "\nSensor 5:" + ar[4] ));
            }).Start();
            _serialPort.Close();
            _serialPort.Dispose();
        }
        private async void appendFile()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(file1))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "," + readings.Replace(" ",","));
                }
            }
            catch (Exception e)
            { }
            try
            {
                BioGasSensor item = new BioGasSensor { sensor1 = ar[0], sensor2 = ar[1], sensor3 = ar[2], sensor4 = ar[3],sensor5 = ar[4] };
                await MobileService.GetTable<BioGasSensor>().InsertAsync(item);
            }
            catch(Exception e)
            { }
        }
        string file1;
        string COMPortSelected = "COM4";
        private void COMPort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            COMPortSelected = COMPort.SelectedItem.ToString();
            int serial = InitializeCOMPorts();
            if (serial == -1)
            {
                System.Windows.MessageBox.Show("Device is not connected at " + COMPortSelected + " !!", "Error");
                btnBrowse.IsEnabled = false;
            }
            else { btnBrowse.IsEnabled = true; InitSerialPort(COMPortSelected); }
            btnMeasure.IsEnabled = false;
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop(); dispatcherTimer.IsEnabled = false;
            _serialPort.Close();
            _serialPort.Dispose();
            btnStop.IsEnabled = false;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                DialogResult result = fbd.ShowDialog();
                txtFile.Text = fbd.SelectedPath;
                file1 = fbd.SelectedPath + "/BioGas.csv";
                if (File.Exists(file1)) { btnMeasure.IsEnabled = true; return; }
                btnMeasure.IsEnabled = true;
            }
            catch { }
        }

    }
}
