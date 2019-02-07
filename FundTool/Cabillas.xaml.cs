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

namespace FundTool
{
    /// <summary>
    /// Interaction logic for Cabillas.xaml
    /// </summary>
    public partial class Cabillas : Window
    {
        public class Opcion
        {
            public double SeccionTeorica { get; set; }
            public String DiametroTeorico { get; set; }
            public double AceroApoyo { get; set; }
            public int OpcionDeAcero { get; set; }
        }
        List<Opcion> tabla;
        List<String> diametrosTeoricos = new List<string> { "5/8", "3/4", "7/8", "1", "1.3/8" };
        List<int> opcionesDeAceroLongitudinal;
        String nombreApoyo;
        double aceroLongitudinalApoyo;
        int numeroApoyo;
        public List<double> seccionTeorica;

        public Cabillas(List<int> Opciones, String nombreApoyo, double aceroLongitudinalApoyo, int Numero, List<double> secteor)
        {
            InitializeComponent();
            this.opcionesDeAceroLongitudinal = Opciones;
            this.nombreApoyo = nombreApoyo;
            this.aceroLongitudinalApoyo = aceroLongitudinalApoyo;
            this.numeroApoyo = Numero;
            this.NombreApoyo.Text = this.nombreApoyo;
            this.NumeroApoyo.Text = this.numeroApoyo.ToString();
            this.seccionTeorica = secteor;
            IniciarGrid();
        }

        public void IniciarGrid()
        {
            this.tabla = new List<Opcion>();
            for (int i = 0; i < this.opcionesDeAceroLongitudinal.Count(); i++)
            {
                Opcion op = new Opcion();
                Opcion aux = op;
                aux.SeccionTeorica = this.seccionTeorica[i];
                aux.DiametroTeorico = this.diametrosTeoricos[i];
                aux.AceroApoyo = this.aceroLongitudinalApoyo;
                aux.OpcionDeAcero = this.opcionesDeAceroLongitudinal[i];
                this.tabla.Add(aux);
            }
            this.GridOpciones.ItemsSource = tabla;
            this.GridOpciones.Columns[0].Header = "Seccion Teorica [cm²]";
            this.GridOpciones.Columns[1].Header = "Diametro Teorico [pulgadas]";
            this.GridOpciones.Columns[2].Header = "Area de Acero (Total)";
            this.GridOpciones.Columns[3].Header = "# de Cabillas";
        }

        private void AceptarSeleccion(object sender, RoutedEventArgs e)
        {

        }
    }
}
