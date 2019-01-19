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
        public class Pilote
        {
            public long Diametro { get; set; }
            public long Longitud { get; set; }
            public long AreaCabillas { get; set; }
            public long EspesorCabezal { get; set; }
            public long EspaciamientoCabillas { get; set; }
            public long DimensionesCabezal { get; set; }
        }
        public class Apoyo
        {
            public long EspaciamientoEntrePilotes { get; set; }
            public List<Pilote> Pilotes { get; set; }
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
        public long? nsptpunta;
        public int? resistenciaConcreto;
        public List<int> diametrosComerciales = new List<int> { 55, 65, 80, 90, 100, 110, 120, 130, 140, 150 }; // centimetros
        public List<MetroGolpe> golpesSuelo;
        public int? profundidadEstudioSuelos;
        public int? longitudPilote;
        public int? espesorRelleno;
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
                //cambiar el nombre del boton
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
                  && !String.IsNullOrEmpty(this.ProfundidadEstudioSuelosG.Text) && this.introdujoGolpes && !String.IsNullOrEmpty(this.NSPTPunta.Text))
            {
                this.longitudPilote = Int32.Parse(this.LongitudPiloteG.Text);
                this.espesorRelleno = Int32.Parse(this.LongitudRellenoG.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloG.Text);
                this.coefFriccionRelleno = Int32.Parse(this.CoefFriccionRellenoG.Text);
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroG.Text)));
                this.nsptpunta = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.NSPTPunta.Text)));
                String texto = ListaNumerosG.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
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
                this.espesorRelleno = Int32.Parse(this.LongitudRellenoC.Text);
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
                this.espesorRelleno = Int32.Parse(this.LongitudRellenoGC.Text);
                this.coefFriccion = Int32.Parse(this.CoefFriccionGC.Text);
                this.coefFriccionSuelo = Int32.Parse(this.CoefFriccionSueloGC.Text);
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroGC.Text)));
                String texto = ListaNumerosGC.SelectedItem.ToString();
                char num = texto[0];
                int cantidad = (int)Char.GetNumericValue(num);
                this.numeroPilotes = cantidad;
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
            if(this.tipoDeSuelo == "Granular")
            {
                //sacar nsptfuste
                long nsptfuste = new long();
                nsptfuste = 0;
                for (int i = 1; i <= this.golpesSuelo.Count; i++)
                {
                    int numero = this.golpesSuelo[i-1].NumeroDeGolpes;
                    nsptfuste = numero + nsptfuste;
                }
                nsptfuste = nsptfuste / this.golpesSuelo.Count;
                //calcular el diametro comercialde pilotes del apoyo
                for (int i = 0; i < this.apoyos.Count; i++)
                {
                    this.apoyos[i].Pilotes = new List<Pilote>();
                    int numeropilotes = new int();
                    double qadmisible = new double();
                    qadmisible = 0;
                    numeropilotes = 1;
                    Boolean qadmisiblemayor = false;
                    for(int j = 0; j < diametrosComerciales.Count; j++)
                    {
                        diametrosComerciales[j] = diametrosComerciales[j]/100;
                        double areapunta = (3.14159265358979) + Math.Pow((diametrosComerciales[j] / 2), 2);
                        double areafuste = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)this.longitudPilote;
                        double friccionnegativa = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)this.espesorRelleno * 0.3;
                        qadmisible = ((4 / 3) * (double)this.nsptpunta * (areapunta)) + ((4 / 600) * (double)nsptfuste * (areafuste)) - friccionnegativa;
                        double areaAceroLongitudinal = (double)this.porcentajeAcero * areapunta;
                        double qestructural = 0.225*(((double)this.resistenciaConcreto * (areapunta)) + ((double)this.resistenciaAcero)*areaAceroLongitudinal);
                        if(qadmisible >= this.apoyos[i].Carga)
                        {
                            qadmisiblemayor = true;
                        }
                        if (!qadmisiblemayor && j==diametrosComerciales.Count()-1) //PENDIENTEPUTO
                        {
                            j = 0;
                            numeropilotes++;
                        }
                    }
                    MessageBox.Show("qadmisible " + qadmisible + " cargaapoyo " + this.apoyos[i].Carga + " numeropilotes " + numeropilotes);
                }
            }
        }

        private void Granular_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
