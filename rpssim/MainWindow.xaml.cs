using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rpssim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool simRunning = false;

        private int _simRunAmount;

        private string _outputConsole;


        private Timer simTimer;

        public event PropertyChangedEventHandler PropertyChanged;


        public int SimRunAmount
        {
            get
            {
                return _simRunAmount;
            }

            set
            {
                if (value != _simRunAmount)
                {
                    _simRunAmount = value;
                    OnPropertyChanged(nameof(SimRunAmount));
                }
            }
        }

        
        public string OutputConsole
        {
            get
            {
                return _outputConsole;
            }

            set
            {
                if (value != _outputConsole)
                {
                    _outputConsole = value;
                    OnPropertyChanged(nameof(OutputConsole));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private async void RunSim(object sender, RoutedEventArgs e)
        {
            RPSResult results = null; 

            if (SimRunAmount < 1)
            {
                MessageBoxResult result = MessageBox.Show("Must input a valid simulation range", "Range Error", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
            else
            {
                if (!simRunning)
                {
                    simRunning = true;

                    StartAndWaitOnResultConsole();

                    var engine = new RPSEngine();
                    
                    await Task.Run(() =>
                    {
                       results = engine.RunSimulation(SimRunAmount);
                    }); 
                    
                    StopTimer();
                    PrintResults(results);

                    simRunning = false;
                }
            }     
        }

        private void PrintResults(RPSResult result)
        {
            OutputConsole = "Sim finished in: " + result.TimeElapsedDuringSimulation + " milliseconds\n";
            OutputConsole += "Total simulations played: " + result.TotalSimulationsRun + "\n";

            OutputConsole += "\n";

            OutputConsole += "Total player one wins: " + result.TotalPlayerOneWins + "\n";
            OutputConsole += "Total player one losses: " + result.TotalPlayerOneLosses + "\n";

            OutputConsole += "\n";

            OutputConsole += "Total player two wins: " + result.TotalPlayerTwoWins + "\n";
            OutputConsole += "Total player two losses: " + result.TotalPlayerTwoLosses + "\n";

            OutputConsole += "\n";
            
            OutputConsole += "Total rock wins: " + result.RockWinCount + "\n";
            OutputConsole += "Total paper wins: " + result.PaperWinCount + "\n";
            OutputConsole += "Total scissors wins: " + result.ScissorWinCount + "\n";

            OutputConsole += "\n";

            OutputConsole += "Total tie games: " + result.TotalTieGames;
        }

        private void StopTimer()
        {
            simTimer.Enabled = false; 
        }

        private void StartAndWaitOnResultConsole()
        {
            OutputConsole = "Starting Sim ";

            simTimer = new Timer();

            simTimer.Interval = 500;

            simTimer.Elapsed += UpdateConsoleOnTimerEnd;

            simTimer.AutoReset = true;
            simTimer.Enabled = true; 
        }

        private void UpdateConsoleOnTimerEnd(object source, ElapsedEventArgs e)
        {
            OutputConsole += ". ";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
