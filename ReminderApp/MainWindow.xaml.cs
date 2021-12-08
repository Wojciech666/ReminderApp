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
            else if(!System.Text.RegularExpressions.Regex.IsMatch(TextboxTimePerdiod.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Type time period as decimal integer in minutes!", "Only numbers!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ButtonSet.IsEnabled = false;
                period = Int32.Parse(TextboxTimePerdiod.Text);
                popUpInfo = TextboxInformation.Text;

                PopupTimeSetting();
            }
            
        }

        private void ClearTextboxes()
        {
            TextboxInformation.Clear();
            TextboxTimePerdiod.Clear();
            period = 0;
            popUpInfo = "";
        }

        private void PopupTimeSetting()
        {
            TimeStruct timeToSet; 
            popupTimer = new System.Windows.Threading.DispatcherTimer();
            timeToSet = MinutesToHourConverter(period);
            popupTimer.Interval = new TimeSpan(timeToSet.hours, timeToSet.minutes,0);
            popupTimer.IsEnabled = true;
            popupTimer.Tick += new EventHandler(PopupTimerTick);
        }

        void PopupTimerTick(object sender, EventArgs e)
        {
            popupTimer.IsEnabled = false;
            PopUp popUp = new PopUp(popUpInfo);
            popUp.ShowDialog();
            bool status = popUp.End;                        

            if (status)
            {
                PopupTimeSetting();
            }
            else
            {
                ButtonSet.IsEnabled = true;
                ClearTextboxes();
            }
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
