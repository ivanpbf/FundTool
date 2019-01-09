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
            public int CoordEjeX { get; set; }
            public int CoordEjeY { get; set; }
            public int Carga { get; set; }
            public int MtoEnEjeX { get; set; }
            public int MtoEnEjeY { get; set; }
            public int FBasalX { get; set; }
            public int FBasalY { get; set; }
        }
        public int? resistenciaAcero;
        public int? resistenciaConcreto;
        public int? anguloFriccion;
        public int? cohesion;
        public int? pesoEspecifico;
        public int? empotramientoDF;
        public String falla;
        public Boolean nivelFreatico;
        public int? cotaNivelFreatico;
        public Boolean datosEnsayoSPT;
        public int? profundidadEstudioSuelos;
        public int? asentamiento;
        public List<MetroGolpe> golpesSuelo;
        public List<Apoyo> apoyos;
        public int? cantidadFundaciones;


        public Directas()
        {
            InitializeComponent();
            this.datosdelsuelo.Visibility = Visibility.Collapsed;
            this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
            this.SolicitacionesGrid.Visibility = Visibility.Collapsed;
            this.GridCantidad.Visibility = Visibility.Collapsed;
            this.GridFinal.Visibility = Visibility.Collapsed;
            this.golpesSuelo = new List<MetroGolpe>();

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


        private void IntrodujoDatosSuelo(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.AnguloFriccion.Text) && !String.IsNullOrEmpty(this.Cohesion.Text) && !String.IsNullOrEmpty(this.PesoEspecifico.Text)
                && !String.IsNullOrEmpty(this.EmpotramientoDF.Text))
            {
                this.anguloFriccion = Int32.Parse(this.AnguloFriccion.Text);
                this.cohesion = Int32.Parse(this.Cohesion.Text);
                this.pesoEspecifico = Int32.Parse(this.PesoEspecifico.Text);
                this.empotramientoDF = Int32.Parse(this.EmpotramientoDF.Text);
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
                        this.cotaNivelFreatico = Int32.Parse(this.CotaNF.Text);
                    }
                    else
                    {
                        MessageBox.Show("Introduzca la Cota de Nivel Freatico");
                        return;
                    }
                }
                if ((Boolean)this.IntroducirSPT.IsChecked)
                {
                    if(!String.IsNullOrEmpty(this.ProfundidadEstudioSuelos.Text) && !String.IsNullOrEmpty(this.Asentamiento.Text))
                    {
                        this.asentamiento = Int32.Parse(this.Asentamiento.Text);
                    }
                    else
                    {
                        MessageBox.Show("Introduzca los Datos del Ensayo S.P.T.");
                        return;
                    }
                }
                this.GridCantidad.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Introduzca los Datos del Suelo");
                return;
            }
        }

        private void AgregarMetroyGolpe(object sender, RoutedEventArgs e)
        {
            String nuevo = Interaction.InputBox("Metro "+(this.golpesSuelo.Count+1), "Agregar Golpe");
            bool esNumero = Microsoft.VisualBasic.Information.IsNumeric(nuevo);
            if (esNumero){
                MetroGolpe nuevom = new MetroGolpe(){ Metro = this.golpesSuelo.Count + 1, NumeroDeGolpes = Int32.Parse(nuevo) };
                this.golpesSuelo.Add(nuevom);
                this.ProfundidadEstudioSuelos.Text = this.golpesSuelo.Count.ToString();
                this.profundidadEstudioSuelos = this.golpesSuelo.Count;
                ObservableCollection<MetroGolpe> obsCollection = new ObservableCollection<MetroGolpe>(this.golpesSuelo);
                DataGridGolpes.DataContext = obsCollection;
            }
            else
            {
                MessageBox.Show("Introduzca un valor numerico");
                return;
            }

        }

        private void IntroducirApoyos(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.NroApoyos.Text) && !this.NroApoyos.Text.Equals("0"))
            {
                int numero = Int32.Parse(this.NroApoyos.Text);
                apoyos = new List<Apoyo>();
                for (int i = 0; i < numero; i++)
                {
                    Apoyo solicitacionnueva = new Apoyo();
                    Apoyo aux = solicitacionnueva;
                    aux.Numero = i+1;
                    Boolean introdujoNombre = false;
                    do
                    {
                        String nombre = Interaction.InputBox("Nombre de Solicitacion " + (i + 1), "Agregar Nombre de Solicitaciones");
                        if(nombre != "")
                        {
                            aux.Nombre = nombre;
                            introdujoNombre = true;
                        }
                        else
                        {
                            return;
                        }
                    } while (introdujoNombre == false);
                    this.apoyos.Add(aux);
                }
                ObservableCollection<Apoyo> obsCollection = new ObservableCollection<Apoyo>(this.apoyos);
                DataGridSolicitaciones.DataContext = obsCollection;
                DataGridSolicitaciones.Columns[0].IsReadOnly = true;
                DataGridSolicitaciones.Columns[1].IsReadOnly = true;
                DataGridSolicitaciones.Columns[2].Header = "Coord. En el eje X (m)";
                DataGridSolicitaciones.Columns[3].Header = "Coord. En el eje Y (m)";
                DataGridSolicitaciones.Columns[4].Header = "Carga (Ton)";
                DataGridSolicitaciones.Columns[5].Header = "Mto. en Eje X (Ton-m";
                DataGridSolicitaciones.Columns[6].Header = "Mto. en Eje Y (Ton-m";
                DataGridSolicitaciones.Columns[7].Header = "F. Basal X (ton)";
                DataGridSolicitaciones.Columns[8].Header = "F. Basal Y (Ton)";
                AceptarValoresSolicitaciones.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Introduzca un numero de Apoyos mayor a 0");
                return;
            }

        }

        private void IntroducirDatosSolicitaciones(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < this.apoyos.Count; i++)
            {
                TextBlock coordx = DataGridSolicitaciones.Columns[2].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                TextBlock coordy = DataGridSolicitaciones.Columns[3].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                TextBlock carga = DataGridSolicitaciones.Columns[4].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                TextBlock mtoejex = DataGridSolicitaciones.Columns[5].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                TextBlock mtoejey = DataGridSolicitaciones.Columns[6].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                TextBlock fbasalx = DataGridSolicitaciones.Columns[7].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                TextBlock fbasaly = DataGridSolicitaciones.Columns[8].GetCellContent(DataGridSolicitaciones.Items[i]) as TextBlock;
                if (coordx == null || coordy == null || carga == null || mtoejex == null || mtoejey == null || fbasalx == null || fbasaly == null)
                {
                    MessageBox.Show("Alguno de los valores esta vacio, por favor introduzca un numero");
                    return;
                }
                this.apoyos[i].Carga = Int32.Parse(carga.Text);
                this.apoyos[i].CoordEjeX = Int32.Parse(coordx.Text);
                this.apoyos[i].CoordEjeY = Int32.Parse(coordy.Text);
                this.apoyos[i].MtoEnEjeX = Int32.Parse(mtoejex.Text);
                this.apoyos[i].MtoEnEjeY = Int32.Parse(mtoejey.Text);
                this.apoyos[i].FBasalX = Int32.Parse(fbasalx.Text);
                this.apoyos[i].FBasalY = Int32.Parse(fbasaly.Text);
                this.GridFinal.Visibility = Visibility.Visible;
            }
        }

        private void IntrodujoCantidadFundaciones(object sender, RoutedEventArgs e)
        {
            int posicion = ListaCantidad.SelectedIndex;
            int cantidad = posicion + 1;
            this.cantidadFundaciones = cantidad;
            this.CuantasFundaciones.Text = cantidad.ToString();
            this.SolicitacionesGrid.Visibility = Visibility.Visible;
            //luego lo siguiente
            return;
        }

        private void CompletarDirectas(object sender, RoutedEventArgs e)
        {
            //aqui hara lo siguiente que seria generar otra ventana y dar resultados?
            //tal vez
        }

    }
}
