   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.5.0, Culture=neutral, PublicKeyToken=3b585c2a5f1a92c1G4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5       ra)   #HamburgerMenuApp.V3.Views.AboutView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      .��5      �   � RowDefinitions  5   
   �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      $��0* �6   �� 5      $��0* �6   �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ RawData��5      $��Raw Data��6$   $��11,10,10.333,9.667q�66   ���	#FF2D2D30���	#FF2D2D30+  %MahApps.Metro.Controls.ControlsHelper   ContentCharacterCasing$ Upper=�6�      HeaderFontWeight$
 Thin"�6�   $��2��6�   .��5      #   LiveCharts.Wpf.CartesianChart  -   5          Name$ 	CartChart��5   !   &    LiveCharts.Wpf.Charts.Base.Chart   AnimationsSpeed$ 0:0:0.5h�64   $��	0,-6,-2,3q�6O   ���#2d2d30
   Pan$
 None=�6x      LegendLocation$ Bottom=�6�   ���	#FFB0B0B0	 J� IsManipulationEnabled	 .  
 � OverridesDefaultStyle
 . ��5      �� 5       �� ImageSource$&  /ra;component/Resources/logo.png��6!    �� Opacity$
 0.03Q�6P    j� Stretch$ Uniform=�6_      Series 5!         LiveCharts.Wpf.LineSeries     LiveCharts.Wpf.Series   Title$	 AF3��5"   $      StrokeThickness$ 2Q�5$   !      Stroke
 �  ��   Fill
 ���� $y�Visible=�5'   %      Values 5!   %   �� *
ChartAF3+   PointGeometry 5#   !   Z� 5'   %     5)      $ F7��5+   %   $ 2Q�5,   !   
 ��  �
 ���� $y�Hidden=�5/   %    5)   %   �� *	ChartF7+ 5*   %   Z� 5/   %     51      $ F3��53   %   $ 2Q�54   %   
 � � �
 ���� $y�Visible=�57   %    51   %   �� *	ChartF3+ 52   %   Z� 57   %     59      $	 FC5��5:   %   $ 2Q�5<   %   
 � ���
 ���� $y�Hidden=�5?   %    59   %   �� *
