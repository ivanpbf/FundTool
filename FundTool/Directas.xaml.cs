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
        public double? resistenciaAcero;
        public double? resistenciaConcreto;
        public double? anguloFriccion;
        public double? cohesion;
        public double? pesoEspecifico;
        public double? empotramientoDF;
        public String falla;
        public Boolean nivelFreatico;
        public double? cotaNivelFreatico;
        public Boolean datosEnsayoSPT;
        public double? profundidadEstudioSuelos;
        public double? asentamiento;
        public double? pesoEspecificoSaturado;
        public Boolean introdujoGolpes;
        public List<MetroGolpe> golpesSuelo;
        public List<Apoyo> apoyos;
        public List<Estrato> estratos;
        public int numeroEstratos;


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
                this.anguloFriccion = Convert.ToDouble(this.AnguloFriccion.Text);
                this.anguloFriccion = Math.Tan((double)this.anguloFriccion);
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
            if (this.nivelFreatico)
            {
                //caso a
                if (this.cotaNivelFreatico >= 0 && this.cotaNivelFreatico <= this.empotramientoDF)
                {
                    List<double> d = new List<double>();
                    d.Add(this.estratos[0].Espesor);
                    for(int i = 0; i < this.estratos.Count(); i++)
                    {
                        if(i != 0)
                        {
                            if (this.empotramientoDF < this.estratos[i].Espesor)
                            {
                                d.Add(this.estratos[i].Espesor);
                            }
                        }   
                    }
                    double? esfuerzoefectivo = (this.cotaNivelFreatico*this.pesoEspecifico);
                    for(int i = 0; i < d.Count(); i++)
                    {
                        esfuerzoefectivo = d[i]*(this.pesoEspecificoSaturado - (2.4 * 907.185)); 
                    }
                    MessageBox.Show("Esfuerzo efectivo Caso A " + esfuerzoefectivo);
                }
                //caso b
                else if (this.cotaNivelFreatico > this.empotramientoDF && this.cotaNivelFreatico < (this.empotramientoDF+1))
                {
                    //this.pesoEspecifico = 
                }
            }
        }

        
    }
}
