using System;
using Autodesk.AutoCAD.ApplicationServices;
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
    /// Interaction logic for Indirectas.xaml
    /// </summary>
    public partial class Indirectas : Window
    {
        public class MetroGolpe
        {
            public int Metro { get; set; }
            public int NumeroDeGolpes { get; set; }
        }
        public class Pilote
        {
            public double Diametro { get; set; }
            public double AreaCabillas { get; set; }
            public double EspaciamientoCabillas { get; set; }
            public double CargaPilote { get; set; }
            public double PosicionX { get; set; }
            public double PosicionY { get; set; }
        }
        public class Apoyo //y cosas de fundacion
        {
            public double EspaciamientoEntrePilotes { get; set; }
            public List<Pilote> Pilotes { get; set; }
            public int Numero { get; set; }
            public double Eficiencia { get; set; }
            public String Nombre { get; set; }
            public double CoordEjeX { get; set; }
            public double CoordEjeY { get; set; }
            public double Carga { get; set; }
            public double MtoEnEjeX { get; set; }
            public double MtoEnEjeY { get; set; }
            public double DimensionColumnaX { get; set; }
            public double DimensionColumnaY { get; set; }
            public double FBasalX { get; set; }
            public double FBasalY { get; set; }
            public double Qadmisible { get; set; }
            public double QadmisibleGrupo { get; set; }
            public double Qestructural { get; set; }
            public double EspesorCabezal { get; set; }
            public double EspaciamientoCabillasApoyo { get; set; }
            public double GrosorCabezal { get; set; }
            public double DiametroPilotes { get; set; }
            public double AceroLongitudinal { get; set; } //lo coloco aqui porque todos los pilotes de un apoyo tendran la misma 
            public double NumeroCabillas { get; set; }
            public double DiametroTeoricoCabillas { get; set; }
            public String DiametroTeoricoPulgadas { get; set; }
            public double SeccionTeorica { get; set; }
            public double AreaAceroX { get; set; } //de cajon
            public double AreaAceroY { get; set; }
            public int CabillasDeCajonX { get; set; } //cajon
            public int CabillasDeCajonY { get; set; }
            public double EspaciamientoCabillasX { get; set; } //cajon
            public double EspaciamientoCabillasY { get; set; }
            public double DistanciaMinimaEntrePilotes { get; set; }
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
            public double Angulo { get; set; }
            public double Cohesion { get; set; }
            public double Peso { get; set; }
            public double CotaInicio { get; set; }
            public double CotaFinal { get; set; }
        }
        public String tipoDeSuelo;
        public int resistenciaAcero;
        public double nsptpunta;
        public int resistenciaConcreto;
        public List<double> diametrosComerciales = new List<double> { 55, 65, 80, 90, 100, 110, 120, 130, 140, 150 }; // centimetros
        public List<double> seccionTeorica = new List<double> { 1.9806, 2.8502, 3.8777, 5.0670, 10.0717 };
        public List<MetroGolpe> golpesSuelo;
        public List<double> valoresS5 = new List<double> { 0.00, 1.6, 1.68, 1.76, 1.85, 1.95, 2.06, 2.15, 2.28, 2.41, 2.55, 2.70, 2.86, 3.02, 3.21, 3.41, 3.62, 3.85, 4.11,
            4.39, 4.68, 5.01, 5.40, 5.75, 6.20, 6.71, 7.27, 7.86, 8.35, 8.86, 9.30, 10.36, 12.12, 13.39, 14.60, 16.16, 17.97,
            20.05, 22.80, 25.20, 28.60, 32.30, 37.10, 42.40, 48.90, 56.56, 66.90, 79.00, 94.900, 114.60, 140.00, 172.00 };
        public double profundidadEstudioSuelos;
        public double longitudPilote;
        public double espesorRelleno;
        public double longitudEfectiva;
        public double coefFriccionRelleno;
        public double porcentajeAcero;
        public int numeroEstratos;
        public Boolean introdujoGolpes;
        public Boolean calculosCorrectos;
        public List<Apoyo> apoyos;
        public List<Estrato> estratos;
        public Boolean finalizo;
        /// <summary>
        /// Inicializacion de la ventana de Indirectas
        /// 
        /// Colapsa ciertos elementos del documento XAML para que se abran de manera que avanza el programa y el usuario
        /// </summary>
        public Indirectas()
        {
            InitializeComponent();
            this.TipoDeSuelo.Visibility = Visibility.Collapsed;
            this.GranularGrid.Visibility = Visibility.Collapsed;
            this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
            this.GridFinal.Visibility = Visibility.Collapsed;
            this.golpesSuelo = new List<MetroGolpe>();
            finalizo = false;
        }

        /// <summary>
        /// Cambiar suelo elegido por el usuario
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void CancelarSuelo(object sender, RoutedEventArgs e)
        {
            this.GranularGrid.Visibility = Visibility.Collapsed;
            this.GranularCohesivoGrid.Visibility = Visibility.Collapsed;
            this.Granular.IsEnabled = true;
            this.GranularCohesivo.IsEnabled = true;
            this.Siguientes.IsEnabled = true;
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
        /// Acepta los materiales seleccionados por el usuario en la interfaz
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void AceptarMateriales(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ResistenciaConcreto.Text) && !String.IsNullOrEmpty(this.ResistenciaAcero.Text))
            {
                this.resistenciaAcero = Int32.Parse(this.ResistenciaAcero.Text);
                this.resistenciaConcreto = Int32.Parse(this.ResistenciaConcreto.Text);
                this.TipoDeSuelo.Visibility = Visibility.Visible;
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

        private void AceptarSuelo(object sender, RoutedEventArgs e)
        {
            if (!(Boolean)this.Granular.IsChecked && !(Boolean)this.GranularCohesivo.IsChecked)
            {
                MessageBox.Show("Por favor elija un tipo de suelo.");
                return;
            }
            else if ((Boolean)this.Granular.IsChecked)
            {
                this.tipoDeSuelo = "Granular";
                this.GranularGrid.Visibility = Visibility.Visible;
                this.Granular.IsEnabled = false;
                this.GranularCohesivo.IsEnabled = false;
                this.Siguientes.IsEnabled = false;
                return;
            }
            else if ((Boolean)this.GranularCohesivo.IsChecked)
            {
                this.tipoDeSuelo = "GranularCohesivo";
                this.GranularCohesivoGrid.Visibility = Visibility.Visible;
                this.Granular.IsEnabled = false;
                this.GranularCohesivo.IsEnabled = false;
                this.Siguientes.IsEnabled = false;
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
            if (!String.IsNullOrEmpty(this.ProfundidadEstudioSuelosG.Text) && !this.ProfundidadEstudioSuelosG.Text.Equals(0))
            {
                int num = Int32.Parse(this.ProfundidadEstudioSuelosG.Text);
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
            ObservableCollection<MetroGolpe> obs = new ObservableCollection<MetroGolpe>();
            obs = this.DataGridGolpes.ItemsSource as ObservableCollection<MetroGolpe>;
            this.golpesSuelo = obs.ToList();
            MessageBox.Show("Se aceptaron los valores correctamente");
            this.SiguienteDatosSueloG.IsEnabled = true;
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
                    apoyonuevo.Carga = 0;
                    apoyonuevo.CoordEjeX = 0;
                    apoyonuevo.CoordEjeY = 0;
                    apoyonuevo.MtoEnEjeX = 0;
                    apoyonuevo.MtoEnEjeY = 0;
                    apoyonuevo.FBasalX = 0;
                    apoyonuevo.FBasalY = 0;
                    apoyonuevo.DimensionColumnaX = 100;
                    apoyonuevo.DimensionColumnaY = 100;
                    apoyonuevo.Nombre = "A-" + apoyonuevo.Numero.ToString();
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


                //cambiar el nombre del boton
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
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                if (this.apoyos[i].Carga == 0)
                {
                    this.apoyos.RemoveAt(i);
                }
            }
            for (int i = 0; i < this.apoyos.Count(); i++)
            {
                this.apoyos[i].Numero = i + 1;
            }
            this.GridFinal.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Se aceptan los datos relacionados al suelo granular
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntrodujoDatosSueloGranular(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LongitudPiloteG.Text) && !String.IsNullOrEmpty(this.CoefFriccionRellenoG.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroG.Text)
                  && !String.IsNullOrEmpty(this.ProfundidadEstudioSuelosG.Text) && this.introdujoGolpes && !String.IsNullOrEmpty(this.NSPTPunta.Text))
            {
                if (Int32.Parse(this.LongitudRellenoG.Text) > Int32.Parse(this.ProfundidadEstudioSuelosG.Text))
                {
                    MessageBox.Show("El espesor de relleno no puede ser mayor que la profundidad del estudio de los suelos.");
                    return;
                }
                this.longitudPilote = Convert.ToDouble(this.LongitudPiloteG.Text);
                this.espesorRelleno = Convert.ToDouble(this.LongitudRellenoG.Text);
                this.coefFriccionRelleno = Convert.ToDouble(this.CoefFriccionRellenoG.Text);
                this.porcentajeAcero = Convert.ToDouble(this.PorcentajeAceroG.Text);
                this.porcentajeAcero = this.porcentajeAcero / 100;
                this.nsptpunta = Convert.ToDouble(this.NSPTPunta.Text);
                this.SolicitacionesGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
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
                DataGridEstratos.Columns[2].Header = "Angulo de Friccion";
                DataGridEstratos.Columns[3].Header = "Cohesion (Ton/m²)";
                DataGridEstratos.Columns[4].Header = "Peso Unitario (Ton/m³)";
                DataGridEstratos.Columns[5].Header = "Cota Inicio (m)";
                DataGridEstratos.Columns[6].Header = "Cota Final (m)";
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
                TextBlock angulo = DataGridEstratos.Columns[2].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cohesion = DataGridEstratos.Columns[3].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock peso = DataGridEstratos.Columns[4].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cotai = DataGridEstratos.Columns[5].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cotaf = DataGridEstratos.Columns[6].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                this.estratos[i].Espesor = Convert.ToDouble(espesor.Text);
                this.estratos[i].Angulo = Convert.ToDouble(angulo.Text);
                this.estratos[i].Cohesion = Convert.ToDouble(cohesion.Text);
                this.estratos[i].Peso = Convert.ToDouble(peso.Text);
                this.estratos[i].CotaInicio = Convert.ToDouble(cotai.Text);
                this.estratos[i].CotaFinal = Convert.ToDouble(cotaf.Text);
                this.SiguienteDatosSueloGC.IsEnabled = true;
            }
        }

        /// <summary>
        /// Se aceptan los datos relacionados al suelo cohesivo
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void IntrodujoDatosSueloGranularCohesivo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LongitudPiloteGC.Text) && !String.IsNullOrEmpty(this.LongitudRellenoGC.Text) &&
                 !String.IsNullOrEmpty(this.CoefFriccionGC.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroGC.Text))
            {
                this.longitudPilote = Convert.ToDouble(this.LongitudPiloteGC.Text);
                this.espesorRelleno = Convert.ToDouble(this.LongitudRellenoGC.Text);
                this.coefFriccionRelleno = Convert.ToDouble(this.CoefFriccionGC.Text);
                this.porcentajeAcero = Convert.ToDouble(this.PorcentajeAceroGC.Text);
                this.porcentajeAcero = this.porcentajeAcero / 100;
                this.SolicitacionesGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
            }
        }

        /// <summary>
        /// El usuario indica que ya selecciono los datos relevantes y el programa 
        /// seguira a hacer los calculos necesarios y finalizar ejecucion
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
        private void CompletarIndirectas(object sender, RoutedEventArgs e)
        {
            calculosCorrectos = false;
            if (this.tipoDeSuelo == "Granular")
            {
                CalculoPilotesGranular();
            }
            else if (this.tipoDeSuelo == "GranularCohesivo")
            {
                CalculoPilotesCohesivo();
            }
            if (calculosCorrectos)
            {
                CalcularAceroLongitudinal();
                MessageBoxResult result = MessageBox.Show("Continuar con los parametros especificados?", "Finaliza", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    finalizo = true;
                    this.Close();
                }
                else
                {
                    this.TipoDeSuelo.Visibility = Visibility.Collapsed;
                    this.GranularGrid.Visibility = Visibility.Collapsed;
                    this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
                    this.GridFinal.Visibility = Visibility.Collapsed;
                }
            }

        }

        /// <summary>
        /// Genera los datos de calculo de cantidad de pilotes para suelo Granular
        /// </summary>
        public void CalculoPilotesGranular()
        {
            double areaAceroLongitudinal = new double();
            //sacar nsptfuste
            double nsptfuste = new double();
            this.longitudEfectiva = this.longitudPilote - this.espesorRelleno;
            nsptfuste = 0;
            double areapunta;
            double areafuste;
            double friccionnegativa;
            double? aPartirDe = this.espesorRelleno;
            for (int i = (int)aPartirDe; i <= this.golpesSuelo.Count; i++)
            {
                int numero = this.golpesSuelo[i - 1].NumeroDeGolpes;
                nsptfuste = numero + nsptfuste;
            }
            nsptfuste = nsptfuste / (int)aPartirDe;
            //si el promedio es mayor a 30, se toma 30
            if (nsptfuste >= 30)
            {
                nsptfuste = 30;
            }
            //calcular el diametro comercial y Q de pilotes del apoyo
            for (int i = 0; i < this.apoyos.Count; i++)
            {
                int numeropilotes = new int();
                double qadmisible = new double();
                qadmisible = 0;
                areapunta = 0;
                areafuste = 0;
                friccionnegativa = 0;
                double ausar = new double(); //este va a ser el menor
                double qestructural = new double();

                qestructural = 0;
                numeropilotes = 1;

                int contador = 0;
                for (int j = 0; j < diametrosComerciales.Count; j++)
                {

                    areapunta = (3.14159265358979) * Math.Pow((diametrosComerciales[j] / 2), 2);
                    areafuste = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)(this.longitudEfectiva * 100);
                    friccionnegativa = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)(this.espesorRelleno * 100) * (double)this.coefFriccionRelleno;
                    double a = (1.333) * nsptpunta * areapunta;
                    double b = (0.0066667) * nsptfuste * areafuste;
                    qadmisible = a + b - friccionnegativa;
                    areaAceroLongitudinal = (double)this.porcentajeAcero * areapunta;
                    qestructural = ((((double)this.resistenciaConcreto * (areapunta)) + (((double)this.resistenciaAcero) * areaAceroLongitudinal))) * 0.225;
                    qadmisible = qadmisible / 1000; //convirtiendo a toneladas
                    qadmisible = qadmisible * numeropilotes;
                    qestructural = qestructural / 1000; //convirtiendo a toneladas
                    qestructural = qestructural * numeropilotes;
                    //convertimos diametro a metros:
                    double aux = this.diametrosComerciales[j] / 100;
                    if (qadmisible < qestructural)
                    {
                        ausar = qadmisible;
                    }
                    else
                    {
                        ausar = qestructural;
                    }
                    //vemos que la menor de ellas sea mayor a la carga
                    if (ausar >= this.apoyos[i].Carga)
                    {
                        if (longitudEfectiva > (6 * aux) && longitudEfectiva <= (30 * aux))
                        {
                            this.apoyos[i].Qadmisible = qadmisible;
                            this.apoyos[i].Qestructural = qestructural;
                            this.apoyos[i].DiametroPilotes = this.diametrosComerciales[j];
                            this.apoyos[i].Pilotes = new List<Pilote>();
                            for (int k = 0; k <= numeropilotes - 1; k++)
                            {
                                Pilote nuevos = new Pilote();
                                nuevos.Diametro = this.diametrosComerciales[j];
                                this.apoyos[i].Pilotes.Add(nuevos);
                            }
                            if (numeropilotes >= 12)
                            {
                                //MessageBox.Show("Demasiados pilotes, no se contempla el caso.");
                                return;
                            }
                            CalculoConjuntoDePilotes(i, numeropilotes);
                            if (this.apoyos[i].QadmisibleGrupo > this.apoyos[i].Carga) 
                            {
                                this.apoyos[i].Qadmisible = (double)(this.apoyos[i].Qadmisible) / (numeropilotes);
                                this.apoyos[i].Qestructural = (double)(this.apoyos[i].Qestructural) / (numeropilotes);
                                //sigue el espaciamiento entre cabillas
                                this.apoyos[i].EspaciamientoCabillasX = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonX;
                                this.apoyos[i].EspaciamientoCabillasY = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonY;
                                //MessageBox.Show("La eficiencia es " + this.apoyos[i].Eficiencia);
                                PosicionPilotes(numeropilotes, i);
                                CalculoCargaAdmisiblePilote(i);
                                break;
                            }
                            else
                            {
                                if (j == (diametrosComerciales.Count() - 1))
                                {
                                    j = -1;
                                    numeropilotes = numeropilotes + 1;
                                    contador++;
                                }
                                else
                                {
                                    contador++;
                                }
                            }   
                        }
                        else if (contador != 0)
                        {
                            //ajuro entra
                            this.apoyos[i].Qadmisible = qadmisible;
                            this.apoyos[i].Qestructural = qestructural;
                            this.apoyos[i].DiametroPilotes = this.diametrosComerciales[j];
                            this.apoyos[i].Pilotes = new List<Pilote>();
                            for (int k = 0; k <= numeropilotes - 1; k++)
                            {
                                Pilote nuevos = new Pilote();
                                nuevos.Diametro = this.diametrosComerciales[j];
                                this.apoyos[i].Pilotes.Add(nuevos);
                            }
                            if (numeropilotes >= 12)
                            {
                                MessageBox.Show("Demasiados pilotes, no se contempla el caso.");
                                return;
                            }
                            CalculoConjuntoDePilotes(i, numeropilotes);
                            if (this.apoyos[i].QadmisibleGrupo > this.apoyos[i].Carga)
                            {
                                this.apoyos[i].Qadmisible = (double)(this.apoyos[i].Qadmisible) / (numeropilotes);
                                this.apoyos[i].Qestructural = (double)(this.apoyos[i].Qestructural) / (numeropilotes);
                                //sigue el espaciamiento entre cabillas
                                this.apoyos[i].EspaciamientoCabillasX = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonX;
                                this.apoyos[i].EspaciamientoCabillasY = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonY;
                                //MessageBox.Show("La eficiencia es " + this.apoyos[i].Eficiencia);
                                PosicionPilotes(numeropilotes, i);
                                CalculoCargaAdmisiblePilote(i);
                                break;
                            }
                            else
                            {
                                contador = 0;
                                if (j == (diametrosComerciales.Count() - 1))
                                {
                                    j = -1;
                                    numeropilotes = numeropilotes + 1;
                                }
                            }
                        }
                        else
                        {
                            if (j == (diametrosComerciales.Count() - 1))
                            {
                                j = -1;
                                numeropilotes = numeropilotes + 1;
                                contador++;
                            }
                            else
                            {
                                contador++;
                            }     
                        }
                    }
                    else if (j == (diametrosComerciales.Count() - 1))
                    {
                        j = -1;
                        numeropilotes = numeropilotes + 1;
                    }
                }
                this.apoyos[i].AceroLongitudinal = areaAceroLongitudinal;
                //pendiente de los diametros comerciales
                //MessageBox.Show("apoyo " + this.apoyos[i].Nombre + " nsptpunta " + nsptpunta + " nsptfuste " + nsptfuste + " area de punta " + areapunta + " friccion negativa " + friccionnegativa + " area fuste" + areafuste);
                //MessageBox.Show("apoyo: " + this.apoyos[i].Nombre + " Numero de pilotes " + this.apoyos[i].Pilotes.Count() + " Qadmisible " + this.apoyos[i].Qadmisible + " Qestructural " + this.apoyos[i].Qestructural + " Qadmisible de grupo" + this.apoyos[i].QadmisibleGrupo + "carga del apoyo " + this.apoyos[i].Carga + " diametro " + this.apoyos[i].DiametroPilotes);
            }
            calculosCorrectos = true;
        }

        /// <summary>
        /// Genera los datos de calculo de cantidad de pilotes para suelo Cohesivo
        /// </summary>
        public void CalculoPilotesCohesivo()
        {
            //peso angulo y cohesion de fuste: (valores primados)
            double areaAceroLongitudinal = new double();
            double pesoPrimado = 0;
            double anguloPrimado = 0;
            double cohesionPrimado = 0;
            double espesorTotal = 0;
            this.longitudEfectiva = this.longitudPilote - this.espesorRelleno;
            for (int i = 0; i < this.estratos.Count(); i++)
            {
                if (i == this.estratos.Count() - 1)
                {
                    double extra = this.estratos[i].CotaFinal - this.longitudPilote;
                    double espesorAuxiliar = this.estratos[i].Espesor - extra;
                    pesoPrimado = pesoPrimado + (this.estratos[i].Peso * espesorAuxiliar);
                    anguloPrimado = anguloPrimado + (this.estratos[i].Angulo * espesorAuxiliar);
                    cohesionPrimado = cohesionPrimado + (this.estratos[i].Cohesion * espesorAuxiliar);
                    espesorTotal = espesorTotal + espesorAuxiliar;
                }
                else
                {
                    pesoPrimado = pesoPrimado + (this.estratos[i].Peso * this.estratos[i].Espesor);
                    anguloPrimado = anguloPrimado + (this.estratos[i].Angulo * this.estratos[i].Espesor);
                    cohesionPrimado = cohesionPrimado + (this.estratos[i].Cohesion * this.estratos[i].Espesor);
                    espesorTotal = espesorTotal + this.estratos[i].Espesor;
                }
            }
            //verificaciones del angulo
            anguloPrimado = Math.Floor(anguloPrimado / espesorTotal);
            cohesionPrimado = cohesionPrimado / espesorTotal;
            pesoPrimado = pesoPrimado / espesorTotal;
            double anguloRadianes = anguloPrimado * (Math.PI / 180);
            double s1;
            double s2;
            double s2primado;
            double s3primado;
            double s5primado;
            if (anguloPrimado < 9)
            {
                //MessageBox.Show("El angulo promedio es de " + anguloPrimado + ", el terreno es muy debil.");
                return;
            }
            else if (anguloPrimado > 60)
            {
                //MessageBox.Show("El angulo promedio es de " + anguloPrimado + ", el terreno es muy duro.");
                return;
            }
            else
            {
                int auxiliar = (int)anguloPrimado - 9;
                s1 = (0.192) * (Math.Pow(Math.Tan((45 * Math.PI / 180) + (anguloRadianes / 2)), 2)) * ((Math.Pow(Math.E, 4.55 * Math.Tan(anguloRadianes))) - 1);
                s1 = Math.Round(s1, 3);
                s2 = (Math.Pow(Math.Tan((45 * Math.PI / 180) + (anguloRadianes / 2)), 2)) * (Math.Pow(Math.E, Math.PI * Math.Tan(anguloRadianes)));
                s2 = Math.Round(s2, 3);
                s2primado = 1 + (0.32 * Math.Pow(Math.Tan(anguloRadianes), 2));
                s2primado = Math.Round(s2primado, 3);
                s3primado = (Math.Tan(anguloRadianes)) * Math.Pow(Math.E, ((double)19 / 30) * (Math.Tan(anguloRadianes)) * (4 + Math.Pow(Math.Tan(anguloRadianes), (double)2 / 3)));
                s3primado = Math.Round(s3primado, 3);
                s5primado = valoresS5[auxiliar];
                s5primado = Math.Round(s5primado, 3);
                //MessageBox.Show("s1 " + s1 + " s2 " + s2 + " s2primado " + s2primado + " s3primado " + s3primado + " s5primado " + s5primado);
            }
            double cohesionPunta = 0;
            for (int i = 0; i < this.estratos.Count; i++)
            {
                if (this.longitudPilote < this.estratos[i].CotaFinal)
                {
                    cohesionPunta = this.estratos[i].Cohesion;
                }
            }
            //MessageBox.Show("Cohesion elegida(en punta) " + cohesionPunta + " cohesion primada " + cohesionPrimado + " angulo primado " + anguloPrimado + " peso primado " + pesoPrimado + " espesor total" + espesorTotal);
            //factores de resistencia:
            for (int i = 0; i < this.apoyos.Count; i++)
            {
                this.apoyos[i].Pilotes = new List<Pilote>();
                int numeropilotes = new int();
                double qadmisible = new double();
                qadmisible = 0;
                double ausar = new double(); //este va a ser el menor
                double qestructural = new double();
                qestructural = 0;
                numeropilotes = 1;
                int contador = 0;
                double? r1;
                //r1 = peso especifico * diametro? * s1/4
                double? r2;
                //r2 = pesoFuste * longitud efectiva * s2 * s2'
                double? r3;
                //r3 = pesoFuste * 2longitudefectiva^2 * s3'/diametro?
                double? r4;
                //r4 = (CohesionFuste/tan(angulo)) * (s2 -1)
                double? r5;
                //r5 = cohesionFuste * 4 longitud efectiva * s5'/diametro?
                for (int j = 0; j < diametrosComerciales.Count; j++)
                {

                    r1 = pesoPrimado * (diametrosComerciales[j] / 100) * (s1 / 4);
                    r2 = pesoPrimado * longitudEfectiva * s2 * s2primado;
                    r3 = pesoPrimado * (2 * (longitudEfectiva * longitudEfectiva)) * (s3primado / (diametrosComerciales[j] / 100));
                    r4 = (cohesionPunta / (Math.Tan(anguloRadianes))) * (s2 - 1);
                    r5 = cohesionPrimado * (4 * longitudEfectiva) * (s5primado / (diametrosComerciales[j] / 100));
                    double areapunta = (3.14159265358979) * Math.Pow((diametrosComerciales[j] / 2), 2);
                    double areafuste = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)(this.longitudEfectiva * 100);
                    double friccionnegativa = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)(this.espesorRelleno * 100) * (double)this.coefFriccionRelleno;
                    friccionnegativa = (double)friccionnegativa / 1000;
                    areaAceroLongitudinal = (double)this.porcentajeAcero * areapunta;
                    qadmisible = (double)(((r1 + r2 + r3 + r4 + r5) * (areapunta / 10000)) / 4) - friccionnegativa;
                    qestructural = 0.225 * (((double)this.resistenciaConcreto * (areapunta)) + ((double)this.resistenciaAcero) * areaAceroLongitudinal);
                    //qadmisible = qadmisible / 1000; creo que no hace falta convirtiendo a toneladas
                    qadmisible = qadmisible * numeropilotes;
                    qestructural = qestructural / 1000; //convirtiendo a toneladas
                    qestructural = qestructural * numeropilotes;
                    //convertimos diametro a metros:
                    double aux = this.diametrosComerciales[j] / 100;
                    if (qadmisible < qestructural)
                    {
                        ausar = qadmisible;
                    }
                    else
                    {
                        ausar = qestructural;
                    }
                    //vemos que la menor de ellas sea mayor a la carga
                    if (ausar >= this.apoyos[i].Carga)
                    {
                        if (longitudEfectiva > (6 * aux) && longitudEfectiva <= (30 * aux))
                        {
                            this.apoyos[i].Qadmisible = qadmisible;
                            this.apoyos[i].Qestructural = qestructural;
                            this.apoyos[i].DiametroPilotes = this.diametrosComerciales[j];
                            this.apoyos[i].Pilotes = new List<Pilote>();
                            for (int k = 0; k <= numeropilotes - 1; k++)
                            {
                                Pilote nuevos = new Pilote();
                                nuevos.Diametro = this.diametrosComerciales[j];
                                this.apoyos[i].Pilotes.Add(nuevos);
                            }
                            if (numeropilotes >= 12)
                            {
                                MessageBox.Show("Demasiados pilotes, no se contempla el caso.");
                                return;
                            }
                            CalculoConjuntoDePilotes(i, numeropilotes);
                            if (this.apoyos[i].QadmisibleGrupo > this.apoyos[i].Carga)
                            {
                                this.apoyos[i].Qadmisible = (double)(this.apoyos[i].Qadmisible) / (numeropilotes);
                                this.apoyos[i].Qestructural = (double)(this.apoyos[i].Qestructural) / (numeropilotes);
                                //sigue el espaciamiento entre cabillas
                                this.apoyos[i].EspaciamientoCabillasX = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonX;
                                this.apoyos[i].EspaciamientoCabillasY = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonY;
                                //MessageBox.Show("La eficiencia es " + this.apoyos[i].Eficiencia);
                                PosicionPilotes(numeropilotes, i);
                                CalculoCargaAdmisiblePilote(i);
                                break;
                            }
                            else
                            {
                                if (j == (diametrosComerciales.Count() - 1))
                                {
                                    j = -1;
                                    numeropilotes = numeropilotes + 1;
                                    contador++;
                                }
                                else
                                {
                                    contador++;
                                }
                            }
                        }
                        else if (contador != 0)
                        {
                            //ajuro entra
                            this.apoyos[i].Qadmisible = qadmisible;
                            this.apoyos[i].Qestructural = qestructural;
                            this.apoyos[i].DiametroPilotes = this.diametrosComerciales[j];
                            this.apoyos[i].Pilotes = new List<Pilote>();
                            for (int k = 0; k <= numeropilotes - 1; k++)
                            {
                                Pilote nuevos = new Pilote();
                                nuevos.Diametro = this.diametrosComerciales[j];
                                this.apoyos[i].Pilotes.Add(nuevos);
                            }
                            if (numeropilotes >= 12)
                            {
                                MessageBox.Show("Demasiados pilotes, no se contempla el caso.");
                                return;
                            }
                            CalculoConjuntoDePilotes(i, numeropilotes);
                            if (this.apoyos[i].QadmisibleGrupo > this.apoyos[i].Carga)
                            {
                                this.apoyos[i].Qadmisible = (double)(this.apoyos[i].Qadmisible) / (numeropilotes);
                                this.apoyos[i].Qestructural = (double)(this.apoyos[i].Qestructural) / (numeropilotes);
                                //sigue el espaciamiento entre cabillas
                                this.apoyos[i].EspaciamientoCabillasX = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonX;
                                this.apoyos[i].EspaciamientoCabillasY = this.apoyos[i].DiametroPilotes / this.apoyos[i].CabillasDeCajonY;
                                //MessageBox.Show("La eficiencia es " + this.apoyos[i].Eficiencia);
                                PosicionPilotes(numeropilotes, i);
                                CalculoCargaAdmisiblePilote(i);
                                break;
                            }
                            else
                            {
                                contador = 0;
                                if (j == (diametrosComerciales.Count() - 1))
                                {
                                    j = -1;
                                    numeropilotes = numeropilotes + 1;
                                }
                            }
                        }
                        else if (j == (diametrosComerciales.Count() - 1))
                        {
                            j = -1;
                            numeropilotes = numeropilotes + 1;
                            contador++;
                        }
                        else
                        {
                            j = -1;
                            numeropilotes = numeropilotes + 1;
                            contador++;
                        }
                    }
                    else if (j == (diametrosComerciales.Count() - 1))
                    {
                        j = -1;
                        numeropilotes = numeropilotes + 1;
                    }
                }
                this.apoyos[i].AceroLongitudinal = areaAceroLongitudinal;
                //pendiente de los diametros comerciales
                //MessageBox.Show("apoyo: " + this.apoyos[i].Nombre + " Numero de pilotes " + this.apoyos[i].Pilotes.Count() + " Qadmisible " + this.apoyos[i].Qadmisible + " Qestructural " + this.apoyos[i].Qestructural + " Qadmisible de grupo" + this.apoyos[i].QadmisibleGrupo + "carga del apoyo " + this.apoyos[i].Carga + " diametro " + this.apoyos[i].DiametroPilotes);
            }
            calculosCorrectos = true;
        }

        /// <summary>
        /// Genera las opciones de acero longitudinal para que el usuario luego elija en una nueva ventana.
        /// </summary>
        public void CalcularAceroLongitudinal()
        {
            for (int i = 0; i < apoyos.Count(); i++)
            {
                List<int> opcionesDeAceroLongitudinal = new List<int>();
                for (int j = 0; j < this.seccionTeorica.Count(); j++)
                {
                    double aceroLongitudinal = this.apoyos[i].AceroLongitudinal / this.seccionTeorica[j];
                    int numeroCabillas = (int)Math.Ceiling(aceroLongitudinal);
                    opcionesDeAceroLongitudinal.Add(numeroCabillas);
                }
                bool? eligio = false;
                System.Windows.Window win = new Cabillas(opcionesDeAceroLongitudinal, this.apoyos[i].Nombre, this.apoyos[i].Numero, this.seccionTeorica);
                eligio = Autodesk.AutoCAD.ApplicationServices.Application.ShowModalWindow(win);
                eligio = true;
                if (eligio.HasValue && eligio.Value)
                {
                    Cabillas instancia = (Cabillas)win;
                    double diametrosteoricos = (double)instancia.DiametroTeoricoCM();
                    this.apoyos[i].DiametroTeoricoCabillas = (double)diametrosteoricos;
                    int numeroCabillas = instancia.NumeroDeCabillas();
                    this.apoyos[i].NumeroCabillas = numeroCabillas;
                    double seccionteo = (double)instancia.SeccionTeoricaEle();
                    this.apoyos[i].SeccionTeorica = seccionteo;
                    String diametroteopulg = (String)instancia.DiametroTeoricoPULG();
                    this.apoyos[i].DiametroTeoricoPulgadas = diametroteopulg;
                    //MessageBox.Show("Apoyo numero:"+ this.apoyos[i].Numero+" numero de cabillas: " + this.apoyos[i].NumeroCabillas+" diametros teoricos "+this.apoyos[i].DiametroTeoricoCabillas+" seccion teorica "+this.apoyos[i].SeccionTeorica+" diametros teoricos pulgadas "+this.apoyos[i].DiametroTeoricoPulgadas);
                    //calculado anteriormente viendo cabillas de cajon
                    this.apoyos[i].CabillasDeCajonX = (int)Math.Ceiling((this.apoyos[i].AreaAceroX / this.apoyos[i].SeccionTeorica));
                    this.apoyos[i].CabillasDeCajonY = (int)Math.Ceiling((this.apoyos[i].AreaAceroY / this.apoyos[i].SeccionTeorica));
                    //MessageBox.Show("Area acero X " + this.apoyos[i].AreaAceroX + " Area de acero Y " + this.apoyos[i].AreaAceroY + " cabillas en X " + this.apoyos[i].CabillasDeCajonX + " cabillas en Y " + this.apoyos[i].CabillasDeCajonY);

                }
            }

        }

        /// <summary>
        /// Calcula la carga admisible de grupo de pilotes
        /// </summary>
        /// <param name="i">Posicion del apoyo en lista de apoyos</param>
        /// <param name="cantPil">Cantidad de pilotes hasta el momento del apoyo</param>
        /// <returns>Si los calculos fueron correctos y se verifica la condicion de conjunto de pilotes, retorna true
        /// sino, retorna false</returns>
        public void CalculoConjuntoDePilotes(int i, int cantPil) //posicion del apoyo
        {
            int m = 0; //filas
            int n = 0; //columnas
            double Tx = 0;
            double Ty = 0;
            double P = (double)this.apoyos[i].Carga / 0.00110231; //p = carga z
            double e = 2.5 * this.apoyos[i].DiametroPilotes; //distancia centro a centro de pilote
            double d = e / 2; //altura util del cabezal
            double a; //ancho de la columna que llega al cabezal
            if (this.apoyos[i].DimensionColumnaY > this.apoyos[i].DimensionColumnaX)
            {
                a = this.apoyos[i].DimensionColumnaY;
            }
            else
            {
                a = this.apoyos[i].DimensionColumnaX;
            }
            switch (cantPil)
            {
                case 1:
                    m = 1;
                    n = 1;
                    Tx = P;
                    Ty = Tx;
                    break;
                case 2:
                    m = 1;
                    n = 2;
                    Tx = (P * (2 * e - a)) / (8 * d);
                    Ty = Tx;
                    break;
                case 3:
                    n = 1;
                    m = 3;
                    //Tx = (P*(2*e*Math.Sqrt(3) - a * Math.Sqrt(2))) / (18 * Math.Sqrt(3) * d); 
                    Tx = (P * e) / (3 * d);
                    Ty = Tx;

                    break;
                case 4:
                    n = 2;
                    m = 2;
                    Tx = (P * (2 * e - a)) / (8 * d);
                    Ty = Tx;
                    break;
                case 5:
                    n = 3;
                    m = 2;
                    Tx = (P * (2 * e - a)) / (10 * d);
                    Ty = Tx;
                    break;
                case 6:
                    n = 4;
                    m = 3; //hexagono
                    Tx = (P * e) / (3 * d);
                    Ty = (P * e) / (2 * Math.Sqrt(3) * d);

                    break;
                case 7:
                    n = 5;
                    m = 3;
                    Tx = (2 * P * e) / (7 * d);
                    Ty = (P * e * Math.Sqrt(3)) / (7 * d);

                    break;
                case 8:
                    n = 5;
                    m = 3;
                    Tx = (5 * P * e) / (16 * d);
                    Ty = (3 * Math.Tan(3) * P * e) / (16 * d);
                    break;
                case 9:
                    n = 3;
                    m = 3;
                    Tx = (Math.Sqrt(2) * P * e) / (6 * d);
                    Ty = Tx;
                    break;
                case 10:
                    n = 5;
                    m = 3;
                    Tx = (4 * P * e) / (10 * d);
                    Ty = (3 * Math.Sqrt(3) * P * e) / (20 * d);

                    break;
                case 11:
                    n = 7;
                    m = 3;
                    Tx = (4 * P * e) / (11 * 2 * e);
                    Ty = (2 * Math.Sqrt(3) * P * e) / (11 * 2 * e);
                    break;
                case 12:
                    n = 4;
                    m = 3;
                    Tx = (P * e) / (2 * 2.15 * e);
                    Ty = (5 * P * e) / (12 * 2.15 * e);

                    break;
                default:
                    if (cantPil > 12)
                    {
                        return;
                    }
                    else
                    {
                        Console.Write("No entro al caso, imposible, 0 pilotes.");
                        return;
                    }
            }
            double angulo = (double)(Math.Atan((this.apoyos[i].DiametroPilotes) / (e))) * 180.0 / Math.PI;
            this.apoyos[i].Eficiencia = 1.0 - ((((n * (m - 1.0)) + (m * (n - 1))) / ((90.0 * m * n))) * (double)angulo);
            //entonces la q admisible de grupo sera
            this.apoyos[i].QadmisibleGrupo = this.apoyos[i].Qadmisible * this.apoyos[i].Eficiencia;
            this.apoyos[i].GrosorCabezal = ((e / 2.0) + 15)/100;
            this.apoyos[i].DistanciaMinimaEntrePilotes = e;
            double Ax = Tx / 2100;
            this.apoyos[i].AreaAceroX = Ax;
            double Ay = Ty / 2100;
            this.apoyos[i].AreaAceroY = Ay;
        }

        /// <summary>
        /// Calcula la posicion donde se encuentran los pilotes dentro del apoyo y ademas busca los vertices del cabezal del apoyo
        /// </summary>
        /// <param name="cantPil">Cantidad de pilotes hasta el momento del apoyo</param>
        /// <param name="i">Posicion del apoyo en lista de apoyos</param>
        public void PosicionPilotes(int cantPil, int i)
        {
            //ademas buscamos la posicion de la columna
            this.apoyos[i].ColumnaV1X = this.apoyos[i].CoordEjeX - this.apoyos[i].DimensionColumnaX / 200;
            this.apoyos[i].ColumnaV1Y = this.apoyos[i].CoordEjeY + this.apoyos[i].DimensionColumnaY / 200;
            this.apoyos[i].ColumnaV2X = this.apoyos[i].CoordEjeX + this.apoyos[i].DimensionColumnaX / 200;
            this.apoyos[i].ColumnaV2Y = this.apoyos[i].CoordEjeY + this.apoyos[i].DimensionColumnaY / 200;
            this.apoyos[i].ColumnaV3X = this.apoyos[i].CoordEjeX - this.apoyos[i].DimensionColumnaX / 200;
            this.apoyos[i].ColumnaV3Y = this.apoyos[i].CoordEjeY - this.apoyos[i].DimensionColumnaY / 200;
            this.apoyos[i].ColumnaV4X = this.apoyos[i].CoordEjeX + this.apoyos[i].DimensionColumnaX / 200;
            this.apoyos[i].ColumnaV4Y = this.apoyos[i].CoordEjeY - this.apoyos[i].DimensionColumnaY / 200;
            //MessageBox.Show("columna " + this.apoyos[i].DimensionColumnaX + " vertice 1 " + this.apoyos[i].ColumnaV1X + " " + this.apoyos[i].ColumnaV1Y);
            this.apoyos[i].DiametroPilotes = this.apoyos[i].DiametroPilotes / 100; //por motivos de calculo
            this.apoyos[i].DistanciaMinimaEntrePilotes = this.apoyos[i].DistanciaMinimaEntrePilotes / 100; // por motivos de calculo
            double k = 1.5 * this.apoyos[i].DiametroPilotes / 2;
            switch (cantPil)
            {
                case 1:
                    this.apoyos[i].Pilotes[0].PosicionX = this.apoyos[i].CoordEjeX;
                    this.apoyos[i].Pilotes[0].PosicionY = this.apoyos[i].CoordEjeY;
                    this.apoyos[i].Vertice1X = this.apoyos[i].CoordEjeX - k;
                    this.apoyos[i].Vertice1Y = this.apoyos[i].CoordEjeY + k;
                    this.apoyos[i].Vertice2X = this.apoyos[i].CoordEjeX + k;
                    this.apoyos[i].Vertice2Y = this.apoyos[i].CoordEjeY + k;
                    this.apoyos[i].Vertice3X = this.apoyos[i].CoordEjeX - k;
                    this.apoyos[i].Vertice3Y = this.apoyos[i].CoordEjeY - k;
                    this.apoyos[i].Vertice4X = this.apoyos[i].CoordEjeX + k;
                    this.apoyos[i].Vertice4Y = this.apoyos[i].CoordEjeY - k;
                    break;
                case 2:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[0].PosicionX - k;
                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[1].PosicionX + k;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[1].PosicionX + k;
                    this.apoyos[i].Pilotes[0].PosicionY = this.apoyos[i].CoordEjeY;
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[0].PosicionY - k;
                    this.apoyos[i].Pilotes[1].PosicionY = this.apoyos[i].CoordEjeY;
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[1].PosicionY + k;
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[1].PosicionY - k;
                    break;
                case 3:
                    apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX - this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[0].PosicionX - k;
                    apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX);
                    apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[2].PosicionX + k;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[2].PosicionX + k;
                    apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[0].PosicionY - k;
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;
                    apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY);
                    apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[2].PosicionY + k;
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[2].PosicionY - k;
                    break;
                case 4:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;

                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[1].PosicionX + k;

                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[1].PosicionY + k;

                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[2].PosicionX - k;

                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[2].PosicionY - k;

                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[3].PosicionX + k;

                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[3].PosicionY - k;
                    break;
                case 5:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;

                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[1].PosicionX + k;

                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[1].PosicionY + k;

                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY);

                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[3].PosicionX - k;

                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[3].PosicionY - k;

                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[4].PosicionX + k;

                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[4].PosicionY - k;
                    break;
                case 6:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[1].PosicionY + k;

                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[2].PosicionX - k;
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[2].PosicionX - k;

                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[3].PosicionX + k;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[3].PosicionX + k;


                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[4].PosicionY - k;

                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[5].PosicionY - k;
                    break;
                case 7:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - ((2.5 * this.apoyos[i].DiametroPilotes) / 2);
                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) + ((2.5 * this.apoyos[i].DiametroPilotes) / 2);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[1].PosicionY + k;

                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[2].PosicionX - k;
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[2].PosicionX - k;

                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY);

                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[4].PosicionX + k;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[4].PosicionX + k;


                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX) - ((2.5 * this.apoyos[i].DiametroPilotes) / 2);
                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[5].PosicionY - k;

                    this.apoyos[i].Pilotes[6].PosicionX = (this.apoyos[i].CoordEjeX) + ((2.5 * this.apoyos[i].DiametroPilotes) / 2);
                    this.apoyos[i].Pilotes[6].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[6].PosicionY - k;
                    break;
                case 8:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;

                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[2].PosicionX + k;

                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[2].PosicionY + k;

                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[5].PosicionX - k;

                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[5].PosicionY - k;

                    this.apoyos[i].Pilotes[6].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[6].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[7].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[7].PosicionX + k;

                    this.apoyos[i].Pilotes[7].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[7].PosicionY - k;
                    break;
                case 9:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;

                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[2].PosicionX + k;

                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[2].PosicionY + k;

                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[6].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[6].PosicionX - k;

                    this.apoyos[i].Pilotes[6].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[6].PosicionY - k;

                    this.apoyos[i].Pilotes[7].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[7].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[8].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[8].PosicionX + k;

                    this.apoyos[i].Pilotes[8].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[8].PosicionY - k;
                    break;
                case 10:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[2].PosicionY + k;

                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) - (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[3].PosicionX - k;

                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[3].PosicionX - k;

                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) - (0.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//(this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX) + (0.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//(this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[6].PosicionX = (this.apoyos[i].CoordEjeX) + (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[6].PosicionX + k;
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[6].PosicionX + k;


                    this.apoyos[i].Pilotes[6].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[7].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Pilotes[7].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[7].PosicionY - k;

                    this.apoyos[i].Pilotes[8].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[8].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[9].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Pilotes[9].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[9].PosicionY - k;
                    break;
                case 11:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);// - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;

                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;

                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) - (0.5*this.apoyos[i].DistanciaMinimaEntrePilotes); //(this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) + (0.5 * this.apoyos[i].DistanciaMinimaEntrePilotes); //(this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) + (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[3].PosicionX + k;

                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[3].PosicionY + k;

                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX);
                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY);// + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[6].PosicionX = (this.apoyos[i].CoordEjeX) + this.apoyos[i].DistanciaMinimaEntrePilotes;
                    this.apoyos[i].Pilotes[6].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[7].PosicionX = (this.apoyos[i].CoordEjeX) - (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[7].PosicionX - k;

                    this.apoyos[i].Pilotes[7].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[7].PosicionY - k;

                    this.apoyos[i].Pilotes[8].PosicionX = (this.apoyos[i].CoordEjeX) - (0.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//(this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[8].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[9].PosicionX = (this.apoyos[i].CoordEjeX) + (0.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//(this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[9].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[10].PosicionX = (this.apoyos[i].CoordEjeX) + (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);//this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[10].PosicionX + k;

                    this.apoyos[i].Pilotes[10].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[10].PosicionY - k;
                    break;
                case 12:
                    this.apoyos[i].Pilotes[0].PosicionX = (this.apoyos[i].CoordEjeX) - this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[0].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice1X = this.apoyos[i].Pilotes[0].PosicionX - k;
                    this.apoyos[i].Vertice1Y = this.apoyos[i].Pilotes[0].PosicionY + k;
                    this.apoyos[i].Pilotes[1].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[1].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[2].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[2].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[3].PosicionX = (this.apoyos[i].CoordEjeX) + (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes);
                    this.apoyos[i].Pilotes[3].PosicionY = (this.apoyos[i].CoordEjeY) + (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice2X = this.apoyos[i].Pilotes[3].PosicionX + k;
                    this.apoyos[i].Vertice2Y = this.apoyos[i].Pilotes[3].PosicionY + k;
                    this.apoyos[i].Pilotes[4].PosicionX = (this.apoyos[i].CoordEjeX) - this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[4].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[5].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[5].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[6].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[6].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[7].PosicionX = (this.apoyos[i].CoordEjeX) + (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes); ;
                    this.apoyos[i].Pilotes[7].PosicionY = (this.apoyos[i].CoordEjeY);
                    this.apoyos[i].Pilotes[8].PosicionX = (this.apoyos[i].CoordEjeX) - this.apoyos[i].DistanciaMinimaEntrePilotes - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[8].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice3X = this.apoyos[i].Pilotes[8].PosicionX - k;
                    this.apoyos[i].Vertice3Y = this.apoyos[i].Pilotes[8].PosicionY - k;
                    this.apoyos[i].Pilotes[9].PosicionX = (this.apoyos[i].CoordEjeX) - (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[9].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[10].PosicionX = (this.apoyos[i].CoordEjeX) + (this.apoyos[i].DistanciaMinimaEntrePilotes / 2);
                    this.apoyos[i].Pilotes[10].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Pilotes[11].PosicionX = (this.apoyos[i].CoordEjeX) + (1.5 * this.apoyos[i].DistanciaMinimaEntrePilotes); ;
                    this.apoyos[i].Pilotes[11].PosicionY = (this.apoyos[i].CoordEjeY) - (this.apoyos[i].DistanciaMinimaEntrePilotes * (Math.Sqrt(3) / 2));
                    this.apoyos[i].Vertice4X = this.apoyos[i].Pilotes[11].PosicionX + k;
                    this.apoyos[i].Vertice4Y = this.apoyos[i].Pilotes[11].PosicionY - k;
                    break;
                default:
                    MessageBox.Show("No se contempla el caso.");
                    break;

            }
            this.apoyos[i].DiametroPilotes = this.apoyos[i].DiametroPilotes * 100;
            this.apoyos[i].DistanciaMinimaEntrePilotes = this.apoyos[i].DistanciaMinimaEntrePilotes * 100;
            // MessageBox.Show("vertices " + this.apoyos[i].Vertice1X + " " + this.apoyos[i].Vertice1Y + " " + this.apoyos[i].Vertice2X + " " + this.apoyos[i].Vertice2Y + " "
              //  + this.apoyos[i].Vertice3X + " " + this.apoyos[i].Vertice3Y + " " + this.apoyos[i].Vertice4X + " " + this.apoyos[i].Vertice4Y);

        }

        /// <summary>
        /// Calcula la carga individual de cada pilote en el apoyo i
        /// </summary>
        /// <param name="i">Posicion del apoyo en lista de apoyos</param>
        /// <returns>Retorna true si se cumple que la carga del mayor pilote no es mayor a la carga admisible
        /// Retorna false si no se cumple la condicion</returns>
        public void CalculoCargaAdmisiblePilote(int i)
        {
            double cargaActuante = (double)this.apoyos[i].Carga;
            double cantidadPilotes = (double)this.apoyos[i].Pilotes.Count();
            double mx = (double)this.apoyos[i].MtoEnEjeX;
            double my = (double)this.apoyos[i].MtoEnEjeY;
            double SumatoriaY = 0;
            double SumatoriaX = 0;
            double a = cargaActuante / cantidadPilotes; //una constante
            for (int j = 0; j < cantidadPilotes; j++)
            {
                SumatoriaY = (double)SumatoriaY + ((this.apoyos[i].Pilotes[j].PosicionY / 100) * (this.apoyos[i].Pilotes[j].PosicionY / 100));
                SumatoriaX = (double)SumatoriaX + ((this.apoyos[i].Pilotes[j].PosicionX / 100) * (this.apoyos[i].Pilotes[j].PosicionX / 100));
            }
            //MessageBox.Show("Sumatoria Y " + SumatoriaY + " Sumatoria X " + SumatoriaX);
            for (int j = 0; j < cantidadPilotes; j++)
            {
                double b = 0;
                if (SumatoriaY != 0)
                {
                    b = (double)(mx * ((this.apoyos[i].Pilotes[j].PosicionY - this.apoyos[i].CoordEjeY) / 100)) / (SumatoriaY);
                }
                double c = 0;
                if (SumatoriaX != 0)
                {
                    c = (double)(my * ((this.apoyos[i].Pilotes[j].PosicionX - this.apoyos[i].CoordEjeX) / 100)) / (SumatoriaX);
                }
                this.apoyos[i].Pilotes[j].CargaPilote = (double)a + b + c;
            }
        }

        
    }
}

