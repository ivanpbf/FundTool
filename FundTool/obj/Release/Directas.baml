   M S B A M L   `   `   `  ���� J  FFundTool, Version=1.0.7117.14804, Culture=neutral, PublicKeyToken=null#clr-namespace:FundToolFundTool      FundTool.Directas   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:FundTool       � Title$  FundTool - Directas��$��848.5��$	��811��.���� $
��Auto=�$
��Auto=�.��� ���#D3D3D3$	��814��$	��786��.+�� -    �Name$ datosdelsuelo��$	��419��$��4,10,0,0q�$	��Top=�
�������$
��Left=�$	��768�� � RowDefinitions �� $
��183* ��� $
��223* �.+��� $��Datos del Suelo��$��Right=�$	��Top=�$
��Bold"�$��
0,15,406,0q��� $��Tipo de Falla��$��Right=�$��
0,20,187,0q�$	��Top=�$
��Bold"��� -    ��Name$ AnguloFriccion��$��Right=�$��23��$��
0,42,327,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��49�� �� TextAlignment$ Center=��� -   $ Cohesion��$��Right=�$��23��$��
0,70,327,0q�$
 Wrap=�$	��Top=�$��49��$ Center=��� $��Angulo de Friccion:��$��Right=�$��
0,39,381,0q�$	��Top=��� $��	Cohesion:��$��Right=�$��
0,67,381,0q�$	��Top=��� $��Peso Especifico:��$��Right=�$��
0,95,381,0q�$	��Top=��� $��Empotramiento DF:��$��Right=�$��0,123,381,0q�$	��Top=��� -   $ PesoEspecifico��$��Right=�$��23��$��
0,98,327,0q�$
 Wrap=�$	��Top=�$��49��$ Center=��� -   $ EmpotramientoDF��$��Right=�$��23��$��0,126,327,0q�$
 Wrap=�$	��Top=�$��49��$ Center=��� $��Grados��$��Right=�$��
0,39,274,0q�$	��Top=��� $��Ton/m²��$��Right=�$��
0,67,274,0q�$	��Top=��� $��Ton/m³��$��Right=�$��
0,95,274,0q�$	��Top=��� $��m��$��Right=�$��0,123,302,0q�$	��Top=��� -    ��Name$ SiguienteDatosSuelo��$��	Siguiente��$��Right=�$��0,200,357,0q�$	��Top=�$��75�� J� RenderTransformOrigin$ 0.533,0.773&�{�.  $��1���� -   $ NSPTDES��$��Right=�$��23��$��	0,94,90,0q�$
 Wrap=�$	��Top=�$��49��$ Center=��� -   $ LimiteLiquido��$��Right=�$��23��$��
0,126,90,0q�$
 Wrap=�$	��Top=�$��49��$ Center=��� -	   $ RelacionVacios��$��Right=�$��23��$��
0,154,90,0q�$
 Wrap=�$	��Top=�$��49��$ Center=�	� -
    	�Name$ FallaL��$��Falla Local��$��Right=�$��
0,46,188,0q�$	��Top=�	 	� 	GroupName$	 TipoDeFalla��
 d� 	IsChecked$

 TrueY�	� -   $ FallaG��$��Falla General��$��Right=�$��
0,67,174,0q�$	��Top=�$	 TipoDeFalla��	� -   $ Granular��$��Suelo Granular��$��Right=�$��	0,48,65,0q�$	��Top=�$	 TipoDeSuelo��	� -   $ Cohesivo��$��Suelo Cohesivo��$��Right=�$��	0,68,62,0q�$	��Top=�$	 TipoDeSuelo���� -   $ NroEstratos��$��Right=�$��23��$��
0,18,255,0q�$
 Wrap=�$	��Top=�$��30��$��1���� -   $ IntroducirEstratosBot��$��Introducir Estratos��$��Right=�$��
0,21,106,0q�$	��Top=�$	��144��$��1��&   System.Windows.Controls.DataGrid  -     Name$ DataGridEstratos��$	��131��$��	10,46,0,0q�$	��Top=�   CanUserAddRows .  $
��Left=�$	��744��$��1��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * ���� �� -   $ AceptarValoresEstratos��$��Aceptar Estratos��$��Right=�$��
0,185,39,0q�$	��Top=�$	��107��{�.  $ 0.458,-0.15&�$��1���� $��Tipo de Suelo (en Punta):��$
��Bold"�$��Right=�$��	0,20,10,0q�$	��Top=��� $��NSPT desfavorable:��$��Right=�$��
0,91,146,0q�$	��Top=��� $��Limite Liquido:��$��Right=�$��0,123,146,0q�$	��Top=��� $��Golpes��$��Right=�$��	0,94,38,0q�$	��Top=��� $��%��$��Right=�$��
0,126,65,0q�$	��Top=��� $��Relacion de Vacios:��$��Right=�$��0,149,146,0q�$	��Top=��� $��Asentamiento Permisible:��$��Right=�$��0,149,360,0q�$	��Top=��� -   $ Asentamiento��$��Right=�$��23��$��0,154,327,0q�$
 Wrap=�$	��Top=�$��30��$ Center=�$ 0.533,1&��� $��mm��$��0,151,287,0q�$	��Top=�$��Right=�$��35��$ 	0.8,0.615&��� $��Numero de Estratos:��$
��Left=�$��
365,15,0,0q�$��1��$	��Top=��� $T��N*De existir nivel freatico, insertar peso unitario saturado de los estratos.
��$
��Left=�$��
10,177,0,0q�$��1��$	��Top=�$��10(�� -   $ 	GridFinal��$
��Left=�$��43��$��
10,761,0,0q�$	��Top=�$	��764��
�������.+��� -   $��Aceptar y Continuar��$
��Bold"�$
��Left=�$��
318,10,0,0q�$	��Top=�$	��127��$ 	0.874,0.7&�� -   $! DatosDelEnsayoSPTGranulares��$
��Left=�$��93��$��
10,145,0,0q�$	��Top=�$	��253�����#e1d3c1.+��� $��Existe Nivel Freatico:��$
��Left=�$��13,4,0,0q�$	��Top=�$
��Bold"�	� -   $
 SiNF��$��Si��$
��Left=�$��
145,10,0,0q�$	��Top=�$	 ExisteNF��	� -   $
 NoNF��$��No��$
��Left=�$��
196,10,0,0q�$	��Top=�$	 ExisteNF��$

 TrueY��� $��Cota del Nivel Freatico:��$
��Left=�$��8,48,0,0q�$	��Top=��� -   $ CotaNF��$
��Left=�$��23��$��
145,51,0,0q�$
 Wrap=�$	��Top=�$��37��$ Center=�{��� *	IsChecked+ �� ElementName$
 SiNF���� $��m��$
��Left=�$��
196,48,0,0q�$	��Top=�� -   $ SolicitacionesGrid��$	��322��$��	6,434,0,0q�$	��Top=�
�������$
��Left=�$	��766��.+��� $��Solicitaciones��$
��Bold"�$
��Left=�$��	10,10,0,0q�$	��Top=��� $��Numero de Apoyos en X:��$
��Left=�$��	10,39,0,0q�$	��Top=��� -   $ 
NroApoyosX��$
��Left=�$��23��$��
153,42,0,0q�$
 Wrap=�$	��Top=�$��30��$ Center=��� -   $��Introducir Apoyos��$
��Left=�$��
28,119,0,0q�$	��Top=�$	��144���� -   $" AceptarValoresSolicitaciones��$��	Siguiente��$
��Left=�$��
45,292,0,0q�$	��Top=�$	��107��{�.  �� $��Numero de Apoyos en Y:��$
��Left=�$��	10,77,0,0q�$	��Top=��� -   $ 
NroApoyosY��$
��Left=�$��23��$��
153,80,0,0q�$
 Wrap=�$	��Top=�$��30��$ Center=�� -   $ 
GridApoyos��$
��Left=�$	��275��$��
212,12,0,0q�$	��Top=�$	��310��
��������� -   $ ApoyosTotales��$
��Left=�$��23��$��153,152,0,0q�$
 Wrap=�$	��Top=�$��30��{�.  $ Center=��� $��Apoyos totales:��$
��Left=�$��
10,149,0,0q�$	��Top=��� -    $ ModificarDatosBoton��$��Modificar Datos��$
��Left=�$��622,292,0,0q�$	��Top=�$��93��{�.  �� $��Numero:��$
��Left=�$��
555,12,0,0q�$	��Top=�$ 0.411,0.615&��� -!   $ NumeroApoyo��$
��Left=�$��23��$��
611,17,0,0q�$
 Wrap=�$	��Top=�$��32��{�.  $ Center=��� $��Nombre:��$
��Left=�$��
653,14,0,0q�$	��Top=��� -"   $ NombreApoyo��$
��Left=�$��23��$��
708,17,0,0q�$
 Wrap=�$	��Top=�$��32��$ Center=��� $��Coordenada en eje X:��$
��Left=�$��
529,68,0,0q�$	��Top=��� $��m��$
��Left=�$��
720,68,0,0q�$	��Top=��� $��Coordenada en eje Y:��$
��Left=�$��
529,94,0,0q�$	��Top=��� $��m��$
��Left=�$��
720,94,0,0q�$	��Top=��� $��
Carga (Z):��$
��Left=�$��
588,40,0,0q�$	��Top=��� -#   $ 
CargaApoyo��$
��Left=�$��23��$��
657,45,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� -$   $ CoordXApoyo��$
��Left=�$��23��$��
657,71,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� -%   $ CoordYApoyo��$
��Left=�$��23��$��
657,97,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $	��Ton��$
��Left=�$��
717,42,0,0q�$	��Top=��� $��Momento en Eje X:��$
��Left=�$��539,123,0,0q�$	��Top=��� -&   $ MtoEjeXApoyo��$
��Left=�$��23��$��657,123,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $��Ton-m��$
��Left=�$��717,120,0,0q�$	��Top=��� $��Momento en Eje Y:��$
��Left=�$��539,149,0,0q�$	��Top=��� -'   $ MtoEjeYApoyo��$
��Left=�$��23��$��657,149,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $��Ton-m��$
��Left=�$��717,146,0,0q�$	��Top=��� -(   $ FBasalXApoyo��$
��Left=�$��23��$��657,177,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $	��Ton��$
��Left=�$��717,175,0,0q�$	��Top=��� $��Cortante Basal X:��$
��Left=�$��550,175,0,0q�$	��Top=��� -)   $ FBasalYApoyo��$
��Left=�$��23��$��657,204,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $	��Ton��$
��Left=�$��714,199,0,0q�$	��Top=��� $��Cortante Basal Y:��$
��Left=�$��550,201,0,0q�$	��Top=��� $��Dimension Columna X:��$
��Left=�$��522,230,0,0q�$	��Top=��� -*   $ DimensionColumnaX��$
��Left=�$��23��$��657,233,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $��cm��$
��Left=�$��720,230,0,0q�$	��Top=��� $��Dimension Columna Y:��$
��Left=�$��522,261,0,0q�$	��Top=��� -+   $ DimensionColumnaY��$
��Left=�$��23��$��657,261,0,0q�$
 Wrap=�$	��Top=�$��58��$ Center=��� $��cm��$
��Left=�$��720,261,0,0q�$	��Top=��� $
��Left=�$	��100��$��
45,184,0,0q�$	��Top=�$	��125��$��imagenfundtool.jpg��� $
��Left=�$	��130��$��	10,10,0,0q�$	��Top=�$	��253��
�������.+��� $��
Materiales��$��0,0,191,107q�$
��Bold"��� $��Resistencia del Concreto:��$
��Left=�$��1,23,0,0q�$	��Top=�$	��143���� -,   $ ResistenciaConcreto��$
��Left=�$��23��$��
144,26,0,0q�$
 Wrap=�$	��Top=�$��45��$ Center=��� $��kg/cm²��$
��Left=�$��
194,23,0,0q�$	��Top=��� $��Resistencia del Acero:
��$
��Left=�$��1,54,0,0q�$	��Top=�$	��133��$��27���� --   $ ResistenciaAcero��$
��Left=�$��23��$��
144,58,0,0q�$
 Wrap=�$	��Top=�$��45��$ Center=��� $��kg/cm²��$
��Left=�$��
194,55,0,0q�$	��Top=��� -.   $ Aceptarm��$��Aceptar��$
��Left=�$��
126,93,0,0q�$	��Top=�$��53���� -/   $ 	Cancelarm��$��Cancelar��$
��Left=�$��
187,93,0,0q�$	��Top=�$��55��