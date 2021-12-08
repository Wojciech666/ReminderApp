using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for PopUp.xaml
    /// </summary>
    public partial class PopUp : Window
    {
        bool endReminding;
        public PopUp(string message)
        {
            InitializeComponent();
            if (this.WindowState == WindowState.Minimized) this.WindowState = WindowState.Normal;
            this.Activate();
            this.Topmost = true; 
            this.Focus();
            TextblockMessage.Text = message;
        }
        public void ShowOverAll()
        {
            if (!this.IsVisible)
            {
                this.Show();
            }

            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }

        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            End = false;
            this.Close();
        }

        public bool End 
        {
            get { return endReminding; }
            private set { endReminding = value; }
        }

        private void ButtonRemindAgain_Click(object sender, RoutedEventArgs e)
        {
            End = true;
            this.Close();
        }
    }
}
