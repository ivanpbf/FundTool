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
    /// Interaction logic for Directas.xaml
    /// </summary>
    public partial class Directas : Window
    {
        public class MetroGolpe{
            public int Metro { get; set; }
            public int NumeroDeGolpes { get; set; }
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


        public Directas()
        {
            InitializeComponent();
            this.datosdelsuelo.Visibility = Visibility.Collapsed;
            this.DatosDelEnsayoSPTGranulares.Visibility = Visibility.Collapsed;
            this.golpesSuelo = new List<MetroGolpe>();

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
                /*this.ResistenciaAcero.IsEnabled = false;
                this.ResistenciaConcreto.IsEnabled = false;
                this.Cancelarm.IsEnabled = false;
                this.Aceptarm.IsEnabled = false;*/
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
                MessageBox.Show("Datos hasta ahora: "+this.resistenciaAcero+ this.resistenciaConcreto + this.anguloFriccion+ this.cohesion + this.pesoEspecifico +
                    this.empotramientoDF + this.falla + this.cotaNivelFreatico + this.profundidadEstudioSuelos + this.asentamiento);
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
    }
}
