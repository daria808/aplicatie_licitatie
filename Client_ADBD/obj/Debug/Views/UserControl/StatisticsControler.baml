   M S B A M L   `   `   `  ����  Client_ADBD7    1Client_ADBD.Views.UserControl.StatisticsControler   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $��1q�5      
�������$��10q�5
      .��5      ��   �� CornerRadius$  10��6   
������ $��1q�69    J� InputBindings 5   
   d� 5       d� Gesture$ 	LeftClicka�6    �� Command 6/   �� *!NavigateToAuctionDetailsCommand+._�5   
   � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��2* �6#   �� 5      $��3* �6#   .+�5      �� $��0��6    �� Orientation$ Vertical=�6)   $��Center=�6@   $��10q�6]   .+�5      �� $��24(�5      $
��Bold"�6*   
���   ���5      �� *	EndTime+ �� StringFormat$ {0:dd}��5   <   �� 5      $��12(�5      
���������5      �� *	EndTime+$ {0:MMM yyyy}��5   *   �� 5      $��12(�5       ���	#FF009688$
��Bold"�6A   ��5      �� *	EndTime+$ {0:dddd}��5    A   �� 5!      $��12(�5"      
���   ���5!      �� *	EndTime+$ 	{0:HH:mm}��5"   *   5#      �� 5&      $��1��6   $��10q�5(      $��UniformToFill=�6    $	��100��68   $	��200��6E   ��5'      �� *	ImagePath+5(   E   �� 5+      $��2��6   $ Vertical=�6)   $��10q�6@   .+�5-      �� $��14(�5.      $
��Bold"�5/      
���   �$
��Wrap=�51      ��5-      �� *Name+51      �� 54      $��12(�55      
�������$
��Wrap=�57      ��54      �� *
Location+57      �� 59      $��12(�5:      
�������$
��Wrap=�5<      ��59      �� *Total+$ Total: {0:N2} €��5<      �� 5>      $��12(�5?      
�������$
��Wrap=�5A      ��5>      �� *	Procent+$ Procent: {0:N2} %��5A      5B      5C      5D      5E      