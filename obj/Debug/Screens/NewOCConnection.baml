   M S B A M L   `   `   `  ����  ocDownloader*    $ocDownloader.Screens.NewOCConnection   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   J FWpfAnimatedGif, Version=1.4.13.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	     � Title$  New ownCloud Connection��5   	   $	��330��6)   $	��300��66    � Icon$&  ..\Images\ocdownloader_white.ico��6B   
������  � WindowStartupLocation$ CenterOwner=�6�    � WindowStyle$
 None=�6�    � 
ResizeMode$ NoResize=�6�    � AllowsTransparency .  � 	Resources 5       �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value��%p  '��       �� 5   
   " ��6   .�5      ��  � ��	 �	#FF2D2D306+   �� 5	       � ��	 5
      �� 5      "
 ��6&   .R�5      �� #��z��6!   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5       5      P� 5       �@J�IsMouseOver . . �5      ��  � ��	 �	#FF33333463   5      5      5      .��5      �� -   5        $��MainWindowBorder��5      $��3q�6%   $
��auto��69   $��Stretch=�6G   $��0q�6e   $��Stretch=�6p   $
��auto��6�   ���	#FF2D2D30 �� CornerRadius$ 0��6�    J� BitmapEffect 5   
   F� 5       F� Color$ Black��6%    F� 	Direction$	 310Q�63    F� ShadowDepth$ 5Q�6C    F� Opacity$ .5Q�6S    F� Softness$ .7Q�6`   ._�5   
   � .+�5      �� $��NEW OWNCLOUD CONNECTION��6   $��Stretch=�66   $	��Top=�6T   $��27��6l   $
��auto��6x   $��5,0,0,0q�6�   ���	#FF2D2D30���	#FF999999$
��Left=�6�   �� 5      -   5        $��30��5      $��20��6    $��Right=�6,   $	��Top=�6H   $��0,5,5,0q�6`   |�.  .��5       �� $��16��6   $��16��6$   $	��Top=�6/   $"��..\Images\shutdown_white.png��6G   5!      �� 5"      $	��252��6   $	��120��6    $'��!..\Images\ConnectionForm\logo.png��6-   $	��Top=�6X   $��0,30,0,0q�6p   �� 5#      $��16��6   $��16��6   $'��!..\Images\ConnectionForm\name.png��6+   $	��Top=�6V   $
��Left=�6n   $��
15,170,0,0q�6�   �� 5$      -   5        $��ConnectionName��5$      $
��Name��6,   
�������$	��235��6O    �� 	MaxLength$ 20��6[   $��20��6j   $	��Top=�6v   $
��Left=�6�   $��
40,167,0,0q�6�    �� MaxLines$ 1��6  �� 5%      $��16��6   $��16��6   $&�� ..\Images\ConnectionForm\url.png��6+   $	��Top=�6U   $
��Left=�6m   $��
15,200,0,0q�6�   �� 5&      -   5        $��ConnectionURL��5&      $��ownCloud URL��6+   
�������$	��235��6V   $��20��6b   $	��Top=�6n   $
��Left=�6�   $��
40,197,0,0q�6�   $ 1��6�   �� 5'      $��16��6   $��16��6   $'��!..\Images\ConnectionForm\user.png��6+   $	��Top=�6V   $
��Left=�6n   $��
15,230,0,0q�6�   �� 5(      -   5        $��ConnectionUsername��5(      $��Username��60   
�������$	��235��6W   $��20��6c   $	��Top=�6o   $
��Left=�6�   $��
40,227,0,0q�6�   $ 1��6�   �� 5)      $��16��6   $��16��6   $+��%..\Images\ConnectionForm\password.png��6+   $	��Top=�6Z   $
��Left=�6r   $��
15,260,0,0q�6�   G� 5*      -   5        $��ConnectionPassword��5*       G� Password$ Password��64   
�������$	��235��6_   $��20��6k   $	��Top=�6w   $
��Left=�6�   $��
40,257,0,0q�6�    G� PasswordChar$ *��6  �� 5+      -   5        $��NewConnectionLoader��5+       J� Opacity$ 1Q�6/   $��16��6;   $��16��6G   "  WpfAnimatedGif.ImageBehavior   AnimatedSource$ ..\Images\loader.gif��6R   $	��Top=�6�   $��Right=�6�   $��
0,292,42,0q�6�   �� 5,      -   5        $��
SaveButton��5,      $��60��6'   $��25��62   $	��Top=�6>   $��Right=�6V   $��
0,287,20,0q�6r   .��5-      �� $
��SAVE��6   
�������6'   5.      5/      50      51      