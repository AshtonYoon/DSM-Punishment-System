   M S B A M L   `   `   `  ����G  CDormitoryGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DormitoryGUI.ViewDormitoryGUI.View  5      $    DormitoryGUI.View.CustomSlider   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:DormitoryGUI.View     5        � 	Resources  5	         WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor  ControlLightColor 	 ControlMediumColor 
 ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush �� EndPoint �� 
StartPoint � Color � Offset $  ProgressBarIndicatorAnimatedFill  SliderButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType  SliderThumbStyle	 A�Name'  !System.Windows.VisualStateManager
   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value  HorizontalSlider � RowDefinitions m�Name J� SnapsToDevicePixels m� 	Placement m� Fill �� Color ��Name �� CornerRadius ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton �� Triggers P� Property P� Value  �� 
TargetName   VerticalSlider! � ColumnDefinitions" �� Triggers��%�  &	        &	 !     &	 B     &	 c     &	 �     &	 �     &	 �     &	 �     &	     &		 %    &	
 F    &	 g    &	 �    &	 �    &	 �    &	     &	 .    &	 O    &	 p    &	 �    &	 �    &	 �    &	 �    &	     &	 2    &	 O    &	 p    &	 �    &	 X    &	 �    &	 �    7  &	 H    7  7  7  &	  �    7  7  7  '�� +    7  7   ��5   
   	#FFE8EDF9��6/   ��5   
   	#FFC5CBF9��69   ��5   
   	#FF7381F9��68   ��5   
   	#FFE8EDF9��6=   ��5   
   	#FFC5CBF9��6<   ��5   
   	#FF888888��6;   ��5   
   	#FFC5CBF9��6;   ��5   
   	#FFDDDDDD��6:   ��5   
   	White��61   ��5   
   	#FF7381F9��66   ��5   
   	#FF211AA9��64   ��5   
   	#FF3843C4��69   ��5   
   	#FF211AA9��67   ��5   
   	#FF444444��6.   ��5   
   /+sc#1, 0.004391443, 0.002428215, 0.242281124��6T   ��5   
   	#FFCCCCCC��64   ��5   
   	#FF888888��65   ��5   
   	#FF444444��63   ��5   
   	#FF888888��6;   ��5   
   	#FF444444��6:   ��5    
   	#FFAAAAAA��6<   ��5!   
   	#FF888888��6;   ��5"   
   	Black��6;   ��5$   
   	#FFC5CBF9��62   ��5%   
   	Black��6>   ��5&   
   	#FFC5CBF9��68   ��5'   
   	#FF3843C4��67   �� 5(   
   $ 0.5,1&�5)      $ 0.5,0&�5*      .��5+      � # �  6   $ 0Q�5,      � 5-      # � 	 6   $	 0.5Q�5.      � 5/      # �  6   $ 1Q�50      51      �� 52   
   $	 0,0&�53      $	 1,0&�54      ��56      � � 57      $ 	#000000FF��6#   $ 0Q�58   #   � 59      $ 	#600000FF��6#   $	 0.4Q�5:   #   � 5;      $ 	#600000FF��6#   $	 0.6Q�5<   #   � 5=      $ 	#000000FF��6#   $ 1Q�5>   #   5?      5A      �� 5B   
   " ��5C      .�5D      ��  �@J�SnapsToDevicePixels . 5E      �� 5F       �@�OverridesDefaultStyle . 5G      �� 5H       � �� .  5I      �� 5J       � |� .  5K      �� 5L       � �� 5M      �� 5N      " ��6&   .R�5O      �� 
������ 6!   5P      5R      5S      �� 5T   
   " n�5U      .�5V      ��  �@J�SnapsToDevicePixels . 5W      �� 5X       �@�OverridesDefaultStyle . 5Y      �� 5Z       � ��$ 14��5[      �� 5\       � ��$ 14��5]      �� 5^       � �� 5_      �� 5`      " n�6&   .R�5a      A� $	 Ellipse��6"   $��1��5b   "   
�������
������ 
 5e        5f   "   CommonStates��. 5g   &     
Normal��62     5h   &   	MouseOver��. 5i   *   �� .��5j   .   �� $* $(Shape.Fill).(SolidColorBrush.Color)�6K   $ Ellipse��5k   K   .g�5l   2     $ 00:00:00.15��6F   $ #DDDDDD��5m   F   5n   /   5o   +   5p   '     5q   &   Pressed��. 5r   *   �� .��5s   .   �� $* $(Shape.Fill).(SolidColorBrush.Color)�6K   $ Ellipse��5t   K   .g�5u   2     $ 00:00:00.15��6F   $ #BBBBBB��5v   F   5w   /   5x   +   5y   '     5z   &   Disabled��. 5{   *   �� .��5|   .   �� $* $(Shape.Fill).(SolidColorBrush.Color)�6K   $ Ellipse��5}   K   .g�5~   2     $ 0��6F   8   5   F   5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�      �� 5�   
   " ��5�      .R�5�      �  5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   #��z��5�   $   �� 5�      $
