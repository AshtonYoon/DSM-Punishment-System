   M S B A M L   `   `   `  ����G  CDormitoryGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:DormitoryGUI.ViewModelDormitoryGUI.ViewModel  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      5clr-namespace:DormitoryGUI.ViewDormitoryGUI.View  5      &     DormitoryGUI.View.CustomComboBox   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :	viewmodel$clr-namespace:DormitoryGUI.ViewModel     5      6sys&clr-namespace:System;assembly=mscorlib     5      1localclr-namespace:DormitoryGUI.View     5        � 	Resources  5      +  %DormitoryGUI.ViewModel.PunishmentList   PunishmentListKey  WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor 	 ControlLightColor 
 ControlMediumColor  ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush �� EndPoint �� 
StartPoint � Color � Offset $  ProgressBarIndicatorAnimatedFill  ComboBoxToggleButton �� 
TargetType � Cursor � ColumnDefinitions'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup	   States   System.Windows.VisualState
   
Storyboard �� TargetProperty �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value ��Name �� CornerRadius F�Name �� Stretch �� Color  ComboBoxTextBox �� 
TargetType �� Property �� Value R� KeyTime  System.Windows.Visibility   Visible R� Value   Hidden d�Name �� 	ClickMode d� 	IsChecked �� Mode �� RelativeSource  ��Name! J� IsHitTestVisible" �� SelectionBoxItem# �� SelectionBoxItemTemplate$ ��Name% �� 
IsReadOnly& �� 
IsReadOnly' �Name( �� IsDropDownOpen) � AllowsTransparency* �Name+ J� SnapsToDevicePixels, � ActualWidth- �� MaxDropDownHeight. K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation/   DirectionalNavigation0 �� Triggers1 P� Property2 P� Value3 �� 
TargetName4 P� 
SourceName��%�  &	        &	      &	 4     &	 U     &	 v     &	 �     &	 �     &	 �     &	 �     &		     &	
 8    &	 Y    &	 z    &	 �    &	 �    &	 �    &	      &	 A    &	 b    &	 �    &	 �    &	 �    &	 �    &	     &	 $    &	 E    &	 b    &	 �    &	 �    &	 k    &	 �    &	 �
    '�� %    7  7  7  '�� �      5   
   6#   ��5   
   	#FFE8EDF9��6/   ��5   
   	#FFC5CBF9��69   ��5   
   	#FF7381F9��68   ��5   
   	#FFE8EDF9��6=   ��5   
   	#FFC5CBF9��6<   ��5   
   	#FF888888��6;   ��5   
   	#FFC5CBF9��6;   ��5   
   	#FFDDDDDD��6:   ��5   
   	White��61   ��5   
   	#FF7381F9��66   ��5   
   	#FF211AA9��64   ��5   
   	#FF3843C4��69   ��5   
   	#FF211AA9��67   ��5   
   	#FF444444��6.   ��5   
   /+sc#1, 0.004391443, 0.002428215, 0.242281124��6T   ��5   
   	#FFCCCCCC��64   ��5    
   	#FF888888��65   ��5!   
   	#FF444444��63   ��5"   
   	#FF888888��6;   ��5#   
   	#FF444444��6:   ��5$   
   	#FFAAAAAA��6<   ��5%   
   	#FF888888��6;   ��5&   
   	Black��6;   ��5(   
   	#FFC5CBF9��62   ��5)   
   	Black��6>   ��5*   
   	#FFC5CBF9��68   ��5+   
   	#FF3843C4��67   �� 5,   
   $ 0.5,1&�5-      $ 0.5,0&�5.      .��5/      � # � 	 6   $ 0Q�50      � 51      # � 
 6   $	 0.5Q�52      � 53      # � 	 6   $ 1Q�54      55      �� 56   
   $	 0,0&�57      $	 1,0&�58      ��5:      � � 5;      $ 	#000000FF��6#   $ 0Q�5<   #   � 5=      $ 	#600000FF��6#   $	 0.4Q�5>   #   � 5?      $ 	#600000FF��6#   $	 0.6Q�5@   #   � 5A      $ 	#000000FF��6#   $ 1Q�5B   #   5C      5E      �� 5F   
   " d�5G      .R�5H      � $
 Hand��6    5I      �� 5J      �� 5K      $��50 �6'    5M        5N      CommonStates��.	 5O        
Normal��6&     5P      	MouseOver��.
 5Q      �� .��5R   "   �� $0 *(Panel.Background).(SolidColorBrush.Color)�6?   $ Border��5S   ?   .g�5T   &     $ 00:00:00.15��6:   $ 	#22FFFFFF��5U   :   5V   #   5W      5X        5Y      Pressed��6&     5Z      Disabled��6:   5[        5\      CheckStates��.	 5]        Checked��.
 5^      �� .��5_   "   �� $0 *(Panel.Background).(SolidColorBrush.Color)�6?   $ Border��5`   ?   .g�5a   &     $ 00:00:00.15��6:   $ 	#55FFFFFF��5b   :   5c   #   5d      5e        5f      	Unchecked��6&     5g      Indeterminate��6&   5h      .+�5j      �� $ Border��6   $��2��5k      $ 22��5l      $	��1.5q�5m      #��� ��5n      
