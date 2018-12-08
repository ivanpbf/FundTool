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
    /// Interaction logic for Directas.xaml
    /// </summary>
    public partial class Directas : Window
    {
        public int? resistenciaAcero;
        public int? resistenciaConcreto;

        public Directas()
        {
            InitializeComponent();
        }

        private void ResistenciaDelConcreto(object sender, TextChangedEventArgs e)
        {
        }

        private void ResistenciaDelAcero(object sender, TextChangedEventArgs e)
        {
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new System.Text.RegularExpressions.Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void AceptarMateriales(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.ResistenciaAcero.Text) || String.IsNullOrEmpty(this.ResistenciaConcreto.Text))
            {
                MessageBox.Show("Alguno de los datos no fue ingresado correctamente o estan vacios");
            }
            else
            {
                this.resistenciaAcero = Int32.Parse(this.ResistenciaAcero.Text);
                this.resistenciaConcreto = Int32.Parse(this.ResistenciaAcero.Text);
                this.ResistenciaAcero.IsEnabled = false;
                this.ResistenciaConcreto.IsEnabled = false;
                this.Cancelarm.IsEnabled = false;
                this.Aceptarm.IsEnabled = false;
            }
        }

        private void CancelarMateriales(object sender, RoutedEventArgs e)
        {
            this.Close(); //cierra la ventana
        }
    }
}
