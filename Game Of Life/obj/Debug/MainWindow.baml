   M S B A M L   `   `   `  ����G  CGame Of Life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Game_Of_LifeGame_Of_Life  5   	       Game_Of_Life.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Game_Of_Life     5   	   -   5          � Title$  Game Of Life��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ CanMinimize=�5
   	    � SizeToContent$ WidthAndHeight=�5   	   .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   .+�5      o� $	��Top=�6   .@�5      m� $��
Simulation��6   .@�5      m� -   5        $��StartMenuItem��5      $��Lancer la simulation��65    m� InputGestureText$ F5��6S   5      m� 5      -   5        $��StopMenuItem��5      $��Arrêter la simulation��64   $ Echap��6S   {�.  5      m� 5      -   5        $��PauseMenuItem��5      $#��Mettre en pause la simulation��65   $ Espace��6\   {�.  5      5      m� 5      $��Motifs��6   .@�5      m� -   5        $��MenuPlaneur��5   1   $��Planeurs��6   61   m� 5       -   5        $��	MenuGener��5    4   $��Générateurs��6   64   m� 5!      -   5        $��
MenuStatio��5!   6   $��Stationnaires��6   66   m� 5"      -   5        $��MenuOsci��5"   5   $��Oscillateurs��6   65   m� 5#      -	   5        $��	MenuAutre��5#   /   $��Autres��6   6/   �� 5$      m� 5%      -
   5        $��Nouveau motif��5%      5'      5(      m� 5)      -   5        $��Options��5)      5*      5+      5,      �� 5-   
   -   5        $��
MainCanvas��5.      $��1��5-      $
��1000��5/      $
��1400��50      $	��800��51      $	��900��52      $��Center=�53      $��Center=�54      
�������55      56      57      