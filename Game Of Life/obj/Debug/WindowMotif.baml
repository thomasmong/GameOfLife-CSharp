   M S B A M L   `   `   `  ����G  CGame Of Life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Game_Of_LifeGame_Of_Life  5   	       Game_Of_Life.WindowMotif   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Game_Of_Life     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  Nouveau motif��5	   	    � SizeToContent$ WidthAndHeight=�5
   	    � 
ResizeMode$ NoResize=�5   	    � ShowInTaskbar .  .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��
MainCanvas��5      $��0��5      $��0��6   $��2��6.   $	��600��5      $	��800��5      5      ]� 5   
   $��1��6   $��0��6!   .+�5      ]� $��2q�6   $
��Left=�6#   $��Center=�6/   .+�5      %  Xceed.Wpf.Toolkit.IntegerUpDown  -   5        $��
StepperRow��5   %   $��80��67   $��Right=�6B   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Maximum$	 500X�6Y      Minimum$ 2X�6g      Value$ 5X�6s   �� 5      $��Nombre de lignes��6   $
��Left=�63   5      ]� 5       $��2q�6   $
��Left=�6#   $��Center=�69   .+�5!      �� $��Nombre de colonnes��6   $
��Left=�65     5"      -   5        $��StepperColumn��5"   %   $��80��6:   $
��Left=�6E   $	 500X�6[   $ 2X�6i   $ 5X�6u   5#      �� 5$      -   5        $��Mettre à jour��5$      $��Right=�5%      $��5q�5&      $��5q�5'      $��Right=�5(      5)      5*      ]� 5+   
   $��2��6   $��0��6!   $��Stretch=�61   $��Center=�6O   .+�5,      ]� $��2q�6   $
��Left=�6#   $��Center=�6/   $��Stretch=�6J   .+�5-      �� $��Nom du motif��6   $
��Left=�6/   $��Center=�6E   �� 5.      -   5        $��NameBox��5.      $
��Left=�6)   $	��150��6?   $��Center=�6K   	 �� MaxLines$	 1��6f   
 �� 	MaxLength$
 20��6s    �� TextAlignment$ Center=�5/      61   50      ]� 51      $��2q�6   $��Center=�6#   $��Right=�6>   $��Right=�6Z   .+�52      �� $��
Catégorie��6   $
��Left=�6,   $��Center=�6B   �� 53      -   5        $��CategBox��53      $
��Left=�6+   $	��100��6A   $��5q�6M   $��Right=�6X   $��Right=�54      $��Center=�55      @�57      �� 58      $��Planeur��6    �� 59      $��Stationnaire��6    �� 5:      $��Générateur��6    �� 5;      $��Oscillateur��6    �� 5<      $��Autre��6    5>      5?      5@      �� 5A   
   -   5        $��ValidButton��5B      $��Enregistrer��5A      {�.  $��1��5D      $��2��5E      $��1��5F      $��Center=�5G      $��Center=�5H      $��5q�5I      $��5q�5J      5K      5L      5M      