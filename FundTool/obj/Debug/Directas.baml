   M S B A M L   `   `   `  ����J  FFundTool, Version=1.0.7117.14805, Culture=neutral, PublicKeyToken=null#clr-namespace:FundToolFundTool  5   	       FundTool.Directas   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:FundTool     5   	     � Title$  FundTool - Directas��5   	   $��848.5��6%   $	��811��64   .��5	      �� $
��Auto=�6   $
��Auto=�68   .��5
   
   � ���#D3D3D3$	��814��6$   $	��786��61   .+�5      � -   5         �Name$ datosdelsuelo��5      $	��419��6*   $��4,10,0,0q�67   $	��Top=�6I   
�������$
��Left=�6{   $	��768��6�    � RowDefinitions 5      �� 5      $
��183* �6$   �� 5      $
��223* �6$   .+�5      �� $��Datos del Suelo��6   $��Right=�62   $	��Top=�6N   $
��Bold"�6f   $��
0,15,406,0q�6x   �� 5      $��Tipo de Falla��6   $��Right=�60   $��
0,20,187,0q�6L   $	��Top=�6`   $
��Bold"�6x   �� 5      -   5         ��Name$ AnguloFriccion��5      $��Right=�62   $��23��6N   $��
0,42,327,0q�6Z    �� TextWrapping$
 Wrap=�6n   $	��Top=�6�   $��49��6�    �� TextAlignment$ Center=�6�   �� 5      -   5        $ Cohesion��5      $��Right=�6,   $��23��6H   $��
0,70,327,0q�6T   $
 Wrap=�6h   $	��Top=�6|   $��49��6�   $ Center=�6�   �� 5      $��Angulo de Friccion:��6   $��Right=�66   $��
0,39,381,0q�6R   $	��Top=�6f   �� 5      $��	Cohesion:��6   $��Right=�6,   $��
0,67,381,0q�6H   $	��Top=�6\   �� 5      $��Peso Especifico:��6   $��Right=�63   $��
0,95,381,0q�6O   $	��Top=�6c   �� 5      $��Empotramiento DF:��6   $��Right=�64   $��0,123,381,0q�6P   $	��Top=�6e   �� 5      -   5        $ PesoEspecifico��5      $��Right=�62   $��23��6N   $��
0,98,327,0q�6Z   $
 Wrap=�6n   $	��Top=�6�   $��49��6�   $ Center=�6�   �� 5      -   5        $ EmpotramientoDF��5      $��Right=�63   $��23��6O   $��0,126,327,0q�6[   $
 Wrap=�6p   $	��Top=�6�   $��49��6�   $ Center=�6�   �� 5      $��Grados��6   $��Right=�6)   $��
