   M S B A M L   `   `   `  ����F  BFactoryTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FactoryTestFactoryTest  5   	       FactoryTest.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:FactoryTest     5   	     � Title$  FactoryTest��5   	   $	��720��6   $	��880��6*    � 
ResizeMode$ NoResize=�66   .��5	      � .+�5
   
   d� -   5         d�Name$ 	btn_Start��5
      $��开始��6*   $
��Left=�67   $��267,508,0,0q�6R   $	��Top=�6g   $��75��6   6�   �� 5   
   -   5         ��Name$ txtDisp��5      $��Stretch=�6#   $��420,400,0,0q�6A    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j   $	��270��6�   $	��400��6�   $ �� HorizontalScrollBarVisibility$
 Auto=�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   �� 5   
   -   5        $ 
Rec_TxtBox��5      $��Stretch=�6&   $	��320��6D   $��
420,40,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $	��400��6�   $
 Auto=�6�   $
 Auto=�6�   �� 5   
   $��接收数据：��6   $
��Left=�6    $��
447,10,0,0q�6;   $	��Top=�6O   $	��115��6g   �� 5   
   $��发送数据：��6   $
��Left=�6    $��447,370,0,0q�6;   $	��Top=�6P   $	��115��6h   �� 5   
   -   5         ��Name$ state_1��5      $��1.进入测试模式��6!   $
��Left=�64   $��	10,20,0,0q�6O   $	��Top=�6b   $	��212��6z   �� 5   
   -   5        $ state_6��5      $��6.读取SIM状态��6!   $
��Left=�65   $��
10,145,0,0q�6P   $	��Top=�6d   $	��212��6|   �� 5   
   -   5        $ state_7��5      $��7.读取IMSI号码��6!   $
��Left=�66   $��
10,170,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5   
   -   5        $ state_8��5      $��8.读取ICCID号码��6!   $
��Left=�67   $��
10,195,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5   
   -   5        $ state_9��5      $��9.读取软件版本��6!   $
��Left=�64   $��
10,220,0,0q�6O   $	��Top=�6c   $	��212��6{   �� 5   
   -	   5        $ state_10��5      $��10.读取电池电量��6"   $
��Left=�66   $��
10,245,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5   
   -
   5        $ state_11��5      $��11.WIFI扫描测试��6"   $
��Left=�68   $��
10,270,0,0q�6S   $	��Top=�6g   $	��212��6   �� 5   
   -   5        $ state_12��5      $��12.关闭WIFI扫描��6"   $
��Left=�68   $��
10,295,0,0q�6S   $	��Top=�6g   $	��212��6   �� 5   
   -   5        $ state_13��5      $��13.删除文件测试��6"   $
��Left=�66   $��
10,320,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5   
   -   5        $ state_14��5      $��14.BLE扫描测试��6"   $
��Left=�67   $��
10,345,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5   
   -   5        $ state_15��5      $��15.关闭BLE扫描��6"   $
��Left=�67   $��
10,370,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5   
   -   5        $ state_16��5      $��16.加速度计测试��6"   $
��Left=�66   $��
10,395,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5   
   -   5        $ state_17��5      $��17.LED闪烁测试��6"   $
��Left=�67   $��
10,420,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5   
   -   5        $ state_18��5      $��18.GPS搜星测试��6"   $
��Left=�67   $��
10,445,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5   
   -   5        $ state_19��5      $��19.关闭GPS搜星��6"   $
��Left=�67   $��
10,470,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5   
   -   5        $ state_20��5      $��20.喇叭发声测试��6"   $
��Left=�66   $��
10,495,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5   
   -   5        $ state_21��5      $��21.MIC录音测试��6"   $
��Left=�67   $��
10,520,0,0q�6R   $	��Top=�6f   $	��212��6~   �� 5    
   -   5        $ state_22��5       $��22.拨打电话测试��6"   $
��Left=�66   $��
10,545,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5!   
   -   5        $ state_23��5!      $��23.挂断电话测试��6"   $
��Left=�66   $��
10,570,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5"   
   -   5        $ state_24��5"      $��24.写入完成标志��6"   $
��Left=�66   $��
10,595,0,0q�6Q   $	��Top=�6e   $	��212��6}   �� 5#   
   -   5        $ state_25��5#      $��25.关机测试��6"   $
��Left=�64   $��
10,620,0,0q�6O   $	��Top=�6c   $	��212��6{   �� 5$   
   -   5         ��Name$ btn_Setting��5$      $��设置��6&   $
��Left=�63   $��267,550,0,0q�6N   $	��Top=�6c   $��75��6{   6�   �� 5%   
   -   5        $ state_2��5%      $��2.检测校准标志��6!   $
��Left=�64   $��	10,45,0,0q�6O   $	��Top=�6b   $	��212��6z   �� 5&   
   -   5        $ state_3��5&      $��3.综测标志检测��6!   $
��Left=�64   $��	10,70,0,0q�6O   $	��Top=�6b   $	��212��6z   �� 5'   
   -   5        $ state_4��5'      $��4.电流测试检测��6!   $
��Left=�64   $��	10,95,0,0q�6O   $	��Top=�6b   $	��212��6z   �� 5(   
   -   5        $ state_5��5(      $��5.老化试验检测��6!   $
��Left=�64   $��
10,120,0,0q�6O   $	��Top=�6c   $	��212��6{   �� 5)   
   -   5        $ Final_State��5)      $�� ��6%   $
��Left=�60   $��
235,23,0,0q�6K   $	��Top=�6_   $	��100��6w   $	��200��6�   $��72(�6�   $��Center=�6�   $��Center=�6�   
�������
����  �$��Microsoft YaHei)�6	  �� 5*   
   -   5        $ 
Tick_Lable��5*      $��00:00:00��6$   $
��Left=�67   $��291,200,0,0q�6R   $	��Top=�6g   $��24(�6   �� 5+   
   -    5        $ Tick_Lable_Copy��5+      $��测试时间：��6)   $
��Left=�69   $��280,155,0,0q�6T   $	��Top=�6i   $��24(�6�   	 J� RenderTransformOrigin$	 0.7,0&�6�   �� 5,   
   -!   5        $ SysteamState_Lable��5,      $�� ��6,   $
��Left=�67   $��227,245,0,0q�6R   $	��Top=�6g   $��14(�6   $	 0.7,0&�6�   $	��215��6�   $��Center=�6�   $��Center=�6�   5.      5/      