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
        public class MetroGolpe
        {
            public int Metro { get; set; }
            public int NumeroDeGolpes { get; set; }
        }
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
            public Boolean Dimensionada { get; set; }
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
        public int anguloFriccion;
        public double cohesion;
        public double pesoEspecifico;
        public double empotramientoDF;
        public String falla;
        public Boolean nivelFreatico;
        public double cotaNivelFreatico;
        public Boolean datosEnsayoSPT;
        public double profundidadEstudioSuelos;
        public double asentamiento;
        public double pesoEspecificoSaturado;
        public int nsptdesfavorable;
        public Boolean introdujoGolpes;
        public List<MetroGolpe> golpesSuelo;
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
            this.golpesSuelo = new List<MetroGolpe>();

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
                this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Visible;
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
                if(this.DatosDelEnsayoSPTGranulares.Visibility == Visibility.Visible)
                {
                    this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Se introducen los estratos que el usuario coloque en una lista de interfaz
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntroducirEstratos(object sender, RoutedEventArgs e)
        {
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
                DataGridEstratos.Columns[2].Header = "Descripcion";
                DataGridEstratos.Columns[3].Header = "Angulo de Friccion";
                DataGridEstratos.Columns[4].Header = "Cohesion (Ton/m²)";
                DataGridEstratos.Columns[5].Header = "Peso Unitario (Ton/m²)";
                DataGridEstratos.Columns[6].Header = "Cota Inicio (m)";
                DataGridEstratos.Columns[7].Header = "Cota Final (m)";
                AceptarValoresEstratos.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Introduzca un numero de Apoyos mayor a 0");
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
                TextBlock descripcion = DataGridEstratos.Columns[2].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
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
                this.SiguienteDatosSuelo.IsEnabled = true;
            }
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
                !String.IsNullOrEmpty(this.LimiteLiquido.Text) && !String.IsNullOrEmpty(this.RelacionVacios.Text))
            {
                this.anguloFriccion = Convert.ToInt32(this.AnguloFriccion.Text);
                this.cohesion = Convert.ToDouble(this.Cohesion.Text);
                this.pesoEspecifico = Convert.ToDouble(this.PesoEspecifico.Text);
                this.empotramientoDF = Convert.ToDouble(this.EmpotramientoDF.Text);
                this.limiteliquido = Convert.ToDouble(this.LimiteLiquido.Text);
                this.limiteliquido = this.limiteliquido / 100;
                this.nsptdesfavorable = Int32.Parse(this.NSPTDES.Text);
                this.relaciondeVacios = Convert.ToDouble(this.RelacionVacios.Text);

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
                    if (!String.IsNullOrEmpty(this.CotaNF.Text) && !String.IsNullOrEmpty(this.PesoEspecificoSaturado.Text))
                    {
                        this.cotaNivelFreatico = Convert.ToDouble(this.CotaNF.Text);
                        this.pesoEspecificoSaturado = (double)Convert.ToDouble(this.PesoEspecificoSaturado.Text);
                    }
                    else
                    {
                        MessageBox.Show("Introduzca los datos de Nivel Freatico");
                        return;
                    }
                }
                if ((Boolean)this.Granular.IsChecked)
                {
                    if (!String.IsNullOrEmpty(this.ProfundidadEstudioSuelos.Text) && !String.IsNullOrEmpty(this.Asentamiento.Text) && this.introdujoGolpes)
                    {
                        this.asentamiento = Convert.ToDouble(this.Asentamiento.Text);
                    }
                    else
                    {
                        MessageBox.Show("Introduzca los Datos del Ensayo S.P.T.");
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
        /// Agrega la cantidad de metros por golpe en una lista de la interfaz
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void AgregarMetroyGolpe(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ProfundidadEstudioSuelos.Text) && !this.ProfundidadEstudioSuelos.Text.Equals(0) && !String.IsNullOrEmpty(this.Asentamiento.Text))
            {
                int num = Int32.Parse(this.ProfundidadEstudioSuelos.Text);
                this.golpesSuelo = new List<MetroGolpe>();
                for (int i = 0; i < num; i++)
                {
                    MetroGolpe nuevom = new MetroGolpe() { Metro = i + 1, NumeroDeGolpes = 0 };
                    this.golpesSuelo.Add(nuevom);
                }
                ObservableCollection<MetroGolpe> obsCollection = new ObservableCollection<MetroGolpe>(this.golpesSuelo);
                this.profundidadEstudioSuelos = this.golpesSuelo.Count;
                DataGridGolpes.DataContext = obsCollection;
                DataGridGolpes.Columns[0].IsReadOnly = true;
                DataGridGolpes.Columns[1].Header = "Numero de Golpes";
                DataGridGolpes.CanUserAddRows = false;
                this.AceptarMetroGolpes.IsEnabled = true;
                this.introdujoGolpes = false;
            }
            else
            {
                MessageBox.Show("Introduzca un Valor");
                return;
            }
        }

        /// <summary>
        /// Acepta los datos de metro por golpe introducidos por el usuario
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void AceptarMetroYGolpe(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.golpesSuelo.Count; i++)
            {
                TextBlock golpe = DataGridGolpes.Columns[1].GetCellContent(DataGridGolpes.Items[i]) as TextBlock;
                if (golpe == null)
                {
                    MessageBox.Show("Alguno de los valores esta vacio, por favor introduzca un numero");
                    return;
                }
                this.golpesSuelo[i].NumeroDeGolpes = Int32.Parse(golpe.Text);
            }
            MessageBox.Show("Se aceptaron los valores correctamente");
            this.introdujoGolpes = true;
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
                this.cohesion = (2 / 3) * this.cohesion;
                this.anguloFriccion = (2 / 3) * this.anguloFriccion;
            }
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                this.apoyos[i].B = 1;
                if (this.nivelFreatico)
                {
                    //caso a
                    if (this.cotaNivelFreatico >= 0 && this.cotaNivelFreatico <= this.empotramientoDF)
                    {
                        List<double> d = new List<double>();
                        d.Add(this.estratos[0].Espesor);
                        for (int j = 0; j < this.estratos.Count(); j++)
                        {
                            if (i != 0)
                            {
                                if (this.empotramientoDF < this.estratos[j].Espesor)
                                {
                                    d.Add(this.estratos[j].Espesor);
                                }
                            }
                        }
                        this.apoyos[i].Esfuerzoefectivo = (this.cotaNivelFreatico * this.pesoEspecifico);
                        for (int j = 0; j < d.Count(); j++)
                        {
                            this.apoyos[i].Esfuerzoefectivo = d[j] * (this.pesoEspecificoSaturado - (2.4 * 907.185));
                        }
                        MessageBox.Show("Esfuerzo efectivo Caso A " + this.apoyos[i].Esfuerzoefectivo);
                    }
                    //caso b
                    else if (this.cotaNivelFreatico > this.empotramientoDF && this.cotaNivelFreatico < (this.empotramientoDF + 1))
                    {
                        this.pesoEspecifico = this.pesoEspecificoSaturado - (2.4 * 907.185) + (1 / this.apoyos[i].B) * (this.pesoEspecifico - (this.pesoEspecificoSaturado - (2.4 * 907.185))); // completar formula
                        this.apoyos[i].Esfuerzoefectivo = this.empotramientoDF * this.pesoEspecifico;

                    }
                    //caso c
                    else
                    {
                        this.apoyos[i].Esfuerzoefectivo = this.empotramientoDF * this.pesoEspecifico;
                    }
                }
                else
                {
                    this.apoyos[i].Esfuerzoefectivo = this.empotramientoDF * this.pesoEspecifico;
                }
                //factores de forma
                this.apoyos[i].Fcs = 1 + (this.apoyos[i].B * this.NQ[this.anguloFriccion]) / (this.NC[this.anguloFriccion]);
                this.apoyos[i].Fqs = 1 + (Math.Tan(this.anguloFriccion));
                this.apoyos[i].Fps = 1 - 0.4 * this.apoyos[i].B;
                //factores de profundidad
                if ((this.empotramientoDF / this.apoyos[i].B) < 1)
                {
                    this.apoyos[i].Fcd = 1 + (0.4 * this.empotramientoDF / this.apoyos[i].B);
                    this.apoyos[i].Fpd = 1;
                    this.apoyos[i].Fqd = 1 + (2 * Math.Tan(this.anguloFriccion) * Math.Pow((1 - Math.Sin(this.anguloFriccion)), 2) * (this.empotramientoDF / this.apoyos[i].B));
                }
                else
                {
                    this.apoyos[i].Fpd = 1;
                    this.apoyos[i].Fcd = 1 + 0.4 * Math.Pow(Math.Tan(this.empotramientoDF / this.apoyos[i].B), -1);
                    this.apoyos[i].Fqd = 1 + (2 * Math.Tan(this.anguloFriccion) * Math.Pow((1 - Math.Sin(this.anguloFriccion)), 2) * Math.Pow(Math.Tan(this.empotramientoDF / this.apoyos[i].B), -1));
                }
                //carga ultima
                double q = this.pesoEspecifico * this.empotramientoDF;
                double pesoMenor = 999999999999;
                for (int j = 0; j < this.estratos.Count; j++)
                {
                    double pesoPrima;
                    if (this.estratos[j].Espesor > this.empotramientoDF)
                    {
                        pesoPrima = this.estratos[j].Peso - (2.4 * 907.185);
                    }
                    else
                    {
                        pesoPrima = this.estratos[j].Peso;
                    }
                    if (pesoPrima < pesoMenor)
                    {
                        pesoMenor = pesoPrima;
                    }
                }
                this.apoyos[i].Qultima = (this.cohesion * this.NC[this.anguloFriccion] * this.apoyos[i].Fcs * this.apoyos[i].Fcd) + (q * this.NQ[this.anguloFriccion] * this.apoyos[i].Fqs * this.apoyos[i].Fqd) + ((1 / 2) * pesoMenor * this.apoyos[i].B * this.NF[this.anguloFriccion] * this.apoyos[i].Fps * this.apoyos[i].Fpd);
                //area de la zapata para cada apoyo
                this.apoyos[i].AreaZapata = (this.apoyos[i].Carga * 3) / this.apoyos[i].Qultima;
                this.apoyos[i].B = Math.Sqrt(this.apoyos[i].AreaZapata);
                MessageBox.Show("Q ultima apoyo " + this.apoyos[i].Numero + " B inicial " + this.apoyos[i].B);
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
                VerificacionAsentamiento(i);
                this.apoyos[i].ColumnaV1X = this.apoyos[i].CoordEjeX - this.apoyos[i].DimensionColumnaX / 200;
                this.apoyos[i].ColumnaV1Y = this.apoyos[i].CoordEjeY + this.apoyos[i].DimensionColumnaY / 200;
                this.apoyos[i].ColumnaV2X = this.apoyos[i].CoordEjeX + this.apoyos[i].DimensionColumnaX / 200;
                this.apoyos[i].ColumnaV2Y = this.apoyos[i].CoordEjeY + this.apoyos[i].DimensionColumnaY / 200;
                this.apoyos[i].ColumnaV3X = this.apoyos[i].CoordEjeX - this.apoyos[i].DimensionColumnaX / 200;
                this.apoyos[i].ColumnaV3Y = this.apoyos[i].CoordEjeY - this.apoyos[i].DimensionColumnaY / 200;
                this.apoyos[i].ColumnaV4X = this.apoyos[i].CoordEjeX + this.apoyos[i].DimensionColumnaX / 200;
                this.apoyos[i].ColumnaV4Y = this.apoyos[i].CoordEjeY - this.apoyos[i].DimensionColumnaY / 200;
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
                if (!this.apoyos[i].Dimensionada)
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
                
            }
            MessageBoxResult result = MessageBox.Show("Continuar con los parametros especificados?", "Finaliza", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Verificaciones que se hacen para saber si 2 o mas apoyos pueden estar conjuntos
        /// </summary>
        public void VerificacionConjuntas()
        {
            for (int i = 0; i < this.apoyos.Count - 1; i++)
            {
                if (this.apoyos.Count > 1)
                {
                    for (int k = i + 1; k <= this.apoyos.Count; k++)
                    {
                        int j = 0;
                        //todo esto para X
                        if (this.apoyos[i].CoordEjeX == this.apoyos[k].CoordEjeX && !this.apoyos[i].ZapataConjuntaY && !this.apoyos[i].ZapataConjuntaY)
                        {
                            j = k;
                            CombinandoZapatasX(i, j);
                            if (VerificacionAsentamientoConjunto(i, j))
                            {
                                this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                                this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                                this.apoyos[i].Vertice2X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
                                this.apoyos[i].Vertice2Y = this.apoyos[j].CoordEjeY + (this.apoyos[j].B / 2);
                                this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                                this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
                                this.apoyos[i].Vertice4X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
                                this.apoyos[i].Vertice4Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice1X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice1Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice2X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice2Y = this.apoyos[j].CoordEjeY + (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice3X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice3Y = this.apoyos[i].CoordEjeY - (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice4X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice4Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
                                this.apoyos[i].Dimensionada = true;
                                this.apoyos[j].Dimensionada = true;
                                break;
                            }
                            else
                            {
                                VerificacionConjuntas();
                            }
                        }
                        else if (this.apoyos[i].CoordEjeY == this.apoyos[k].CoordEjeY && !this.apoyos[i].ZapataConjuntaX && !this.apoyos[i].ZapataConjuntaX)
                        {
                            j = k;
                            CombinandoZapatasY(i, j);
                            if (VerificacionAsentamientoConjunto(i, j))
                            {
                                //dimensionamos
                                this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX- (this.apoyos[i].B / 2);
                                this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY+(this.apoyos[i].B/2);
                                this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX +(this.apoyos[i].B/2);
                                this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                                this.apoyos[i].Vertice3X = this.apoyos[j].CoordEjeX - (this.apoyos[j].B / 2);
                                this.apoyos[i].Vertice3Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
                                this.apoyos[i].Vertice4X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
                                this.apoyos[i].Vertice4Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice1X = this.apoyos[i].CoordEjeX - (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice1Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice2X = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice2Y = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
                                this.apoyos[j].Vertice3X = this.apoyos[j].CoordEjeX - (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice3Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice4X = this.apoyos[j].CoordEjeX + (this.apoyos[j].B / 2);
                                this.apoyos[j].Vertice4Y = this.apoyos[j].CoordEjeY - (this.apoyos[j].B / 2);
                                this.apoyos[i].Dimensionada = true;
                                this.apoyos[j].Dimensionada = true;
                                break;
                            }
                            else
                            {
                                VerificacionConjuntas();
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Verificaciones del asentamiento de apoyos conjuntos
        /// </summary>
        /// <param name="i">Apoyo I</param>
        /// <param name="j">Apoyo J</param>
        /// <returns>Al verificar si el asentamiento esta correcto, retorna true
        /// de lo contrario retorna false</returns>
        private Boolean VerificacionAsentamientoConjunto(int i, int j)
        {
            double conjunto = this.apoyos[i].MaximoApoyo - this.apoyos[j].MaximoApoyo;
            conjunto = Math.Abs(conjunto);
            if (conjunto < (this.apoyos[i].L / 100))
            {
                return true;
            }
            else
            {
                this.apoyos[i].B = this.apoyos[i].B + 0.5;
                this.apoyos[j].B = this.apoyos[j].B + 0.5;
                MessageBox.Show("Incrementa B porque no se cumple verificacion de asentamiento conjunto");
                return false;
            }

        }

        /// <summary>
        /// Verifica que el asentamiento de un apoyo este correcto
        /// </summary>
        /// <param name="i">Apoyo I en la lista de apoyos</param>
        private void VerificacionAsentamiento(int i)
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
                        p = this.apoyos[i].Carga / (this.apoyos[i].B * this.apoyos[i].Ltotal);
                    }
                    else
                    {
                        p = this.apoyos[i].Carga / (Math.Pow(this.apoyos[i].B, 2));
                    }
                    double Cb = 0;
                    if (this.apoyos[i].B <= 121.92) //estas comparaciones son en centimetros
                    {
                        Cb = 1;
                    }
                    if (this.apoyos[i].B < 182.88)
                    {
                        Cb = 0.95;
                    }
                    if (this.apoyos[i].B < 243.84)
                    {
                        Cb = 0.90;
                    }
                    if (this.apoyos[i].B < 304.8)
                    {
                        Cb = 0.85;
                    }
                    if (this.apoyos[i].B >= 121.92)
                    {
                        Cb = 0.80;
                    }
                    double maximoApoyo = (5 * p) / ((Nac - 1.5) * Cb);
                    if (maximoApoyo > this.asentamiento)
                    {
                        this.apoyos[i].B = this.apoyos[i].B + 0.50;
                    }
                    else
                    {
                        this.apoyos[i].MaximoApoyo = maximoApoyo;
                        MessageBox.Show("maximo apoyo " + this.apoyos[i].MaximoApoyo + " Nac " + Nac + " P " + p + " Cb " + Cb + " B final " + this.apoyos[i].B);
                        seCumple = true;
                    }
                }
                else
                {
                    double CC = 0.009 * (limiteliquido - 10);
                    double empezar = this.empotramientoDF;
                    double terminar = this.empotramientoDF + this.apoyos[i].B;
                    double h=0;
                    for(int j = 0; j < this.estratos.Count; j++)
                    {
                        if(this.estratos[j].CotaInicio >= empezar && this.estratos[j].CotaFinal <= terminar)
                        {
                            if(this.estratos[j].Descripcion == "Cohesivo")
                            {
                                h = this.estratos[j].Espesor;
                            }
                        }
                    }
                    double desde = this.empotramientoDF + this.apoyos[i].B;
                    double p0 = this.apoyos[i].Carga;
                    double gamapav = this.apoyos[i].Qmax;
                    double maximoApoyo = ((CC * h) / (1 + this.relaciondeVacios)) * (Math.Log((p0 * gamapav) / p0));
                    if (maximoApoyo > this.asentamiento)
                    {
                        this.apoyos[i].B = this.apoyos[i].B + 0.50;
                    }
                    else
                    {
                        this.apoyos[i].MaximoApoyo = maximoApoyo;
                        MessageBox.Show("maximo apoyo " + this.apoyos[i].MaximoApoyo + " gamaPav " + gamapav + " h " + h + " CC " + CC + " B final " + this.apoyos[i].B);
                        seCumple = true;
                    }
                }
            }
            
        }

        /// <summary>
        /// Si se cumple que 2 zapatas tienen la posibilidad de combinarse en direccion X, procede a hacer los calculos
        /// y verificaciones necesarias
        /// </summary>
        /// <param name="i">Apoyo I en la lista de apoyos</param>
        /// <param name="j">Apoyo J en la lista de apoyos</param>
        private void CombinandoZapatasX(int i, int j)
        {
            double distancia1 = 0;
            double distancia2 = 0;
            double superposicionbulbos = 0;
            double distanciaEntreEllos;
            double factorE1 = this.apoyos[i].FactorEX;
            double sumatoriaMomentos1 = this.apoyos[i].SumatoriaMomentosX;
            double factorE2 = this.apoyos[j].FactorEX;
            double sumatoriaMomentos2 = this.apoyos[j].SumatoriaMomentosX;
            double qmax1 = this.apoyos[i].Qmax;
            double qmin1 = this.apoyos[i].Qmin;
            double qmax2 = this.apoyos[j].Qmax;
            double qmin2 = this.apoyos[j].Qmin;
            double qadm1 = this.apoyos[i].Qadmisible;
            double qadm2 = this.apoyos[j].Qadmisible;
            //verificamos superposicion geometrica
            distancia1 = this.apoyos[i].CoordEjeX + (this.apoyos[i].B / 2);
            distancia2 = this.apoyos[j].CoordEjeX - (this.apoyos[i].B / 2);
            double L = this.apoyos[i].CoordEjeX - this.apoyos[j].CoordEjeX;
            distanciaEntreEllos = distancia1 - distancia2;
            if (distanciaEntreEllos <= 0) //superposicion geometrica
            {
                this.apoyos[i].ZapataConjuntaX = true;
                this.apoyos[j].ZapataConjuntaX = true;
                MessageBox.Show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion geometrica");
                return;
            }
            superposicionbulbos = (this.apoyos[i].B / 2) + (this.apoyos[j].B / 2) + ((this.apoyos[i].B / 2) * Math.Tan(30)) + ((this.apoyos[j].B / 2) * Math.Tan(30));
            if (superposicionbulbos <= distanciaEntreEllos)
            {
                this.apoyos[i].ZapataConjuntaX = true;
                this.apoyos[j].ZapataConjuntaX = true;
                MessageBox.Show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion bulbos");
                return;
            }
            //esto para X     
            if (factorE1 > this.apoyos[i].B / 6)
            {
                qmin1 = 0;
                qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
            }
            else
            {
                qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
                qmin1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 - (6 * (factorE1) / this.apoyos[i].B));
            }
            if (factorE2 > this.apoyos[j].B / 6)
            {
                qmin2 = 0;
                qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
            }
            else
            {
                qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
                qmin2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 - (6 * (factorE2) / this.apoyos[j].B));
            }
            double baux1 = this.apoyos[i].B;
            double baux2 = this.apoyos[j].B;
            if (qmax1 > qadm1)
            {
                double[] aux = new double[3];
                aux = RepeticionPaso3(i, factorE1, qmin1, qmax1, qadm1, baux1);
                baux1 = aux[0];
                qmax1 = aux[1];
                qmin1 = aux[2];
            }
            if (qmax2 > qadm2)
            {
                double[] aux = new double[3];
                aux = RepeticionPaso3(j, factorE2, qmin2, qmax2, qadm2, baux2);
                baux2 = aux[0];
                qmax2 = aux[1];
                qmin2 = aux[2];

            }
            //verificamos si se combinan 
            if((qmax1 + qmin2)> qadm1)
            {
                this.apoyos[i].Qadmisible = qadm1;
                this.apoyos[j].Qadmisible = qadm2;
                // se combinaran
                this.apoyos[i].ZapataConjuntaX = true;
                this.apoyos[j].ZapataConjuntaX = true;
                double Ltotal = Math.Abs(L) + 2;
                this.apoyos[i].L = Math.Abs(L);
                this.apoyos[j].L = Math.Abs(L);
                this.apoyos[i].Ltotal = Ltotal;
                this.apoyos[j].Ltotal = Ltotal;
                this.apoyos[i].B = (this.apoyos[i].Carga * 907.185) / (this.apoyos[i].Qadmisible * 907.185 * Ltotal); //907.185 es ton a kg
                this.apoyos[j].B = (this.apoyos[j].Carga * 907.185) / (this.apoyos[j].Qadmisible * 907.185 * Ltotal);
                this.apoyos[i].Qmax = qmax1;
                this.apoyos[i].Qmin = qmin1;
                this.apoyos[i].FactorEX = factorE1;
                this.apoyos[i].SumatoriaMomentosX = sumatoriaMomentos1;
                this.apoyos[j].Qmax = qmax2;
                this.apoyos[j].Qmin = qmin2;
                this.apoyos[j].FactorEX = factorE2;
                this.apoyos[j].SumatoriaMomentosX = sumatoriaMomentos2;
                MessageBox.Show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " nueva B"+this.apoyos[i].B+" y "+this.apoyos[j].B);

                return;
            }
            else
            {
                return;  //no se combinan
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
            double distancia1 = 0;
            double distancia2 = 0;
            double superposicionbulbos = 0;
            double distanciaEntreEllos;
            //verificamos superposicion geometrica
            distancia1 = this.apoyos[i].CoordEjeY + (this.apoyos[i].B / 2);
            distancia2 = this.apoyos[j].CoordEjeY - (this.apoyos[i].B / 2);
            double factorE1 = this.apoyos[i].FactorEY;
            double sumatoriaMomentos1 = this.apoyos[i].SumatoriaMomentosY;
            double factorE2 = this.apoyos[j].FactorEY;
            double sumatoriaMomentos2 = this.apoyos[j].SumatoriaMomentosY;
            double qmax1 = this.apoyos[i].Qmax;
            double qmin1 = this.apoyos[i].Qmin;
            double qmax2 = this.apoyos[j].Qmax;
            double qmin2 = this.apoyos[j].Qmin;
            double qadm1 = this.apoyos[i].Qadmisible;
            double qadm2 = this.apoyos[j].Qadmisible;
            double L = this.apoyos[i].CoordEjeY - this.apoyos[j].CoordEjeY;
            distanciaEntreEllos = distancia1 - distancia2;
            if (distanciaEntreEllos <= 0) //superposicion geometrica
            {
                this.apoyos[i].ZapataConjuntaY = true;
                this.apoyos[j].ZapataConjuntaY = true;
                double Ltotal = Math.Abs(L) + 2;
                this.apoyos[i].L = Math.Abs(L);
                this.apoyos[j].L = Math.Abs(L);
                this.apoyos[i].Ltotal = Ltotal;
                this.apoyos[j].Ltotal = Ltotal;
                this.apoyos[i].B = (this.apoyos[i].Carga * 907.185) / (this.apoyos[i].Qadmisible * 907.185 * Ltotal); //907.185 es ton a kg
                this.apoyos[j].B = (this.apoyos[j].Carga * 907.185) / (this.apoyos[j].Qadmisible * 907.185 * Ltotal);
                MessageBox.Show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion geometrica");
                return;
            }
            superposicionbulbos = (this.apoyos[i].B / 2) + (this.apoyos[j].B / 2) + ((this.apoyos[i].B / 2) * Math.Tan(30)) + ((this.apoyos[j].B / 2) * Math.Tan(30));
            if (superposicionbulbos <= distanciaEntreEllos)
            {
                this.apoyos[i].ZapataConjuntaY = true;
                this.apoyos[j].ZapataConjuntaY = true;
                double Ltotal = Math.Abs(L) + 2;
                this.apoyos[i].L = Math.Abs(L);
                this.apoyos[j].L = Math.Abs(L);
                this.apoyos[i].Ltotal = Ltotal;
                this.apoyos[j].Ltotal = Ltotal;
                this.apoyos[i].B = (this.apoyos[i].Carga * 907.185) / (this.apoyos[i].Qadmisible * 907.185 * Ltotal); //907.185 es ton a kg
                this.apoyos[j].B = (this.apoyos[j].Carga * 907.185) / (this.apoyos[j].Qadmisible * 907.185 * Ltotal);
                MessageBox.Show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " Se combinan por superposicion bulbos");
                return;
            }
            //esto para y
            if (factorE1 > this.apoyos[i].B / 6)
            {
                qmin1 = 0;
                qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
            }
            else
            {
                qmax1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 + (6 * (factorE1) / this.apoyos[i].B));
                qmin1 = (this.apoyos[i].Carga / Math.Pow(this.apoyos[i].B, 2)) * (1 - (6 * (factorE1) / this.apoyos[i].B));
            }
            if (factorE2 > this.apoyos[j].B / 6)
            {
                qmin2 = 0;
                qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
            }
            else
            {
                qmax2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 + (6 * (factorE2) / this.apoyos[j].B));
                qmin2 = (this.apoyos[j].Carga / Math.Pow(this.apoyos[j].B, 2)) * (1 - (6 * (factorE2) / this.apoyos[j].B));
            }
            double baux1 = this.apoyos[i].B;
            double baux2 = this.apoyos[j].B;
            if (qmax1 > qadm1)
            {
                double[] aux = new double[3];
                aux = RepeticionPaso3(i, factorE1, qmin1, qmax1, qadm1, baux1);
                baux1 = aux[0];
                qmax1 = aux[1];
                qmin1 = aux[2];
            }
            if (qmax2 > qadm2)
            {
                double[] aux = new double[3];
                aux = RepeticionPaso3(j, factorE2, qmin2, qmax2, qadm2, baux2);
                baux2 = aux[0];
                qmax2 = aux[1];
                qmin2 = aux[2];

            }
            //verificamos si se combinan 
            if ((qmax1 + qmin2) > qadm1)
            {
                this.apoyos[i].Qadmisible = qadm1;
                this.apoyos[j].Qadmisible = qadm2;
                // se combinaran
                this.apoyos[i].ZapataConjuntaX = true;
                this.apoyos[j].ZapataConjuntaX = true;
                double Ltotal = Math.Abs(L) + 2;
                this.apoyos[i].L = Math.Abs(L);
                this.apoyos[j].L = Math.Abs(L);
                this.apoyos[i].Ltotal = Ltotal;
                this.apoyos[j].Ltotal = Ltotal;
                this.apoyos[i].B = (this.apoyos[i].Carga * 907.185) / (this.apoyos[i].Qadmisible * 907.185 * Ltotal); //907.185 es ton a kg
                this.apoyos[j].B = (this.apoyos[j].Carga * 907.185) / (this.apoyos[j].Qadmisible * 907.185 * Ltotal);
                this.apoyos[i].Qmax = qmax1;
                this.apoyos[i].Qmin = qmin1;
                this.apoyos[i].FactorEY = factorE1;
                this.apoyos[i].SumatoriaMomentosY = sumatoriaMomentos1;
                this.apoyos[j].Qmax = qmax2;
                this.apoyos[j].Qmin = qmin2;
                this.apoyos[j].FactorEY = factorE2;
                this.apoyos[j].SumatoriaMomentosY = sumatoriaMomentos2;
                MessageBox.Show("Se combinan las zapatas " + this.apoyos[i].Numero + " y " + this.apoyos[j].Numero + " nueva B" + this.apoyos[i].B + " y " + this.apoyos[j].B);
                return;
            }
            else
            {
                return; //no se combinan
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
   