0,39,274,0q�6E   $	��Top=�6Y   �� 5      $��Ton/m²��6   $��Right=�6)   $��
0,67,274,0q�6E   $	��Top=�6Y   �� 5      $��Ton/m³��6   $��Right=�6)   $��
0,95,274,0q�6E   $	��Top=�6Y   �� 5      $��m��6   $��Right=�6$   $��0,123,302,0q�6@   $	��Top=�6U   �� 5      -   5         ��Name$ SiguienteDatosSuelo��5      $��	Siguiente��66   $��Right=�6J   $��0,200,357,0q�6f   $	��Top=�6{   $��75��6�    J� RenderTransformOrigin$ 0.533,0.773&�6�   {�.  $��1��6�   �� 5      -   5        $ NSPTDES��5      $��Right=�6+   $��23��6G   $��	0,94,90,0q�6S   $
 Wrap=�6f   $	��Top=�6z   $��49��6�   $ Center=�6�   �� 5       -   5        $ LimiteLiquido��5       $��Right=�61   $��23��6M   $��
0,126,90,0q�6Y   $
 Wrap=�6m   $	��Top=�6�   $��49��6�   $ Center=�6�   �� 5!      -	   5        $ RelacionVacios��5!      $��Right=�62   $��23��6N   $��
0,154,90,0q�6Z   $
 Wrap=�6n   $	��Top=�6�   $��49��6�   $ Center=�6�   	� 5"      -
   5         	�Name$ FallaL��5"      $��Falla Local��6.   $��Right=�6D   $��
0,46,188,0q�6`   $	��Top=�6t   	 	� 	GroupName$	 TipoDeFalla��6�   
 d� 	IsChecked$

 TrueY�6�   	� 5#      -   5        $ FallaG��5#      $��Falla General��6.   $��Right=�6F   $��
0,67,174,0q�6b   $	��Top=�6v   $	 TipoDeFalla��6�   	� 5$      -   5        $ Granular��5$      $��Suelo Granular��60   $��Right=�6I   $��	0,48,65,0q�6e   $	��Top=�6x   $	 TipoDeSuelo��6�   6�   	� 5%      -   5        $ Cohesivo��5%      $��Suelo Cohesivo��60   $��Right=�6I   $��	0,68,62,0q�6e   $	��Top=�6x   $	 TipoDeSuelo��6�   6�   �� 5&      -   5        $ NroEstratos��5&      $��Right=�6/   $��23��6K   $��
0,18,255,0q�6W   $
 Wrap=�6k   $	��Top=�6   $��30��6�   $��1��6�   �� 5'      -   5        $ IntroducirEstratosBot��5'      $��Introducir Estratos��68   $��Right=�6V   $��
0,21,106,0q�6r   $	��Top=�6�   $	��144��6�   $��1��6�   &   System.Windows.Controls.DataGrid  5(      -   5          Name$ DataGridEstratos��5(      $	��131��65   $��	10,46,0,0q�6B   $	��Top=�6U      CanUserAddRows .  $
��Left=�6�   $	��744��6�   $��1��6�      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   ��6m   �� 6�   �� 5)      -   5        $ AceptarValoresEstratos��5)      $��Aceptar Estratos��69   $��Right=�6T   $��
0,185,39,0q�6p   $	��Top=�6�   $	��107��6�   {�.  $ 0.458,-0.15&�6�   $��1��6�   �� 5*      $��Tipo de Suelo (en Punta):��6   $
��Bold"�6<   $��Right=�6N   $��	0,20,10,0q�6j   $	��Top=�6}   �� 5+      $��NSPT desfavorable:��6   $��Right=�65   $��
0,91,146,0q�6Q   $	��Top=�6e   �� 5,      $��Limite Liquido:��6   $��Right=�62   $��0,123,146,0q�6N   $	��Top=�6c   �� 5-      $��Golpes��6   $��Right=�6)   $��	0,94,38,0q�6E   $	��Top=�6X   �� 5.      $��%��6   $��Right=�6$   $��
0,126,65,0q�6@   $	��Top=�6T   �� 5/      $��Relacion de Vacios:��6   $��Right=�66   $��0,149,146,0q�6R   $	��Top=�6g   �� 50      $��Asentamiento Permisible:��6   $��Right=�6;   $��0,149,360,0q�6W   $	��Top=�6l   �� 51      -   5        $ Asentamiento��51      $��Right=�60   $��23��6L   $��0,154,327,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $��30��6�   $ Center=�6�   $ 0.533,1&�6�   �� 52      $��mm��6   $��0,151,287,0q�6%   $	��Top=�6:   $��Right=�6R   $��35��6n   $ 	0.8,0.615&�6y   �� 53      $��Numero de Estratos:��6   $
��Left=�66   $��
365,15,0,0q�6Q   $��1��6e   $	��Top=�6r   �� 54      $T��N*De existir nivel freatico, insertar peso unitario saturado de los estratos.
��6   $
��Left=�6y   $��
10,177,0,0q�6�   $��1��6�   $	��Top=�6�   $��10(�6�   56      � 57      -   5        $ 	GridFinal��57      $
��Left=�6&   $��43��6A   $��
10,761,0,0q�6M   $	��Top=�6a   $	��764��6y   
�������.+�58      �� -   5        $��Aceptar y Continuar��58      $
��Bold"�67   $
��Left=�6I   $��
318,10,0,0q�6d   $	��Top=�6x   $	��127��6�   $ 	0.874,0.7&�6�   59      � 5:      -   5        $! DatosDelEnsayoSPTGranulares��5:      $
��Left=�68   $��93��6S   $��
10,145,0,0q�6_   $	��Top=�6s   $	��253��6�   ���#e1d3c1.+�5;      �� $��Existe Nivel Freatico:��6   $
��Left=�69   $��13,4,0,0q�6T   $	��Top=�6f   $
��Bold"�6~   	� 5<      -   5        $
 SiNF��5<      $��Si��6,   $
��Left=�69   $��
145,10,0,0q�6T   $	��Top=�6h   $	 ExisteNF��6�   	� 5=      -   5        $
 NoNF��5=      $��No��6,   $
��Left=�69   $��
196,10,0,0q�6T   $	��Top=�6h   $	 ExisteNF��6�   $

 TrueY�6�   �� 5>      $��Cota del Nivel Freatico:��6   $
��Left=�6;   $��8,48,0,0q�6V   $	��Top=�6h   �� 5?      -   5        $ CotaNF��5?      $
��Left=�6*   $��23��6E   $��
145,51,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $��37��6�   $ Center=�6�   {�6�   �� *	IsChecked+ �� ElementName$
 SiNF��6�   �� 5@      $��m��6   $
��Left=�6$   $��
196,48,0,0q�6?   $	��Top=�6S   5A      � 5B      -   5        $ SolicitacionesGrid��5B      $	��322��6/   $��	6,434,0,0q�6<   $	��Top=�6O   
�������$
��Left=�6�   $	��766��6�   .+�5D      �� $��Solicitaciones��6   $
��Bold"�61   $
��Left=�6C   $��	10,10,0,0q�6^   $	��Top=�6q   �� 5E      $��Numero de Apoyos en X:��6   $
��Left=�69   $��	10,39,0,0q�6T   $	��Top=�6g   �� 5F      -   5        $ 
NroApoyosX��5F      $
��Left=�6.   $��23��6I   $��
153,42,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��30��6�   $ Center=�6�   �� 5G      -   5        $��Introducir Apoyos��5G      $
��Left=�65   $��
28,119,0,0q�6P   $	��Top=�6d   $	��144��6|   6�   �� 5H      -   5        $" AceptarValoresSolicitaciones��5H      $��	Siguiente��6?   $
��Left=�6S   $��
45,292,0,0q�6n   $	��Top=�6�   $	��107��6�   {�.  6�   �� 5I      $��Numero de Apoyos en Y:��6   $
��Left=�69   $��	10,77,0,0q�6T   $	��Top=�6g   �� 5J      -   5        $ 
NroApoyosY��5J      $
��Left=�6.   $��23��6I   $��
153,80,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��30��6�   $ Center=�6�   � 5K      -   5        $ 
GridApoyos��5K      $
��Left=�6+   $	��275��6F   $��
212,12,0,0q�6S   $	��Top=�6g   $	��310��6   
�������6�   �� 5L      -   5        $ ApoyosTotales��5L      $
��Left=�61   $��23��6L   $��153,152,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $��30��6�   {�.  $ Center=�6�   �� 5M      $��Apoyos totales:��6   $
��Left=�62   $��
10,149,0,0q�6M   $	��Top=�6a   �� 5N      -    5        $ ModificarDatosBoton��5N      $��Modificar Datos��66   $
��Left=�6P   $��622,292,0,0q�6k   $	��Top=�6�   $��93��6�   {�.  6�   �� 5O      $��Numero:��6   $
��Left=�6*   $��
555,12,0,0q�6E   $	��Top=�6Y   $ 0.411,0.615&�6q   �� 5P      -!   5        $ NumeroApoyo��5P      $
��Left=�6/   $��23��6J   $��
611,17,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��32��6�   {�.  $ Center=�6�   �� 5Q      $��Nombre:��6   $
��Left=�6*   $��
653,14,0,0q�6E   $	��Top=�6Y   �� 5R      -"   5        $ NombreApoyo��5R      $
��Left=�6/   $��23��6J   $��
708,17,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��32��6�   $ Center=�6�   �� 5S      $��Coordenada en eje X:��6   $
��Left=�67   $��
529,68,0,0q�6R   $	��Top=�6f   �� 5T      $��m��6   $
��Left=�6$   $��
720,68,0,0q�6?   $	��Top=�6S   �� 5U      $��Coordenada en eje Y:��6   $
��Left=�67   $��
529,94,0,0q�6R   $	��Top=�6f   �� 5V      $��m��6   $
��Left=�6$   $��
720,94,0,0q�6?   $	��Top=�6S   �� 5W      $��
Carga (Z):��6   $
��Left=�6-   $��
588,40,0,0q�6H   $	��Top=�6\   �� 5X      -#   5        $ 
CargaApoyo��5X      $
��Left=�6.   $��23��6I   $��
657,45,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��58��6�   $ Center=�6�   6�   �� 5Y      -$   5        $ CoordXApoyo��5Y      $
��Left=�6/   $��23��6J   $��
657,71,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��58��6�   $ Center=�6�   6�   �� 5Z      -%   5        $ CoordYApoyo��5Z      $
��Left=�6/   $��23��6J   $��
657,97,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��58��6�   $ Center=�6�   6�   �� 5[      $	��Ton��6   $
��Left=�6&   $��
717,42,0,0q�6A   $	��Top=�6U   �� 5\      $��Momento en Eje X:��6   $
��Left=�64   $��539,123,0,0q�6O   $	��Top=�6d   �� 5]      -&   5        $ MtoEjeXApoyo��5]      $
��Left=�60   $��23��6K   $��657,123,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5^      $��Ton-m��6   $
��Left=�6(   $��717,120,0,0q�6C   $	��Top=�6X   �� 5_      $��Momento en Eje Y:��6   $
��Left=�64   $��539,149,0,0q�6O   $	��Top=�6d   �� 5`      -'   5        $ MtoEjeYApoyo��5`      $
��Left=�60   $��23��6K   $��657,149,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5a      $��Ton-m��6   $
��Left=�6(   $��717,146,0,0q�6C   $	��Top=�6X   �� 5b      -(   5        $ FBasalXApoyo��5b      $
��Left=�60   $��23��6K   $��657,177,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5c      $	��Ton��6   $
��Left=�6&   $��717,175,0,0q�6A   $	��Top=�6V   �� 5d      $��Cortante Basal X:��6   $
��Left=�64   $��550,175,0,0q�6O   $	��Top=�6d   �� 5e      -)   5        $ FBasalYApoyo��5e      $
��Left=�60   $��23��6K   $��657,204,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5f      $	��Ton��6   $
��Left=�6&   $��714,199,0,0q�6A   $	��Top=�6V   �� 5g      $��Cortante Basal Y:��6   $
��Left=�64   $��550,201,0,0q�6O   $	��Top=�6d   �� 5h      $��Dimension Columna X:��6   $
��Left=�67   $��522,230,0,0q�6R   $	��Top=�6g   �� 5i      -*   5        $ DimensionColumnaX��5i      $
��Left=�65   $��23��6P   $��657,233,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5j      $��cm��6   $
��Left=�6%   $��720,230,0,0q�6@   $	��Top=�6U   �� 5k      $��Dimension Columna Y:��6   $
��Left=�67   $��522,261,0,0q�6R   $	��Top=�6g   �� 5l      -+   5        $ DimensionColumnaY��5l      $
��Left=�65   $��23��6P   $��657,261,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5m      $��cm��6   $
��Left=�6%   $��720,261,0,0q�6@   $	��Top=�6U   �� 5n      $
��Left=�6   $	��100��63   $��
45,184,0,0q�6@   $	��Top=�6T   $	��125��6l   $��imagenfundtool.jpg��6x   5o      � 5p      $
��Left=�6   $	��130��6.   $��	10,10,0,0q�6;   $	��Top=�6N   $	��253��6f   
�������.+�5q      �� $��
Materiales��6   $��0,0,191,107q�6-   $
��Bold"�6B   �� 5r      $��Resistencia del Concreto:��6   $
��Left=�6<   $��1,23,0,0q�6W   $	��Top=�6i   $	��143��6�   �� 5s      -,   5        $ ResistenciaConcreto��5s      $
��Left=�67   $��23��6R   $��
144,26,0,0q�6^   $
 Wrap=�6r   $	��Top=�6�   $��45��6�   $ Center=�6�   �� 5t      $��kg/cm²��6   $
��Left=�6)   $��
194,23,0,0q�6D   $	��Top=�6X   �� 5u      $��Resistencia del Acero:
��6   $
��Left=�6>   $��1,54,0,0q�6Y   $	��Top=�6k   $	��133��6�   $��27��6�   �� 5v      --   5        $ ResistenciaAcero��5v      $
��Left=�64   $��23��6O   $��
144,58,0,0q�6[   $
 Wrap=�6o   $	��Top=�6�   $��45��6�   $ Center=�6�   �� 5w      $��kg/cm²��6   $
��Left=�6)   $��
194,55,0,0q�6D   $	��Top=�6X   �� 5x      -.   5        $ Aceptarm��5x      $��Aceptar��6+   $
��Left=�6=   $��
126,93,0,0q�6X   $	��Top=�6l   $��53��6�   6�   �� 5y      -/   5        $ 	Cancelarm��5y      $��Cancelar��6,   $
��Left=�6?   $��
187,93,0,0q�6Z   $	��Top=�6n   $��55��6�   6�   5z      5{      5|      5}      