   M S B A M L   `   `   `  ����  ra.    (HamburgerMenuApp.V3.Views.RepositoryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      R NMahApps.Metro.IconPacks, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null:Custom-http://metro.mahapps.com/winfx/xaml/iconpacks  5      -   5        .��5      � $��0,0,0.333,0.333q�6     � RowDefinitions  5   
   �� 5      $
��524* �6   �� 5      $	��57* �6    � ColumnDefinitions 5   
   �� 5      $	��11* �6   �� 5      $
��114* �6   �� 5      $
��192* �6   �� 5      $
��792* �6   �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ listBox��5      $��10,10,0.333,10q�6;   ���	#FF2D2D30$��3��6j   ��6#   �� 6~   � 5   
   $��3��6   $��9.667,10,10.333,10.333q�6   $��2��6?    5      �� 5      $
��409* �6#   �� 5      $
��281* �6#     5      �� 5       $��125.333 �6    �� 5!      $	��134 �6    �� 5"      .+�5$      �� -   5         ��Name$ RecordingMetadata��5$      $��
Repository��62   $��0,0,-0.667,0q�6F   ���	#FF2D2D30���	#FF2D2D30���	#FF2D2D30$��2��6�   $��2��6�   $	��258��6�   $	��Top=�6�   .��5%      � $��10,0,-2.333,-5.333q�6    5&      �� 5'      $��6* �6+   �� 5(      $
��375* �6+   �� 5)      $
��193* �6+   �� 5*      $
��178* �6+     5,      �� 5-      $��6* �6(   �� 5.      $	��67* �6(   �� 5/      $
��104* �6(   �� 50      $	��47* �6(   .+�52      �� -   5         ��Name$ RepositoryName��52      $��1,33,10.667,5.667q�67    �� TextWrapping$
 Wrap=�6R   ���	#FF2D2D30
����������	#FF2D2D30$��1��6�   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Repository ��6�   $��1��6�   $��2��6�   .  (MahApps.Metro.IconPacks.PackIconMaterial  53      �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=1.9.0.0, Culture=neutral, PublicKeyToken=null]]   Kind$ Database=�6.    J� RenderTransformOrigin$ 	2.188,1.5&�6>   $��20��6`   $��20��6l   $��1,6,0,0q�6w   
�������$��1��6�   $��1��6�   �� 54      -   5        	 ��Name$	 RecordingName_label��54      $��Repository Name��69   $
��Left=�6S   $��32��6n   $��22,1,0,0q�6z   $	��Top=�6�   $	��476��6�   
�������$��16(�6�   $��1��6�   $��2��6�   $��1��6�   �� 55      -   5        $ RepositoryDescription��55      $��1,37.333,10.667,13.667q�6=   $
 Wrap=�6]   ���	#FF2D2D30
����������	#FF2D2D30$��2��6�   $, &This is a description of my recording.��6�   $��3��6
  $��1��6  �� 56      -   5        $ 	 RecordingDescription_Label��56      $��Description��6@   $
��Left=�6V   $��29��6q   $��22,3.333,0,0q�6}   $	��Top=�6�   $	��476��6�   
�������$��16(�6�   $��2��6�   $��2��6�   $��1��6�     57      $ MessageText=�6.   $ 	2.188,1.5&�6A   $��20��6c   $��20��6o   $��1,10.333,0,0q�6z   
�������$��2��6�   $��1��6�   �� 58      -   5        $ RepositoryId��58      $��8.333,33,9.667,5.667q�65   $
 Wrap=�6S   ���	#FF2D2D30
����������	#FF2D2D30$��1��6�   $ ID��6�   $��3��6�   
 �� IsReadOnlyCaretVisible
 .  �� 
IsReadOnly . $
��Bold"�6    59      $ FormatListNumbers=�6.   $ 	2.188,1.5&�6G   $��20��6i   $��20��6u   $��8.333,6,0,0q�6�   
�������$��1��6�   $��3��6�   �� 5:      -	   5        $	 RecordingName_label_Copy��5:      $��Repository ID��6>   $
��Left=�6V   $��32��6q   $��33.333,1,0,0q�6}   $	��Top=�6�   $	��140��6�   
�������$��16(�6�   $��1��6�   $��3��6�   �� 5;      -
   5         ��Name$ GuestModeButton_Copy��5;      $��Save Changes��5<         AccentedSquareButtonStyle#���   5=      $��9.333,9.333,10.667,13q�6A   ���	#FF2D2D30
����������	#FF2D2D30+  %MahApps.Metro.Controls.ControlsHelper   ContentCharacterCasing$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��3��6	  $��3��6  {�.  6&  5>      5?      �� 5@      -   5        $ RecordingMetadata_Copy��5@      $��Commits��67   $��0,10.667,-0.667,0.333q�6H   ���	#FF2D2D30���	#FF2D2D30���	#FF2D2D30$��2��6�   $��2��6�   .��5A      �� $
��TODO��6   $
��Left=�6'   $��27��6B   $��10,0,0,0q�6N   $	��Top=�6`   $��45��6x   
�������$ -6.489,-3.395&�6�   5B      5C      �� 5D   
   -   5        $ GuestModeButton��5D      $��Create new Repository��5E      #���   5F      $��8.667,10,10.333,8.333q�6A   ���	#FF2D2D30
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��2��6	  $��1��6  �� 5G   
   -   5        $ GuestModeButton_Copy1��5G      $��Delete��5H      #���   5I      $��9.667,10,11.333,8.333q�6A   ���	#FF252525
����������	#FF2D2D30$ Normal=�6�   $��Normal"�6�   $��Calibri Light)�6�   $��1��6	  $��1��6  5J      5K      