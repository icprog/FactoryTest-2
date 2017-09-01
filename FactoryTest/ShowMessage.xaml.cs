using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FactoryTest
{
    /// <summary>
    /// ShowMessage.xaml 的交互逻辑
    /// </summary>
    public delegate void CloseWindowsHandler(bool state);
    public partial class ShowMessage : Window
    {
        
        public event CloseWindowsHandler CloseWindowsEvent;
        private DispatcherTimer AutoCloseWindows = new DispatcherTimer();
        public ShowMessage(bool state, String ShowMessages,UInt32 sec)
        {
            InitializeComponent();
            if (sec == 0)
            {
                if (state)
                {
                    btn_OK.Visibility = Visibility.Visible;
                    btn_CANCEL.Visibility = Visibility.Visible;
                    btn_ENTER.Visibility = Visibility.Hidden;
                }
                else
                {
                    btn_OK.Visibility = Visibility.Hidden;
                    btn_CANCEL.Visibility = Visibility.Hidden;
                    btn_ENTER.Visibility = Visibility.Visible;
                }
            }
            
            
            else if(sec>0)
            {
                btn_OK.Visibility = Visibility.Hidden;
                btn_CANCEL.Visibility = Visibility.Hidden;
                btn_ENTER.Visibility = Visibility.Hidden;
                AutoCloseWindows.Interval = new TimeSpan(0, 0, 0, Convert.ToInt32(sec) , 0);
                AutoCloseWindows.Tick += new EventHandler(AutoCloseWindows_Tick);
                AutoCloseWindows.Start();
            }
            label_SHOW.Content = ShowMessages;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void AutoCloseWindows_Tick(object sender, EventArgs e)
        {
            AutoCloseWindows.Stop();
            AutoCloseWindows.Tick -= AutoCloseWindows_Tick;
            CloseWindowsEvent(true);



            this.Close();
        }

        private void btn_CANCEL_Click(object sender, RoutedEventArgs e)
        {

            CloseWindowsEvent(false);


           
            this.Close();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            CloseWindowsEvent(true);



            this.Close();
        }

        private void btn_ENTER_Click(object sender, RoutedEventArgs e)
        {
            CloseWindowsEvent(true);



            this.Close();
        }
    }
}
