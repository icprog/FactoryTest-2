   M S B A M L   `   `   `  ���� F  BFactoryTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FactoryTestFactoryTest      FactoryTest.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:FactoryTest       � Title$  
MainWindow��$	��720��$	��800�� � 
ResizeMode$ NoResize=�.��� .+�d� -    d�Name$ 	btn_Start��$��开始��$
��Left=�$��267,508,0,0q�$	��Top=�$��75���� -    ��Name$ txtDisp��$��Stretch=�$��380,394,14,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��200��$	��400��$ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=��� -   $ 
Rec_TxtBox��$
��Left=�$	��320��$��
380,40,0,0q�$
 Wrap=�$	��Top=�$	��400��$
 Auto=�$
 Auto=��� $��接收数据：��$
��Left=�$��
380,10,0,0q�$	��Top=�$	��115���� $��发送数据：��$
��Left=�$��380,365,0,0q�$	��Top=�$	��115���� -    ��Name$ state_1��$��1.进入测试模式��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��212���� -   $ state_2��$��2.读取SIM状态��$
��Left=�$��	10,35,0,0q�$	��Top=�$	��212���� -   $ state_3��$��3.读取IMSI号码��$
��Left=�$��	10,60,0,0q�$	��Top=�$	��212���� -   $ state_4��$��4.读取ICCID号码��$
��Left=�$��	10,85,0,0q�$	��Top=�$	��212���� -   $ state_5��$��5.读取软件版本��$
��Left=�$��
10,110,0,0q�$	��Top=�$	��212���� -	   $ state_6��$��6.读取电池电量��$
��Left=�$��
10,135,0,0q�$	��Top=�$	��212���� -
   $ state_7��$��7.WIFI扫描测试��$
��Left=�$��
10,160,0,0q�$	��Top=�$	��212���� -   $ state_8��$��8.关闭WIFI扫描��$
��Left=�$��
10,185,0,0q�$	��Top=�$	��212���� -   $ state_9��$��9.删除文件测试��$
��Left=�$��
10,210,0,0q�$	��Top=�$	��212���� -   $ state_10��$��10.BLE扫描测试��$
��Left=�$��
10,235,0,0q�$	��Top=�$	��212���� -   $ state_11��$��11.关闭BLE扫描��$
��Left=�$��
10,260,0,0q�$	��Top=�$	��212���� -   $ state_12��$��12.加速度计测试��$
��Left=�$��
10,285,0,0q�$	��Top=�$	��212���� -   $ state_13��$��13.LED闪烁测试��$
��Left=�$��
10,310,0,0q�$	��Top=�$	��212���� -   $ state_14��$��14.GPS搜星测试��$
��Left=�$��
10,335,0,0q�$	��Top=�$	��212���� -   $ state_15��$��15.关闭GPS搜星��$
��Left=�$��
10,360,0,0q�$	��Top=�$	��212���� -   $ state_16��$��16.喇叭发声测试��$
��Left=�$��
10,385,0,0q�$	��Top=�$	��212���� -   $ state_17��$��17.MIC录音测试��$
��Left=�$��
10,410,0,0q�$	��Top=�$	��212���� -   $ state_18��$��18.拨打电话测试��$
��Left=�$��
10,435,0,0q�$	��Top=�$	��212���� -   $ state_19��$��19.挂断电话测试��$
��Left=�$��
10,460,0,0q�$	��Top=�$	��212���� -   $ state_20��$��20.写入完成标志��$
��Left=�$��
10,485,0,0q�$	��Top=�$	��212���� -   $ state_21��$��21.关机测试��$
��Left=�$��
10,510,0,0q�$	��Top=�$	��212���� -    ��Name$ btn_Setting��$��设置��$
��Left=�$��267,550,0,0q�$	��Top=�$��75���� -   $ state_22��$��22.检测校准标志��$
��Left=�$��
10,535,0,0q�$	��Top=�$	��212���� -   $ state_23��$��23.综测标志检测��$
��Left=�$��
10,560,0,0q�$	��Top=�$	��212���� -   $ state_24��$��24.电流测试检测��$
��Left=�$��
10,585,0,0q�$	��Top=�$	��212���� -   $ state_25��$��25.老化试验检测��$
��Left=�$��
10,610,0,0q�$	��Top=�$	��212��