��Auto �6$   .+�5�      m� $ TopTick��6    . $	 Top=�5�      $��4��5�      $y�	Collapsed=�5�       5�      �� 5�      $ White��6*   5�      �� 5�      $ TrackBackground��6   $��0q�5�      $ 2��5�      $��4��5�      $��1��5�      $��0q�5�      ���#AAAAAA5�      ]� 5�      $ 
PART_Track��5�      $��1��5�       5�      �� 5�      8��  $��Slider.DecreaseLarge��5�   '    5�      n� 5�      8�� 6     5�      �� 5�      8�� $��Slider.IncreaseLarge��5�   '   5�      m� 5�      $ 
BottomTick��6    . $��2��5�      # z��5�      $ Bottom=�5�      $��4��5�      $y�	Collapsed=�5�      5�       5�      P� 5�       �@��TickPlacement$ TopLeft=�5�      . �5�      �� $  TopTick��6    � y�$ Visible=�5�      5�      P� 5�       �@��TickPlacement$ BottomRight=�5�      . �5�      �� $  
BottomTick��6    � y�$ Visible=�5�      5�      P� 5�       �@��TickPlacement$
 Both=�5�      . �5�      �� $  TopTick��6    � y�$ Visible=�5�      �� 5�      $  
BottomTick��6    � y�$ Visible=�5�      5�      5�      �� 5�   
   " ��5�      .R�5�      � ! 5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   #��z��5�   '   �� 5�      $
��Auto �6'   .+�5�      m� $ TopTick��6    . $
 Left=�5�      $��4��5�      $y�	Collapsed=�5�       5�      �� 5�      # �  6*   5�      �� 5�      $ TrackBackground��6   $��0q�5�      $ 2��5�      $��4��5�      $��1��5�      $��1q�5�      ��5�      �� 5�      $	 0,0&�6.   $	 1,0&�5�   .   ��5�   "   � � 5�   &   # �  63   $	 0.0Q�5�   3   � 5�   &   # �  63   $	 1.0Q�5�   3   5�   #   5�      ��5�      �� 5�      $	 1,0&�6.   $ 0.25,0&�5�   .   .��5�      � # �  6+   $ 0Q�5�   +   � 5�      # �  6+   $ 1Q�5�   +   5�      5     ]� 5     $ 
PART_Track��5     $��1��5      5     �� 5     8��  $��Slider.DecreaseLarge��5  '    5     n� 5	     8�� 6     5     �� 5     8�� $��Slider.IncreaseLarge��5  '   5     m� 5     $ 
BottomTick��6    . $��2��5     # z��5     $ Right=�5     $��4��5     $y�	Collapsed=�5     5      5     P� 5      �@��TickPlacement$ TopLeft=�5     . �5     �� $  TopTick��6    � y�$ Visible=�5     5     P� 5      �@��TickPlacement$ BottomRight=�5      . �5!     �� $  
BottomTick��6    � y�$ Visible=�5#     5$     P� 5%      �@��TickPlacement$
 Both=�5&     . �5'     �� $  TopTick��6    � y�$ Visible=�5)     �� 5*     $  
BottomTick��6    � y�$ Visible=�5,     5-     5/     �� 50  
   " ��6   .�51     ��  �@J�SnapsToDevicePixels . 52     �� 53      �@�OverridesDefaultStyle . 54     " 55     P� 56      �@��Orientation$ 
Horizontal=�57     . �58     ��  � ��$	 104��59     �� 5:      � ��$ 21��5;     �� 5<      � ��8   5=     5>     P� 5?      �@��Orientation$ Vertical=�5@     . �5A     ��  � ��$ 21��5B     �� 5C      � ��$	 104��5D     �� 5E      � ��8  5F     5G     5I     5J     .��5K     � .+�5L  
   �� -   5        # ��Name$# slider��5L     $ �� Ticks$$ 	1,2,3,4,5R�5M     
�������% �� IsSnapToTickEnabled% . & �� TickPlacement$& BottomRight=�5P     ' �� AutoToolTipPlacement$' BottomRight=�5Q     ( �� AutoToolTipPrecision$( 2��5R     ) � Minimum$) 1Q�5S     * � Maximum$* 5Q�5T     + � Value$+ 1Q�5U     5V     5W     