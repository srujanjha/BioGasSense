   M S B A M L   `   `   `  ����F  BBioGasSense, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BioGasSenseBioGasSense  5   	       BioGasSense.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BioGasSense     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � .+�5
   
   �� .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��20(�6   $
��10,0q�6*   $��	COM Port:��68   �� 5      -   5         ��Name$ COMPort��5      $��15(�6,   $
��10,0q�6:   6H   5      �� 5      $ 
Horizontal=�6   $��10q�62   .+�5      �� -   5         ��Name$ txtFile��5      $
��Left=�6+   $��30��6F    �� TextWrapping$
 Wrap=�6R   $�� ��6f   $	��Top=�6n   $	��369��6�   �� 5      -   5         ��Name$ 	btnBrowse��5      $��Browse��6,   $	��5,0q�6=   6J   �� 5      -   5        $ 
btnRefresh��5      $y�	Collapsed=�6-   $��Refresh��6D   $	��5,0q�6V   6c   5      �� 5      -   5        $ 
btnMeasure��5      $��20(�6)   $��Start��67   $��Center=�6`   �� 5      -   5        $ btnStop��5      {�.  $��20(�68   $
��Stop��6F   $��Center=�6k   �� 5      -   5         ��Name$ txtValue��5      $��18(�6*   $
��Wrap=�68   $��10q�6L   $	��480��6X   $	��130��6d   � 5      -   5         �Name$ prgBar��5      $��15��6*    � IsIndeterminate .  66   5      5      5      