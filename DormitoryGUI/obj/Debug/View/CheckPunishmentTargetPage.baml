   M S B A M L   `   `   `  ����G  CDormitoryGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DormitoryGUI.ViewDormitoryGUI.View  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      ?$clr-namespace:DormitoryGUI.ViewModelDormitoryGUI.ViewModel  5      5clr-namespace:DormitoryGUI.ViewDormitoryGUI.View  5	      1    +DormitoryGUI.View.CheckPunishmentTargetPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      1localclr-namespace:DormitoryGUI.View     5      6sys&clr-namespace:System;assembly=mscorlib     5      :	viewmodel$clr-namespace:DormitoryGUI.ViewModel     5      0viewclr-namespace:DormitoryGUI.View     5	        N� Title$'  !다벌점 교육 대상자 조회��5       � 	Resources 5      +  %DormitoryGUI.ViewModel.PunishmentList   PunishmentGoodListKey  PunishmentBadListKey(  "DormitoryGUI.ViewModel.StudentList  StudentListKey  WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor 	 SelectedBackgroundColor 
 SelectedUnfocusedColor  ControlLightColor  ControlMediumColor  ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush �� EndPoint �� 
StartPoint � Color � Offset $  ProgressBarIndicatorAnimatedFill �� 
TargetType �� Property �� Value	 �� 
TargetType
 � ColumnDefinitions � RowDefinitions J� SnapsToDevicePixels �� Columns �� Path �� RelativeSource! �� ColumnHeaderContainerStyle �� ColumnHeaderTemplate# �� ColumnHeaderTemplateSelector �� AllowsColumnReorder �� ColumnHeaderContextMenu �� ColumnHeaderToolTip-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation �� CanContentScroll �� CanHorizontallyScroll �� CanVerticallyScroll �� Orientation �� ScrollableWidth � Maximum �� ViewportWidth �� ViewportSize �� HorizontalOffset  � Value,! �� %ComputedHorizontalScrollBarVisibility" �� ScrollableHeight# �� ViewportHeight$ �� VerticalOffset*% �� #ComputedVerticalScrollBarVisibility   GridViewColumnHeaderGripper& �� Padding'  !System.Windows.VisualStateManager'   VisualStateGroups%  System.Windows.VisualStateGroup(   States   System.Windows.VisualState) ��Name* ��Name+ n�Name, �� Triggers- P� Property. P� Value/ � ActualWidth0 � ActualHeight1 �� Color2 �� Triggers3 �� 
TargetName4   
Storyboard5 �� 
TargetName6 �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame7 �� KeyTime8 �� Value��%�  &	        &	      &	 &     &	 9     &	 Z     &	 {     &	 �     &	 �     &	 �     &		 �     &	
      &	 A    &	 ^    &	     &	 �    &	 �    &	 �    &	     &	 F    &	 g    &	 �    &	 �    &	 �    &	 �    &	     &	 -    &	 J    &	 k    &	 �    &	 �    &	 �    &	 �    (�� �    5K      *'%GridView.GridViewScrollViewerStyleKey+)&	  �    '�� �    7   '�� �    7  '��     7 	 7 
   5   
   6#     5   
   6#     5   
   6    ��5   
   	#FFE8EDF9��6/   ��5   
   	#FFC5CBF9��69   ��5   
   	#FF7381F9��68   ��5   
   	#FFE8EDF9��6=   ��5   
   	#FFC5CBF9��6<   ��5   
   	#FF888888��6;   ��5   
   	#FFCEFFD1��6;   ��5   
   	#FFDDDDDD��6:   ��5   
   	White��61   ��5   
   	#FF7381F9��66   ��5   
   	#FF211AA9��64   ��5   
   	#FF3843C4��69   ��5    
   	#FF211AA9��67   ��5!   
   	#FF444444��6.   ��5"   
   /+sc#1, 0.004391443, 0.002428215, 0.242281124��6T   ��5$   
   	#FFCCCCCC��64   ��5%   
   	#FF888888��65   ��5&   
   	#FF444444��63   ��5'   
   	#FF888888��6;   ��5(   
   	#FF444444��6:   ��5)   
   	#FFAAAAAA��6<   ��5*   
   	#FF888888��6;   ��5+   
   	Black��6;   ��5-   
   	#FFC5CBF9��62   ��5.   
   	Black��6>   ��5/   
   	#FFC5CBF9��68   ��50   
   	#FF3843C4��67   �� 51   
   $ 0.5,1&�52      $ 0.5,0&�53      .��54      � # �  6   $ 0Q�55      � 56      # �  6   $	 0.5Q�57      � 58      # �  6   $ 1Q�59      5:      �� 5;   
   $	 0,0&�5<      $	 1,0&�5=      ��5?      � � 5@      $ 	#000000FF��6#   $ 0Q�5A   #   � 5B      $ 	#600000FF��6#   $	 0.4Q�5C   #   � 5D      $ 	#600000FF��6#   $	 0.6Q�5E   #   � 5F      $ 	#000000FF��6#   $ 1Q�5G   #   5H      5J      �� 5K   
   " ��5L      .�5M      ��  � �� 5N      �� 5O      "	 ��6&   .R�5P      � #��z��6   
 5Q      �� 5R   "   $��* �63   �� 5S   "   $
��Auto �63    5U      �� 5V   "   $��* �60   �� 5W   "   $
��Auto �60   .+�5Y      ]� #��z��6(   .+�5Z   "   �� $	��Top=�6/   $��Hidden=�5[   /   $��Hidden=�5\   /   |�.  .��5^   &   �� $��2,0,2,0q�6A   # z 5s   A    5_   A   �� $" TemplatedParent.View.Columns� �� *TemplatedParent+ 5a   A   �� $5 /TemplatedParent.View.ColumnHeaderContainerStyle� �� *TemplatedParent+ 5d   A   �� $/ )TemplatedParent.View.ColumnHeaderTemplate� �� *TemplatedParent+ 5g   A   �� $7 1TemplatedParent.View.ColumnHeaderTemplateSelector� �� *TemplatedParent+ 5j   A   �� $. (TemplatedParent.View.AllowsColumnReorder� �� *TemplatedParent+ 5m   A   �� $2 ,TemplatedParent.View.ColumnHeaderContextMenu� �� *TemplatedParent+ 5p   A   �� $. (TemplatedParent.View.ColumnHeaderToolTip� �� *TemplatedParent+5s   A   5u   #   �� 5v   "   $!��PART_ScrollContentPresenter��69   $ Local=�5w   9    .  .   .  5z   9   5{      �� 5|      $��PART_HorizontalScrollBar��6(   
������ $ 
Horizontal=�5~   (   $��1��5   (   # z 5�   (   # z 5�   (   #  z 5�   (   #y�z! 5�   (   �� 5�      $��PART_VerticalScrollBar��6(   
������ $��1��5�   (   # z" 5�   (   # z# 5�   (   #  z$ 5�   (   #y�z% 5�   (   5�      5�      5�      5�      �� 5�   
   " n�5�      .�5�      ��  � ��$ 18��5�      �� 5�       � ��
 ���� 5�      �� 5�       � �� 5�      �� 5�      "	 n�6&   .R�5�      �� #& z��6!   
������ ._�5�      �� $��Center=�6(   $��1��5�   (   #��z��5�   (   5�      5�      5�      �� 5�       � �� 5�      �� 5�      $ 0.5,1&�6*   $ 0.5,0&�5�   *   .��5�      � $ Black��6'   $ 0Q�5�   '   � 5�      $ White��6'   $ 1Q�5�   '   5�      5�      5�      �� 5�   
   " ��5�      .�5�      ��  � ��$ Center=�5�      �� 5�       � ��$ Center=�5�      �� 5�       � ��# � �5�      �� 5�       � �� 5�      �� 5�      "	 ��6&   .R�5�      � ' 5�        5�   "   CommonStates��.( 5�   &     
Normal��62     5�   &   	MouseOver��62     5�   &   Pressed��62     5�   &   Disabled��62   5�   #   .+�5�      �� $) HeaderBorder��6%   $��0,1,0,1q�5�   %   $& 2,0,2,0q�5�   %   
������ ._�5�   "   �� $* HeaderContent��63   $��0,0,0,1q�5�   3   ��. #��z��5�   3   #��z��5�   3   # z 5�   3   5�      n� 5�      $+ PART_HeaderGripper��6$   $��Right=�5�   $   $��0,0,-9,0q�5�   $   8��  5�   $   5�      5�      5�      , 5�      P� 5�      - �@��Role$. Floating=�5�      . �5�      ��  �@J�Opacity$	 0.7Q�5�      �� 5�       � �� 5�      �� 5�      "	 ��6.   .R�5�   "   �� $��PART_FloatingHeaderCanvas��6)   .+�5�   &   �� ���	#60000000#��z/ 5�   0   #��z0 5�   0   5�   #   5�      5�      5�      P� 5�      - �@��Role$. Padding=�5�      . �5�      ��  � �� 5�      �� 5�      "	 ��6.   .R�5�   "   �� $��HeaderBorder��6)   $��0,1,0,1q�5�   )   ��5�   &   �� 5�   *   #1 �  6:   ��5�   &   �� 5�   *   $	 0,0&�6>   $	 0,1&�5   >   ��5  2   � � 5  6   # �  6C   $	 0.0Q�5  C   � 5  6   # �  6C   $	 1.0Q�5  C   5  3   5	  +   5  #   5     5     5     5     �� 5  
   " ��5     .�5     ��  �@J�SnapsToDevicePixels . 5     �� 5      �@�OverridesDefaultStyle . 5     �� 5      � ��$ Hidden=�5     �� 5      � ��$ Visible=�5     �� 5      � �� . 5     �� 5      � ��$ Center=�5     �� 5       � �� 5!     �� 5"     "	 ��6&   .R�5#     �� $��Border��6!   $��0q�5$  !   ��5%     �� 5&  "   81   62   ._�5(     �� #��� $�5)  !   .��5+  "   �� 5,     5-     2 5.     P� 5/     - �@��
IsGrouping. . . �51  "   ��  � �� .  52  )   53     P� 54     - � {�. .  . �56  "   �� $3 Border��6)    � �� 58  &   �� 59  *   #1 �  6:   5;  #   5<     5>     5@     5A     �� 5B  
   " ��5C     .�5D     ��  �@J�SnapsToDevicePixels . 5E     �� 5F      �@�OverridesDefaultStyle . 5G     �� 5H      � ��$ Center=�5I     �� 5J      � �� 5K     �� 5L     "	 ��6&   .R�5M     �� $) Border��6!   $& 2q�5N  !    . 
������ ' 5Q       5R  "   CommonStates��.( 5S  &     
Normal��62     5T  &   	MouseOver��62     5U  &   Disabled��62   5V  #     5W  "   SelectionStates��.( 5X  &     
Unselected��62     5Y  &   Selected��.4 5Z  *   �� .��5[  .   �� $5 Border��6K   $06 *(Panel.Background).(SolidColorBrush.Color)�5\  K   .g�5]  2     $7 00:00:00.15��6F   88   5^  F   5_  /   5`  +   5a  '     5b  &   SelectedUnfocused��.4 5c  *   �� .��5d  .   �� $5 Border��6K   $06 *(Panel.Background).(SolidColorBrush.Color)�5e  K   .g�5f  2     $7 00:00:00.15��6F   88  5g  F   5h  /   5i  +   5j  '   5k  #   ._�5m     �� #��z��63   5n     5o     5q     5r     5s     .��5t     � ���#DDD
 5u  
   �� 5v     $
