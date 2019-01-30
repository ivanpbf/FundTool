﻿using System;
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
            public long Diametro { get; set; }
            public long Longitud { get; set; }
            public long AceroLongitudinal { get; set; }
            public long AreaCabillas { get; set; }
            public long EspaciamientoCabillas { get; set; }
        }
        public class Apoyo //y cosas de fundacion
        {
            public long EspaciamientoEntrePilotes { get; set; }
            public List<Pilote> Pilotes { get; set; }
            public int Numero { get; set; }
            public double Eficiencia { get; set; }
            public String Nombre { get; set; }
            public long CoordEjeX { get; set; }
            public long CoordEjeY { get; set; }
            public long Carga { get; set; }
            public long MtoEnEjeX { get; set; }
            public long MtoEnEjeY { get; set; }
            public long FBasalX { get; set; }
            public long FBasalY { get; set; }
            public double Qadmisible { get; set; }
            public double Qestructural { get; set; }
            public long EspesorCabezal { get; set; }
            public long EspaciamientoCabillasApoyo { get; set; }
            public long DimensionesCabezal { get; set; }
        }
        public class Estrato
        {
            public String Nombre { get; set; }
            public long Espesor { get; set; }
            public String Descripcion { get; set; }
            public long Angulo { get; set; }
            public long Cohesion { get; set; }
            public long Peso { get; set; }
        }
        public String tipoDeSuelo;
        public int? resistenciaAcero;
        public long? nsptpunta;
        public int? resistenciaConcreto;
        public List<int> diametrosComerciales = new List<int> { 55, 65, 80, 90, 100, 110, 120, 130, 140, 150 }; // centimetros
        public List<double> seccionTeorica = new List<double> { 0.7133, 1.2667, 1.9806, 2.8502, 3.8777, 5.0670, 10.0717 };
        public List<MetroGolpe> golpesSuelo;
        public long? profundidadEstudioSuelos;
        public long? longitudPilote;
        public long? espesorRelleno;
        public long? longitudEfectiva;
        public long? coefFriccionSuelo;
        public long? coefFriccionRelleno;
        public long? porcentajeAcero;
        public int? numeroPilotes;
        public long? cohesionFuste;
        public long? cohesionPunta;
        public long? factorAdherencia;
        public int? numeroEstratos;
        public long? coefFriccion;
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
            for (int i = 0; i < this.golpesSuelo.Count(); i++)
            {
                TextBlock golpe = new TextBlock();
                golpe = this.DataGridGolpes.Columns[1].GetCellContent(DataGridGolpes.Items[i]) as TextBlock;
                if (golpe == null)
                {
                    MessageBox.Show("Alguno de los valores esta vacio, por favor introduzca un numero");
                    return;
                }
                this.golpesSuelo[i].NumeroDeGolpes = Int32.Parse(golpe.Text);
            }
            MessageBox.Show("Se aceptaron los valores correctamente");
            this.SiguienteDatosSueloG.IsEnabled = true;
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
                this.apoyos[numero - 1].Carga = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CargaApoyo.Text)));
                this.apoyos[numero - 1].CoordEjeX = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoordXApoyo.Text)));
                this.apoyos[numero - 1].CoordEjeY = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoordYApoyo.Text)));
                this.apoyos[numero - 1].MtoEnEjeX = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.MtoEjeXApoyo.Text)));
                this.apoyos[numero - 1].MtoEnEjeY = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.MtoEjeYApoyo.Text)));
                this.apoyos[numero - 1].FBasalX = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.FBasalXApoyo.Text)));
                this.apoyos[numero - 1].FBasalY = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.FBasalYApoyo.Text)));
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
                if(Int32.Parse(this.LongitudRellenoG.Text) > Int32.Parse(this.ProfundidadEstudioSuelosG.Text))
                {
                    MessageBox.Show("El espesor de relleno no puede ser mayor que la profundidad del estudio de los suelos.");
                    return;
                }
                this.longitudPilote = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.LongitudPiloteG.Text)));
                this.espesorRelleno = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.LongitudRellenoG.Text)));
                this.coefFriccionSuelo = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoefFriccionSueloG.Text)));
                this.coefFriccionRelleno = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoefFriccionRellenoG.Text)));
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroG.Text)));
                this.nsptpunta = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.NSPTPunta.Text)));
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
                this.longitudPilote = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.LongitudPiloteC.Text)));
                this.espesorRelleno = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.LongitudRellenoC.Text)));
                this.coefFriccionSuelo = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoefFriccionSueloC.Text)));
                this.coefFriccionRelleno = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoefFriccionRellenoC.Text)));
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroC.Text)));
                this.cohesionFuste = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CohesionFusteC.Text)));
                this.cohesionPunta = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CohesionPuntaC.Text)));
                this.factorAdherencia = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.FactorAdherenciaC.Text)));
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
                this.estratos[i].Espesor = Convert.ToInt64(Math.Floor(Convert.ToDouble(espesor.Text)));
                this.estratos[i].Angulo = Convert.ToInt64(Math.Floor(Convert.ToDouble(angulo.Text)));
                this.estratos[i].Cohesion = Convert.ToInt64(Math.Floor(Convert.ToDouble(cohesion.Text)));
                this.estratos[i].Peso = Convert.ToInt64(Math.Floor(Convert.ToDouble(peso.Text)));
                this.SiguienteDatosSueloGC.IsEnabled = true;
            }
        }

        private void IntrodujoDatosSueloGranularCohesivo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LongitudPiloteGC.Text) && !String.IsNullOrEmpty(this.LongitudRellenoGC.Text) &&
                !String.IsNullOrEmpty(this.CoefFriccionSueloGC.Text) && !String.IsNullOrEmpty(this.CoefFriccionGC.Text)
                && !String.IsNullOrEmpty(this.PorcentajeAceroGC.Text))
            {
                this.longitudPilote = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.LongitudPiloteGC.Text)));
                this.espesorRelleno = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.LongitudRellenoGC.Text)));
                this.coefFriccion = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoefFriccionGC.Text)));
                this.coefFriccionSuelo = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.CoefFriccionSueloGC.Text)));
                this.porcentajeAcero = Convert.ToInt64(Math.Floor(Convert.ToDouble(this.PorcentajeAceroGC.Text)));
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
            if (this.tipoDeSuelo == "Granular")
            {
                //sacar nsptfuste
                long nsptfuste = new long();
                this.longitudEfectiva = this.longitudPilote - this.espesorRelleno;
                nsptfuste = 0;
                long? aPartirDe = this.espesorRelleno;
                for (int i = (int)aPartirDe; i <= this.golpesSuelo.Count; i++)
                {
                    int numero = this.golpesSuelo[i - 1].NumeroDeGolpes;
                    nsptfuste = numero + nsptfuste;
                }
                nsptfuste = nsptfuste / this.golpesSuelo.Count;
                //calcular el diametro comercial y Q de pilotes del apoyo
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
                    Pilote nuevos = new Pilote();
                    int contador = 0;
                    for (int j = 0; j < diametrosComerciales.Count; j++)
                    {
                        nuevos.Diametro = this.diametrosComerciales[j];
                        double areapunta = (3.14159265358979) + Math.Pow((diametrosComerciales[j] / 2), 2);
                        double areafuste = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)this.longitudPilote;
                        double friccionnegativa = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)this.espesorRelleno * 0.3;
                        qadmisible = ((4 / 3) * (double)this.nsptpunta * (areapunta)) + ((4 / 600) * (double)nsptfuste * (areafuste)) - friccionnegativa;
                        double areaAceroLongitudinal = (double)this.porcentajeAcero * areapunta;
                        qestructural = 0.225 * (((double)this.resistenciaConcreto * (areapunta)) + ((double)this.resistenciaAcero) * areaAceroLongitudinal);
                        qadmisible = qadmisible / 1000; //convirtiendo a toneladas
                        qadmisible = qadmisible * numeropilotes;
                        qestructural = qestructural / 1000; //convirtiendo a toneladas
                        qestructural = qestructural * numeropilotes;
                        //convertimos diametro a metros:
                        //OJO QUE EL DIAMETRO NO SEA SIEMPRE 150, REVISAR
                        long aux = nuevos.Diametro / 100;
                        if (qadmisible < qestructural)
                        {
                            ausar = qadmisible;
                        }
                        else
                        {
                            ausar = qestructural;
                        }
                        //vemos cual es menor y esa se va a comparar con la carga
                        if (ausar >= this.apoyos[i].Carga)
                        {
                            if (longitudEfectiva >= 6 * aux && longitudEfectiva <= 30 * aux)
                            {
                                if (contador == 0 && j == (diametrosComerciales.Count() - 1))
                                {
                                    j = -1;
                                    numeropilotes = numeropilotes + 1;
                                    contador++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (j == (diametrosComerciales.Count() - 1))
                        {
                            j = -1;
                            numeropilotes = numeropilotes + 1;
                        }
                    }
                    for (int j = 1; j <= numeropilotes; j++)
                    {
                        this.apoyos[i].Pilotes.Add(nuevos);
                    }
                    this.apoyos[i].Qadmisible = qadmisible;
                    this.apoyos[i].Qestructural = qestructural;
                    //pendiente de los diametros comerciales
                    MessageBox.Show("apoyo: " + this.apoyos[i].Nombre + " Numero de pilotes " + this.apoyos[i].Pilotes.Count() + " Qadmisible " + qadmisible + " Qestructural " + qestructural + " carga del apoyo " + this.apoyos[i].Carga);
                }
                ///falta
            }
            else if (this.tipoDeSuelo == "GranularCohesivo")
            {
                //peso angulo y cohesion de fuste: (valores primados)
                double areaAceroLongitudinal  = new double();
                double pesoPromedio = 0;
                double anguloPromedio = 0;
                double cohesionPromedio = 0;
                double espesorTotal = 0;
                this.longitudEfectiva = this.longitudPilote - this.espesorRelleno;
                for (int i = 0; i < this.estratos.Count(); i++)
                {
                    pesoPromedio = pesoPromedio + (this.estratos[i].Peso * this.estratos[i].Espesor);
                    anguloPromedio = anguloPromedio + (this.estratos[i].Angulo * this.estratos[i].Espesor);
                    cohesionPromedio = cohesionPromedio + (this.estratos[i].Cohesion * this.estratos[i].Espesor);
                    espesorTotal = espesorTotal + this.estratos[i].Espesor;
                }
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
                    Pilote nuevos = new Pilote();
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
                        nuevos.Diametro = this.diametrosComerciales[j];
                        double s1 = 0.192 * (Math.Pow(Math.Tan(45 + (anguloPromedio / 2)), 2)) * ((Math.Pow(Math.E, 4.55 * Math.Tan(anguloPromedio))) - 1);
                        r1 = pesoPromedio * (nuevos.Diametro / 100) * (s1 / 4);
                        double s2 = (Math.Pow(Math.Tan(45+(anguloPromedio/2)),2)) * (Math.Pow(Math.E, Math.Tan(anguloPromedio)));
                        double s2primado = 1+(0.32*Math.Pow(Math.Tan(anguloPromedio),2));
                        r2 = pesoPromedio * longitudEfectiva * s2 * s2primado;
                        double s3primado = (Math.Tan(anguloPromedio)) * Math.Pow(Math.E, (19 / 30) * (Math.Tan(anguloPromedio)) * (4 + Math.Pow(Math.Tan(anguloPromedio), 2 / 3)));
                        r3 = pesoPromedio * (2*(longitudEfectiva * longitudEfectiva)) * (s3primado / (nuevos.Diametro / 100));
                        r4 = (cohesionPromedio / (Math.Tan(anguloPromedio))) * (s2 - 1);
                        double s5primado = 3; //OJOOOOOOOOOOOOOOOOOOOOOOOOOOOOO NO ESSSSSSSSS 3
                        r5 = cohesionPromedio * (4 * longitudEfectiva) * (s5primado / (nuevos.Diametro / 100));
                        double areapunta = (3.14159265358979) + Math.Pow((diametrosComerciales[j] / 2), 2);
                        double areafuste = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)this.longitudPilote;
                        double friccionnegativa = (2 * 3.14159265358979) * (diametrosComerciales[j] / 2) * (double)this.espesorRelleno * 0.3;
                        areaAceroLongitudinal = (double)this.porcentajeAcero * areapunta;
                        qadmisible = (double) (((r1 + r2 + r3 + r4 + r5) * areapunta)/4) - friccionnegativa;
                        qestructural = 0.225 * (((double)this.resistenciaConcreto * (areapunta)) + ((double)this.resistenciaAcero) * areaAceroLongitudinal);
                        qadmisible = qadmisible / 1000; //convirtiendo a toneladas
                        qadmisible = qadmisible * numeropilotes;
                        qestructural = qestructural / 1000; //convirtiendo a toneladas
                        qestructural = qestructural * numeropilotes;
                        //convertimos diametro a metros:
                        //OJO QUE EL DIAMETRO NO SEA SIEMPRE 150, REVISAR
                        long aux = nuevos.Diametro / 100;
                        if (qadmisible < qestructural)
                        {
                            ausar = qadmisible;
                        }
                        else
                        {
                            ausar = qestructural;
                        }
                        //vemos cual es menor y esa se va a comparar con la carga
                        if (ausar >= this.apoyos[i].Carga)
                        {
                            if (longitudEfectiva >= 6 * aux && longitudEfectiva <= 30 * aux)
                            {
                                if (contador == 0 && j == (diametrosComerciales.Count() - 1))
                                {
                                    j = -1;
                                    numeropilotes = numeropilotes + 1;
                                    contador++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else if (j == (diametrosComerciales.Count() - 1))
                        {
                            j = -1;
                            numeropilotes = numeropilotes + 1;
                        }
                    }
                    for (int j = 1; j <= numeropilotes; j++)
                    {
                        this.apoyos[i].Pilotes.Add(nuevos);
                    }
                    this.apoyos[i].Qadmisible = qadmisible;
                    this.apoyos[i].Qestructural = qestructural;
                    //pendiente de los diametros comerciales
                    MessageBox.Show("apoyo: " + this.apoyos[i].Nombre + " Numero de pilotes " + this.apoyos[i].Pilotes.Count() + " Qadmisible " + qadmisible + " Qestructural " + qestructural + " carga del apoyo " + this.apoyos[i].Carga);
                }
                //calculo del acero longitudinal
                List<int> opcionesDeAceroLongitudinal = new List<int>();
                for (int i = 0; i < this.seccionTeorica.Count(); i++)
                {
                    double aceroLongitudinal = areaAceroLongitudinal / this.seccionTeorica[i];
                    int numeroCabillas = (int)Math.Ceiling(aceroLongitudinal);
                    opcionesDeAceroLongitudinal.Add(numeroCabillas);
                }
                bool? oked = false;
                System.Windows.Window win = new Cabillas();
                oked = Autodesk.AutoCAD.ApplicationServices.Application.ShowModalWindow(win);

                if (oked.HasValue && oked.Value)
                {
                    /* input1 = win.Property1;
                       input2 = win.Property2;*/
                    // DO something based in inputs
                }
            }
            //calculando el conjunto de pilotes
            for(int i = 0; i < this.apoyos.Count(); i++)
            {
                int cantPilotes = this.apoyos[i].Pilotes.Count();
                int m =0; //filas
                int n = 0; //columnas
                double S = new double();
                S = 2.5 * this.apoyos[i].Pilotes[0].Diametro;
                switch (cantPilotes)
                {
                    case 1:
                        m = 1;
                        n = 1;
                        break;
                    case 2:
                        m = 1;
                        n = 2;
                        break;
                    case 3:
                        n = 3;
                        m = 1;
                        break;
                    case 4:
                        n = 2;
                        m = 2;
                        break;
                    case 5:
                        n = 3;
                        m = 2;
                        break;
                    case 6:
                        n = 4;
                        m = 3;
                        break;
                    case 7:
                        n = 5;
                        m = 3;
                        break;
                    case 8:
                        n = 5;
                        m = 3;
                        break;
                    case 9:
                        n = 3;
                        m = 3;
                        break;
                    case 10:
                        n = 5;
                        m = 3;
                        break;
                    case 11:
                        n = 7;
                        m = 3;
                        break;
                    case 12:
                        n = 4;
                        m = 3;
                        break;
                    default:
                        if(cantPilotes > 12)
                        {
                            double resta = cantPilotes % 2;
                            //mas tarde
                        }
                        else
                        {
                            Console.Write("No entro al caso, imposible");
                        }
                        break;
                }
                if(cantPilotes == 1)
                {
                    this.apoyos[i].Eficiencia = this.apoyos[i].Qadmisible;
                }
                else
                {
                    
                    this.apoyos[i].Eficiencia = 1 - ((((n * (m - 1)) + (m * (n - 1))) / ((90 * m * n))) * (Math.Atan((this.apoyos[i].Pilotes[0].Diametro) / (S))));
                }
                

            }
        }

        private void Granular_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
