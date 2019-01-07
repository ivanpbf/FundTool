using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        public class Estrato
        {
            public String Nombre { get; set; }
            public String Espesor { get; set; }
            public int Angulo { get; set; }
            public int Cohesion { get; set; }
            public int Peso { get; set; }
        }
        public String tipoDeSuelo;
        public int? resistenciaAcero;
        public int? resistenciaConcreto;
        public List<MetroGolpe> golpesSuelo;
        public int? profundidadEstudioSuelos;
        public int? diametroInicial;
        public int? longitudPilote;
        public int? longitudRelleno;
        public int? coefFriccionSuelo;
        public int? coefFriccionRelleno;
        public int? porcentajeAcero;
        public int? numeroPilotes;
        public int? cargaActuante;
        public int? momentoX;
        public int? momentoY;
        public int? cohesionFuste;
        public int? cohesionPunta;
        public int? factorAdherencia;
        public int? numeroEstratos;
        public int? coefFriccion;
        public int? cantidadFundaciones;
        public List<Estrato> estratos;


        public Indirectas()
        {
            InitializeComponent();
            this.TipoDeSuelo.Visibility = Visibility.Collapsed;
            this.GranularGrid.Visibility = Visibility.Collapsed;
            this.GridCantidad.Visibility = Visibility.Collapsed;
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
            e.Handled = new System.Text.RegularExpressions.Regex("[^0-9]+").IsMatch(e.Text);
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

        private void AgregarMetroyGolpeGranular(object sender, RoutedEventArgs e)
        {
            String nuevo = Interaction.InputBox("Metro " + (this.golpesSuelo.Count + 1), "Agregar Golpe");
            bool esNumero = Microsoft.VisualBasic.Information.IsNumeric(nuevo);
            if (esNumero)
            {
                MetroGolpe nuevom = new MetroGolpe() { Metro = this.golpesSuelo.Count + 1, NumeroDeGolpes = Int32.Parse(nuevo) };
                this.golpesSuelo.Add(nuevom);
                this.ProfundidadEstudioSuelosG.Text = this.golpesSuelo.Count.ToString();
                this.profundidadEstudioSuelos = this.golpesSuelo.Count;
                ObservableCollection<MetroGolpe> obsCollection = new ObservableCollection<MetroGolpe>(this.golpesSuelo);
                DataGridGolpesG.DataContext = obsCollection;
            }
            else
            {
                MessageBox.Show("Introduzca un valor numerico");
                return;
            }

        }

        private void IntrodujoDatosSueloGranular(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.DiametroInicialG.Text) && !String.IsNullOrEmpty(this.LongitudPiloteG.Text) && !String.IsNullOrEmpty(this.CoefFriccionSueloG.Text) &&
                  !String.IsNullOrEmpty(this.CoefFriccionRellenoG.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroG.Text) && !String.IsNullOrEmpty(this.CargaActuanteG.Text)
                  && !String.IsNullOrEmpty(this.ProfundidadEstudioSuelosG.Text))
            {
                this.diametroInicial = Int32.Parse(this.DiametroInicialG.Text);
                this.longitudPilote = Int32.Parse(this.LongitudPiloteG.Text);
                this.longitudRelleno = Int32.Parse(this.LongitudRellenoG.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloG.Text);
                this.coefFriccionRelleno = Int32.Parse(this.CoefFriccionRellenoG.Text);
                this.porcentajeAcero = Int32.Parse(this.PorcentajeAceroG.Text);
                String texto = ListaNumerosG.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
                this.cargaActuante = Int32.Parse(this.CargaActuanteG.Text);
                this.momentoX = Int32.Parse(this.MomentoXG.Text);
                this.momentoY = Int32.Parse(this.MomentoYG.Text);
                /*Luego
                 * Hara algo relacionado con todo lo que pidio, primero terminar las de los otros suelos
                 al parecer Granular usa Meyerhof*/
                this.GridCantidad.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
            }
        }

        private void IntrodujoDatosSueloCohesivo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.DiametroInicialC.Text) && !String.IsNullOrEmpty(this.LongitudPiloteC.Text) && !String.IsNullOrEmpty(this.CoefFriccionSueloC.Text) &&
                  !String.IsNullOrEmpty(this.CoefFriccionRellenoC.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroC.Text) && !String.IsNullOrEmpty(this.CargaActuanteC.Text)
                  && !String.IsNullOrEmpty(this.CohesionFusteC.Text) && !String.IsNullOrEmpty(this.CohesionPuntaC.Text) && !String.IsNullOrEmpty(this.FactorAdherenciaC.Text))
            {
                this.diametroInicial = Int32.Parse(this.DiametroInicialC.Text);
                this.longitudPilote = Int32.Parse(this.LongitudPiloteC.Text);
                this.longitudRelleno = Int32.Parse(this.LongitudRellenoC.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloC.Text);
                this.coefFriccionRelleno = Int32.Parse(this.CoefFriccionRellenoC.Text);
                this.porcentajeAcero = Int32.Parse(this.PorcentajeAceroC.Text);
                String texto = ListaNumerosG.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
                this.cargaActuante = Int32.Parse(this.CargaActuanteC.Text);
                this.momentoX = Int32.Parse(this.MomentoXC.Text);
                this.momentoY = Int32.Parse(this.MomentoYC.Text);
                this.cohesionFuste = Int32.Parse(this.CohesionFusteC.Text);
                this.cohesionPunta = Int32.Parse(this.CohesionPuntaC.Text);
                this.factorAdherencia = Int32.Parse(this.FactorAdherenciaC.Text);
                /*Luego
                 * Hara algo relacionado con todo lo que pidio, primero terminar las de los otros suelos
                 al parecer cohesion usa skempton?*/
                this.GridCantidad.Visibility = Visibility.Visible;

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
                    this.estratos.Add(aux);
                }
                Estrato punta = new Estrato();
                punta.Nombre = "Punta";
                punta.Espesor = "Punta";
                ObservableCollection<Estrato> obsCollection = new ObservableCollection<Estrato>(this.estratos);
                DataGridEstratos.DataContext = obsCollection;
                DataGridEstratos.Columns[0].IsReadOnly = true;
                DataGridEstratos.Columns[1].Header = "Espesor (m)";
                DataGridEstratos.Columns[2].Header = "Angulo de Friccion";
                DataGridEstratos.Columns[3].Header = "Cohesion (Ton/m²)";
                DataGridEstratos.Columns[4].Header = "Peso Unitario (Ton/m²)";
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
                TextBlock angulo = DataGridEstratos.Columns[2].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock cohesion = DataGridEstratos.Columns[3].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                TextBlock peso = DataGridEstratos.Columns[4].GetCellContent(DataGridEstratos.Items[i]) as TextBlock;
                if (espesor == null || angulo == null || cohesion == null || peso == null)
                {
                    MessageBox.Show("Alguno de los valores esta vacio, por favor introduzca un numero");
                    return;
                }
                if (this.estratos[i].Nombre == "Punta")
                {
                    this.estratos[i].Espesor = "Punta";
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
            if (!String.IsNullOrEmpty(this.DiametroInicialGC.Text) && !String.IsNullOrEmpty(this.LongitudPiloteGC.Text) && !String.IsNullOrEmpty(this.LongitudRellenoGC.Text) &&
                !String.IsNullOrEmpty(this.CoefFriccionSueloGC.Text) && !String.IsNullOrEmpty(this.CoefFriccionGC.Text) && !String.IsNullOrEmpty(this.PorcentajeAceroGC.Text)
                && !String.IsNullOrEmpty(this.CargaActuanteGC.Text))
            {
                this.diametroInicial = Int32.Parse(this.DiametroInicialGC.Text);
                this.longitudPilote = Int32.Parse(this.LongitudPiloteGC.Text);
                this.longitudRelleno = Int32.Parse(this.LongitudRellenoGC.Text);
                this.coefFriccion = Int32.Parse(this.CoefFriccionGC.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloGC.Text);
                this.porcentajeAcero = Int32.Parse(this.PorcentajeAceroGC.Text);
                String texto = ListaNumerosGC.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
                this.cargaActuante = Int32.Parse(this.CargaActuanteGC.Text);
                this.momentoX = Int32.Parse(this.MomentoXGC.Text);
                this.momentoY = Int32.Parse(this.MomentoYGC.Text);
                /*Luego
                 * Hara algo relacionado con todo lo que pidio, primero terminar las de los otros suelos
                 al parecer cohesion usa caquot-kerisel*/
                this.GridCantidad.Visibility = Visibility.Visible;

            }
            else
            {
                MessageBox.Show("Alguno de los datos importantes esta vacio");
                return;
            }
        }

        private void IntrodujoCantidadFundaciones(object sender, RoutedEventArgs e)
        {
            String texto = ListaCantidad.SelectedItem.ToString();
            if (texto.StartsWith("1"))
            {
                String completo = texto.Substring(0, 1);
                int cantidad = Int32.Parse(completo);
                this.cantidadFundaciones = cantidad;
                this.CuantasFundaciones.Text = this.cantidadFundaciones.ToString();
                //luego lo siguiente
                return;
            }
            else
            {
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.cantidadFundaciones = cantidad;
                this.CuantasFundaciones.Text = this.cantidadFundaciones.ToString();
                //luego lo siguiente
                return;
            }
        }

    }
}
