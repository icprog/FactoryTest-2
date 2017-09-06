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
using TestTextBoxWaterMark;
namespace FactoryTest
{
    /// <summary>
    /// SettingWindows.xaml 的交互逻辑
    /// </summary>
    public delegate void CloseSettingWindowsHandler();
    public partial class SettingWindows : Window
    {
        public event CloseSettingWindowsHandler CloseWindowsEvent;
        int MinLevel_lastindex = 0, MaxLevel_lastindex=1;
        public SettingWindows()
        {
            InitializeComponent();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

           
            Properties.Settings.Default.Save();
            CloseWindowsEvent();
            this.Close();
        }

       

        private void MinLevel_DropDownClosed(object sender, EventArgs e)
        {
            if (MinLevel.SelectedIndex > MaxLevel.SelectedIndex)
            {
                Bat_LevelWarring.Content = "最小电量应该小于等于最大电量 ！";
                MinLevel.SelectedIndex = MinLevel_lastindex;
            }
        }

        private void MinLevel_DropDownOpened(object sender, EventArgs e)
        {
            Bat_LevelWarring.Content = "";
            MinLevel_lastindex = MinLevel.SelectedIndex;
        }

        private void MaxLevel_DropDownClosed(object sender, EventArgs e)
        {
            if (MinLevel.SelectedIndex > MaxLevel.SelectedIndex)
            {
                Bat_LevelWarring.Content = "最大电量应该大于等于最小电量 ！";
                MaxLevel.SelectedIndex = MaxLevel_lastindex;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if(e.Cancel)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaxLevel_DropDownOpened(object sender, EventArgs e)
        {
            Bat_LevelWarring.Content = "";
            MaxLevel_lastindex = MaxLevel.SelectedIndex;
        }
    }

    
    }
    