������ 5o   4   �� 5p      $��0��6   $ 	10,0,0,10��5q      $��1q�5r      
������ 5s   4   F� 5t      $ Arrow��6   $��1��5u      $��Center=�5v      $��Center=�5w      "����        � 	=  	= � z     	$��	4,11,6,10q�5x   2   $
 Fill=�6E   $��15��6T   $	��7.5��6_   ��5y      �� 5z      $ Black��6*   5|      5}      5~      �� 5   
   " ��5�      .R�5�      �� $ PART_ContentHost��6   |�.  #��z��5�      5�      �� 5�   
   " ��5�      .�5�      ��  � ��
 �   �5�      �� 5�       �@J�SnapsToDevicePixels . 5�      �� 5�       �@�OverridesDefaultStyle . 5�      �� 5�       � ��$
 Auto=�5�      �� 5�       � ��$
 Auto=�5�      �� 5�       � �� . 5�      �� 5�       � ��$	 120��5�      �� 5�       � ��$ 20��5�      �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      �  5�        5�   "   CommonStates��.	 5�   &     
Normal��62     5�   &   	MouseOver��62     5�   &   Disabled��.
 5�   *   �� .��5�   .   �� $ PART_EditableTextBox��6K   $M G(TextElement.Foreground).                       (SolidColorBrush.Color)�5�   K   .g�5�   2     $ 0��6F   8   5�   F   5�   /   5�   +   5�   '   5�   #     5�   "   
EditStates��.	 5�   &     Editable��.
 5�   *   �� .��5�   .   T� $ (UIElement.Visibility)�6L   $ PART_EditableTextBox��5�   L   ..�5�   2   i� $ 0��6I   # Z 5�   I   5�   /   T� 5�   .   $ (UIElement.Visibility)�6L   $ ContentSite��5�   L   ..�5�   2   i� $ 0��6I   # Z 5�   I   5�   /   5�   +   5�   '     5�   &   
Uneditable��62   5�   #   .+�5�      d� $ ToggleButton��6+   8�� $��2��5�   +   |�.  $ Press=�5�   +    5�   +   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� 5�      $  ContentSite��6/   ! .  #��z" 5�   /   #��z# 5�   /   #��z��5�   /   $"��/Font/#NanumSquareRound Bold)�5�   /   $��20, 0q�5�   /   $��Center=�5�   /   $
��Left=�5�   /   �� 5�      $$ PART_EditableTextBox��6&   8�� $
��Left=�5�   &   $��Center=�5�   &   $��15, 10q�5�   &   |�. 
������ $y�Hidden=�5�   &   #& z% 5�   &   ��5�   &   Z� 5�   &   � 5�      $' Popup��6$   $��Bottom=�5�   $   #��z( 5�   $   ) . |�.  $��Slide=�5�   $   .��5�   "   � $* DropDown��6'   + . #��z, 5�   '   #��z- 5�   '   .+�5�   &   �� $ DropDownBorder��6-   $ 30��5�   -   $��1q�5�   -   ��5�   *   �� 5�   .   $
 #555��6>   ��5�   *   �� 5�   .   # � 	 6>   5�   '   �� 5�   &   $
��0, 5q�63   + . .��5�   *   �� . . $/ 	Contained=�5�   5   5�   '   5�   #   5�      5�      0 5�      P� 5�      1 �@��HasItems2 .  . �5�   "   �� $3 DropDownBorder��6)    � ��$ 95��5�   )   5�      P� 5�      1 �@��
IsGrouping2 . . �5�   "   ��  � �� .  5�   )   5�      P� 5�      $4 Popup��6&   1 �@�AllowsTransparency2 . . �5  "   �� $3 DropDownBorder��6)    �@��CornerRadius$ 30��5  )   �� 5  "   $3 DropDownBorder��6)    � ��$ 0, 1.5, 0, 0q�5  )   5     5	     5     5     �� 5  
   " ��5     .�5     ��  �@�Cursor$
 Hand��5     �� 5      � ��
 �   �5     �� 5      �@J�SnapsToDevicePixels . 5     �� 5      � ��$ 20(�6)   66   �� 5      �@�OverridesDefaultStyle . 5     �� 5      � ��$
 Left=�5     �� 5      � ��$ Center=�5     �� 5      � ��$ 20, 15q�5     �� 5      � ��$" /Font/#NanumSquareRound Bold)�5     �� 5       � �� 5!     �� 5"     " ��6&   .R�5#     �� $ Border��6!   + . 
������  5&       5'  "   SelectionStates��.	 5(  &     
Unselected��62     5)  &   Selected��.
 5*  *   �� .��5+  .   �� $ Border��6K   $0 *(Panel.Background).(SolidColorBrush.Color)�5,  K   .g�5-  2     $ 0��6F   $ White��6R   5.  /   5/  +   50  '     51  &   SelectedUnfocused��.
 52  *   �� .��53  .   �� $ Border��6K   $0 *(Panel.Background).(SolidColorBrush.Color)�54  K   .g�55  2     $ 0��6F   $ White��6R   56  /   57  +   58  '   59  #   ._�5;     �� 5<     5=     5?     5@     5A     .��5B     � .+�5C  
   �� -   5        5 ��Name$5 comboBox��5C     ��5D     �� 6 �� Source#6 [  ��5F     �� 5G     .R�5H     �� 7 �� Orientation$7 
Horizontal=�6!   .+�5I     �� ��6$   �� 8 �� Path$
8 Name��� 5J     $�� ��6$   �� 5K     
�������$��(��6:   �� 5L     
���������6:   �� $8 MinPoint��� 5M     
�������$	��점��6:   �� 5N     
�������$��)��6:   5O     5P     5R     5S     5T     