   M S B A M L   `   `   `  ���� F  BFactoryTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FactoryTestFactoryTest  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ;"clr-namespace:TestTextBoxWaterMarkTestTextBoxWaterMark  ;"clr-namespace:TestTextBoxWaterMarkTestTextBoxWaterMark  ?$clr-namespace:FactoryTest.PropertiesFactoryTest.Properties       FactoryTest.SettingWindows   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:FactoryTest     9System&clr-namespace:System;assembly=mscorlib     CTestTextBoxWaterMark"clr-namespace:TestTextBoxWaterMark     1my"clr-namespace:TestTextBoxWaterMark     2p$clr-namespace:FactoryTest.Properties     -     � Title$  设置��$	��360��$	��480�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���  � 	Resources ��%    .+��� -    ��Name$ Cal_Flag��$$��跳过检测校准完成标志��$
��Left=�$��	10,21,0,0q�$	��Top=� d� 	IsChecked �� %  FactoryTest.Properties.Settings   Default �� Source# Z  �� Path$ Skip_Cal�	 �� Mode$	 TwoWay=��� -   $ 
Final_Flag��$$��跳过检测综测完成标志��$
��Left=�$��
237,21,0,0q�$	��Top=� �� # Z $ 
Skip_Final�$	 TwoWay=��� -   $ Current_Flag��$$��跳过电流测试通过标志��$
��Left=�$��	10,41,0,0q�$	��Top=� �� # Z $ Skip_Current�$	 TwoWay=��� -   $ 
Aging_Flag��$$��跳过老化试验通过标志��$
��Left=�$��
237,41,0,0q�$	��Top=� �� # Z $ 
Skip_Aging�$	 TwoWay=��� -   $ NoCallOut_Flag��$��跳过拨打电话��$
��Left=�$��	10,61,0,0q�$	��Top=� �� # Z $ Skip_Call_Out�$	 TwoWay=��� $��电池电量范围:��$
��Left=�$��	17,91,0,0q�$	��Top=��� -   
 ��Name$
 MinLevel��$
��Left=�$��
107,93,0,0q�$	��Top=�$��63�� �� SelectedIndex �� # Z $ Bat_Min_Level�$	 TwoWay=�.@���0%����10%����20%����30%����40%����50%����60%����70%����80%����90%����100%���� $��~��$
��Left=�$��
175,89,0,0q�$	��Top=��� -   $
 MaxLevel��$
��Left=�$��
199,93,0,0q�$	��Top=�$��63�� �� # Z $ Bat_Max_Level�$	 TwoWay=�.@���0%����10%����20%����30%����40%����50%����60%����70%����80%����90%����100%���� $��设备软件版本:��$
��Left=�$��
17,119,0,0q�$	��Top=��� -	    ��Name$ 
SW_VERSION��$
��Left=�$��20��$��107,121,0,0q� �� TextWrapping$ NoWrap=�$	��Top=�$	��300��$��14(� �� 	MaxLength$ 30������ # Z $ 
SW_Version�$	 TwoWay=��� $��扫描WIFI名称:��$
��Left=�$��
17,144,0,0q�$	��Top=��� -
   $ 	WIFI_SSID��$
��Left=�$��20��$��107,146,0,0q�$
 Wrap=�$	��Top=�$	��300��$��14(�$ 30������ # Z $ 	WIFI_SSID�$	 TwoWay=��� $��WIFI信号阈值:��$
��Left=�$��
17,169,0,0q�$	��Top=�1   +TestTextBoxWaterMark.PerfectWateMarkTextBox  -     Name$ 	WIFI_RSSI��$
��Left=�$��20��$��107,171,0,0q�$
 Wrap=�$	��Top=�$��63��$��12(�$ 4��   WateMark$ -1~-100������ # Z $ 	WIFI_RSSI�$	 TwoWay=�.���� $��dB��$
��Left=�$��170,168,0,0q�$	��Top=��� $��BLE信号阈值:��$
��Left=�$��
17,194,0,0q�$	��Top=�  -   $ BLE_RSSI��$
��Left=�$��20��$��107,196,0,0q�$
 Wrap=�$	��Top=�$��63��$��12(�$ 4��$ -1~-100������ # Z $ BT_RSSI�$	 TwoWay=�.���� $��dB��$
��Left=�$��170,194,0,0q�$	��Top=��� $��
BLE MAJOR:��$
��Left=�$��
17,219,0,0q�$	��Top=�*   $TestTextBoxWaterMark.WateMarkTextBox  -     Name$ 	BLE_MAJOR��$
��Left=�$��20��$��107,221,0,0q�$
 Wrap=�$	��Top=�$��63�� J� RenderTransformOrigin$ 0.474,0.208&�$��12(�$ 5��   WateMark$ 0~65535������ # Z $ BT_MAJOR�$	 TwoWay=�.���� $��
BLE MINOR:��$
��Left=�$��
17,244,0,0q�$	��Top=�  -   $ 	BLE_MINOR��$
��Left=�$��20��$��107,246,0,0q�$
 Wrap=�$	��Top=�$��63��$ 0.474,0.208&�$��12(�$ 5��$ 0~65535������ # Z $ BT_MINOR�$	 TwoWay=�.���� -   $
��Left=�$	��Top=�$��84��$��119,288,0,0q�$��33��$��保存���� -   $��取消��$
��Left=�$	��Top=�$��84��$��270,288,0,0q�$��33���� -    ��Name$ Bat_LevelWarring��$�� ��$
��Left=�$��
270,91,0,0q�$	��Top=�
���  ��