��0.5* �6   �� 5w     $��3* �6   �� 5x     $��2* �6    5z  
   �� 5{     $
��0.8* �6   �� 5|     $��3* �6   �� 5}     $
��0.8* �6   .+�5  
   � $��0��6   $��1��5�     .+�5�     &    DormitoryGUI.View.SideMenuButton  -   5        9  Name$9 
BackButton��5�  #   :   ButtonBackground: �#90dfaa;   ImageSource$; /Image/left-arrow.png��5�  #   < � ToolTip< 5�     _� 5�     $��0q�6   .��5�     �� .+�5�     �� $
��Bold"�6(   뒤로가기6@   5�     5�     5�     5�     � 5�  
   $��1��6   $��1��5�     
 5�     �� 5�     $��* �6#   �� 5�     $��2* �6#   .+�5�     � $��1��6   ���#efefef.+�5�     �� -   5        = ��Name$= StudentList��5�     $��10q�5�     $��0q�5�     ��5�     �� > �� Source#> [ ? �� View? 5�     �� 5�     @ �� AllowsColumnReorder@ . .P�5�     �� $��학번��6-   A �� DisplayMemberBindingA 5�  -   �� $ ClassNumber��� 5�     $��이름��6-   A 5�  -   �� $
 Name��� 5�     $��단계��6-   A 5�  -   �� $ CurrentStep�5�     5�     5�     5�     � 5�  
   $��2��6   $��1��5�     $��55,0,0,0q�6   .+�5�     �� $��Center=�5�     B �� Orientation$B Vertical=�5�     .+�5�     	� -   5        $��전체��5�  3   $��35��6@   C d� 	IsChecked$
C TrueY�6L   D 	� 	GroupName$D Target��6]   E � LayoutTransformE 5�     �� 5�     F �� ScaleX$
F 1.25Q�6)   G �� ScaleY$
G 1.25Q�67   6G   5�     	� 5�     -   5        $��1단계��5�  3   $��35��6A   $D Target��6M   E 5�     �� 5�     $
F 1.25Q�6)   $
G 1.25Q�67   6G   5�     	� 5�     -   5        H 	�Name$H SecondTarget��5�     $��2단계��5�  3   $��35��6A   $D Target��6M   E 5�     �� 5�     $
F 1.25Q�6)   $
G 1.25Q�67   6G   5�     	� 5�     -   5        $��3단계��5�  3   $��35��6A   $D Target��6M   E 5�     �� 5�     $
F 1.25Q�6)   $
G 1.25Q�67   6G   5�     5�     5�     5�     5�     