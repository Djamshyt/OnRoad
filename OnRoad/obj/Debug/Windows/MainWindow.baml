   M S B A M L   `   `   `  ����A  =OnRoad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:OnRoad.WindowsOnRoad.Windows  5   	       OnRoad.Windows.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:OnRoad.Windows     5   	   D @ModernWpf, Version=0.9.4.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.4.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	     � Title$  
MainWindow��5	   	   $	��550��6   $	��380��6)    � Icon$ /img/logo.png��5
       � WindowStartupLocation$ CenterScreen=�5       � AllowsTransparency . 
������  � WindowStyle$
 None=�5       � 
ResizeMode$ NoResize=�5      .��5      � .+�5      �  � RowDefinitions 5      �� 5      �� 5      .+�5      ��  �� CornerRadius$ 10��6   $��2��6   ��5      �� 5      .��5      �  � Color$ 	#FF7BF6A0��6   	 � Offset$		 0.0Q�6'   � 5      $ 	#FF3812CD��6   $	 1Q�6'   5      5      �� 5!      $��Center=�6   .+�5"      �� -   5        $��Х��5"      
�������$��Right=�5#      $��
0 -40 10 0q�5$      
 J� Opacity$	
 0.5Q�5%      
������ 5'      �� 5(      $��/img/logo.png��6   $	��150��6#   �� 5)      $��Авторизация:��6   $��Light"�5*      $��	helvetica)�5+      $��22(�5,      
�������$��Center=�5.      5/      �� 53      $��1��6   .+�54      �� $��Логин:��6   
�������$��65 0 0 0q�56   	   �� 57      -   5         ��Name$ LoginBox��5;      $��	Helvetica)�57      $	��255��58      $��0 0 0 10q�59      $	
 0.5Q�5:      5;      �� 5<      $��Пароль:��6   
�������$��65 0 0 0q�5>   	   G� 5?      -   5         G�Name$ PasswordBox��5C      $��	Helvetica)�5?      $	��255��5@      $��0q�5A      $	
 0.5Q�5B      5C      �� 5E      -   5        $	��100��5E      $��40��6   $��
Войти��6%   $��Center=�65   $��10q�6R   6^   �� 5F      -   5        $	��100��5F      $��40��6   $��Регистрация��6%   $��Center=�6;   6X   5H      5I      5J      5K      