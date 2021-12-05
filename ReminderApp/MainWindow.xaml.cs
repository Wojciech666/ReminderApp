using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer popupTimer;
        DateTime Time;
        int period;
        string popUpInfo;
        struct TimeStruct
        {
            public int minutes;
            public int hours;
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            if (TextboxInformation.Text == "" || TextboxTimePerdiod.Text == "")
            {
                MessageBox.Show("The input fields cannot be empty!", "Empty fileds", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Time = DateTime.Now;
                period = Int32.Parse(TextboxTimePerdiod.Text);
                popUpInfo = TextboxInformation.Text;

                TimeStruct timeToSet;
                popupTimer = new System.Windows.Threading.DispatcherTimer();
                timeToSet = MinutesToHourConverter(period);
                popupTimer.Interval = new TimeSpan(0, timeToSet.hours, timeToSet.minutes);
                popupTimer.IsEnabled = true;
                popupTimer.Tick += new EventHandler(popupTimer_Tick);
            }
            
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            TimeStruct timeToSet; 
            popupTimer = new System.Windows.Threading.DispatcherTimer();
            timeToSet = MinutesToHourConverter(period);
            popupTimer.Interval = new TimeSpan(0,timeToSet.hours, timeToSet.minutes);
            popupTimer.IsEnabled = true;
            popupTimer.Tick += new EventHandler(popupTimer_Tick);
        }

        void popupTimer_Tick(object sender, EventArgs e)
        {
            popupTimer.IsEnabled = false;
            PopUp popUp = new PopUp(popUpInfo);
            popUp.Show();
            if (!popUp.IsVisible)
            {
                popUp.Show();
            }

            if (popUp.WindowState == WindowState.Minimized)
            {
                popUp.WindowState = WindowState.Normal;
            }

            popUp.Activate();
            popUp.Topmost = true;  // important
            popUp.Topmost = false; // important
            popUp.Focus();         // important
        }
      
        private bool TextboxValidator(string inputText)
        {
            if (new Regex(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(inputText))
                return true;
            else
                return false;
        }

        private TimeStruct MinutesToHourConverter(int period)
        {
            TimeStruct time;
            if (period < 60)
            {
                time.hours = 0;
                time.minutes = period;
                return time;
            }
            else if (period >= 60)
            {
                time.hours = period / 60;
                time.minutes = period % 60;
                return time;
            }
            else 
            {
                time.hours = 0;
                time.minutes = 0;
                return time;
            }
        }
    }
}
