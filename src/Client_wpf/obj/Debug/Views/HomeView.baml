   M S B A M L   `   `   `  ����  ra(    "HamburgerMenuApp.V3.Views.HomeView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f9Custom,http://metro.mahapps.com/winfx/xaml/controls  5      R NMahApps.Metro.IconPacks, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5        � 	Resources  5         ControlMargin  ControlMargin2  ControlMargin_icon  ControlMargin_settings��%�   &	        &	       &	 @     &	 `     u�5   
   0 0 0 40q�64   u�5   
   0 40 0 0q�65   u�5   
   0 0 0 20q�69   u�5   
   0 0 40 0q�6=   5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      �� 5      �� 5      $��0* �6   .+�5   
   �� $��Sensors Placement��6   $��9.333,10,10,10q�6.   ���	#FF2D2D30���	#FF2D2D30$��Segoe UI Light)�6u   ���	#FF1B1B1B$��2��6�   .��5       �� $��0,-14,-2.333,-3q�6   $.��(/ra;component/Resources/brain_status.png��6-    J� Opacity$	 0.7Q�6_   5!      �� 5"   
   $��Headset Status��6   $��10,10,10.667,10q�6+   ���	#FF2D2D30���	#FF2D2D30$��Segoe UI Light)�6s   ���	#FF1B1B1B$��1��6�   .��5#      � $��0,0,-2,-2.667q�6    5$      �� 5%      $	��91* �6$   �� 5&      $	��70* �6$   �� 5'      $	��73* �6$   .+�5)      .  (MahApps.Metro.IconPacks.PackIconMaterial  �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null]]   Kind$ Cached=�6-    J� RenderTransformOrigin$ 	2.188,1.5&�6<   $��20��6^   $��27��6j   $��9,12,0,0q�6u   
�������6�   �� 5*      $��Status��6   $
��Left=�6)   $��32��6D   $��43,9,-114.667,0q�6P   $	��Top=�6i   $	��641��6�   
�������$��16(�6�   -  'MahApps.Metro.Controls.MetroProgressBar  5+      -   5          Name$ BatteryLevel��5+   *   $��6��6@   $��9,46.667,10.333,0q�6K   $	��Top=�6f   $��1��6~   �� 5,      $��Battery��6   $
��Left=�6*   $��33��6E   $��43,8.667,0,0q�6Q   $	��Top=�6g   $	��488��6   
�������$��16(�6�   $��1��6�     5-      -   5        $ SignalLevel��5-   *   $��0��6?   $��9,45.667,10.333,0q�6J   $	��Top=�6e    � Maximum$ 3Q�6}    � SmallChange$ 1Q�6�   $��2��6�   $ 0.5,0.5&�6�   z�5.      V� 5/      .~�50      �� 	 �� ScaleY$	 -1Q�6-   �� 51      �� 52      T� 53      54      56      �� 57      $��Signal Quality��6   $
��Left=�61   $��38��6L   $��43,6.667,0,0q�6X   $	��Top=�6n   $	��477��6�   
�������$��16(�6�   $��2��6�     58      $ Battery=�6-   $ 	2.188,1.5&�6=   $��20��6_   $��20��6k   $��9,11.667,0,0q�6v   
�������
 � ToolTip$3
 -Number of sensor samples recorded per second.��6�   $��1��6�     59      $ Signal=�6-   $ 	2.188,1.5&�6<   $��20��6^   $��20��6j   $��9,9.667,0,0q�6u   
�������$��2��6�   �� 5:      -   5         ��Name$ HeadsetVersion��5:      $��Dongle Not Connected��60   $��9,41,10.333,0q�6O   
�������$��18(�6y   $��Segoe UI Light)�6�   $��50��6�   $	��Top=�6�   $
��Bold"�6�   ��6�   Z� 6�   5;      5<      �� 5=   
   $��Console��6   $��10,10,10,10.333q�6$   ���	#FF2D2D30���	#FF2D2D30$��Segoe UI Light)�6l   ���	#FF1B1B1B$��1��6�   $��1��6�   $��2��6�   .��5>      �� $��240,10,220,21q�6   $&�� /ra;component/Resources/logo.png��6+   $
 0.05Q�6U   5?      5@      5A      