ChartFC5+ 5;   %   Z� 5?   %     5A      $ T7��5B   %   $ 2Q�5D   %   
 � ���
 ���� $y�Hidden=�5G   %    5A   %   �� *	ChartT7+ 5C   %   Z� 5G   %     5I      $ P7��5J   %   $ 2Q�5L   %   
 ���f�
 ���� $y�Visible=�5O   %    5I   %   �� *	ChartP7+ 5K   %   Z� 5O   %     5Q      $ O1��5R   %   $ 2Q�5T   %   
 �r���
 ���� $y�Hidden=�5W   %    5Q   %   �� *	ChartO1+ 5S   %   Z� 5W   %     5Y      $ O2��5Z   %   $ 2Q�5\   %   
 ����
 ���� $y�Hidden=�5_   %    5Y   %   �� *	ChartO2+ 5[   %   Z� 5_   %     5a      $ P8��5b   %   $ 2Q�5d   %   
 �i��
 ���� $y�Hidden=�5g   %    5a   %   �� *	ChartP8+ 5c   %   Z� 5g   %     5i      $ T8��5j   %   $ 2Q�5l   %   
 � ��
 ���� $y�Hidden=�5o   %    5i   %   �� *	ChartT8+ 5k   %   Z� 5o   %     5q      $	 FC6��5r   %   $ 2Q�5t   %   
 � ���
 ���� $y�Hidden=�5w   %    5q   %   �� *
ChartFC6+ 5s   %   Z� 5w   %     5y      $ F4��5z   %   $ 2Q�5|   %   
 ����
 ���� $y�Hidden=�5   %    5y   %   �� *	ChartF4+ 5{   %   Z� 5   %     5�      $ F8��5�   %   $ 2Q�5�   %   
 ��� �
 ���� $y�Hidden=�5�   %    5�   %   �� *	ChartF8+ 5�   %   Z� 5�   %     5�      $	 AF4��5�   %   $ 2Q�5�   %   
 �#�k�
 ���� $y�Hidden=�5�   %    5�   %   �� *
ChartAF4+ 5�   %   Z� 5�   %      AxisX 5�         LiveCharts.Wpf.Axis     LabelFormatter 6   �� *DateTimeFormatter+   MaxValue 5�      �� *	AxisMax+   MinValue 5�      �� *	AxisMin+   Unit 5�      �� *
AxisUnit+   	Separator 5�         LiveCharts.Wpf.Separator  5�         Step 6,   �� *
AxisStep+5�      5�      5�      �� 5�   
   $��Emotions / Expression��6   $��11,9.667,10,10.333q�62   ���	#FF2D2D30���	#FF2D2D30$ Upper=�6}   $
 Thin"�6�   $��1��6�   .��5�      �� $
��TODO��6   $
��Left=�6#   $��27��6>   $	��Top=�6J   $��45��6b   
�������6m   5�      �� 5�   
   $��Marking Tools��6   $��10,9.667,10.333,10.333q�6*   ���	#FF2D2D30���	#FF2D2D30$ Upper=�6y   $
 Thin"�6�   $��1��6�   $��1��6�   .��5�      � $��10,0,0.667,-1q�6   .+�5�      � $
��Left=�6   $	��314��62   $��545,735,-545,-735q�6?   $	��Top=�6Z   $	��554��6r   .+�5�      �� -   5         ��Name$ GuestModeButton_Copy4��5�      $��Delete��5�         AccentedSquareButtonStyle#���   5�      $��10,10,431,209q�6I   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   �� 5�      -   5        $ GuestModeButton_Copy5��5�      $��Delete��5�      #���   5�      $��155,10,286,209q�6I   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   �� 5�      -   5        $ GuestModeButton_Copy6��5�      $��Delete��5�      #���   5�      $��293,10,148,209q�6I   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   �� 5�      -   5        $ GuestModeButton_Copy7��5�      $��Delete��5�      #���   5�      $��431,10,10,209q�6I   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   5�      �� 5�      -   5        $ Mark1Btn��5�      $��1��5�      #���   5�      $��
15,0,426,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6&  �� 5�      -   5        $ Mark2Btn��5�      $��2��5�      #���   5�      $��147,0,294,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6'  �� 5�      -	   5        $ Mark3Btn��5�      $��3��5�      #���   5�      $��283,0,159,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6'  �� 5�      -
   5        $ Mark4Btn��5�      $��4��5�      #���   5�      $��
421,0,20,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6&  �� 5�      -   5        $ Mark5Btn��5�      $��5��5�      #���   5�      $��15,82,426,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6'  �� 5�      -   5        $ Mark6Btn��5�      $��6��5�      #���   5�      $��147,82,294,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6(  �� 5�      -   5        $ Mark7Btn��5�      $��7��5�      #���   5�      $��283,82,158,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  6(  �� 5�      -   5        $ Mark8Btn��5�      $��8��5�      #���   5�      $��421,82,20,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��66��6  $	��Top=�6  	 . 6D  �� 5�      -   5         ��Name$ 
CustomMark��5�      $��15,160,159,0q�6/    �� TextWrapping$
 Wrap=�6E   ���	#FF2D2D30
����������	#FF2D2D30*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Repository ��6�   $��Custom Marker
��6�   $��41��6�   $	��Top=�6�   �� 5�      -   5        $ Mark8Btn_Copy��5�      $��
Add Custom��5�      #���   5�      $��368,157,20,0q�6E   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�     J� RenderTransformOrigin$  0.136,0.416&�6  $��44��6(  $	��Top=�64  6L  5�      5�      5�      5�      