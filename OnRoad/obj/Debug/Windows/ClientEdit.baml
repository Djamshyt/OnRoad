   M S B A M L   `   `   `  ����A  =OnRoad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:OnRoad.WindowsOnRoad.Windows  5   	       OnRoad.Windows.ClientEdit   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:OnRoad.Windows     5   	   D @ModernWpf, Version=0.9.4.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.4.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5        � Icon$  /img/logo.png��5	       � WindowStartupLocation$ CenterScreen=�5
       � AllowsTransparency . 
������  � WindowStyle$
 None=�5       � 
ResizeMode$ NoResize=�5       � Title$ 
ClientEdit��5      $	��550��6   $	��380��6#   .��5      '  !ModernWpf.Controls.ScrollViewerEx  .��5      �  � RowDefinitions 5      �� 5      $��1* �6   �� 5      $	��10* �6   .+�5      ��  �� CornerRadius$ 10��6   $��2q�6   
���   �$��2��6F   ��5      �� 5      .��5      �  � Color$ White��6   	 � Offset$		 0.0Q�6#   � 5      $ 	#FFFAF4F4��6   $	 1Q�6'   5      5      �� 5      $��	0 15 0 20q�6   .+�5      �� $2��,Редактировать/Добавить:��6   $��Light"�5       $��	helvetica)�5!      $��22(�5"      
���   �$��Center=�5$      $
��68,0q�69   �� 5%      -   5        $��Х��5%      
���   �$��Right=�5&      $��
0 -40 10 0q�5'      5(      5)      �� 5+      $��1��6   .+�5,      �� $��	Helvetica)�6   $	��255��5-      $��0 0 0 10q�5.      
 J� Opacity$	
 0.5Q�5/      1  +ModernWpf.Controls.Primitives.ControlHelper   PlaceholderText$ Фамилия��51      ��50      �� *clnt.Surname+ �� UpdateSourceTrigger$ PropertyChanged=�51      �� 52      $��	Helvetica)�6   $	��255��53      $��0 0 0 10q�54      $	
 0.5Q�55      $ Имя��57      ��56      �� *	clnt.Name+$ PropertyChanged=�57      �� 58      $��	Helvetica)�6   $	��255��59      $��0 0 0 10q�5:      $	
 0.5Q�5;      $ Отчество��5=      ��5<      �� *clnt.Patronymic+$ PropertyChanged=�5=      �� 5>      $��	Helvetica)�6   $	��255��5?      $��0 0 0 10q�5@      $	
 0.5Q�5A      $ Возраст��5C      ��5B      �� *
clnt.Age+$ PropertyChanged=�5C      �� 5D      $��	Helvetica)�6   $	��255��5E      $��0 0 0 10q�5F      $	
 0.5Q�5G      $! Номер телефона��5I      ��5H      �� *clnt.PhoneNumber+$ PropertyChanged=�5I      �� 5J      $��	Helvetica)�6   $	��255��5K      $��0 0 0 10q�5L      $	
 0.5Q�5M      $ 
Почта��5O      ��5N      �� *	clnt.Mail+$ PropertyChanged=�5O      �� 5P      -   5         ��Name$ 	GenderBox��5U      $��	Helvetica)�5P      $��Center=�5Q   	   $	��255��5R      $��0 0 0 10q�5S      $	
 0.5Q�5T      5U      5V      �� 5W      -   5        $��1��5W      $	��100��5X      $��40��5Y      $��Сохранить��5Z      $��Center=�5[      $��Bottom=�5\      $��0 0 0 25q�5]      5^      5_      5`      5a      