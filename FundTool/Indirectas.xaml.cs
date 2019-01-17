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
    /// Interaction logic for Indirectas.xaml
    /// </summary>
    public partial class Indirectas : Window
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
            public int CoordEjeX { get; set; }
            public int CoordEjeY { get; set; }
            public int Carga { get; set; }
            public int MtoEnEjeX { get; set; }
            public int MtoEnEjeY { get; set; }
            public int FBasalX { get; set; }
            public int FBasalY { get; set; }
        }
        public class Estrato
        {
            public String Nombre { get; set; }
            public String Espesor { get; set; }
            public String Descripcion { get; set; }
            public int Angulo { get; set; }
            public int Cohesion { get; set; }
            public int Peso { get; set; }
        }
        public String tipoDeSuelo;
        public int? resistenciaAcero;
        public int? resistenciaConcreto;
        public List<MetroGolpe> golpesSuelo;
        public int? profundidadEstudioSuelos;
        public int? longitudPilote;
        public int? longitudRelleno;
        public int? coefFriccionSuelo;
        public int? coefFriccionRelleno;
        public long? porcentajeAcero;
        public int? numeroPilotes;
        public int? cohesionFuste;
        public int? cohesionPunta;
        public int? factorAdherencia;
        public int? numeroEstratos;
        public int? coefFriccion;
        public int? cantidadFundaciones;
        public Boolean introdujoGolpes;
        public List<Apoyo> apoyos;
        public List<Estrato> estratos;


        public Indirectas()
        {
            InitializeComponent();
            this.TipoDeSuelo.Visibility = Visibility.Collapsed;
            this.GranularGrid.Visibility = Visibility.Collapsed;
            this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
            this.GridFinal.Visibility = Visibility.Collapsed;
            this.golpesSuelo = new List<MetroGolpe>();

        }

        private void CancelarSuelo(object sender, RoutedEventArgs e)
        {
            this.CohesivoGrid.Visibility = Visibility.Collapsed;
            this.GranularGrid.Visibility = Visibility.Collapsed;
            this.GranularCohesivoGrid.Visibility = Visibility.Collapsed;
            this.Granular.IsEnabled = true;
            this.Cohesivo.IsEnabled = true;
            this.GranularCohesivo.IsEnabled = true;
            this.Siguientes.IsEnabled = true;
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void NumericOnlyDecimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }


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

        private void CancelarMateriales(object sender, RoutedEventArgs e)
        {
            this.Close(); //cierra la ventana
        }

        private void AceptarSuelo(object sender, RoutedEventArgs e)
        {
            if (!(Boolean)this.Granular.IsChecked && !(Boolean)this.Cohesivo.IsChecked && !(Boolean)this.GranularCohesivo.IsChecked)
            {
                MessageBox.Show("Por favor elija un tipo de suelo.");
                return;
            }
            else if ((Boolean)this.Granular.IsChecked)
            {
                this.tipoDeSuelo = "Granular";
                this.GranularGrid.Visibility = Visibility.Visible;
                this.Granular.IsEnabled = false;
                this.Cohesivo.IsEnabled = false;
                this.GranularCohesivo.IsEnabled = false;
                this.Siguientes.IsEnabled = false;
                return;
            }
            else if ((Boolean)this.Cohesivo.IsChecked)
            {
                this.tipoDeSuelo = "Cohesivo";
                this.CohesivoGrid.Visibility = Visibility.Visible;
                this.Granular.IsEnabled = false;
                this.Cohesivo.IsEnabled = false;
                this.GranularCohesivo.IsEnabled = false;
                this.Siguientes.IsEnabled = false;
                return;
            }
            else if ((Boolean)this.GranularCohesivo.IsChecked)
            {
                this.tipoDeSuelo = "GranularCohesivo";
                this.GranularCohesivoGrid.Visibility = Visibility.Visible;
                this.Granular.IsEnabled = false;
                this.Cohesivo.IsEnabled = false;
                this.GranularCohesivo.IsEnabled = false;
                this.Siguientes.IsEnabled = false;
                return;
            }
        }

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
                    apoyonuevo.Nombre = apoyonuevo.Numero.ToString();
                    apoyos.Add(apoyonuevo);
                }
                this.ApoyosTotales.Text = apoyos.Count().ToString();
                //agregando columnas
                for (int i = 0; i < numerox; i++)
                {
                    ColumnDefinition gridcol = new ColumnDefinition();
                    GridApoyos.ColumnDefinitions.Add(gridcol);
                }
                //agregando filas
                for (int i = 0; i < numeroy; i++)
                {
                    RowDefinition gridro = new RowDefinition();
                    GridApoyos.RowDefinitions.Add(gridro);
                }
                //agregando botones
                int aux = 1;
                for (int j = 0; j < numeroy; j++)
                {
                    for (int i = 0; i < numerox; i++)
                    {
                        Button boton = new Button();
                        boton.Content = aux.ToString();
                        boton.Click += new RoutedEventHandler(this.BuscarApoyo);
                        Grid.SetRow(boton, j);
                        Grid.SetColumn(boton, i);
                        GridApoyos.Children.Add(boton);
                        aux++;
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
            this.NombreApoyo.Text = this.apoyos[numero - 1].Nombre;
            this.CargaApoyo.Text = this.apoyos[numero - 1].Carga.ToString();
            this.NumeroApoyo.Text = this.apoyos[numero - 1].Numero.ToString();
            this.CoordXApoyo.Text = this.apoyos[numero - 1].CoordEjeX.ToString();
            this.CoordYApoyo.Text = this.apoyos[numero - 1].CoordEjeY.ToString();
            this.MtoEjeXApoyo.Text = this.apoyos[numero - 1].MtoEnEjeX.ToString();
            this.MtoEjeYApoyo.Text = this.apoyos[numero - 1].MtoEnEjeY.ToString();
            this.FBasalXApoyo.Text = this.apoyos[numero - 1].FBasalX.ToString();
            this.FBasalYApoyo.Text = this.apoyos[numero - 1].FBasalY.ToString();
        }

        private void IntroducirDatosApoyo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.NombreApoyo.Text) && !String.IsNullOrEmpty(this.CargaApoyo.Text) && !String.IsNullOrEmpty(this.CoordXApoyo.Text) && !String.IsNullOrEmpty(this.CoordYApoyo.Text)
                && !String.IsNullOrEmpty(this.MtoEjeXApoyo.Text) && !String.IsNullOrEmpty(this.MtoEjeYApoyo.Text) && !String.IsNullOrEmpty(this.FBasalXApoyo.Text) && !String.IsNullOrEmpty(this.FBasalYApoyo.Text))
            {
                int numero = Int32.Parse(this.NumeroApoyo.Text);
                this.apoyos[numero - 1].Nombre = this.NombreApoyo.Text;
                this.apoyos[numero - 1].Carga = Int32.Parse(this.CargaApoyo.Text);
                this.apoyos[numero - 1].CoordEjeX = Int32.Parse(this.CoordXApoyo.Text);
                this.apoyos[numero - 1].CoordEjeY = Int32.Parse(this.CoordYApoyo.Text);
                this.apoyos[numero - 1].MtoEnEjeX = Int32.Parse(this.MtoEjeXApoyo.Text);
                this.apoyos[numero - 1].MtoEnEjeY = Int32.Parse(this.MtoEjeYApoyo.Text);
                this.apoyos[numero - 1].FBasalX = Int32.Parse(this.FBasalXApoyo.Text);
                this.apoyos[numero - 1].FBasalY = Int32.Parse(this.FBasalYApoyo.Text);
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

        private void IntrodujoDatosSueloGranular(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LongitudPiloteG.Text) && !String.IsNullOrEmpty(this.CoefFriccionSueloG.Text) &&
                  !String.IsNullOrEmpty(this.CoefFriccionRellenoG.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroG.Text)
                  && !String.IsNullOrEmpty(this.ProfundidadEstudioSuelosG.Text) && this.introdujoGolpes)
            {
                this.longitudPilote = Int32.Parse(this.LongitudPiloteG.Text);
                this.longitudRelleno = Int32.Parse(this.LongitudRellenoG.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloG.Text);
                this.coefFriccionRelleno = Int32.Parse(this.CoefFriccionRellenoG.Text);
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroG.Text)));
                String texto = ListaNumerosG.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
                /*Luego
                 * Hara algo relacionado con todo lo que pidio, primero terminar las de los otros suelos
                 al parecer Granular usa Meyerhof*/
                this.SolicitacionesGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
            }
        }

        private void IntrodujoDatosSueloCohesivo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LongitudPiloteC.Text) && !String.IsNullOrEmpty(this.CoefFriccionSueloC.Text) &&
                  !String.IsNullOrEmpty(this.CoefFriccionRellenoC.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroC.Text)
                  && !String.IsNullOrEmpty(this.CohesionFusteC.Text) && !String.IsNullOrEmpty(this.CohesionPuntaC.Text) && !String.IsNullOrEmpty(this.FactorAdherenciaC.Text))
            {
                this.longitudPilote = Int32.Parse(this.LongitudPiloteC.Text);
                this.longitudRelleno = Int32.Parse(this.LongitudRellenoC.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloC.Text);
                this.coefFriccionRelleno = Int32.Parse(this.CoefFriccionRellenoC.Text);
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroC.Text)));
                String texto = ListaNumerosG.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
                this.cohesionFuste = Int32.Parse(this.CohesionFusteC.Text);
                this.cohesionPunta = Int32.Parse(this.CohesionPuntaC.Text);
                this.factorAdherencia = Int32.Parse(this.FactorAdherenciaC.Text);
                /*Luego
                 * Hara algo relacionado con todo lo que pidio, primero terminar las de los otros suelos
                 al parecer cohesion usa skempton?*/
                this.SolicitacionesGrid.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
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
                    aux.Espesor = "0";
                    this.estratos.Add(aux);
                }
                Estrato punta = new Estrato();
                punta.Nombre = "Punta";
                punta.Espesor = "Punta";
                this.estratos.Add(punta);
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
                if (espesor == null || angulo == null || cohesion == null || peso == null)
                {
                    MessageBox.Show("Alguno de los valores esta vacio, por favor introduzca un numero");
                    return;
                }
                if (this.estratos[i].Nombre == "Punta")
                {
                    if (String.IsNullOrEmpty(espesor.Text))
                    {
                        this.estratos[i].Espesor = "Punta";
                    }
                }
                else
                {
                    this.estratos[i].Espesor = espesor.Text;
                }
                this.estratos[i].Angulo = Int32.Parse(angulo.Text);
                this.estratos[i].Cohesion = Int32.Parse(cohesion.Text);
                this.estratos[i].Peso = Int32.Parse(peso.Text);
                this.SiguienteDatosSueloGC.IsEnabled = true;
            }
        }

        private void IntrodujoDatosSueloGranularCohesivo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LongitudPiloteGC.Text) && !String.IsNullOrEmpty(this.LongitudRellenoGC.Text) &&
                !String.IsNullOrEmpty(this.CoefFriccionSueloGC.Text) && !String.IsNullOrEmpty(this.CoefFriccionGC.Text)
                && !String.IsNullOrEmpty(this.PorcentajeAceroGC.Text))
            {
                this.longitudPilote = Int32.Parse(this.LongitudPiloteGC.Text);
                this.longitudRelleno = Int32.Parse(this.LongitudRellenoGC.Text);
                this.coefFriccion = Int32.Parse(this.CoefFriccionGC.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloGC.Text);
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroGC.Text)));
                String texto = ListaNumerosGC.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
                /*Luego
                 * Hara algo relacionado con todo lo que pidio, primero terminar las de los otros suelos
                 al parecer cohesion usa caquot-kerisel*/
                this.SolicitacionesGrid.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
            }
        }


        private void CompletarIndirectas(object sender, RoutedEventArgs e)
        {
            //aqui hara lo siguiente que seria generar otra ventana y dar resultados? dependiendo del metodo a usar del tipo de suelo
            //tal vez
        }

    }
}
