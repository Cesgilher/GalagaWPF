   M S B A M L   `   `   `  ����D  @GalagaWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:GalagaWPF	GalagaWPF  5   	       GalagaWPF.GamePage   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:GalagaWPF     5   	   -   5          � Title$  GamePage��5	   	   $	��500��6   $	��800��6'   .��5
      � .+�5   
   �� -   5         ��Name$ myCanvas��5      $
|�True��6#   
���   �.+�5      �� -   5        $��enemiesLabel��5      
�������$��16(�6;   $��	ExtraBold"�6I   .��6`   Enemies left: 06q   �� 5      -   5        $��
scoreLabel��5      
�������$��16(�69   $��	ExtraBold"�6G   $
��Left=�6^    �� Left$	 364��6y   $��Center=�6�   .��6�   
Score: 06�   �� 5      -   5        $��
levelLabel��5      
�������$��16(�69   $��	ExtraBold"�6G   $
��Left=�6^   $	 674��6y   $��Center=�6�   .��6�   
Level: 06�   �� 5      -   5         ��Name$ player��5      
�������$��65��65   $��55��6A   $	 387��6L   
 �� Top$	 409��6^   5      �� 5   
   -   5         ��Name$ countdownMessage��5      $��48(�6.   
�������$��Center=�6O   $��Center=�6l   $y�Visible=�6�   �� 5   
   -   5        $��escapePanel��5      $��1��6(   $��Center=�65   $��Center=�6R   $	��252��6m   $	��200��6z   $y�	Collapsed=�6�   .+�5      �� -	   5        $��
Go to Menu��5      $��Stretch=�6A   $��63��6]   �� 5      -
   5        $��Go to Login��5      $��Stretch=�6C   $��63��6_   �� 5      -   5        $��Go to Highscores��5      $��Stretch=�6L   $��63��6h   �� 5      -   5        $��
Close Game��5      $��Stretch=�6=   $��63��6Y   5      5      5      