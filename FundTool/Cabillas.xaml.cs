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
        List<String> diametrosTeoricos = new List<string> { "3/8", "1/2", "5/8", "3/4", "7/8", "1", "1.3/8" };
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

        }
    }
}
