using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using USB;
namespace FactoryTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
   
    public partial class MainWindow : Window
    {
        public List<String> port;
        public List<String> nowport;
        public static SerialPort serial = new SerialPort();
        private DispatcherTimer autoDetectionTimer = new DispatcherTimer();
        private Thread thread;
        private Thread FactoryTestThread;
        public int count = 0;
        private int currentstate = 0;
        private string receiveData;
        private static string OKString = "-------------OK",FailString= "-------------FAIL";
        private string receiveText;
        private delegate void MyDelegate(int value);
        private delegate void UpdateUiRecTextDelegate(string text);
        private delegate void UpdateUiSendTextDelegate(string text);
        private delegate void UpdateUiTextColorDelegate(int num);
        private delegate void UpdateUiTextFailDelegate(int num);
        private static String SIM_IMSI_NUM = "460";
        private static String SW_VERSION = "V0.4_201708250950_Debug";
        private static int MAX_LEVEL = 100, MIN_LEVEL = 50, WIFI_RSSI = -55, BLE_RSSI = -75, BLE_MAJOR = 37022, BLE_MINOR=402;
        private static String WIFI_SSID = "TP-LINK_8C22";
        private static int gx = 0, gy = 0, gz = 0,WIFI_SCAN_TIMES=0,MAX_WIFI_SCAN_TIMES=3;
        private bool Rec_state;
        private enum SystemStates
        {
            START_STATE=0,

        };
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;



        }

       
        #region 自动更新串口号
        //自动检测串口名
        private void GetValuablePortName()
        {
            
            string[] serialPortName = System.IO.Ports.SerialPort.GetPortNames();

            foreach (string name in serialPortName)
            {
                
            }
        }

        //自动检测串口时间到
        //private void AutoDectionTimer_Tick(object sender, EventArgs e)
        //{

          

        //    if (btn_Start.IsChecked == true)
        //    {
        //        HIDD_VIDPID[] t = USB.EZUSB.AllVidPid;
        //        if(t!=null)
        //        {
        //            if(t.Length>0)
        //            {
        //                count++;
        //                if(count>=4)
        //                {
        //                    MessageBox.Show("发现设备!");
        //                    autoDetectionTimer.Stop();
        //                }
                        
        //            }
        //        }
        //    }
        //    else
        //    {
               
        //    }
        //}
        #endregion

        private void btn_Start_Checked(object sender, RoutedEventArgs e)
        {
            //autoDetectionTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //autoDetectionTimer.Tick += new EventHandler(AutoDectionTimer_Tick);
            btn_Start.Content = "停止";
            count = 0;
            //autoDetectionTimer.Start();
            thread = new Thread(EndTest);
            thread.Start();
        }

        private void btn_Start_Unchecked(object sender, RoutedEventArgs e)
        {
            btn_Start.Content = "开始";
            if (serial.IsOpen) serial.Close();
            if (thread != null) thread.Abort();
            if (FactoryTestThread != null) FactoryTestThread.Abort();
            //autoDetectionTimer.Stop();
        }
        public  void EndTest()
        {
            int i = 10;
            MyDelegate d = new MyDelegate(AutoDectionTimer_Tick);
            MyDelegate e = new MyDelegate(DectionTimeOut);
            while (i > 0)
            {
                this.Dispatcher.Invoke(d, i);
                i--;
                Thread.Sleep(500);
                //this.Dispatcher.Invoke(d, 0);
                //this.Dispatcher.BeginInvoke(d, 0);
            }
            this.Dispatcher.Invoke(e, i);
            if (thread != null)
                thread.Abort();
            
        }

        private void DectionTimeOut(int value)
        {
            txtDisp.Text = "搜索设备超时\r\n";
            //btn_Start.Checked -= btn_Start_Checked;
            //btn_Start.Unchecked -= btn_Start_Unchecked;
            btn_Start.IsChecked = false;

        }

        private void btn_Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindows sw = new SettingWindows();
            sw.ShowDialog();
        }

        private void AutoDectionTimer_Tick(int vaule)
        {



            if (btn_Start.IsChecked == true)
            {
                HIDD_VIDPID[] t = USB.EZUSB.AllVidPid;
                if (t != null)
                {
                    if (t.Length > 0)
                    {
                        count++;
                        if (count >= 4)
                        {
                            txtDisp.Text = "发现设备！\r\n";
                            if(thread!=null)
                            thread.Abort();
                            try
                            {
                                serial.PortName = t[0].PortName;
                                serial.BaudRate = 115200;
                                serial.DataBits = 8;
                                serial.StopBits = StopBits.One;
                                serial.Parity = Parity.None;
                                serial.ReadTimeout = 500;
                                serial.WriteTimeout = 500;
                                serial.Encoding = Encoding.UTF8;
                                serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Serial_DataReceived);
                                //Thread.Sleep(1000);
                                serial.Open();
                               
                                FactoryTestThread = new Thread(FactoryTestProgress);
                                FactoryTestThread.Start();
                            }
                            catch
                            {
                                txtDisp.Text = "无法连接到设备，请检查后重试！\r\n";
                                btn_Start.IsChecked = false;
                                if (serial.IsOpen) serial.Close();
                            }

                            //autoDetectionTimer.Stop();
                        }
                        else
                        {
                            switch (vaule % 6)
                            {
                                case 5: txtDisp.Text = "正在搜索设备·"; break;
                                case 4: txtDisp.Text = "正在搜索设备··"; break;
                                case 3: txtDisp.Text = "正在搜索设备···"; break;
                                case 2: txtDisp.Text = "正在搜索设备····"; break;
                                case 1: txtDisp.Text = "正在搜索设备·····"; break;
                                case 0: txtDisp.Text = "正在搜索设备······"; break;
                            }
                        }

                    }
                    else
                    {
                        switch (vaule % 6)
                        {
                            case 5: txtDisp.Text = "正在搜索设备·"; break;
                            case 4: txtDisp.Text = "正在搜索设备··"; break;
                            case 3: txtDisp.Text = "正在搜索设备···"; break;
                            case 2: txtDisp.Text = "正在搜索设备····"; break;
                            case 1: txtDisp.Text = "正在搜索设备·····"; break;
                            case 0: txtDisp.Text = "正在搜索设备······"; break;
                        }
                    }
                }
                else
                {
                    switch(vaule%6)
                    {
                        case 5: txtDisp.Text = "正在搜索设备·";break;
                        case 4: txtDisp.Text = "正在搜索设备··"; break;
                        case 3: txtDisp.Text = "正在搜索设备···"; break;
                        case 2: txtDisp.Text = "正在搜索设备····"; break;
                        case 1: txtDisp.Text = "正在搜索设备·····"; break;
                        case 0: txtDisp.Text = "正在搜索设备······"; break;
                    }
                    
                }
            }
            else
            {

            }
        }
 
        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String str= serial.ReadExisting();
            receiveData += str;
            Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiRecTextDelegate(ShowData), str);
        }
        private void ShowData(string text)
        {
             receiveText += text;

            //更新接收字节数
            //receiveBytesCount += (UInt32)receiveText.Length;
            //statusReceiveByteTextBlock.Text = receiveBytesCount.ToString();

            //没有关闭数据显示

            //字符串显示

                Rec_TxtBox.AppendText(text);
                Rec_TxtBox.ScrollToEnd();

           

        }
        private void ShowSendData(string text)
        {
           

            //更新接收字节数
            //receiveBytesCount += (UInt32)receiveText.Length;
            //statusReceiveByteTextBlock.Text = receiveBytesCount.ToString();

            //没有关闭数据显示

            //字符串显示

            txtDisp.AppendText(text);
            txtDisp.ScrollToEnd();



        }
        private void UpdateColor(int num)
        {


            //更新接收字节数
            //receiveBytesCount += (UInt32)receiveText.Length;
            //statusReceiveByteTextBlock.Text = receiveBytesCount.ToString();

            //没有关闭数据显示

            //字符串显示

            switch(num)
            {
                case 1:
                    {
                        String str = state_1.Content.ToString();
                         state_1.Foreground = new SolidColorBrush(Colors.Blue);
                        state_1.Content = str+ OKString;
                    }break;
                case 2:
                    {
                        String str = state_2.Content.ToString();
                        state_2.Foreground = new SolidColorBrush(Colors.Blue);
                        state_2.Content = str+ OKString;
                    }
                    break;
                case 3:
                    {
                        String str = state_3.Content.ToString();
                        state_3.Foreground = new SolidColorBrush(Colors.Blue);
                        state_3.Content = str + OKString;
                    }break;
                case 4:
                    {
                        String str = state_4.Content.ToString();
                        state_4.Foreground = new SolidColorBrush(Colors.Blue);
                        state_4.Content = str + OKString;
                    }
                    break;
                case 5:
                    {
                        String str = state_5.Content.ToString();
                        state_5.Foreground = new SolidColorBrush(Colors.Blue);
                        state_5.Content = str + OKString;
                    }
                    break;
                case 6:
                    {
                        String str = state_6.Content.ToString();
                        state_6.Foreground = new SolidColorBrush(Colors.Blue);
                        state_6.Content = str + OKString;
                    }
                    break;
                case 7:
                    {
                        String str = state_7.Content.ToString();
                        state_7.Foreground = new SolidColorBrush(Colors.Blue);
                        state_7.Content = str + OKString;
                    }
                    break;
                case 8:
                    {
                        String str = state_8.Content.ToString();
                        state_8.Foreground = new SolidColorBrush(Colors.Blue);
                        state_8.Content = str + OKString;
                    }
                    break;
                case 9:
                    {
                        String str = state_9.Content.ToString();
                        state_9.Foreground = new SolidColorBrush(Colors.Blue);
                        state_9.Content = str + OKString;
                    }
                    break;
                case 10:
                    {
                        String str = state_10.Content.ToString();
                        state_10.Foreground = new SolidColorBrush(Colors.Blue);
                        state_10.Content = str + OKString;
                    }
                    break;
                case 11:
                    {
                        String str = state_11.Content.ToString();
                        state_11.Foreground = new SolidColorBrush(Colors.Blue);
                        state_11.Content = str + OKString;
                        ShowMessage sw = new ShowMessage(false, "请平放设备以便测试GSENSOR !",2);
                        sw.Owner = this;
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSubWindows);
                        sw.ShowDialog();
                    }
                    break;
                case 12:
                    {
                        ShowMessage sw = new ShowMessage(false, "请翻转设备 !",0);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSubWindows);
                        sw.ShowDialog();

                    }
                    break;
                case 13:
                    {
                        String str = state_12.Content.ToString();
                        state_12.Foreground = new SolidColorBrush(Colors.Blue);
                        state_12.Content = str + OKString;
                    }
                    break;
                case 14:
                    {
                        ShowMessage sw = new ShowMessage(true, "LED是否闪烁 ？", 0);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSub2Windows);
                        sw.ShowDialog();
                    }
                    break;
                case 15:
                    {
                        String str = state_13.Content.ToString();
                        state_13.Foreground = new SolidColorBrush(Colors.Blue);
                        state_13.Content = str + OKString;
                    }
                    break;
                case 16:
                    {
                        String str = state_14.Content.ToString();
                        state_14.Foreground = new SolidColorBrush(Colors.Blue);
                        state_14.Content = str + OKString;
                    }
                    break;
                case 17:
                    {
                        String str = state_15.Content.ToString();
                        state_15.Foreground = new SolidColorBrush(Colors.Blue);
                        state_15.Content = str + OKString;
                    }
                    break;
                case 18:
                    {
                        ShowMessage sw = new ShowMessage(true, "扬声器是否有声音 ？", 0);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSub2Windows);
                        sw.ShowDialog();
                    }
                    break;
                case 19:
                    {
                        String str = state_16.Content.ToString();
                        state_16.Foreground = new SolidColorBrush(Colors.Blue);
                        state_16.Content = str + OKString;
                    }break;
                case 20:
                    {
                        ShowMessage sw = new ShowMessage(false, "MIC测试开始，2S后将开始录音", 2);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSubWindows);
                        sw.ShowDialog();
                    }
                    break;
                case 21:
                    {
                        ShowMessage sw = new ShowMessage(false, "正在进行录音，录音将持续5S", 5);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSubWindows);
                        sw.ShowDialog();
                    }
                    break;
                case 22:
                    {
                        ShowMessage sw = new ShowMessage(true, "扬声器播放的是否是刚才录制的声音？",0);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSub2Windows);
                        sw.ShowDialog();
                    }
                    break;
                case 23:
                    {
                        String str = state_17.Content.ToString();
                        state_17.Foreground = new SolidColorBrush(Colors.Blue);
                        state_17.Content = str + OKString;
                    }
                    break;
                case 24:
                    {
                        ShowMessage sw = new ShowMessage(true, "是否拨通10086？", 0);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSub2Windows);
                        sw.ShowDialog();
                    }
                    break;
                case 25:
                    {
                        String str = state_18.Content.ToString();
                        state_18.Foreground = new SolidColorBrush(Colors.Blue);
                        state_18.Content = str + OKString;
                    }
                    break;
                case 26:
                    {
                        String str = state_19.Content.ToString();
                        state_19.Foreground = new SolidColorBrush(Colors.Blue);
                        state_19.Content = str + OKString;
                    }
                    break;
                case 27:
                    {
                        String str = state_20.Content.ToString();
                        state_20.Foreground = new SolidColorBrush(Colors.Blue);
                        state_20.Content = str + OKString;
                    }
                    break;
                case 28:
                    {
                        ShowMessage sw = new ShowMessage(true, "设备是否关机", 0);
                        sw.Owner = this;
                        //sw.state = false;
                        //sw.ShowMessages = "请翻转设备！";
                        Rec_state = false;
                        sw.CloseWindowsEvent += new CloseWindowsHandler(RecFromSub2Windows);
                        sw.ShowDialog();
                    }
                    break;
                case 29:
                    {
                        String str = state_21.Content.ToString();
                        state_21.Foreground = new SolidColorBrush(Colors.Blue);
                        state_21.Content = str + OKString;
                    }
                    break;
            }



        }
        void RecFromSubWindows(bool state)
        {
            if (state == true) Rec_state = true;
        }
        void RecFromSub2Windows(bool state)
        {
            if (state == true) Rec_state = true;
            else Rec_state = false ;
        }
        private void UpdateFail(int num)
        {
            //更新接收字节数
            //receiveBytesCount += (UInt32)receiveText.Length;
            //statusReceiveByteTextBlock.Text = receiveBytesCount.ToString();

            //没有关闭数据显示

            //字符串显示

            switch (num)
            {
                case 1:
                    {
                        String str = state_1.Content.ToString();
                        state_1.Foreground = new SolidColorBrush(Colors.Red);
                        state_1.Content = str + FailString;
                    }
                    break;
                case 2:
                    {
                        String str = state_2.Content.ToString();
                        state_2.Foreground = new SolidColorBrush(Colors.Red);
                        state_2.Content = str + FailString;
                    }
                    break;
                case 3:
                    {
                        String str = state_3.Content.ToString();
                        state_3.Foreground = new SolidColorBrush(Colors.Red);
                        state_3.Content = str + FailString;
                    }
                    break;
                case 4:
                    {
                        String str = state_4.Content.ToString();
                        state_4.Foreground = new SolidColorBrush(Colors.Red);
                        state_4.Content = str + FailString;
                    }
                    break;
                case 5:
                    {
                        String str = state_5.Content.ToString();
                        state_5.Foreground = new SolidColorBrush(Colors.Red);
                        state_5.Content = str + FailString;
                        
                    }
                    break;
                case 6:
                    {
                        String str = state_6.Content.ToString();
                        state_6.Foreground = new SolidColorBrush(Colors.Red);
                        state_6.Content = str + FailString;
                    }
                    break;
                case 7:
                    {
                        String str = state_7.Content.ToString();
                        state_7.Foreground = new SolidColorBrush(Colors.Red);
                        state_7.Content = str + FailString;
                    }
                    break;
                case 8:
                    {
                        String str = state_8.Content.ToString();
                        state_8.Foreground = new SolidColorBrush(Colors.Red);
                        state_8.Content = str + FailString;
                    }
                    break;
                case 9:
                    {
                        String str = state_9.Content.ToString();
                        state_9.Foreground = new SolidColorBrush(Colors.Red);
                        state_9.Content = str + FailString;
                    }
                    break;
                case 10:
                    {
                        String str = state_10.Content.ToString();
                        state_10.Foreground = new SolidColorBrush(Colors.Red);
                        state_10.Content = str + FailString;
                    }
                    break;
                case 11:
                    {
                        String str = state_11.Content.ToString();
                        state_11.Foreground = new SolidColorBrush(Colors.Red);
                        state_11.Content = str + FailString;
                    }
                    break;
                case 13:
                    {
                        String str = state_12.Content.ToString();
                        state_12.Foreground = new SolidColorBrush(Colors.Red);
                        state_12.Content = str + FailString;
                    }
                    break;
                case 15:
                    {
                        String str = state_13.Content.ToString();
                        state_13.Foreground = new SolidColorBrush(Colors.Red);
                        state_13.Content = str + FailString;
                    }
                    break;
                case 19:
                    {
                        String str = state_16.Content.ToString();
                        state_16.Foreground = new SolidColorBrush(Colors.Red);
                        state_16.Content = str + FailString;
                    }
                    break;
                case 23:
                    {
                        String str = state_17.Content.ToString();
                        state_17.Foreground = new SolidColorBrush(Colors.Red);
                        state_17.Content = str + FailString;
                    }
                    break;
                case 25:
                    {
                        String str = state_18.Content.ToString();
                        state_18.Foreground = new SolidColorBrush(Colors.Red);
                        state_18.Content = str + FailString;
                    }
                    break;
                case 27:
                    {
                        String str = state_20.Content.ToString();
                        state_20.Foreground = new SolidColorBrush(Colors.Red);
                        state_20.Content = str + FailString;
                    }
                    break;
                case 29:
                    {
                        String str = state_21.Content.ToString();
                        state_21.Foreground = new SolidColorBrush(Colors.Red);
                        state_21.Content = str + FailString;
                    }
                    break;


            }
        }
        private void FactoryTestProgress()
        {
            int i = 120;
            int state = 0;
            while (i>0)
            {
                if (serial.IsOpen)
                {
                    switch(currentstate)
                    {
                        case 0:
                            {
                                SendData(serial, "AT+TEST=TEST_MODE\r\n");
                                currentstate = 1;
                            }
                            break;
                        case 1:
                            {
                                if ((receiveData != "") && (receiveData != null))
                                {
                                    if((receiveData.Contains("Already enter TEST_MODE\r\n")==true)||(receiveData.Contains("\r\n#######################Entering factory test!")==true))
                                    {
                                        currentstate = 2;
                                        state = 1;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                        
                                    }
                                }
                            }break;
                        case 2:
                            {
                                 
                                SendData(serial, "AT+TEST=SIM_READY\r\n");
                                currentstate = 3;
                            }
                            break;
                        case 3:
                            {
                                if (receiveData.Contains("\r\nSIM_READY\r\n") == true)
                                {
                                    currentstate = 4;
                                    state = 2;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 4:
                            {
                                SendData(serial, "AT+TEST=SIM_IMSI\r\n");
                                currentstate = 5;
                            }
                            break;
                        case 5:
                            {
                                if (receiveData.Contains("\r\nSIM_IMSI:"+ SIM_IMSI_NUM) == true)
                                {
                                    currentstate = 6;
                                    state = 3;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 6:
                            {
                                SendData(serial, "AT+TEST=SIM_ICCID\r\n");
                                currentstate = 7;
                            }
                            break;
                        case 7:
                            {
                                if (receiveData.Contains("\r\nWRITE_ICCID SUCESS , SIM_ICCID:") == true)
                                {
                                    currentstate = 8;
                                    state = 4;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 8:
                            {
                                SendData(serial, "AT+TEST=SW_VERSION\r\n");
                                currentstate = 9;
                            }
                            break;
                        case 9:
                            {
                                if (receiveData.Contains("\r\nSW_VERSION:CMCC-DST1A_" ) == true)
                                {
                                    currentstate = 10;
                                    state = 5;
                                    if (receiveData.Contains(SW_VERSION)==true)
                                    {
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                    else
                                    {
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                        if (FactoryTestThread != null) FactoryTestThread.Abort();
                                    }
                                   
                                   
                                }
                            }
                            break;
                        case 10:
                            {
                                SendData(serial, "AT+TEST=BATTERY_LEVEL\r\n");
                                currentstate = 11;
                            }
                            break;
                        case 11:
                            {
                                if (receiveData.Contains("\r\nBATTERY_LEVEL:") == true)
                                {
                                    int len = receiveData.IndexOf("BATTERY_LEVEL:");
                                    String bat = receiveData.Remove(0, len+14);
                                    bat= bat.Remove(bat.IndexOf('%', 1));
                                    Int16 bat_level = Convert.ToInt16(bat);
                                    currentstate = 12;
                                    state = 6;
                                    if ((bat_level >= MIN_LEVEL)&&(bat_level<=MAX_LEVEL))
                                    {
                                       
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                    else
                                    {
                                       
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                        if (FactoryTestThread != null) FactoryTestThread.Abort();
                                    }
                                    
                                }
                            }
                            break;
                        case 12:
                            {
                                SendData(serial, "AT+TEST=WIFI_SCAN\r\n");
                                currentstate = 13;
                            }
                            break;
                        case 13:
                            {
                                if (receiveData.Contains(WIFI_SSID) == true)
                                {
                                    String tmp = receiveData;
                                    int len= tmp.IndexOf("rssi:",(tmp.IndexOf(WIFI_SSID)-17));
                                    String str= tmp.Substring(len + 5, 4);
                                    str = str.Replace(" ", "");
                                    Int16 rssi = Convert.ToInt16(str);
                                    
                                    state = 7;

                                    if (rssi > WIFI_RSSI)
                                    {
                                        currentstate = 14;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                    else
                                    {
                                        if (WIFI_SCAN_TIMES < MAX_WIFI_SCAN_TIMES * 2 * 5)
                                        {
                                            currentstate = 13;
                                        }
                                        else
                                        {

                                            Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                            if (FactoryTestThread != null) FactoryTestThread.Abort();
                                        }
                                        WIFI_SCAN_TIMES++;
                                    }
                                    
                                   

                                }
                                    
                                
                            }
                            break;
                        case 14:
                            {
                                SendData(serial, "AT+TEST=WIFI_OFF\r\n");
                                currentstate = 15;
                            }
                            break;
                        case 15:
                            {
                                if (receiveData.Contains("\r\nWIFI_OFF\r\n") == true)
                                {
                                    currentstate = 16;
                                    state = 8;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                                    
                            }
                            break;
                        case 16:
                            {
                                SendData(serial, "AT+TEST=DEL_TEST_FILE\r\n");
                                currentstate = 17;

                            }
                            break;
                        case 17:
                            {
                                if (receiveData.Contains("\r\nDelet test file sucess\r\n") == true)
                                {
                                    currentstate = 18;
                                    state = 9;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                                    

                            }
                            break;
                        case 18:
                            {
                                SendData(serial, "AT+TEST=BT_SCAN\r\n");
                                currentstate = 19;

                            }
                            break;
                        case 19:
                            {
                                if ((receiveData.Contains(BLE_MAJOR.ToString()) == true)&&(receiveData.Contains(BLE_MINOR.ToString()) == true))
                                {
                                    String tmp = receiveData;
                                    int len = tmp.IndexOf("rssi:", (tmp.IndexOf(BLE_MAJOR.ToString()) - 18));
                                    String str = tmp.Substring(len + 5, 4);
                                    str = str.Replace(" ", "");
                                    Int16 rssi = Convert.ToInt16(str);
                                    if (rssi > BLE_RSSI)
                                    {
                                        currentstate = 20;
                                        state = 10;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                    else
                                    {
                                        state = 10;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    }

                                }


                            }
                            break;
                        case 20:
                            {
                                SendData(serial, "AT+TEST=BT_OFF\r\n");
                                currentstate = 21;

                            }
                            break;
                        case 21:
                            {
                                if (receiveData.Contains("\r\nBT_OFF\r\n") == true)
                                {
                                    currentstate = 22;
                                    state = 11;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }

                            }
                            break;
                        case 22:
                            {
                                if (Rec_state)
                                {
                                    SendData(serial, "AT+TEST=GSENSOR\r\n");
                                    currentstate = 23;
                                }

                            }
                            break;
                        case 23:
                            {
                               if((receiveData.Contains("\r\ngsensor_x:") == true)&& (receiveData.Contains("\r\ngsensor_y:") == true)&&(receiveData.Contains("\r\ngsensor_z:") == true))
                                {
                                    String str = receiveData;
                                    int len=str.IndexOf("gsensor_x:") + 10;
                                    str=str.Substring(len, 3).Replace("\r","");
                                    str = str.Replace("\n", "");
                                    gx = Convert.ToInt32(str);
                                    str = receiveData;
                                    len = str.IndexOf("gsensor_y:") + 10;
                                    str = str.Substring(len, 3).Replace("\r", "");
                                    str = str.Replace("\n", "");
                                    gy = Convert.ToInt32(str);
                                    str = receiveData;
                                    len = str.IndexOf("gsensor_z:") + 10;
                                    str = str.Substring(len, 3).Replace("\r", "");
                                    str = str.Replace("\n", "");
                                    gz = Convert.ToInt32(str);
                                    currentstate = 24;
                                    state = 12;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }

                            }
                            break;
                        case 24:
                            {
                                if(Rec_state)
                                {
                                    SendData(serial, "AT+TEST=GSENSOR\r\n");
                                    currentstate = 25;
                                }
                            }
                            break;
                        case 25:
                            {
                                String str = receiveData;
                                int nx = 0, ny = 0, nz = 0;
                                int len = str.LastIndexOf("gsensor_x:") + 10;
                                str = str.Substring(len, 3).Replace("\r", "");
                                str = str.Replace("\n", "");
                                nx = Convert.ToInt32(str);
                                str = receiveData;
                                len = str.LastIndexOf("gsensor_y:") + 10;
                                str = str.Substring(len, 3).Replace("\r", "");
                                str = str.Replace("\n", "");
                                ny = Convert.ToInt32(str);
                                str = receiveData;
                                len = str.LastIndexOf("gsensor_z:") + 10;
                                str = str.Substring(len, 3).Replace("\r", "");
                                str = str.Replace("\n", "");
                                nz = Convert.ToInt32(str);
                                state = 13;
                                if ((nx==gx)&&(ny==gy)&&(nz==gz))
                                {
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    if (FactoryTestThread != null) FactoryTestThread.Abort();
                                }
                                else
                                {
                                    
                                    
                                    if (((gz>0)&&(nz<0))|| ((gz < 0) && (nz > 0)))
                                    {
                                        currentstate = 26;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                    else
                                    {
                                        state = 12;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                        currentstate = 24;
                                    }
                                }
                               
                                
                            }
                            break;
                        case 26:
                            {
                                SendData(serial, "AT+TEST=LED\r\n");
                                currentstate = 27;
                            }
                            break;
                        case 27:
                            {
                                if(receiveData.Contains("\r\nLED blink start\r\n"))
                                {
                                    state = 14;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    currentstate = 28;
                                }
                            }break;
                        case 28:
                            {
                                if(Rec_state)
                                {
                                    if (receiveData.Contains("\r\nLED blink stop\r\n"))
                                    {
                                        state = 15;
                                        currentstate = 29;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);    
                                    }
                                    
                                }
                                else
                                {
                                    state = 15;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    if (FactoryTestThread != null) FactoryTestThread.Abort();
                                }

                            }
                            break;
                        case 29:
                            {
                                SendData(serial, "AT+TEST=GPS_ON\r\n");
                                currentstate = 30;
                            }
                            break;
                        case 30:
                            {
                               if(receiveData.Contains("latitude:") ==true)
                                {
                                    state = 16;
                                    currentstate = 31;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 31:
                            {
                                SendData(serial, "AT+TEST=GPS_OFF\r\n");
                                currentstate = 32;
                            }
                            break;
                        case 32:
                            {
                                if (receiveData.Contains("\r\nGPS_OFF\r\n") == true)
                                {
                                    state = 17;
                                    currentstate = 33;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 33:
                            {
                                SendData(serial, "AT+TEST=SPEAKER\r\n");
                                currentstate = 34;
                            }
                            break;
                        case 34:
                            {
                                if(receiveData.Contains("\r\nSPEAKER\r\n") == true)
                                {
                                    state = 18;
                                    currentstate = 35;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 35:
                            {
                                if(Rec_state)
                                {
                                    if (receiveData.Contains("\r\nPLAY OVER\r\n") == true)
                                    {
                                        state = 19;
                                        currentstate = 36;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                }
                                else
                                {
                                    state = 19;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    if (FactoryTestThread != null) FactoryTestThread.Abort();
                                }


                                
                            }
                            break;
                        case 36:
                            {
                                state = 20;
                                currentstate = 37;
                                Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                            }
                            break;
                        case 37:
                            {
                                if(Rec_state)
                                {
                                    SendData(serial, "AT+TEST=MIC\r\n");
                                    currentstate = 38;
                                }
                                
                            }
                            break;
                        case 38:
                            {
                                if(receiveData.Contains("\r\nSTART RECORD\r\n") ==true)
                                {
                                    state = 21;
                                    currentstate = 39;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }break;
                        case 39:
                            {
                                if(Rec_state)
                                {
                                    if(receiveData.Contains("\r\nSTART PLAY SOUND")==true)
                                    {
                                        state = 22;
                                        currentstate = 40;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                }
                            }break;
                        case 40:
                            {
                                if (Rec_state)
                                {
                                    if (receiveData.Contains("\r\nDelet record file sucess") == true)
                                    {
                                        state = 23;
                                        if(true)currentstate = 46;
                                        else currentstate = 41;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                }
                                else
                                {
                                    state = 23;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    if (FactoryTestThread != null) FactoryTestThread.Abort();
                                }
                            }
                            break;
                        case 41:
                            {
                                SendData(serial, "AT+TEST=CALL_OUT,10086\r\n");
                                currentstate = 42;
                            }
                            break;
                        case 42:
                            {
                                if (receiveData.Contains("\r\nCALLING 10086")==true)
                                {
                               
                                    state = 24;
                                    currentstate = 43;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }break;
                        case 43:
                            {
                               if(Rec_state)
                                {
                                    state = 25;
                                    currentstate = 44;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                   
                                    
                                }
                               else
                                {
                                    state = 25;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    if (FactoryTestThread != null) FactoryTestThread.Abort();
                                }
                            }break;
                        case 44:
                            {
                                    SendData(serial, "AT+TEST=HANG_UP\r\n");
                                    currentstate = 45;
                            }
                            break;
                        case 45:
                            {
                                if (receiveData.Contains("\r\nHANG_UP\r\n") == true)
                                {
                                    state = 26;
                                    currentstate = 46;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 46:
                            {
                                SendData(serial, "AT+TEST=WRITE_FLAG\r\n");
                                currentstate = 47;
                            }
                            break;
                        case 47:
                            {
                                if (receiveData.Contains("\r\nWRITE_FLAG\r\n"))
                                {
                                    int len = receiveData.LastIndexOf("\r\n");
                                    int sec_len = receiveData.LastIndexOf("\r\n", len - 10);
                                    string sub_string= receiveData.Substring(sec_len, len - sec_len);
                                    if(sub_string.Contains("OK"))
                                    {
                                        state = 27;
                                        currentstate = 48;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                    }
                                    else if(sub_string.Contains("ERROR"))
                                    {
                                        state = 27;
                                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                        if (FactoryTestThread != null) FactoryTestThread.Abort();
                                    }
                                 }
                            }
                            break;
                        case 48:
                            {
                                SendData(serial, "AT+TEST=SHUTDOWN\r\n");
                                currentstate = 49;
                            }
                            break;
                        case 49:
                            {
                                if(receiveData.Contains("START SHUTDOWN") ==true)
                                {
                                    state = 28;
                                    currentstate = 50;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                            }
                            break;
                        case 50:
                            {
                                if(Rec_state)
                                {
                                    state = 29;
                                    currentstate = 51;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextColorDelegate(UpdateColor), state);
                                }
                                else
                                {
                                    state = 29;
                                    currentstate = 51;
                                    Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextFailDelegate(UpdateFail), state);
                                    if (FactoryTestThread != null) FactoryTestThread.Abort();
                                }
                            }break;


                    }
                    
                }
                i--;
                Thread.Sleep(500);
            }

        }
        private void SendData(SerialPort serial, String text)
        {
            try
            {
                if (serial != null)
                {
                    if (serial.IsOpen)
                    {
                        serial.Write(text);
                        //sendBytesCount += (UInt32)text.Length;
                        //statusSendByteTextBlock.Text = sendBytesCount.ToString();
                        Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiSendTextDelegate(ShowSendData), text);
                       
                    }
                }
            }
            catch
            {

            }

        }
       

    }
}
