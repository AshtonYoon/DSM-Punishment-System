   M S B A M L   `   `   `  ����G  CDormitoryGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DormitoryGUI.ViewDormitoryGUI.View  5      0    *DormitoryGUI.View.PermissionManagementPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:DormitoryGUI.View     5        N� Title$  PermissionManagementPage��5
      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��6* �6   �� 5      $��* �6   .+�5   
   �� $"��/Image/line-illustration.png��6   $��2��5      $��2��5      $��Bottom=�5      $��Right=�5       J� Opacity$ .3Q�5      $	��500��5      $��1��5      $��0,0,-77,-85q�5      $��1��5      � 5    
   $��0��6   $��1��5!      .+�5"      &    DormitoryGUI.View.SideMenuButton  -   5          Name$ 
BackButton��5"   #      ButtonBackground �#90dfaa   ImageSource$ /Image/left-arrow.png��5$   #    � ToolTip 5%      _� 5&      $��0q�6   .��5'      �� .+�5(      �� $
��Bold"�6(   뒤로가기6@   5)      5*      5,      5-      �� 5.   
   $��1��6   $��1��5/      $��Center=�50      $��Center=�51      .+�52      �� $��Center=�6   .+�53      �� $��	관리자��6   �� 54       �� Orientation$ 
Horizontal=�6   $��
0, 5, 0, 0q�55      .+�56      �� -   5        	 ��Name$	 ManagerPoint��56      $��상벌점 관리��57      �� 58      -   5        $	 ManagerStudent��58      $��학생 관리��59      $��20, 0, 0, 0q�5:      5;      5<      �� 5=      $��Center=�6   $��0, 30, 0, 0q�5>      .+�5?      �� $��사감��6   �� 5@      $ 
Horizontal=�6   $��
0, 5, 0, 0q�5A      .+�5B      �� -   5        $	 
CoachPoint��5B      $��상벌점 관리��5C      �� 5D      -   5        $	 CoachStudent��5D      $��학생 관리��5E      $��20, 0, 0, 0q�5F      5G      5H      �� 5I      $��Center=�6   $��0, 30, 0, 0q�5J      .+�5K      �� $��교사��6   �� 5L      $ 
Horizontal=�6   $��
0, 5, 0, 0q�5M      .+�5N      �� -   5        $	 TeacherPoint��5N      $��상벌점 관리��5O      �� 5P      -   5        $	 TeacherStudent��5P      $��학생 관리��5Q      $��20, 0, 0, 0q�5R      5S      5T      5U      5V      5W      