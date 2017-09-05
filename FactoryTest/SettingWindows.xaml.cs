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

namespace FactoryTest
{
    /// <summary>
    /// SettingWindows.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindows : Window
    {
        private String lastwifi_rssi = "";
        public SettingWindows()
        {
            InitializeComponent();
           
        }
        
        private bool Prepareushort(ref string text)
        {
            int i = 0;
            if (text.Length > 5)
                return false;


            string t = "";

            foreach (char c in text)
            {
                i++;
                if (c >= '0' && c <= '9')
                {
                    if (Convert.ToUInt16(t) <= 65535)
                        t += c;
                    if (t.Length > 5) return false;
                    if (Convert.ToUInt32(t) >= 65535)
                        t = t.Remove(t.IndexOf(c), 1);
                }
            }



            if (t.Length > 5)
                return false;
            text = t;
            return true;
        }

        private void WIFI_RSSI_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }

    
    }
    
