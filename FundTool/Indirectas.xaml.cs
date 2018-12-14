using System;
using System.Collections.Generic;
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

namespace FundTool
{
    /// <summary>
    /// Interaction logic for Indirectas.xaml
    /// </summary>
    public partial class Indirectas : Window
    {
        public String tipoDeSuelo;
        public int? resistenciaAcero;
        public int? resistenciaConcreto;
        public Indirectas()
        {
            InitializeComponent();
            this.TipoDeSuelo.Visibility = Visibility.Collapsed;
            this.GranularGrid.Visibility = Visibility.Collapsed;

        }

        private void CancelarSuelo(object sender, RoutedEventArgs e)
        {
            this.Close(); //cierra la ventana
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
    }

}
