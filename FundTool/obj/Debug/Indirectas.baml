   M S B A M L   `   `   `  ����J  FFundTool, Version=1.0.7117.14805, Culture=neutral, PublicKeyToken=null#clr-namespace:FundToolFundTool  5   	       FundTool.Indirectas   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:FundTool     5   	     � Title$  FundTool - Indirectas��5   	   $	��841��6'   $	��801��64   .��5	      �� $
��Auto=�6   $
��Auto=�68   .��5
   
   � ���#D3D3D3$	��812��6$   .+�5      � -   5         �Name$ GranularGrid��5      $	��420��6)   $��
273,10,0,0q�66   $	��Top=�6J   
�������$y�	Collapsed=�6|    � RowDefinitions 5      �� 5      $
��132* �6$   �� 5      $
��145* �6$   .+�5      �� $��Datos Generales��6   $
��Bold"�62   $��Right=�6D   $��	0,8,374,0q�6`   $	��Top=�6s    J� RenderTransformOrigin$ 0.703,0.731&�6�   �� 5      $��Datos del Pilote��6   $
��Bold"�63   $��Right=�6E   $��
0,36,214,0q�6a   $	��Top=�6u   �� 5      $��Longitud Pilote:��6   $��Right=�63   $��
0,62,382,0q�6O   $	��Top=�6c   �� 5      $��Espesor Relleno:��6   $��Right=�63   $��0,102,379,0q�6O   $	��Top=�6d   �� 5      $��Porcentaje de Acero:��6   $��Right=�67   $��
0,62,103,0q�6S   $	��Top=�6g   �� 5      -   5         ��Name$ LongitudPiloteG��5      $��Right=�63   $��23��6O   $��
0,65,318,0q�6[    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $��49��6�   6�   �� 5      -   5        $ LongitudRellenoG��5      $��Right=�64   $��21��6P   $��0,107,318,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $��49��6�   6�   �� 5      -   5        $ CoefFriccionRellenoG��5      $��Right=�68   $��23��6T   $��0,148,318,0q�6`   $
 Wrap=�6u   $	��Top=�6�   $��49��6�   6�   �� 5      -   5        $ PorcentajeAceroG��5      $��Right=�64   $��23��6P   $��	0,63,49,0q�6\   $
 Wrap=�6o   $	��Top=�6�   $��49��6�   6�   �� 5      -   5        $ 	NSPTPunta��5      $��Right=�6-   $��23��6I   $��
0,150,49,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��49��6�   6�   �� 5      -   5        $ ProfundidadEstudioSuelosG��5      $��Right=�6=   $��23��6Y   $��0,197,203,0q�6e   $
 Wrap=�6z   $	��Top=�6�   $��30��6�   $ 0.583,0.435&�6�    �� TextAlignment$ Center=�6�   $��2��6�   �� 5      $��m��6   $��Right=�6$   $��
0,62,293,0q�6@   $	��Top=�6T   �� 5      $��m��6   $��Right=�6$   $��0,102,293,0q�6@   $	��Top=�6U   �� 5      $��%��6   $��Right=�6$   $��	0,62,24,0q�6@   $	��Top=�6S   �� 5      $��Suelo Granular��6   $
��Bold"�61   $��Right=�6C   $��	0,0,237,0q�6_   $	��Top=�6r   �� 5      -   5         ��Name$ SiguienteDatosSueloG��5      $��	Siguiente��67   $��Right=�6K   $��
0,190,24,0q�6g   $	��Top=�6{   $��75��6�   $ 0.533,0.773&�6�   {�.  $��1��6�   �� 5       $"��Coef. de Friccion
 Relleno:��6   $��Right=�6G   $��0,141,385,0q�6c   $	��Top=�6x   �� 5!      $��kg/cm²��6   $��Right=�6)   $��0,147,265,0q�6E   $	��Top=�6Z   �� 5"      $*��$Profundidad del Sondeo Exploratorio:��6   $��Right=�6G   $	��Top=�6c   $��0,193,238,0q�6{   $��2��6�   �� 5#      $��m��6   $��Right=�6$   $��0,194,178,0q�6@   $	��Top=�6U   $��2��6m   �� 5$      -	   5        $��Actualizar Lista��5$      $��Right=�64   $��0,0,78,0q�6P   $	��Top=�6b   $��95��6z   $��18��6�   $��1��6�   &   System.Windows.Controls.DataGrid  5%      -
   5          Name$ DataGridGolpes��5%      $	��138��63   $��
0,25,178,0q�6@   $	��Top=�6T   	   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 * 6l   $��Right=�6�   $	��248��6�   $ 0.484,0.541&�6�   $��1��6�   ��6|   �� 6�   �� 5&      -   5        $ AceptarMetroGolpes��5&      $��Aceptar��65   $��Right=�6G   $��
0,143,98,0q�6c   $	��Top=�6w   $��75��6�   {�.  $��1��6�   �� 5'      $&�� Numero de Ensayo 
SPT de Punta:��6   $��Right=�6K   $��0,131,103,0q�6g   $	��Top=�6|   $ 1.211,0.077&�6�   5(      � 5)      -   5        $ GranularCohesivoGrid��5)      $	��420��61   $��	10,10,9,0q�6>   $	��Top=�6Q   
�������$y�	Collapsed=�6�   .+�5*      �� $��Datos del Pilote��6   $
��Bold"�63   $��Right=�6E   $��
0,36,222,0q�6a   $	��Top=�6u   �� 5+      $��Longitud Pilote:��6   $��Right=�63   $��
0,60,387,0q�6O   $	��Top=�6c   �� 5,      $��Espesor Relleno:��6   $��Right=�63   $��0,103,387,0q�6O   $	��Top=�6d   �� 5-      $��Porcentaje de Acero:��6   $��Right=�67   $��0,108,140,0q�6S   $	��Top=�6h   �� 5.      -   5        $ LongitudPiloteGC��5.      $��Right=�64   $��23��6P   $��
0,63,326,0q�6\   $
 Wrap=�6p   $	��Top=�6�   $��49��6�   6�   �� 5/      -   5        $ LongitudRellenoGC��5/      $��Right=�65   $��26��6Q   $��0,108,326,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $��49��6�   $ 0.517,0.526&�6�   �� 50      -   5        $ CoefFriccionGC��50      $��Right=�62   $��23��6N   $��	0,63,86,0q�6Z   $
 Wrap=�6m   $	��Top=�6�   $��49��6�   6�   �� 51      -   5        $ PorcentajeAceroGC��51      $��Right=�65   $��23��6Q   $��
0,111,86,0q�6]   $
 Wrap=�6q   $	��Top=�6�   $��49��6�   6�   �� 52      $��m��6   $��Right=�6$   $��
0,60,301,0q�6@   $	��Top=�6T   �� 53      $��m��6   $��Right=�6$   $��0,108,301,0q�6@   $	��Top=�6U   �� 54      $��%��6   $��Right=�6$   $��
0,108,61,0q�6@   $	��Top=�6T   �� 55      $��Suelo Granular-Cohesivo��6   $
��Bold"�6:   $��Right=�6L   $��
0,10,195,0q�6h   $	��Top=�6|   �� 56      -   5        $ SiguienteDatosSueloGC��56      $��	Siguiente��68   $��Right=�6L   $��0,388,360,0q�6h   $	��Top=�6}   $��75��6�   $ 0.533,0.773&�6�   {�.  6�   �� 57      $��Coef. de Friccion:��6   $��Right=�65   $��
0,60,140,0q�6Q   $	��Top=�6e   $ 
0.52,0.577&�6}   �� 58      $��kg/cm²��6   $��Right=�6)   $��	0,60,33,0q�6E   $	��Top=�6X   �� 59      $��Numero de Estratos:��6   $
��Bold"�66   $��Right=�6H   $��0,190,278,0q�6d   $	��Top=�6y   �� 5:      -   5        $ NroEstratos��5:      $��Right=�6/   $��23��6K   $��0,193,247,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��30��6�   6�   �� 5;      -   5        $��Introducir Estratos��5;      $��Right=�67   $��
0,196,96,0q�6S   $	��Top=�6g   $	��144��6   6�     5<      -   5        $ DataGridEstratos��5<      $	��130��65   $��
0,226,11,0q�6B   $	��Top=�6V   
   CanUserAddRows
 .  $��Right=�6�   $	��737��6�   $	 * 6�   ��6n   �� 6�   �� 5=      -   5        $ AceptarValoresEstratos��5=      $��Aceptar Estratos��69   $��Right=�6T   $��
0,373,42,0q�6p   $	��Top=�6�   $	��107��6�   {�.  6�   �� 5>      $]��W * Excluya el relleno y coloque solo los estratos 
que esten en contacto con el pilote.��6   $��Right=�6~   $��0,356,530,0q�6�   $	��Top=�6�   $��10(�6�   �� 5?      $��Datos del Suelo��6   $
��Bold"�62   $��Right=�6D   $��0,165,222,0q�6`   $	��Top=�6u   5@      � 5A      -   5        $ 	GridFinal��5A      $
��Left=�6&   $��44��6A   $��	4,762,0,0q�6M   $	��Top=�6`   $	��763��6x   
�������.+�5B      �� -   5        $��Aceptar y Continuar��5B      $
��Bold"�67   $
��Left=�6I   $��
321,14,0,0q�6d   $	��Top=�6x   $	��127��6�   $ 	0.874,0.7&�6�   5C      � 5D      -   5        $ SolicitacionesGrid��5D      $	��322��6/   $��	3,435,9,0q�6<   $	��Top=�6O   
�������.+�5F      �� $��Solicitaciones��6   $
��Bold"�61   $
��Left=�6C   $��	10,10,0,0q�6^   $	��Top=�6q   �� 5G      $��Numero de Apoyos en X:��6   $
��Left=�69   $��	10,39,0,0q�6T   $	��Top=�6g   �� 5H      -   5        $ 
NroApoyosX��5H      $
��Left=�6.   $��23��6I   $��
153,42,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��30��6�   $ Center=�6�   �� 5I      -   5        $��Introducir Apoyos��5I      $
��Left=�65   $��
28,119,0,0q�6P   $	��Top=�6d   $	��144��6|   6�   �� 5J      -   5        $" AceptarValoresSolicitaciones��5J      $��	Siguiente��6?   $
��Left=�6S   $��
45,292,0,0q�6n   $	��Top=�6�   $	��107��6�   {�.  6�   �� 5K      $��Numero de Apoyos en Y:��6   $
��Left=�69   $��	10,77,0,0q�6T   $	��Top=�6g   �� 5L      -   5        $ 
NroApoyosY��5L      $
��Left=�6.   $��23��6I   $��
153,80,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��30��6�   $ Center=�6�   � 5M      -   5        $ 
GridApoyos��5M      $
��Left=�6+   $	��275��6F   $��
212,12,0,0q�6S   $	��Top=�6g   $	��310��6   
�������6�   �� 5N      -   5        $ ApoyosTotales��5N      $
��Left=�61   $��23��6L   $��153,152,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $��30��6�   {�.  $ Center=�6�   �� 5O      $��Apoyos totales:��6   $
��Left=�62   $��
10,149,0,0q�6M   $	��Top=�6a   �� 5P      -   5        $ ModificarDatosBoton��5P      $��Modificar Datos��66   $
��Left=�6P   $��622,292,0,0q�6k   $	��Top=�6�   $��93��6�   {�.  6�   �� 5Q      $��Numero:��6   $
��Left=�6*   $��
555,12,0,0q�6E   $	��Top=�6Y   $ 0.411,0.615&�6q   �� 5R      -    5        $ NumeroApoyo��5R      $
��Left=�6/   $��23��6J   $��
611,17,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��32��6�   {�.  $ Center=�6�   �� 5S      $��Nombre:��6   $
��Left=�6*   $��
653,14,0,0q�6E   $	��Top=�6Y   �� 5T      -!   5        $ NombreApoyo��5T      $
��Left=�6/   $��23��6J   $��
708,17,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��32��6�   $ Center=�6�   �� 5U      $��Coordenada en eje X:��6   $
��Left=�67   $��
529,68,0,0q�6R   $	��Top=�6f   �� 5V      $��m��6   $
��Left=�6$   $��
720,68,0,0q�6?   $	��Top=�6S   �� 5W      $��Coordenada en eje Y:��6   $
��Left=�67   $��
529,94,0,0q�6R   $	��Top=�6f   �� 5X      $��m��6   $
��Left=�6$   $��
720,94,0,0q�6?   $	��Top=�6S   �� 5Y      $��
Carga (Z):��6   $
��Left=�6-   $��
588,40,0,0q�6H   $	��Top=�6\   �� 5Z      -"   5        $ 
CargaApoyo��5Z      $
��Left=�6.   $��23��6I   $��
657,45,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��58��6�   $ Center=�6�   6�   �� 5[      -#   5        $ CoordXApoyo��5[      $
��Left=�6/   $��23��6J   $��
657,71,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��58��6�   $ Center=�6�   6�   �� 5\      -$   5        $ CoordYApoyo��5\      $
��Left=�6/   $��23��6J   $��
657,97,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $��58��6�   $ Center=�6�   6�   �� 5]      $	��Ton��6   $
��Left=�6&   $��
717,42,0,0q�6A   $	��Top=�6U   �� 5^      $��Momento en Eje X:��6   $
��Left=�64   $��539,123,0,0q�6O   $	��Top=�6d   �� 5_      -%   5        $ MtoEjeXApoyo��5_      $
��Left=�60   $��23��6K   $��657,123,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5`      $��Ton-m��6   $
��Left=�6(   $��717,120,0,0q�6C   $	��Top=�6X   �� 5a      $��Momento en Eje Y:��6   $
��Left=�64   $��539,149,0,0q�6O   $	��Top=�6d   �� 5b      -&   5        $ MtoEjeYApoyo��5b      $
��Left=�60   $��23��6K   $��657,149,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5c      $��Ton-m��6   $
��Left=�6(   $��717,146,0,0q�6C   $	��Top=�6X   �� 5d      -'   5        $ FBasalXApoyo��5d      $
��Left=�60   $��23��6K   $��657,177,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5e      $	��Ton��6   $
��Left=�6&   $��717,175,0,0q�6A   $	��Top=�6V   �� 5f      $��Cortante Basal X:��6   $
��Left=�64   $��550,175,0,0q�6O   $	��Top=�6d   �� 5g      -(   5        $ FBasalYApoyo��5g      $
��Left=�60   $��23��6K   $��657,204,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5h      $	��Ton��6   $
��Left=�6&   $��714,199,0,0q�6A   $	��Top=�6V   �� 5i      $��Cortante Basal Y:��6   $
��Left=�64   $��550,201,0,0q�6O   $	��Top=�6d   �� 5j      $��Dimension Columna X:��6   $
��Left=�67   $��522,230,0,0q�6R   $	��Top=�6g   �� 5k      -)   5        $ DimensionColumnaX��5k      $
��Left=�65   $��23��6P   $��657,233,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5l      $��cm��6   $
��Left=�6%   $��720,230,0,0q�6@   $	��Top=�6U   �� 5m      $��Dimension Columna Y:��6   $
��Left=�67   $��522,261,0,0q�6R   $	��Top=�6g   �� 5n      -*   5        $ DimensionColumnaY��5n      $
��Left=�65   $��23��6P   $��657,261,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $��58��6�   $ Center=�6�   6�   �� 5o      $��cm��6   $
��Left=�6%   $��720,261,0,0q�6@   $	��Top=�6U   �� 5p      $
��Left=�6   $	��100��63   $��
45,184,0,0q�6@   $	��Top=�6T   $	��125��6l   $��imagenfundtool.jpg��6x   5q      � 5r      -+   5        $ 
Materiales��5r      $
��Left=�6'   $	��130��6B   $��	10,10,0,0q�6O   $	��Top=�6b   $	��263��6z   
�������.+�5s      �� $��
Materiales��6   $��0,0,191,107q�6-   $
��Bold"�6B   �� 5t      $��Resistencia del Concreto:��6   $
��Left=�6<   $��1,23,0,0q�6W   $	��Top=�6i   $	��143��6�   �� 5u      -,   5        $ ResistenciaConcreto��5u      $
��Left=�67   $��23��6R   $��
144,26,0,0q�6^   $
 Wrap=�6r   $	��Top=�6�   $��45��6�   6�   �� 5v      $��kg/cm²��6   $
��Left=�6)   $��
194,23,0,0q�6D   $	��Top=�6X   �� 5w      $��Resistencia del Acero:
��6   $
��Left=�6>   $��1,54,0,0q�6Y   $	��Top=�6k   $	��133��6�   $��27��6�   �� 5x      --   5        $ ResistenciaAcero��5x      $
��Left=�64   $��23��6O   $��
144,58,0,0q�6[   $
 Wrap=�6o   $��
��6�   $	��Top=�6�   $��45��6�   6�   �� 5y      $��kg/cm²��6   $
��Left=�6)   $��
194,55,0,0q�6D   $	��Top=�6X   �� 5z      -.   5        $ Aceptarm��5z      $��Aceptar��6+   $
��Left=�6=   $��
136,93,0,0q�6X   $	��Top=�6l   $��53��6�   6�   �� 5{      -/   5        $ 	Cancelarm��5{      $��Cancelar��6,   $
��Left=�6?   $��
198,93,0,0q�6Z   $	��Top=�6n   $��55��6�   6�   5|      � 5}      -0   5        $ TipoDeSuelo��5}      $
��Left=�6(   $��89��6C   $��
10,145,0,0q�6O   $	��Top=�6c   $	��263��6{   
�������.+�5~      �� $��Tipo de Suelo (en Punta):��6   $
��Bold"�6<   $
��Left=�6N   $��	10,10,0,0q�6i   $	��Top=�6|   	� 5      -1   5         	�Name$ Granular��5      $��Suelo Granular��60   $
��Left=�6I   $��	10,38,0,0q�6d   $	��Top=�6w    	� 	GroupName$ TipoDeSuelo��6�   	� 5�      -2   5        $ GranularCohesivo��5�      $��Suelo Cohesivo��68   $
��Left=�6Q   $��	10,58,0,0q�6l   $	��Top=�6   $ TipoDeSuelo��6�   �� 5�      -3   5        $ 
Siguientes��5�      $��	Siguiente��6-   $
��Left=�6A   $��
198,16,0,0q�6\   $	��Top=�6p   $��53��6�   6�   �� 5�      -4   5        $ 	Cancelars��5�      $��Cambiar��6,   $
��Left=�6>   $��
196,58,0,0q�6Y   $	��Top=�6m   $��55��6�   6�   5�      5�      5�      5�      