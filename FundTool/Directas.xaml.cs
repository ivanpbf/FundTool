using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace FundTool
{
    /// <summary>
    /// Interaction logic for Directas.xaml
    /// </summary>
    public partial class Directas : Window
    {
        public class Apoyo
        {
            public int Numero { get; set; }
            public String Nombre { get; set; }
            public double CoordEjeX { get; set; }
            public double CoordEjeY { get; set; }
            public double Carga { get; set; }
            public double MtoEnEjeX { get; set; }
            public double MtoEnEjeY { get; set; }
            public double FBasalX { get; set; }
            public double FBasalY { get; set; }
            public double DimensionColumnaX { get; set; }
            public double DimensionColumnaY { get; set; }
            public double AreaZapata { get; set; }
            public double B { get; set; }
            public double Fcs { get; set; }
            public double Fqs { get; set; }
            public double Fps { get; set; }
            public double Fpd { get; set; }
            public double Fcd { get; set; }
            public double Fqd { get; set; }
            public double Qultima { get; set; }
            public double Qadmisible { get; set; }
            public double Esfuerzoefectivo { get; set; }
            public Boolean ZapataConjuntaX { get; set; }
            public Boolean ZapataConjuntaY { get; set; }
            public double L { get; set; }
            public double Ltotal { get; set; }
            public double MaximoApoyo { get; set; }
            public double FactorEX { get; set; }
            public double FactorEY { get; set; }
            public double Qmax { get; set; }
            public double Qmin { get; set; }
            public double SumatoriaMomentosX { get; set; }
            public double SumatoriaMomentosY { get; set; }
            public double DistorsionAngular { get; set; }
            public List<int> combinados { get;set; }
            public Boolean Dimensionada { get; set; }
            public Boolean Dimensionar { get; set; }
            //tal vez aqui va lo de las dimensiones del cuadro
            public double Vertice1X { get; set; }
            public double Vertice1Y { get; set; }
            public double Vertice2X { get; set; }
            public double Vertice2Y { get; set; }
            public double Vertice3X { get; set; }
            public double Vertice3Y { get; set; }
            public double Vertice4X { get; set; }
            public double Vertice4Y { get; set; }
            public double ColumnaV1X { get; set; }
            public double ColumnaV1Y { get; set; }
            public double ColumnaV2X { get; set; }
            public double ColumnaV2Y { get; set; }
            public double ColumnaV3X { get; set; }
            public double ColumnaV3Y { get; set; }
            public double ColumnaV4X { get; set; }
            public double ColumnaV4Y { get; set; }
        }
        public class Estrato
        {
            public String Nombre { get; set; }
            public double Espesor { get; set; }
            public String Descripcion { get; set; }
            public double Angulo { get; set; }
            public double Cohesion { get; set; }
            public double Peso { get; set; }
            public double CotaInicio { get; set; }
            public double CotaFinal { get; set; }
        }
        public String tipoDeSuelo;
        public double resistenciaAcero;
        public double resistenciaConcreto;
        public double anguloFriccion;
        public double cohesion;
        public double pesoEspecifico;
        public double empotramientoDF;
        public String falla;
        public Boolean nivelFreatico;
        public double cotaNivelFreatico;
        public Boolean datosEnsayoSPT;
        public double profundidadEstudioSuelos;
        public double asentamiento;
        public int nsptdesfavorable;
        public Boolean introdujoGolpes;
        public List<Apoyo> apoyos;
        public List<Estrato> estratos;
        public int numeroEstratos;
        public double limiteliquido;
        public double relaciondeVacios;
        public List<double> NC = new List<double> {5.14, 5.38, 5.63, 5.90, 6.19, 6.49, 6.81, 7.16, 7.53, 7.92, 8.35, 8.80, 9.28, 9.81, 10.37, 10.98, 11.63, 12.34, 13.10, 13.93, 14.83, 15.82, 16.88, 18.05,
        19.32, 20.72, 22.25, 23.94, 25.80, 27.86, 30.14, 32.67, 35.49, 38.64, 42.16, 46.12, 50.59, 55.63, 61.35, 67.87, 75.31, 83.86, 93.71, 105.11, 118.37, 133.88, 152.10, 173.64, 199.26, 229.93, 266.89};
        public List<double> NQ = new List<double> {1, 1.09, 1.20, 1.31, 1.43, 1.57, 1.72, 1.88, 2.06, 2.25, 2.47, 2.71, 2.97, 3.26, 3.59, 3.94, 4.34, 4.77, 5.26, 5.80, 6.40, 7.07, 7.82, 8.66, 9.60, 10.66,
        11.85, 13.20, 14.72, 16.44, 18.40, 20.63, 23.18, 26.09, 29.44, 33.3, 37.75, 42.92, 48.93, 55.96, 64.20, 73.90, 85.38, 99.02, 115.31, 134.88, 158.51, 187.21, 222.31, 265.51, 319.07};
        public List<double> NF = new List<double> {0.00, 0.077, 0.15, 0.24, 0.34, 0.45, 0.57, 0.71, 0.86, 1.03, 1.22, 1.44, 1.69, 1.97, 2.29, 2.65, 3.06, 3.53, 4.07, 4.68, 5.39, 6.20, 7.13, 8.20, 9.44, 10.88,
        12.54, 14.47, 16.72, 19.34, 22.40, 25.99, 30.22, 35.19, 41.06, 48.03, 56.31, 66.19, 78.03, 92.25, 109.41, 130.22, 155.55, 186.54, 224.64, 271.76, 330.35, 403.67, 496.01, 613.16, 762.89};
        public Boolean finalizo;


        /// <summary>
        /// Inicializacion de la ventana de Directas
        /// 
        /// Colapsa ciertos elementos del documento XAML para que se abran de manera que avanza el programa y el usuario
        /// </summary>
        public Directas()
        {
            InitializeComponent();
            this.datosdelsuelo.Visibility = Visibility.Collapsed;
            this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
            this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
            this.GridFinal.Visibility = Visibility.Collapsed;
            finalizo = false;
        }

        /// <summary>
        /// Permite que los datos ingresados sean solo numeros y decimales
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
        /// <summary>
        /// Permite que los datos ingresados sean solo numeros y decimales para datagrids
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void DataGrid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Permite que los datos ingresados sean solo numeros, decimales y negativos
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void NumericNegativeDecimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[-]?\d+(?:\.\d{0,2})?$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        /// <summary>
        /// Acepta los materiales seleccionados por el usuario en la interfaz
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void AceptarMateriales(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ResistenciaConcreto.Text) && !String.IsNullOrEmpty(this.ResistenciaAcero.Text))
            {
                this.resistenciaAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.ResistenciaAcero.Text)));
                this.resistenciaConcreto = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.ResistenciaConcreto.Text)));
                this.datosdelsuelo.Visibility = Visibility.Visible;
                this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Visible;
            }
            else if (String.IsNullOrEmpty(this.ResistenciaConcreto.Text))
            {
                MessageBox.Show("Introduzca la Resistencia del Concreto");
                return;
            }
            else if (String.IsNullOrEmpty(this.ResistenciaAcero.Text))
            {
                MessageBox.Show("Introduzca la Resistencia del Acero");
                return;
            }

        }

        /// <summary>
        /// Cierra la ventana.
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void CancelarMateriales(object sender, RoutedEventArgs e)
        {
            this.Close(); //cierra la ventana
        }

        /// <summary>
        /// Verifica que el suelo granular este elegido, de ser asi hace visible 
        /// la parte de la interfaz sobre los datos del ensayo SPT
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void Granular_Checked(object sender, RoutedEventArgs e)
        {
            if ((Boolean)Granular.IsChecked)
            {
                this.tipoDeSuelo = "Granular";
            }
        }

        /// <summary>
        /// Verifica que el suelo cohesivo este elegido, de ser asi hace invisible 
        /// la parte de la interfaz sobre los datos del ensayo SPT
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void Cohesivo_Checked(object sender, RoutedEventArgs e) 
        {
            if ((Boolean)Cohesivo.IsChecked)
            {
                this.tipoDeSuelo = "Cohesivo";
            }
        }

        /// <summary>
        /// Se introducen los estratos que el usuario coloque en una lista de interfaz
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntroducirEstratos(object sender, RoutedEventArgs e)
        {
            if (AceptarValoresEstratos.IsEnabled)
            {
                DataGridEstratos.Columns.Remove(DataGridEstratos.Columns[2]);
            }
            if (!String.IsNullOrEmpty(this.NroEstratos.Text) && !this.NroEstratos.Text.Equals("0"))
            {
                int numero = Int32.Parse(this.NroEstratos.Text);
                this.numeroEstratos = numero;
                estratos = new List<Estrato>();
                for (int i = 0; i < numero; i++)
                {
                    Estrato estratonuevo = new Estrato();
                    Estrato aux = estratonuevo;
                    aux.Nombre = "E-" + (i + 1);
                    aux.Espesor = 0;
                    this.estratos.Add(aux);
                }
                ObservableCollection<Estrato> obsCollection = new ObservableCollection<Estrato>(this.estratos);
                DataGridEstratos.DataContext = obsCollection;
                DataGridEstratos.Columns[0].IsReadOnly = true;
                DataGridEstratos.Columns[1].Header = "Espesor (m)";
                DataGridComboBoxColumn descripcion = new DataGridComboBoxColumn();
                descripcion.ItemsSource = new List<String> { "Granular", "Cohesivo"};
                descripcion.Header = "Descripcion";
                descripcion.TextBinding = new Binding("Descripcion");
                DataGridEstratos.Columns[2] = descripcion;
                DataGridEstratos.Columns[3].Header = "Angulo de \nFriccion";
                DataGridEstratos.Columns[4].Header = "Cohesion \n(Ton/m²)";
                DataGridEstratos.Columns[5].Header = "Peso Unitario \n(Ton/m³)";
                DataGridEstratos.Columns[6].Header = "Cota Inicio \n(m)";
                DataGridEstratos.Columns[7].Header = "Cota Final \n(m)";
                AceptarValoresEstratos.IsEnabled = true;

            }
            else
            {
                MessageBox.Show("Introduzca un numero de Estratos mayor a 0");
                return;
            }

        }

        /// <summary>
        /// Acepta los datos seleccionados por el usuario respecto a los estratos
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntroducirDatosEstratos(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.estratos.Count; i++)
            {
                TextBlock espesor = DataGridEstratos.Columns[1].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                ComboBox ele = DataGridEstratos.Columns[2].GetCellContent(DataGridEstratos.Items[i]) as ComboBox;
                String descripcion = ele.Text;
                TextBlock angulo = DataGridEstratos.Columns[3].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cohesion = DataGridEstratos.Columns[4].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock peso = DataGridEstratos.Columns[5].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cotai = DataGridEstratos.Columns[6].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cotaf = DataGridEstratos.Columns[7].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                this.estratos[i].Espesor = Convert.ToDouble(espesor.Text);
                this.estratos[i].Angulo = Convert.ToDouble(angulo.Text);
                this.estratos[i].Cohesion = Convert.ToDouble(cohesion.Text);
                this.estratos[i].Peso = Convert.ToDouble(peso.Text);
                this.estratos[i].CotaInicio = Convert.ToDouble(cotai.Text);
                this.estratos[i].CotaFinal = Convert.ToDouble(cotaf.Text);
                this.estratos[i].Descripcion = descripcion;
            }
            MessageBox.Show("Se introdujeron los datos de estratos correctamente.");
            this.SiguienteDatosSuelo.IsEnabled = true;
        }

        /// <summary>
        /// Se aceptan los datos relacionados atodo el suelo en base a las opciones marcadas
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntrodujoDatosSuelo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.AnguloFriccion.Text) && !String.IsNullOrEmpty(this.Cohesion.Text) && !String.IsNullOrEmpty(this.PesoEspecifico.Text)
                && !String.IsNullOrEmpty(this.EmpotramientoDF.Text) && !String.IsNullOrEmpty(this.tipoDeSuelo) && !string.IsNullOrEmpty(this.NSPTDES.Text) &&
                !String.IsNullOrEmpty(this.LimiteLiquido.Text) && !String.IsNullOrEmpty(this.RelacionVacios.Text) && !String.IsNullOrEmpty(this.Asentamiento.Text))
            {
                this.anguloFriccion = Convert.ToInt32(this.AnguloFriccion.Text);
                this.cohesion = Convert.ToDouble(this.Cohesion.Text);
                this.pesoEspecifico = Convert.ToDouble(this.PesoEspecifico.Text);
                this.empotramientoDF = Convert.ToDouble(this.EmpotramientoDF.Text);
                this.limiteliquido = Convert.ToDouble(this.LimiteLiquido.Text);
                this.nsptdesfavorable = Int32.Parse(this.NSPTDES.Text);
                this.relaciondeVacios = Convert.ToDouble(this.RelacionVacios.Text);
                this.asentamiento = Convert.ToDouble(this.Asentamiento.Text);
                if ((Boolean)this.FallaL.IsChecked)
                {
                    this.falla = "local";
                }
                else
                {
                    this.falla = "general";
                }
                if ((Boolean)this.SiNF.IsChecked)
                {
                    if (!String.IsNullOrEmpty(this.CotaNF.Text))
                    {
                        this.cotaNivelFreatico = Convert.ToDouble(this.CotaNF.Text);
                        this.nivelFreatico = true;
                    }
                    else
                    {
                        MessageBox.Show("Introduzca los datos de Nivel Freatico");
                        return;
                    }
                }
                this.SolicitacionesGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Introduzca los Datos del Suelo");
                return;
            }
        }

        /// <summary>
        /// Introduce y crea los apoyos sin datos que seran usados por el usuario
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntroducirApoyos(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.NroApoyosX.Text) && !this.NroApoyosX.Text.Equals("0") && !String.IsNullOrEmpty(this.NroApoyosY.Text) && !this.NroApoyosY.Text.Equals("0"))
            {
                this.GridApoyos.Children.Clear();
                this.GridApoyos.RowDefinitions.Clear();
                this.GridApoyos.ColumnDefinitions.Clear();
                int numerox = Int32.Parse(this.NroApoyosX.Text);
                int numeroy = Int32.Parse(this.NroApoyosY.Text);
                int totales = numerox * numeroy;
                //agregando apoyos
                apoyos = new List<Apoyo>();
                for (int i = 0; i < totales; i++)
                {
                    Apoyo apoyonuevo = new Apoyo();
                    apoyonuevo.Numero = i + 1;
                    apoyonuevo.Carga = (double)0;
                    apoyonuevo.CoordEjeX = (double)0;
                    apoyonuevo.CoordEjeY = (double)0;
                    apoyonuevo.MtoEnEjeX = (double)0;
                    apoyonuevo.MtoEnEjeY = (double)0;
                    apoyonuevo.FBasalX = (double)0;
                    apoyonuevo.FBasalY = (double)0;
                    apoyonuevo.DimensionColumnaX = (double)100;
                    apoyonuevo.DimensionColumnaY = (double)100;
                    apoyonuevo.Nombre = "A-" + apoyonuevo.Numero.ToString();
                    apoyonuevo.ZapataConjuntaX = false;
                    apoyonuevo.ZapataConjuntaY = false;
                    apoyos.Add(apoyonuevo);
                }
                this.ApoyosTotales.Text = apoyos.Count().ToString();
                //agregando columnas
                for (int i = 0; i < (numerox * 2) - 1; i++)
                {
                    ColumnDefinition gridcol = new ColumnDefinition();
                    GridApoyos.ColumnDefinitions.Add(gridcol);
                }
                //agregando filas
                for (int i = 0; i < (numeroy * 2) - 1; i++)
                {
                    RowDefinition gridro = new RowDefinition();
                    GridApoyos.RowDefinitions.Add(gridro);
                }
                //agregando botones
                int aux = 1;
                for (int j = 0; j < (numeroy * 2) - 1; j++)
                {
                    for (int i = 0; i < (numerox * 2) - 1; i++)
                    {
                        Button boton = new Button();
                        boton.Content = "";
                        if (j % 2 != 0)
                        {
                            boton.IsEnabled = false;
                        }
                        else
                        {
                            if (i % 2 != 0)
                            {
                                boton.IsEnabled = false;
                            }
                            else
                            {
                                boton.Content = aux.ToString();
                                boton.Click += new RoutedEventHandler(this.BuscarApoyo);
                                aux++;
                            }
                        }
                        Grid.SetRow(boton, j);
                        Grid.SetColumn(boton, i);
                        GridApoyos.Children.Add(boton);
                    }
                }
                ModificarDatosBoton.IsEnabled = true;
                AceptarValoresSolicitaciones.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Introduzca un numero de Apoyos mayor a 0");
                return;
            }
        }

        /// <summary>
        /// Busca el apoyo que el usuario selecciono en la interfaz
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void BuscarApoyo(object sender, RoutedEventArgs e)
        {
            Button elboton = (Button)sender;
            int numero = Int32.Parse((String)elboton.Content);
            this.NombreApoyo.Text = this.apoyos[numero - 1].Nombre;
            this.CargaApoyo.Text = this.apoyos[numero - 1].Carga.ToString();
            this.NumeroApoyo.Text = this.apoyos[numero - 1].Numero.ToString();
            this.CoordXApoyo.Text = this.apoyos[numero - 1].CoordEjeX.ToString();
            this.CoordYApoyo.Text = this.apoyos[numero - 1].CoordEjeY.ToString();
            this.MtoEjeXApoyo.Text = this.apoyos[numero - 1].MtoEnEjeX.ToString();
            this.MtoEjeYApoyo.Text = this.apoyos[numero - 1].MtoEnEjeY.ToString();
            this.FBasalXApoyo.Text = this.apoyos[numero - 1].FBasalX.ToString();
            this.FBasalYApoyo.Text = this.apoyos[numero - 1].FBasalY.ToString();
            this.DimensionColumnaX.Text = this.apoyos[numero - 1].DimensionColumnaX.ToString();
            this.DimensionColumnaY.Text = this.apoyos[numero - 1].DimensionColumnaY.ToString();
        }

        /// <summary>
        /// Acepta los datos modificados por el usuario sobre el apoyo seleccionado
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntroducirDatosApoyo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.NombreApoyo.Text) && !String.IsNullOrEmpty(this.CargaApoyo.Text) && !String.IsNullOrEmpty(this.CoordXApoyo.Text) && !String.IsNullOrEmpty(this.CoordYApoyo.Text)
                && !String.IsNullOrEmpty(this.MtoEjeXApoyo.Text) && !String.IsNullOrEmpty(this.MtoEjeYApoyo.Text) && !String.IsNullOrEmpty(this.FBasalXApoyo.Text) && !String.IsNullOrEmpty(this.FBasalYApoyo.Text)
                && !String.IsNullOrEmpty(this.DimensionColumnaX.Text) && !String.IsNullOrEmpty(this.DimensionColumnaY.Text))
            {
                int numero = Int32.Parse(this.NumeroApoyo.Text);
                this.apoyos[numero - 1].Nombre = this.NombreApoyo.Text;
                this.apoyos[numero - 1].Carga = (double)Convert.ToDouble(this.CargaApoyo.Text);
                this.apoyos[numero - 1].CoordEjeX = (double)Convert.ToDouble(this.CoordXApoyo.Text);
                this.apoyos[numero - 1].CoordEjeY = (double)Convert.ToDouble(this.CoordYApoyo.Text);
                this.apoyos[numero - 1].MtoEnEjeX = (double)Convert.ToDouble(this.MtoEjeXApoyo.Text);
                this.apoyos[numero - 1].MtoEnEjeY = (double)Convert.ToDouble(this.MtoEjeYApoyo.Text);
                this.apoyos[numero - 1].FBasalX = (double)Convert.ToDouble(this.FBasalXApoyo.Text);
                this.apoyos[numero - 1].FBasalY = (double)Convert.ToDouble(this.FBasalYApoyo.Text);
                this.apoyos[numero - 1].DimensionColumnaX = (double)Convert.ToDouble(this.DimensionColumnaX.Text);
                this.apoyos[numero - 1].DimensionColumnaY = (double)Convert.ToDouble(this.DimensionColumnaY.Text);
                this.apoyos[numero - 1].Dimensionada = false;
                this.apoyos[numero - 1].Dimensionar = true;
                MessageBox.Show("Se introdujeron los datos correctamente.");
            }
            else
            {
                MessageBox.Show("Alguno de los valores esta vacio, por favor llene los datos.");
                return;
            }
        }

        /// <summary>
        /// Genera una nueva parte en la interfaz identificando que ya el usuario introdujo los datos de las solicitaciones
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntroducirDatosSolicitaciones(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < this.apoyos.Count(); i++)
            {
                if(this.apoyos[i].Carga == 0)
                {
                    this.apoyos.RemoveAt(i);
                }
            }
            for(int i= 0; i < this.apoyos.Count(); i++)
            {
                this.apoyos[i].Numero = i + 1;
            }
            this.GridFinal.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// El usuario indica que ya selecciono los datos relevantes y el programa 
        /// seguira a hacer los calculos y metodos necesarios para finalizar ejecucion
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void CompletarDirectas(object sender, RoutedEventArgs e)
        {
            if (this.falla == "local")
            {
                this.cohesion = (0.666666666667) * (double)this.cohesion;
                this.anguloFriccion = Math.Atan((0.666666666667) * Math.Tan(this.anguloFriccion * Math.PI/180));
                this.anguloFriccion = this.anguloFriccion * 180 / Math.PI;
                this.anguloFriccion = Math.Floor(this.anguloFriccion);
            }
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                double pesoMenor = 999999999999;
                this.apoyos[i].B = 1;
                if (this.nivelFreatico)
                {
                    ////messagebox.show("Existe nivel freatico");
                    //caso a
                    if (this.cotaNivelFreatico >= 0 && this.cotaNivelFreatico < this.empotramientoDF)
                    {
                        //messagebox.show("Caso A nivel freatico >= 0 y <= DF");
                        double esp = 0;
                        for (int j = 0; j < this.estratos.Count(); j++)
                        {
                            if(this.estratos[j].CotaInicio < this.empotramientoDF)
                            {
                                if (this.estratos[j].CotaInicio <= this.cotaNivelFreatico && this.estratos[j].CotaFinal > this.cotaNivelFreatico)
                                {
                                    if(this.estratos[j].CotaFinal < this.empotramientoDF)
                                    {
                                        this.estratos[j].Peso = this.estratos[j].Peso - 1;
                                        //messagebox.show("Nuevo peso del estrato " + (j + 1) + " es " + this.estratos[j].Peso);
                                        esp = esp + (this.estratos[j].Espesor * this.estratos[j].Peso);
                                    }
                                    else
                                    {
                                        this.estratos[j].Peso = this.estratos[j].Peso - 1;
                                        //messagebox.show("Nuevo peso del estrato " + (j + 1) + " es " + this.estratos[j].Peso);
                                        double espesorese = this.empotramientoDF - this.estratos[j].CotaInicio;
                                        esp = esp + (espesorese * this.estratos[j].Peso);
                                    }

                                }
                                /*else if (this.estratos[j].CotaFinal > this.empotramientoDF)
                                {
                                    double espesorese = this.empotramientoDF - this.estratos[j].CotaInicio;
                                    double pesoDeEse = this.estratos[j].Peso - 1;
                                    esp = esp + (espesorese * this.estratos[j].Peso);
                                }*/
                                else
                                {
                                    if(this.estratos[j].CotaInicio > this.cotaNivelFreatico)
                                    {
                                        this.estratos[j].Peso = this.estratos[j].Peso - 1;
                                        //messagebox.show("Nuevo peso del estrato " + (j + 1) + " es " + this.estratos[j].Peso);
                                        esp = esp + (this.estratos[j].Espesor * this.estratos[j].Peso);
                                    }
                                    else
                                    {
                                        esp = esp + (this.estratos[j].Espesor * this.estratos[j].Peso);
                                    }
                                }
                                //messagebox.show("Acumulado " + esp);
                            }
                        }
                        this.apoyos[i].Esfuerzoefectivo = esp;
                        //messagebox.show("Esfuerzo efectivo Caso A " + this.apoyos[i].Esfuerzoefectivo);
                        for (int j = 0; j < this.estratos.Count; j++)
                        {
                            if (this.estratos[j].Peso < pesoMenor)
                            {
                                pesoMenor = this.estratos[j].Peso;
                            }
                        }
                    }
                    //caso b
                    else if (this.cotaNivelFreatico > this.empotramientoDF && this.cotaNivelFreatico < (this.empotramientoDF + 1))
                    {
                        for (int j = 0; j < this.estratos.Count; j++)
                        {
                            if(((this.estratos[j].CotaFinal - this.cotaNivelFreatico) > 0) && (((this.empotramientoDF+1)-this.estratos[j].CotaInicio) > 0))
                            {
                                //messagebox.show("Para el Caso B entra estrato " + (j + 1) + " a verificar si su peso es menor");
                                if (this.estratos[j].Peso < pesoMenor)
                                {
                                    pesoMenor = this.estratos[j].Peso;
                                }
                            }    
                        }
                        //messagebox.show("Menor agarrado " + pesoMenor);
                        pesoMenor = pesoMenor + ((this.cotaNivelFreatico - this.empotramientoDF) / 1) * (pesoMenor - 1);
                        this.apoyos[i].Esfuerzoefectivo = this.empotramientoDF * this.pesoEspecifico;
                        //messagebox.show("Nuevo peso menor en el Caso B es " + pesoMenor);
                    }
                    //caso c
                    else
                    {
                        this.apoyos[i].Esfuerzoefectivo = this.empotramientoDF * this.pesoEspecifico;
                        //messagebox.show("Esfuerzo efectivo Caso C " + this.apoyos[i].Esfuerzoefectivo);
                    }
                }
                else
                {
                    this.apoyos[i].Esfuerzoefectivo = this.empotramientoDF * this.pesoEspecifico;
                    for (int j = 0; j < this.estratos.Count; j++)
                    {
                        if (this.estratos[j].Peso < pesoMenor)
                        {
                            pesoMenor = this.estratos[j].Peso;
                        }
                    }
                }
                //factores de forma
                this.apoyos[i].Fcs = 1 + (this.apoyos[i].B * this.NQ[(int)this.anguloFriccion]) / (this.NC[(int)this.anguloFriccion]);
                this.apoyos[i].Fqs = 1 + (Math.Tan(this.anguloFriccion * Math.PI / 180));
                this.apoyos[i].Fps = 1 - 0.4 * this.apoyos[i].B;
                //factores de profundidad
                if ((this.empotramientoDF / this.apoyos[i].B) < 1)
                {
                    this.apoyos[i].Fcd = 1 + (0.4 * this.empotramientoDF / this.apoyos[i].B);
                    this.apoyos[i].Fpd = 1;
                    this.apoyos[i].Fqd = 1 + (2 * Math.Tan(this.anguloFriccion * Math.PI / 180) * Math.Pow((1 - Math.Sin(this.anguloFriccion * Math.PI / 180)), 2) * (this.empotramientoDF / this.apoyos[i].B));
                }
                else
                {
                    this.apoyos[i].Fpd = 1;
                    this.apoyos[i].Fcd = 1 + 0.4 * Math.Atan(this.empotramientoDF / this.apoyos[i].B);
                    this.apoyos[i].Fqd = 1 + (2 * Math.Tan(this.anguloFriccion * Math.PI / 180)) * Math.Pow((1 - Math.Sin(this.anguloFriccion * Math.PI / 180)), 2) * Math.Atan(this.empotramientoDF / this.apoyos[i].B);
                }
                //carga ultima
                //double q = this.pesoEspecifico * this.empotramientoDF;
                double q = this.apoyos[i].Esfuerzoefectivo;
                this.apoyos[i].combinados = new List<int>();
                this.apoyos[i].Qultima = ((double)this.cohesion * this.NC[(int)this.anguloFriccion] * this.apoyos[i].Fcs * this.apoyos[i].Fcd) + (q * this.NQ[(int)this.anguloFriccion] * this.apoyos[i].Fqs * this.apoyos[i].Fqd) + ((0.5) * pesoMenor * this.apoyos[i].B * this.NF[(int)this.anguloFriccion] * this.apoyos[i].Fps * this.apoyos[i].Fpd);
                this.apoyos[i].Qultima = Math.Round(this.apoyos[i].Qultima, 3);
                /*messagebox.show("[Apoyo] " + this.apoyos[i].Numero + " ([cohesion] " + (double)this.cohesion + " [NC] " + this.NC[(int)this.anguloFriccion] + " [FCS] " + this.apoyos[i].Fcs + " [FCD] " + this.apoyos[i].Fcd + " multiplicacion de esto es "
                    + (this.cohesion * this.NC[(int)this.anguloFriccion] * this.apoyos[i].Fcs * this.apoyos[i].Fcd) + ") + ([q] " + q + " [NQ] " + this.NQ[(int)this.anguloFriccion] + " [FQS] " + this.apoyos[i].Fqs + " [FQD] " + this.apoyos[i].Fqd + " multiplicacion de esto" +
                    (q * this.NQ[(int)this.anguloFriccion] * this.apoyos[i].Fqs * this.apoyos[i].Fqd) + ") + (1/2 * [pesoMenor] " + pesoMenor + " [B] " + this.apoyos[i].B + " [NF] " + this.NF[(int)this.anguloFriccion] + " [FPS] " + this.apoyos[i].Fps + " [FPD] " + this.apoyos[i].Fpd +
                    " multiplicacion de esto " + ((0.5) * pesoMenor * this.apoyos[i].B * this.NF[(int)this.anguloFriccion] * this.apoyos[i].Fps * this.apoyos[i].Fpd) + ")");*/
                //messagebox.show("Q ultima apoyo " + this.apoyos[i].Qultima);
                //area de la zapata para cada apoyo
                this.apoyos[i].AreaZapata = (this.apoyos[i].Carga * 3) / this.apoyos[i].Qultima;
                this.apoyos[i].B = Math.Sqrt(this.apoyos[i].AreaZapata);
                this.apoyos[i].B = Math.Round(this.apoyos[i].B,1); //redondeada
                //messagebox.show("Area zapata "+ this.apoyos[i].AreaZapata+" B "+ this.apoyos[i].B);
                //messagebox.show("Q ultima apoyo " + this.apoyos[i].Qultima);
                this.apoyos[i].SumatoriaMomentosX = this.apoyos[i].MtoEnEjeY + this.empotramientoDF * this.apoyos[i].FBasalX;
                this.apoyos[i].SumatoriaMomentosY = this.apoyos[i].MtoEnEjeX + this.empotramientoDF * this.apoyos[i].FBasalY;
                this.apoyos[i].Qadmisible = this.apoyos[i].Qultima / 3;
                this.apoyos[i].FactorEX = (this.apoyos[i].SumatoriaMomentosX) / (this.apoyos[i].Carga); // sumatoria v siendo la carga del apoyo
                this.apoyos[i].FactorEY = (this.apoyos[i].SumatoriaMomentosY) / (this.apoyos[i].Carga); // sumatoria v siendo la carga del apoyo
                //EN X
                if (this.apoyos[i].FactorEX > (this.apoyos[i].B / 6))
                {
                    this.apoyos[i].Qmin = 0;
                    this.apoyos[i].Qmax = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (this.apoyos[i].FactorEX) / this.apoyos[i].B));
                }
                else
                {
                    this.apoyos[i].Qmax = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (this.apoyos[i].FactorEX) / this.apoyos[i].B));
                    this.apoyos[i].Qmin = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 - (6 * (this.apoyos[i].FactorEX) / this.apoyos[i].B));
                }
                //EN Y
                if (this.apoyos[i].FactorEY > (this.apoyos[i].B / 6))
                {
                    this.apoyos[i].Qmin = 0;
                    this.apoyos[i].Qmax = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (this.apoyos[i].FactorEY) / this.apoyos[i].B));
                }
                else
                {
                    this.apoyos[i].Qmax = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (this.apoyos[i].FactorEY) / this.apoyos[i].B));
                    this.apoyos[i].Qmin = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 - (6 * (this.apoyos[i].FactorEY) / this.apoyos[i].B));
                }
                Boolean verificacionCierta = false;
                verificacionCierta = VerificacionAsentamiento(i);
                if (!verificacionCierta)
                {
                    return;
                }
                else
                {
                    this.apoyos[i].ColumnaV1X = this.apoyos[i].CoordEjeX - this.apoyos[i].DimensionColumnaX / 200;
                    this.apoyos[i].ColumnaV1Y = this.apoyos[i].CoordEjeY + this.apoyos[i].DimensionColumnaY / 200;
                    this.apoyos[i].ColumnaV2X = this.apoyos[i].CoordEjeX + this.apoyos[i].DimensionColumnaX / 200;
                    this.apoyos[i].ColumnaV2Y = this.apoyos[i].CoordEjeY + this.apoyos[i].DimensionColumnaY / 200;
                    this.apoyos[i].ColumnaV3X = this.apoyos[i].CoordEjeX - this.apoyos[i].DimensionColumnaX / 200;
                    this.apoyos[i].ColumnaV3Y = this.apoyos[i].CoordEjeY - this.apoyos[i].DimensionColumnaY / 200;
                    this.apoyos[i].ColumnaV4X = this.apoyos[i].CoordEjeX + this.apoyos[i].DimensionColumnaX / 200;
                    this.apoyos[i].ColumnaV4Y = this.apoyos[i].CoordEjeY - this.apoyos[i].DimensionColumnaY / 200;
                }    
            }
            VerificacionConjuntas();
            FinalizarDirectas();
        }

        /// <summary>
        /// Para cada apoyo dimensionada genera el cabezal y luego procede a dibujar
        /// </summary>
        public void FinalizarDirectas()
        {
            for(int i = 0; i < this.apoyos.Count; i++)
            {
                if (!this.apoyos[i].Dimensionada && this.apoyos[i].Dimensionar)
                {
                    this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice4X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
                    this.apoyos[i].Vertice4Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
                }
                //messagebox.show("finales: B = " + this.apoyos[i].B + " Qultima =" + this.apoyos[i].Qultima);
            }
            MessageBoxResult result = MessageBox.Show("Continuar con los parametros especificados?", "Finaliza", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                finalizo = true;
                this.Close();
            }
            else
            {
                this.datosdelsuelo.Visibility = Visibility.Collapsed;
                this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
                this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
                this.GridFinal.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Verificaciones que se hacen para saber si 2 o mas apoyos pueden estar conjuntos
        /// </summary>
        public void VerificacionConjuntas()
        {
            //messagebox.show("Se pasa a verificar si estan conjuntas");
            for (int i = 0; i < this.apoyos.Count - 1; i++)
            {   
                if (this.apoyos.Count > 1)
                {
                    for (int k = i + 1; k <= this.apoyos.Count-1; k++)
                    {
                        if (this.apoyos[i].CoordEjeX == this.apoyos[k].CoordEjeX && !this.apoyos[i].ZapataConjuntaY && !this.apoyos[i].ZapataConjuntaY)
                        {
                            CombinandoZapatasX(i, k);
                        }
                        else if (this.apoyos[i].CoordEjeY == this.apoyos[k].CoordEjeY && !this.apoyos[i].ZapataConjuntaX && !this.apoyos[i].ZapataConjuntaX)
                        {
                            CombinandoZapatasY(i, k);
                        }
                    }

                }
            }
            Combinacion();
        }


        /// <summary>
        /// Combina todas las conjuntas para saber bien la longitud
        /// </summary>
        public void Combinacion()
        {
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                if (this.apoyos[i].ZapataConjuntaY) //para combinar en horizontal
                {
                    int combinadaCon = i;
                    for (int j = i + 1; j < (this.apoyos.Count()/* - 1*/); j++)
                    {
                        if (this.apoyos[j].combinados.Contains(combinadaCon))
                        {
                            combinadaCon = j;
                            if (!this.apoyos[i].combinados.Contains(j))
                            {
                                this.apoyos[i].combinados.Add(j);
                                this.apoyos[j].combinados.Add(i);
                            }
                        }
                    }
                }
                if (this.apoyos[i].ZapataConjuntaX) //para combinar en vertical
                {
                    int combinadaCon = i;
                    for (int j = i + 1; j < (this.apoyos.Count() - 1); j++)
                    {
                        if (this.apoyos[j].combinados.Contains(combinadaCon))
                        {
                            combinadaCon = j;
                            if (!this.apoyos[i].combinados.Contains(j))
                            {
                                this.apoyos[i].combinados.Add(j);
                                this.apoyos[j].combinados.Add(i);
                            }
                        }
                    }
                }
                //messagebox.show("Raquel, el apoyo " + this.apoyos[i].Numero + " esta combinado con una cantidad de " + this.apoyos[i].combinados.Count());
            }
            //luego de tener todas las combinaciones, se pasa a sumar sus Ls SI se combina con mas de 1 porque ya hicimos antes la de 1 y 1
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                if (this.apoyos[i].combinados.Count > 1)
                {
                    double L = 0;
                    double qacumulada = this.apoyos[i].Carga;
                    for (int j = 0; j < this.apoyos[i].combinados.Count(); j++)
                    {
                        double num = this.apoyos[i].combinados[j];
                        if (this.apoyos[i].ZapataConjuntaY)
                        {
                            qacumulada = qacumulada + this.apoyos[(int)num].Carga;
                            if (j == (this.apoyos[i].combinados.Count() - 1)) //esta en el ultimo
                            {
                                double d = this.apoyos[i].CoordEjeX - this.apoyos[(int)num].CoordEjeX;
                                L = Math.Abs(d);
                            }
                        }
                        if (this.apoyos[i].ZapataConjuntaX)
                        {
                            qacumulada = qacumulada + this.apoyos[(int)num].Carga;
                            if (j == (this.apoyos[i].combinados.Count() - 1)) //esta en el ultimo
                            {
                                double d = this.apoyos[i].CoordEjeY - this.apoyos[(int)num].CoordEjeY;
                                L = Math.Abs(d);
                            }
                        }
                    }
                    if(L != 0)
                    {
                        double Ltotal = Math.Abs(L) + 2;
                        this.apoyos[i].L = Math.Abs(L);
                        this.apoyos[i].Ltotal = Ltotal;
                        this.apoyos[i].B = (qacumulada) / (this.apoyos[i].Qadmisible * Ltotal);
                        //messagebox.show("Apoyo " + this.apoyos[i].Numero + " L nueva " + this.apoyos[i].L + " Ltotal nueva " + this.apoyos[i].Ltotal + " B nueva " + this.apoyos[i].B);
                        //messagebox.show("Verificacion de asentamiento nuevo?? de " + this.apoyos[i].Numero);
                        VerificacionAsentamiento(i);
                    }
                }
            }
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                if (this.apoyos[i].combinados.Count > 2 && this.apoyos[i].Dimensionar)
                {
                    DimensionandoComb(i);
                    for (int j = 0; j < this.apoyos[i].combinados.Count(); j++)
                    {
                        double num = this.apoyos[i].combinados[j];
                        this.apoyos[(int)num].Dimensionar = false;
                        this.apoyos[(int)num].B = this.apoyos[i].B;
                        this.apoyos[(int)num].L = this.apoyos[i].L;
                        this.apoyos[(int)num].Vertice1X = this.apoyos[i].Vertice1X;
                        this.apoyos[(int)num].Vertice1Y = this.apoyos[i].Vertice1Y;
                        this.apoyos[(int)num].Vertice2X = this.apoyos[i].Vertice2X;
                        this.apoyos[(int)num].Vertice2Y = this.apoyos[i].Vertice2Y;
                        this.apoyos[(int)num].Vertice3X = this.apoyos[i].Vertice3X;
                        this.apoyos[(int)num].Vertice3Y = this.apoyos[i].Vertice3Y;
                        this.apoyos[(int)num].Vertice4X = this.apoyos[i].Vertice4X;
                        this.apoyos[(int)num].Vertice4Y = this.apoyos[i].Vertice4Y;
                    }
                }
            }
        }
        /// <summary>
        /// Metodo para varias combinadas, se agarra solo 1
        /// </summary>
        /// /// <param name="i">Apoyo I</param>
        public void DimensionandoComb(int i)
        {
            if (this.apoyos[i].ZapataConjuntaY)
            {
                this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - (1);
                this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX + (this.apoyos[i].L + 1);
                this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - (1);
                this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice4X = this.apoyos[i].CoordEjeX + (this.apoyos[i].L + 1);
                this.apoyos[i].Vertice4Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
                this.apoyos[i].Dimensionada = true;
            }
            else
            {
                this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + (1);
                this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + (1);
                this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].L + 1);
                this.apoyos[i].Vertice4X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
                this.apoyos[i].Vertice4Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].L + 1);
                this.apoyos[i].Dimensionada = true;
            }
        }


        /// <summary>
        /// Al tener verificacion cierta, se pasan a combinar (horizontal)
        /// </summary>
        /// /// <param name="i">Apoyo I</param>
        /// <param name="j">Apoyo J</param>
        public void CombinandoMismaY(int i, int j)
        {
            this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - (1);
            this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX + (this.apoyos[i].L + 1);
            this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - (1);
            this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice4X = this.apoyos[i].CoordEjeX + (this.apoyos[i].L + 1);
            this.apoyos[i].Vertice4Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
            this.apoyos[j].Vertice1X = this.apoyos[j].CoordEjeX - (this.apoyos[j].L + 1);
            this.apoyos[j].Vertice1Y = this.apoyos[j].CoordEjeY + (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice2X = this.apoyos[j].CoordEjeX + (1);
            this.apoyos[j].Vertice2Y = this.apoyos[j].CoordEjeY + (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice3X = this.apoyos[j].CoordEjeX - (this.apoyos[j].L + 1);
            this.apoyos[j].Vertice3Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice4X = this.apoyos[j].CoordEjeX + (1);
            this.apoyos[j].Vertice4Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
            this.apoyos[j].Dimensionada = true;
            this.apoyos[i].Dimensionada = true;
        }

        /// <summary>
        /// Al tener verificacion cierta, se pasan a combinar cuando las X son iguales (vertical)
        /// </summary>
        /// /// <param name="i">Apoyo I</param>
        /// <param name="j">Apoyo J</param>
        public void CombinandoMismaX(int i, int j)
        {
            this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + (1);
            this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + (1);
            this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].L + 1);
            this.apoyos[i].Vertice4X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
            this.apoyos[i].Vertice4Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].L + 1);
            this.apoyos[j].Vertice1X = this.apoyos[j].CoordEjeX - (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice1Y = this.apoyos[j].CoordEjeY + (this.apoyos[j].L + 1);
            this.apoyos[j].Vertice2X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice2Y = this.apoyos[j].CoordEjeY + (this.apoyos[j].L + 1);
            this.apoyos[j].Vertice3X = this.apoyos[j].CoordEjeX - (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice3Y = this.apoyos[j].CoordEjeY - (1);
            this.apoyos[j].Vertice4X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
            this.apoyos[j].Vertice4Y = this.apoyos[j].CoordEjeY - (1);
            this.apoyos[j].Dimensionada = true;
            this.apoyos[i].Dimensionada = true;
        }

        /// <summary>
        /// Verificaciones del asentamiento de apoyos conjuntos
        /// </summary>
        /// <param name="i">Apoyo I</param>
        /// <param name="j">Apoyo J</param>
        /// <returns>Al verificar si el asentamiento esta correcto, retorna true
        /// de lo contrario retorna false</returns>
        private Boolean VerificacionDistorcionAngular(int i, int j)
        {
            double conjunto = this.apoyos[i].MaximoApoyo - this.apoyos[j].MaximoApoyo;
            conjunto = Math.Abs(conjunto);
            //messagebox.show("En verificacion de distorcion angular conjunto, de los apoyos "+this.apoyos[i].Numero+" asentamiento "+this.apoyos[i].MaximoApoyo+" - "+this.apoyos[j].Numero+" asentamiento"+this.apoyos[j].MaximoApoyo+", el asentamiento es " + conjunto);
            if (conjunto < (this.apoyos[i].L / 100))
            {
                return true;
            }
            else
            {
                this.apoyos[i].B = this.apoyos[i].B + 0.1;
                this.apoyos[j].B = this.apoyos[j].B + 0.1;
                //messagebox.show("Incrementa B porque no se cumple verificacion de asentamiento conjunto");
                return false;
            }

        }

        /// <summary>
        /// Verifica que el asentamiento de un apoyo este correcto
        /// </summary>
        /// <param name="i">Apoyo I en la lista de apoyos</param>
        private Boolean VerificacionAsentamiento(int i)
        {
            Boolean seCumple = false;
            while (!seCumple)
            {
                if (this.tipoDeSuelo == "Granular")
                {
                    double Nac = 15 + 0.5 * (this.nsptdesfavorable - 15);
                    double p = 0;
                    if (this.apoyos[i].ZapataConjuntaX || this.apoyos[i].ZapataConjuntaY)
                    {
                        //messagebox.show("Esta conjunta P=carga/B*L");
                        p = this.apoyos[i].Carga / ((this.apoyos[i].B * 3.28) * (this.apoyos[i].Ltotal * 3.28));
                    }
                    else
                    {
                        p = this.apoyos[i].Carga / (Math.Pow(this.apoyos[i].B * 3.28, 2));
                    }
                    //messagebox.show("P es " + p + " carga es " + this.apoyos[i].Carga+" B es "+this.apoyos[i].B);
                    double Cb = 0;
                    if (this.apoyos[i].B <= 1.22) //estas comparaciones son en metros, de pies a metros
                    {
                        Cb = 1;
                    }
                    else if (this.apoyos[i].B < 1.83)
                    {
                        Cb = 0.95;
                    }
                    else if (this.apoyos[i].B < 2.44)
                    {
                        Cb = 0.90;
                    }
                    else if (this.apoyos[i].B < 3.05)
                    {
                        Cb = 0.85;
                    }
                    else if (this.apoyos[i].B < 3.66)
                    {
                        double auxpies = this.apoyos[i].B * 3.281;
                        Cb = (-1 * 0.025 * (auxpies)) + 1.10;
                        Cb = Math.Round(Cb, 3);
                    }
                    else
                    {
                        Cb = 0.80;
                    }
                    double maximoApoyo = (5 * p) / ((Nac - 1.5) * Cb);
                    maximoApoyo = maximoApoyo * 25.4;
                    //messagebox.show("maximo apoyo " + maximoApoyo + " Nac " + Nac + " P " + p + " Cb " + Cb + " B al momento" + this.apoyos[i].B);
                    if (maximoApoyo > (this.asentamiento))
                    {
                        this.apoyos[i].B = this.apoyos[i].B + 0.1;
                    }
                    else
                    {
                        this.apoyos[i].MaximoApoyo = maximoApoyo;
                        seCumple = true;
                    }
                }
                else
                { //cohesivo
                    double CC = (double)0.009 * (limiteliquido - 10);
                    double empezar = this.empotramientoDF;
                    double terminar = this.empotramientoDF + this.apoyos[i].B;
                    double h=0;
                    double paux =99999999;
                    double p0 = 0;
                    for (int j = 0; j < this.estratos.Count; j++) //h
                    {
                        if(this.estratos[j].Descripcion == "Cohesivo")
                        {
                            //messagebox.show("El estrato " + (j + 1) + " es cohesivo");
                            if (this.estratos[j].CotaInicio < empezar && this.estratos[j].CotaFinal <= terminar && this.estratos[j].CotaFinal >= empezar)
                            {
                                //messagebox.show("Cota inicial < DF y Cota Final <= DF+B");
                                h = h + (this.estratos[j].CotaFinal - empezar);
                                if (this.estratos[j].Peso < paux)
                                {
                                    paux = this.estratos[j].Peso;
                                    //messagebox.show("Nuevo peso menor " + paux);
                                }
                            }
                            else if (this.estratos[j].CotaInicio >= empezar && this.estratos[j].CotaFinal <= terminar)
                            {
                                //messagebox.show("Cota inicial >= DF y Cota Final <= DF+B");
                                h = h + this.estratos[j].Espesor;
                                if (this.estratos[j].Peso < paux)
                                {
                                    paux = this.estratos[j].Peso;
                                    //messagebox.show("Nuevo peso menor " + paux);
                                }
                            }
                            else if (this.estratos[j].CotaInicio < empezar && this.estratos[j].CotaFinal >= terminar)
                            {
                                //messagebox.show("Cota inicial < DF y Cota Final >= DF+B");
                                h = h + this.apoyos[i].B;
                                if (this.estratos[j].Peso < paux)
                                {
                                    paux = this.estratos[j].Peso;
                                    //messagebox.show("Nuevo peso menor " + paux);

                                }
                            }
                            else if (this.estratos[j].CotaInicio >= empezar && this.estratos[j].CotaFinal >= terminar && this.estratos[j].CotaInicio <= terminar)
                            {
                                //messagebox.show("Cota inicial >= DF y Cota Final >= DF+B");
                                h = h + (terminar - this.estratos[j].CotaInicio);
                                if (this.estratos[j].Peso < paux)
                                {
                                    paux = this.estratos[j].Peso;
                                    //messagebox.show("Nuevo peso menor " + paux);
                                }
                            }
                        }
                    }
                    h = h / 2;
                    p0 = paux * h;
                    //messagebox.show("p0 " + p0 + " p menor " + paux + " h " + h);
                    double desde = this.empotramientoDF + this.apoyos[i].B;
                    double gamapav = this.apoyos[i].Carga / (Math.Pow(this.apoyos[i].B,2));
                    double maximoApoyo = (((double)CC * h) / (1 + this.relaciondeVacios)) * (Math.Log10((p0 + gamapav) / p0));
                    maximoApoyo = maximoApoyo * 1000; //a mts
                    //messagebox.show("maximo apoyo " + maximoApoyo + " gamaPav " + gamapav + " h " + h + " CC " + CC + " B al momento " + this.apoyos[i].B);
                    if(h == 0)
                    {
                        //messagebox.show("No se encontro un estrato de descripcion Cohesivo entre 0 y DF. Revisar datos");
                        return false;
                    }
                    if (maximoApoyo > this.asentamiento)
                    {
                        this.apoyos[i].B = this.apoyos[i].B + 0.1;
                    }
                    else
                    {
                        this.apoyos[i].MaximoApoyo = maximoApoyo;
                        seCumple = true;
                    }
                }
            }
            return seCumple;
        }

        /// <summary>
        /// Si se cumple que 2 zapatas tienen la posibilidad de combinarse en direccion X, procede a hacer los calculos
        /// y verificaciones necesarias
        /// </summary>
        /// <param name="i">Apoyo I en la lista de apoyos</param>
        /// <param name="j">Apoyo J en la lista de apoyos</param>
        private void CombinandoZapatasX(int i, int j)
        {
            //messagebox.show("Entra " + (i + 1) + " y " + (j + 1) + " a verificar combinarse por misma X");
            //double distancia1 = 0;
            //double distancia2 = 0;
            double superposicionbulbos = 0;
            //double distanciaEntreEllos;
            double r = Math.Abs(this.apoyos[i].CoordEjeY - this.apoyos[j].CoordEjeY);
            double bmediosambos = 0;
            if (this.apoyos[i].ZapataConjuntaY)
            {
                bmediosambos = (1) + (this.apoyos[j].B / 2);
                //messagebox.show("bmedioambos " + bmediosambos + " 1 del combinado " + 1 + " + B/2 del otro B=" + (this.apoyos[j].B / 2) + " y r vale " + r);
            }
            else
            {
                bmediosambos = (this.apoyos[i].B / 2) + (this.apoyos[j].B / 2);
                //messagebox.show("bmedioambos " + bmediosambos + " B/2 del primero, B= " + (this.apoyos[i].B / 2) + " + B/2 del otro B=" + (this.apoyos[j].B / 2) + " y r vale " + r);
            }
            double factorE1 = this.apoyos[i].FactorEX;
            double factorE2 = this.apoyos[j].FactorEX;
            double qmax1 = this.apoyos[i].Qmax;
            double qmin1 = this.apoyos[i].Qmin;
            double qmax2 = this.apoyos[j].Qmax;
            double qmin2 = this.apoyos[j].Qmin;
            double qadm1 = this.apoyos[i].Qadmisible;
            double qadm2 = this.apoyos[j].Qadmisible;
            //verificamos superposicion geometrica
            //messagebox.show("Entra " + (i + 1) + " y " + (j + 1) + " a verificar superposicion geometrica");
            //distancia1 = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
            //distancia2 = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
            double L = this.apoyos[i].CoordEjeY - this.apoyos[j].CoordEjeY;
            //distanciaEntreEllos = distancia1 - distancia2;
            if (bmediosambos >= r) //superposicion geometrica
            {
                L = Math.Abs(L);
                double Ltotal = Math.Abs(L) + 2;
                this.apoyos[i].L = Math.Abs(L);
                this.apoyos[j].L = Math.Abs(L);
                this.apoyos[i].Ltotal = Ltotal;
                this.apoyos[j].Ltotal = Ltotal;
                this.apoyos[i].ZapataConjuntaX = true;
                this.apoyos[j].ZapataConjuntaX = true;
                double cargaAmbos = this.apoyos[i].Carga + this.apoyos[j].Carga;
                //messagebox.show("carga combinada " + cargaAmbos);
                this.apoyos[i].B = (cargaAmbos) / (this.apoyos[i].Qadmisible * Ltotal); 
                this.apoyos[j].B = (cargaAmbos) / (this.apoyos[j].Qadmisible * Ltotal);
                //messagebox.show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion geometrica");
                //messagebox.show("Verificacion de asentamiento de " + this.apoyos[i].Numero);
                VerificacionAsentamiento(i);
                //messagebox.show("Verificacion de asentamiento de " + this.apoyos[j].Numero);
                VerificacionAsentamiento(j);
                this.apoyos[i].combinados.Add(j);
                this.apoyos[j].combinados.Add(i);
                if (VerificacionDistorcionAngular(i, j))
                {
                    CombinandoMismaX(i, j);
                }
                return;
            }
            //messagebox.show("Entra " + (i + 1) + " y " + (j + 1) + " a verificar superposicion de bulbos");
            superposicionbulbos = (this.apoyos[i].B / 2) + (this.apoyos[j].B / 2) + ((this.apoyos[i].B / 2) * Math.Tan(30 * Math.PI / 180)) + ((this.apoyos[j].B / 2) * Math.Tan(30 * Math.PI / 180));
            //messagebox.show("superposicion valor " + superposicionbulbos + " r " + r);
            if (superposicionbulbos >= r)
            {
                //esto para X     
                if (factorE1 > this.apoyos[i].B / 6)
                {
                    //messagebox.show("Factor E1 es mayor que el B del apoyo " + (i + 1) + " entre 6");
                    qmin1 = 0;
                    qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
                    //messagebox.show("qmin de 1 " + qmin1 + " qmax nueva " + qmax1);
                }
                else
                {
                    //messagebox.show("Factor E1 no es mayor que el B del apoyo " + (i + 1) + " entre 6");
                    qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
                    qmin1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 - (6 * (factorE1) / this.apoyos[i].B));
                    //messagebox.show("qmin1 nueva " + qmin1 + " qmax1 nueva " + qmax1);
                }
                if (factorE2 > this.apoyos[j].B / 6)
                {
                    //messagebox.show("Factor E2 es mayor que el B del apoyo " + (j + 1) + " entre 6");
                    qmin2 = 0;
                    qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
                    //messagebox.show("qmin de 2 " + qmin1 + " qmax2 nueva " + qmax1);
                }
                else
                {
                    qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
                    qmin2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 - (6 * (factorE2) / this.apoyos[j].B));
                    //messagebox.show("qmin2 nueva " + qmin1 + " qmax2 nueva " + qmax1);

                }
                double baux1 = this.apoyos[i].B;
                double baux2 = this.apoyos[j].B;
                if (qmax1 > qadm1)
                {
                    //messagebox.show("QMAX1 es mayor que QADM1");
                    double[] aux = new double[3];
                    aux = RepeticionPaso3(i, factorE1, qmin1, qmax1, qadm1, baux1);
                    baux1 = aux[0];
                    qmax1 = aux[1];
                    qmin1 = aux[2];
                    //messagebox.show("Valores del paso 3 repetido para 1 son b " + baux1 + " qmax1 " + qmax1 + " qmin1" + qmin1);
                }
                if (qmax2 > qadm2)
                {
                    //messagebox.show("QMAX2 es mayor que QADM2");
                    double[] aux = new double[3];
                    aux = RepeticionPaso3(j, factorE2, qmin2, qmax2, qadm2, baux2);
                    baux2 = aux[0];
                    qmax2 = aux[1];
                    qmin2 = aux[2];
                    //messagebox.show("Valores del paso 3 para 2 son b " + baux2 + " qmax2 " + qmax2 + " qmin2" + qmin2);

                }
                //verificamos si se combinan 
                //messagebox.show("Verificamos si se combinan por las cosas de momento");
                //messagebox.show("Sumatoria momentos Y del primero " + this.apoyos[i].SumatoriaMomentosY + " sumatoria de momentos Y del otro" + this.apoyos[j].SumatoriaMomentosY);
                Boolean cumple = false;
                if (this.apoyos[i].SumatoriaMomentosY > 0 && this.apoyos[j].SumatoriaMomentosY > 0)
                {
                    if ((qmax1 + qmin2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmax1 + qmin2 > qadm1");
                        cumple = true;
                    }
                }
                else if (this.apoyos[i].SumatoriaMomentosY > 0 && this.apoyos[j].SumatoriaMomentosY < 0)
                {
                    if ((qmax1 + qmax2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmax1 + qmax2 > qadm1");
                        cumple = true;
                    }
                }
                else if (this.apoyos[i].SumatoriaMomentosY < 0 && this.apoyos[j].SumatoriaMomentosY > 0)
                {
                    if ((qmin1 + qmin2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmin1 + qmin2 > qadm1");
                        cumple = true;
                    }
                }
                else
                {
                    if ((qmin1 + qmax2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmin1 + qmax2 > qadm1");
                        cumple = true;
                    }
                }
                if (cumple)
                {
                    // se combinaran
                    L = Math.Abs(L);
                    double Ltotal = Math.Abs(L) + 2;
                    this.apoyos[i].L = Math.Abs(L);
                    this.apoyos[j].L = Math.Abs(L);
                    this.apoyos[i].Ltotal = Ltotal;
                    this.apoyos[j].Ltotal = Ltotal;
                    this.apoyos[i].ZapataConjuntaX = true;
                    this.apoyos[j].ZapataConjuntaX = true;
                    double cargaAmbos = this.apoyos[i].Carga + this.apoyos[j].Carga;
                    //messagebox.show("carga combinada " + cargaAmbos);
                    this.apoyos[i].B = (cargaAmbos) / (this.apoyos[i].Qadmisible * Ltotal);
                    this.apoyos[j].B = (cargaAmbos) / (this.apoyos[j].Qadmisible * Ltotal);
                    this.apoyos[i].Qmax = qmax1;
                    this.apoyos[i].Qmin = qmin1;
                    this.apoyos[i].FactorEX = factorE1;
                    this.apoyos[j].Qmax = qmax2;
                    this.apoyos[j].Qmin = qmin2;
                    this.apoyos[j].FactorEX = factorE2;
                    //messagebox.show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " nueva B" + this.apoyos[i].B + " y " + this.apoyos[j].B);
                    //messagebox.show("para apoyo " + this.apoyos[i].Numero + " carga " + this.apoyos[i].Carga + " / qadmisible " + this.apoyos[i].Qadmisible + " L' " + Ltotal + " resultado B " + this.apoyos[i].B);
                    //messagebox.show("para apoyo " + this.apoyos[j].Numero + " carga " + this.apoyos[j].Carga + " / qadmisible " + this.apoyos[j].Qadmisible + " L' " + Ltotal + " resultado B " + this.apoyos[j].B);
                    //messagebox.show("Verificacion de asentamiento de " + this.apoyos[i].Numero);
                    VerificacionAsentamiento(i);
                    //messagebox.show("Verificacion de asentamiento de " + this.apoyos[j].Numero);
                    VerificacionAsentamiento(j);
                    this.apoyos[i].combinados.Add(j);
                    this.apoyos[j].combinados.Add(i);
                    if (VerificacionDistorcionAngular(i, j))
                    {
                        CombinandoMismaX(i, j);
                    }
                    return;
                }
                else
                {
                    return;  //no se combinan
                }
            }
        }

        /// <summary>
        /// Si se cumple que 2 zapatas tienen la posibilidad de combinarse en direccion Y, procede a hacer los calculos
        /// y verificaciones necesarias
        /// </summary>
        /// <param name="i">Apoyo I en la lista de apoyos</param>
        /// <param name="j">Apoyo J en la lista de apoyos</param>
        private void CombinandoZapatasY(int i, int j)
        {
            //messagebox.show("Entra " + (i + 1) + " y " + (j + 1) + " a verificar combinarse por misma Y");
            //double distancia1 = 0;
            //double distancia2 = 0;
            double superposicionbulbos = 0;
            //double distanciaEntreEllos;
            //verificamos superposicion geometrica
            //messagebox.show("Entra " + (i + 1) + " y " + (j + 1) + " a verificar superposicion geometrica");
            //distancia1 = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
            //distancia2 = this.apoyos[j].CoordEjeX - (this.apoyos[j].B / 2);
            double r = Math.Abs(this.apoyos[i].CoordEjeX - this.apoyos[j].CoordEjeX);
            double bmediosambos = 0;
            if (this.apoyos[i].ZapataConjuntaY)
            {
                bmediosambos = (1) + (this.apoyos[j].B / 2);
                //messagebox.show("bmedioambos " + bmediosambos + " 1 del combinado " + 1 + " + B/2 del otro B=" + (this.apoyos[j].B/2) + " y r vale " + r);
            }
            else
            {
                bmediosambos = (this.apoyos[i].B / 2) + (this.apoyos[j].B / 2);
                //messagebox.show("bmedioambos " + bmediosambos + " B/2 del primero, B= " + (this.apoyos[i].B/2) + " + B/2 del otro B=" + (this.apoyos[j].B/2)+" y r vale "+r);
            }
            double factorE1 = this.apoyos[i].FactorEY;
            double factorE2 = this.apoyos[j].FactorEY;
            double qmax1 = this.apoyos[i].Qmax;
            double qmin1 = this.apoyos[i].Qmin;
            double qmax2 = this.apoyos[j].Qmax;
            double qmin2 = this.apoyos[j].Qmin;
            double qadm1 = this.apoyos[i].Qadmisible;
            double qadm2 = this.apoyos[j].Qadmisible;
            double L = this.apoyos[i].CoordEjeX - this.apoyos[j].CoordEjeX;
            //distanciaEntreEllos = distancia1 - distancia2;
            if (bmediosambos >= r) //superposicion geometrica
            {
                L = Math.Abs(L);
                double Ltotal = Math.Abs(L) + 2;
                this.apoyos[i].L = Math.Abs(L);
                this.apoyos[j].L = Math.Abs(L);
                this.apoyos[i].Ltotal = Ltotal;
                this.apoyos[j].Ltotal = Ltotal;
                this.apoyos[i].ZapataConjuntaY = true;
                this.apoyos[j].ZapataConjuntaY = true;
                double cargaAmbos = this.apoyos[i].Carga + this.apoyos[j].Carga;
                //messagebox.show("carga combinada " + cargaAmbos);
                this.apoyos[i].B = (cargaAmbos) / (this.apoyos[i].Qadmisible * Ltotal);
                this.apoyos[j].B = (cargaAmbos) / (this.apoyos[j].Qadmisible * Ltotal);
                //messagebox.show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion geometrica");
                //messagebox.show("Verificacion de asentamiento de " + this.apoyos[i].Numero);
                VerificacionAsentamiento(i);
                //messagebox.show("Verificacion de asentamiento de " + this.apoyos[j].Numero);
                VerificacionAsentamiento(j);
                this.apoyos[i].combinados.Add(j);
                this.apoyos[j].combinados.Add(i);
                if (VerificacionDistorcionAngular(i, j))
                {
                    CombinandoMismaY(i, j);
                }
                return;
            }
            //messagebox.show("Entra " + (i + 1) + " y " + (j + 1) + " a verificar superposicion de bulbos");
            superposicionbulbos = (this.apoyos[i].B / 2) + (this.apoyos[j].B / 2) + ((this.apoyos[i].B / 2) * Math.Tan(30 * Math.PI/180)) + ((this.apoyos[j].B / 2) * Math.Tan(30 * Math.PI / 180));
            //messagebox.show("superposicion valor "+superposicionbulbos+" r "+r);
            if (superposicionbulbos >= r)
            {
                //messagebox.show("Entra en superposicion de bulbos");
                if (factorE1 > this.apoyos[i].B / 6)
                {
                    //messagebox.show("Factor E1 es mayor que el B del apoyo " + (i + 1) + " entre 6");
                    qmin1 = 0;
                    qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
                    //messagebox.show("qmin de 1 " + qmin1 + " qmax nueva " + qmax1);
                }
                else
                {
                    //messagebox.show("Factor E1 no es mayor que el B del apoyo " + (i + 1)+" entre 6");
                    qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
                    qmin1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 - (6 * (factorE1) / this.apoyos[i].B));
                    //messagebox.show("qmin1 nueva " + qmin1 + " qmax1 nueva " + qmax1);
                }
                if (factorE2 > this.apoyos[j].B / 6)
                {
                    //messagebox.show("Factor E2 es mayor que el B del apoyo " + (j + 1) + " entre 6");
                    qmin2 = 0;
                    qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
                    //messagebox.show("qmin de 2 " + qmin1 + " qmax2 nueva " + qmax1);
                }
                else
                {
                    qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
                    qmin2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 - (6 * (factorE2) / this.apoyos[j].B));
                    //messagebox.show("qmin2 nueva " + qmin1 + " qmax2 nueva " + qmax1);

                }
                double baux1 = this.apoyos[i].B;
                double baux2 = this.apoyos[j].B;
                if (qmax1 > qadm1)
                {
                    //messagebox.show("QMAX1 es mayor que QADM1");
                    double[] aux = new double[3];
                    aux = RepeticionPaso3(i, factorE1, qmin1, qmax1, qadm1, baux1);
                    baux1 = aux[0];
                    qmax1 = aux[1];
                    qmin1 = aux[2];
                    //messagebox.show("Valores del paso 3 repetido para 1 son b " + baux1 + " qmax1 " + qmax1 + " qmin1" + qmin1);
                }
                if (qmax2 > qadm2)
                {
                    //messagebox.show("QMAX2 es mayor que QADM2");
                    double[] aux = new double[3];
                    aux = RepeticionPaso3(j, factorE2, qmin2, qmax2, qadm2, baux2);
                    baux2 = aux[0];
                    qmax2 = aux[1];
                    qmin2 = aux[2];
                    //messagebox.show("Valores del paso 3 para 2 son b " + baux2 + " qmax2 " + qmax2 + " qmin2" + qmin2);

                }
                //verificamos si se combinan 
                //messagebox.show("Verificamos si se combinan por las cosas de momento");
                //messagebox.show("Sumatoria momentos X del primero " + this.apoyos[i].SumatoriaMomentosX + " sumatoria de momentos X del otro" + this.apoyos[j].SumatoriaMomentosX);
                Boolean cumple = false;
                if (this.apoyos[i].SumatoriaMomentosX > 0 && this.apoyos[j].SumatoriaMomentosX > 0)
                {
                    if ((qmax1 + qmin2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmax1 + qmin2 > qadm1");
                        cumple = true;
                    }
                }
                else if (this.apoyos[i].SumatoriaMomentosX > 0 && this.apoyos[j].SumatoriaMomentosX < 0)
                {
                    if ((qmax1 + qmax2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmax1 + qmax2 > qadm1");
                        cumple = true;
                    }
                }
                else if (this.apoyos[i].SumatoriaMomentosX < 0 && this.apoyos[j].SumatoriaMomentosX > 0)
                {
                    if ((qmin1 + qmin2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmin1 + qmin2 > qadm1");
                        cumple = true;
                    }
                }
                else
                {
                    if ((qmin1 + qmax2) > qadm1)
                    {
                        //messagebox.show("Se cumple que qmin1 + qmax2 > qadm1");
                        cumple = true;
                    }
                }
                if (cumple)
                {
                    this.apoyos[i].Qadmisible = qadm1;
                    this.apoyos[j].Qadmisible = qadm2;
                    // se combinaran
                    L = Math.Abs(L);
                    double Ltotal = Math.Abs(L) + 2;
                    this.apoyos[i].L = Math.Abs(L);
                    this.apoyos[j].L = Math.Abs(L);
                    this.apoyos[i].Ltotal = Ltotal;
                    this.apoyos[j].Ltotal = Ltotal;
                    this.apoyos[i].ZapataConjuntaY = true;
                    this.apoyos[j].ZapataConjuntaY = true;
                    double cargaAmbos = this.apoyos[i].Carga + this.apoyos[j].Carga;
                    //messagebox.show("carga combinada " + cargaAmbos);
                    this.apoyos[i].B = (cargaAmbos) / (this.apoyos[i].Qadmisible * Ltotal);
                    this.apoyos[j].B = (cargaAmbos) / (this.apoyos[j].Qadmisible * Ltotal);
                    this.apoyos[i].Qmax = qmax1;
                    this.apoyos[i].Qmin = qmin1;
                    this.apoyos[i].FactorEY = factorE1;
                    this.apoyos[j].Qmax = qmax2;
                    this.apoyos[j].Qmin = qmin2;
                    this.apoyos[j].FactorEY = factorE2;
                    //messagebox.show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion bulbos");
                    //messagebox.show("para apoyo " + this.apoyos[i].Numero + " carga " + this.apoyos[i].Carga + " / qadmisible " + this.apoyos[i].Qadmisible + " L' " + Ltotal + " resultado B " + this.apoyos[i].B);
                    //messagebox.show("para apoyo " + this.apoyos[j].Numero + " carga " + this.apoyos[j].Carga + " / qadmisible " + this.apoyos[j].Qadmisible + " L' " + Ltotal + " resultado B " + this.apoyos[j].B);
                    //messagebox.show("Verificacion de asentamiento de " + this.apoyos[i].Numero);
                    VerificacionAsentamiento(i);
                    //messagebox.show("Verificacion de asentamiento de " + this.apoyos[j].Numero);
                    VerificacionAsentamiento(j);
                    this.apoyos[i].combinados.Add(j);
                    this.apoyos[j].combinados.Add(i);
                    if (VerificacionDistorcionAngular(i, j))
                    {
                        CombinandoMismaY(i, j);
                    }
                    return;
                }
                else
                {
                    return; //no se combinan
                }
            }
        }

        /// <summary>
        /// Repite el paso 3 de los pasos para combinar zapata, los nuevos valores seran retornados
        /// </summary>
        /// <param name="i">Apoyo I en la lista de apoyos</param>
        /// <param name="factorE1">Factor E del apoyo</param>
        /// <param name="qmin1">Q Minima del apoyo</param>
        /// <param name="qmax1">Q maxima del apoyo</param>
        /// <param name="qadm1">Q admisible del apoyo</param>
        /// <param name="b">Valor de B del apoyo</param>
        /// <returns>Nuevos valores que se calcularon se retornaran</returns>
        private double[] RepeticionPaso3(int i, double factorE1, double qmin1, double qmax1, double qadm1, double b) //revisando que qmax no sea mayor a qadm
        {
            //messagebox.show("Se repite el paso 3");
            b = b + 0.1;
            if (factorE1 > b / 6)
            {
                qmin1 = 0;
                qmax1 = (this.apoyos[i].Carga / Math.Pow(b, 2)) * (1 + (6 * (factorE1) / b));
            }
            else
            {
                qmax1 = (this.apoyos[i].Carga / Math.Pow(b, 2)) * (1 + (6 * (factorE1) / b));
                qmin1 = (this.apoyos[i].Carga / Math.Pow(b, 2)) * (1 - (6 * (factorE1) / b));
            }
            if (qmax1 > qadm1)
            {      
                RepeticionPaso3(i, factorE1, qmin1, qmax1, qadm1, b);
            }
            double[] aux = new double[3] { b, qmax1, qmin1};
            return aux;
        }

        
    }
}
   