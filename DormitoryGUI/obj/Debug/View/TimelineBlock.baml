   M S B A M L   `   `   `  ����G  CDormitoryGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DormitoryGUI.ViewDormitoryGUI.View  5      %    DormitoryGUI.View.TimelineBlock   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:DormitoryGUI.View     5      $	��300��5      $	��400��5	      $
��Left=�5
      .��5      �   � RowDefinitions  5   
   �� 5      $��1* �6   �� 5      $��4* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $��4* �6#   .+�5      A� -   5         A�Name$ 
TintCircle��5      $��0��5      $��5, 15q�5      $��10��5      
������ ��5      �� 5       �� Color$ #D46A6A��6&   5      �� 5!      $��1��6   $��0, 35, 4, 35q�5"      ���#CCCCCC5#      5$      �� 5&   
   $��1��6   $��25, 0, 10, 0q�5'      $��Center=�5(      .+�5)      �� -   5         ��Name$ ContentBorder��5)      $��1��5*      ���#FF7F7F �� CornerRadius$ 20��5,      $��0, 0, 0, 30q�5-       J� BitmapEffect 5.      F� 5/       F� Opacity$	 0.2Q�6-   ._�51      � $	��250��6     52      �� 53      $��* �6(   �� 54      $��* �6(   �� 55      $��* �6(   .+�58      �� -   5         ��Name$ 
PointCause��58      $��0��59      $��벌점 사유��5:      $��Center=�5;      $��Bottom=�5<      $'��!/Font/#NanumSquareRound ExtraBold)�5=      $��30(�5>      
�������5?      �� 5A      -   5        $ 
PointValue��5A      $��1��5B      $
��n점��5C      $��Center=�5D      $��Center=�5E      $'��!/Font/#NanumSquareRound ExtraBold)�5F      $��24(�5G      
�������5H      �� 5J      -   5        	 ��Name$	 RemoveButton��5J      $��2��5K      $��삭제��5L      $��25, 0, 25, 30q�5M      
������ 
�������$��2q�5P      
 � Cursor$

 Hand��5Q      $"��/Font/#NanumSquareRound Bold)�5R      $��20(�5S      
������� � 	Resources 5U       �� 
TargetType �� Property �� Value��%_   '��       �� 5V      " ��6$   .�5W   "   ��  �@��CornerRadius$ 30��6A   5X      5Y      5Z      5[      5\      �� 5^      -   5        $ 
CreateTime��5^      $��2017-1-10 22:45:00��5_      $��0, -15, 0, 0q�5`      $��Center=�5a      $��Center=�5b      $"��/Font/#NanumSquareRound Bold)�5c      $��18(�5d      ���#4445e      5f      5g      5h      