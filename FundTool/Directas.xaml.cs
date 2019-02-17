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
        public class MetroGolpe{
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
            public double Esfuerzoefectivo { get; set; }
            public Boolean ZapataConjunta { get; set; }
        }
        public class Estrato
        {
            public String Nombre { get; set; }
            public double Espesor { get; set; }
            public String Descripcion { get; set; }
            public double Angulo { get; set; }
            public double Cohesion { get; set; }
            public double Peso { get; set; }
        }
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
        public Boolean introdujoGolpes;
        public List<MetroGolpe> golpesSuelo;
        public List<Apoyo> apoyos;
        public List<Estrato> estratos;
        public int numeroEstratos;
        public List<double> NC = new List<double> {5.7, 5.9, 6.1, 6.3, 6.51, 6.74, 6.97, 7.22, 7.47, 7.74, 8.02, 8.32, 8.63, 8.96, 9.31, 9.67, 10.06, 10.47, 10.90, 11.36, 11.85, 12.37, 12.92, 13.51,
        14.14, 14.80, 15.53, 16.30, 17.13, 18.03, 18.99, 20.03, 21.16, 22.39, 23.72, 25.18, 26.77, 28.51, 30.43, 32.53, 34.87, 37.45, 40.33, 43.54, 47.13, 51.17, 55.63, 60.91, 66.80, 73.55, 81.31};
        public List<double> NQ = new List<double> { 1, 1.07, 1.14, 1.22, 1.30, 1.39, 1.49, 1.59, 1.70, 1.82, 1.94, 2.08, 2.22, 2.38, 2.55, 2.73, 2.92, 3.13, 3.36, 3.61, 3.88, 4.17, 4.48, 4.82, 5.2,
        5.6, 6.05, 6.54, 7.07, 7.66, 8.31, 9.03, 9.82, 10.69, 11.67, 12.75, 13.97, 15.32, 16.85, 18.56, 20.50, 22.70, 25.21, 28.06, 31.34, 35.11, 39.48, 44.45, 50.46, 57.41, 65.60};
        public List<double> NF = new List<double> { 0.00, 0.005, 0.02, 0.04, 0.055, 0.074, 0.1, 0.128, 0.16, 0.20, 0.24, 0.30, 0.35, 0.42, 0.48, 0.47, 0.77, 0.776, 0.88, 1.03, 1.12, 1.35, 1.55, 1.74,
        1.97, 2.25, 2.59, 2.88, 3.29, 3.76, 4.39, 4.83, 5.51, 6.32, 7.22, 8.35, 9.41, 10.90, 12.75, 14.71, 17.22, 19.75, 22.50, 26.25, 30.40, 36, 41.70, 49.30, 59.25, 71.45, 85.75};
        public List<Apoyo> menoresApoyosX;
        public List<Apoyo> menoresApoyosY;

        


        public Directas()
        {
            InitializeComponent();
            this.datosdelsuelo.Visibility = Visibility.Collapsed;
            this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
            this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
            this.GridFinal.Visibility = Visibility.Collapsed;
            this.golpesSuelo = new List<MetroGolpe>();

        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void NumericNegativeDecimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[-]?\d+(?:\.\d{0,2})?$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

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

        private void CancelarMateriales(object sender, RoutedEventArgs e)
        {
            this.Close(); //cierra la ventana
        }

        private void IntroducirSPT_Checked(object sender, RoutedEventArgs e)
        {
            if ((Boolean)IntroducirSPT.IsChecked)
            {
                this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Visible;
            }
            else
            {
                this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
            }
        }

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
                AceptarValoresEstratos.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Introduzca un numero de Apoyos mayor a 0");
                return;
            }

        }

        private void IntroducirDatosEstratos(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.estratos.Count; i++)
            {
                TextBlock espesor = DataGridEstratos.Columns[1].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock descripcion = DataGridEstratos.Columns[2].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock angulo = DataGridEstratos.Columns[3].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cohesion = DataGridEstratos.Columns[4].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock peso = DataGridEstratos.Columns[5].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                this.estratos[i].Espesor = Convert.ToDouble(espesor.Text);
                this.estratos[i].Angulo = Convert.ToDouble(angulo.Text);
                this.estratos[i].Cohesion = Convert.ToDouble(cohesion.Text);
                this.estratos[i].Peso = Convert.ToDouble(peso.Text);
                this.SiguienteDatosSuelo.IsEnabled = true;
            }
        }

        private void IntrodujoDatosSuelo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.AnguloFriccion.Text) && !String.IsNullOrEmpty(this.Cohesion.Text) && !String.IsNullOrEmpty(this.PesoEspecifico.Text)
                && !String.IsNullOrEmpty(this.EmpotramientoDF.Text))
            {
                this.anguloFriccion = Convert.ToInt32(this.AnguloFriccion.Text);
                this.cohesion = Convert.ToDouble(this.Cohesion.Text);
                this.pesoEspecifico = Convert.ToDouble(this.PesoEspecifico.Text);
                this.empotramientoDF = Convert.ToDouble(this.EmpotramientoDF.Text);
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
                if ((Boolean)this.IntroducirSPT.IsChecked)
                {
                    if(!String.IsNullOrEmpty(this.ProfundidadEstudioSuelos.Text) && !String.IsNullOrEmpty(this.Asentamiento.Text) && this.introdujoGolpes)
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

        private void AgregarMetroyGolpe(object sender, RoutedEventArgs e)
        {
             if(!String.IsNullOrEmpty(this.ProfundidadEstudioSuelos.Text) && !this.ProfundidadEstudioSuelos.Text.Equals(0))
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

        private void AceptarMetroYGolpe(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.golpesSuelo.Count; i++)
            {
                TextBlock golpe = DataGridGolpes.Columns[1].GetCellContent(DataGridGolpes.Items[i]) as TextBlock;
                if(golpe == null)
                {
                    MessageBox.Show("Alguno de los valores esta vacio, por favor introduzca un numero");
                    return;
                }
                this.golpesSuelo[i].NumeroDeGolpes = Int32.Parse(golpe.Text);
            }
            MessageBox.Show("Se aceptaron los valores correctamente");
            this.introdujoGolpes = true;
        }

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
                    apoyonuevo.DimensionColumnaX = 1;
                    apoyonuevo.DimensionColumnaY = 1;
                    apoyonuevo.Nombre = apoyonuevo.Numero.ToString();
                    apoyos.Add(apoyonuevo);
                }
                this.ApoyosTotales.Text = apoyos.Count().ToString();
                //agregando columnas
                for (int i = 0; i < (numerox*2)-1; i++)
                {
                    ColumnDefinition gridcol = new ColumnDefinition();
                    GridApoyos.ColumnDefinitions.Add(gridcol);
                }
                //agregando filas
                for (int i = 0; i < (numeroy*2)-1; i++)
                {
                    RowDefinition gridro = new RowDefinition();
                    GridApoyos.RowDefinitions.Add(gridro);
                }
                //agregando botones
                int aux = 1;
                for (int j = 0; j < (numeroy*2)-1; j++)
                {
                    for (int i = 0; i < (numerox*2)-1; i++)
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

        private void BuscarApoyo(object sender, RoutedEventArgs e)
        {
            Button elboton = (Button)sender;
            int numero = Int32.Parse((String)elboton.Content);
            this.NombreApoyo.Text = this.apoyos[numero-1].Nombre;
            this.CargaApoyo.Text = this.apoyos[numero-1].Carga.ToString("0.0");
            this.NumeroApoyo.Text = this.apoyos[numero-1].Numero.ToString("0.0");
            this.CoordXApoyo.Text = this.apoyos[numero-1].CoordEjeX.ToString("0.0");
            this.CoordYApoyo.Text = this.apoyos[numero-1].CoordEjeY.ToString("0.0");
            this.MtoEjeXApoyo.Text = this.apoyos[numero-1].MtoEnEjeX.ToString("0.0");
            this.MtoEjeYApoyo.Text = this.apoyos[numero-1].MtoEnEjeY.ToString("0.0");
            this.FBasalXApoyo.Text = this.apoyos[numero-1].FBasalX.ToString("0.0");
            this.FBasalYApoyo.Text = this.apoyos[numero-1].FBasalY.ToString("0.0");
            this.DimensionColumnaX.Text = this.apoyos[numero - 1].DimensionColumnaX.ToString("0.0");
            this.DimensionColumnaY.Text = this.apoyos[numero - 1].DimensionColumnaY.ToString("0.0");
        }

        private void IntroducirDatosApoyo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.NombreApoyo.Text) && !String.IsNullOrEmpty(this.CargaApoyo.Text) && !String.IsNullOrEmpty(this.CoordXApoyo.Text) && !String.IsNullOrEmpty(this.CoordYApoyo.Text)
                && !String.IsNullOrEmpty(this.MtoEjeXApoyo.Text) && !String.IsNullOrEmpty(this.MtoEjeYApoyo.Text) && !String.IsNullOrEmpty(this.FBasalXApoyo.Text) && !String.IsNullOrEmpty(this.FBasalYApoyo.Text)
                && !String.IsNullOrEmpty(this.DimensionColumnaX.Text) && !String.IsNullOrEmpty(this.DimensionColumnaY.Text))
            {
                int numero = Int32.Parse(this.NumeroApoyo.Text);
                this.apoyos[numero - 1].Nombre = this.NombreApoyo.Text;
                this.apoyos[numero - 1].Carga = Convert.ToDouble(this.CargaApoyo.Text);
                this.apoyos[numero - 1].CoordEjeX = Convert.ToDouble(this.CoordXApoyo.Text);
                this.apoyos[numero - 1].CoordEjeY = Convert.ToDouble(this.CoordYApoyo.Text);
                this.apoyos[numero - 1].MtoEnEjeX = Convert.ToDouble(this.MtoEjeXApoyo.Text);
                this.apoyos[numero - 1].MtoEnEjeY = Convert.ToDouble(this.MtoEjeYApoyo.Text);
                this.apoyos[numero - 1].FBasalX = Convert.ToDouble(this.FBasalXApoyo.Text);
                this.apoyos[numero - 1].FBasalY = Convert.ToDouble(this.FBasalYApoyo.Text);
                this.apoyos[numero - 1].DimensionColumnaX = Convert.ToDouble(this.DimensionColumnaX.Text);
                this.apoyos[numero - 1].DimensionColumnaY =Convert.ToDouble(this.DimensionColumnaX.Text);
                MessageBox.Show("Se introdujeron los datos correctamente.");
            }
            else
            {
                MessageBox.Show("Alguno de los valores esta vacio, por favor llene los datos.");
                return;
            }
        }

        private void IntroducirDatosSolicitaciones(object sender, RoutedEventArgs e)
        {
            this.GridFinal.Visibility = Visibility.Visible;
        }


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
                //verificaciones
            }
            
        } 
    }
}
