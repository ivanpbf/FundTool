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
            public String Diametro_Teorico_Pulgadas { get; set; }
            public double Seccion_Teorica { get; set; }
            public double Diametro_Teorico { get; set; }
            //public double Area_De_Acero { get; set; }
            public int Numero_De_Cabillas { get; set; }
        }
        List<Opcion> tabla;
        List<double> diametrosTeoricos;
        List<String> diametrosTeoricosPulgadas;
        List<int> opcionesDeAceroLongitudinal;
        String nombreApoyo;
        //double aceroLongitudinalApoyo;
        int numeroApoyo;
        public double diametroTeoricocm; //diametro teorico elegido, igual lo de abajo
        public int numeroCabillas;
        public String diametroTeoricopulg;
        public double seccionTeoricaopcion;
        public List<double> seccionTeorica;

        public Cabillas(List<int> Opciones, String nombreApoyo, int Numero, List<double> secteor)
        {
            InitializeComponent();
            this.opcionesDeAceroLongitudinal = new List<int>();
            this.opcionesDeAceroLongitudinal = Opciones;
            this.nombreApoyo = nombreApoyo;
            //this.aceroLongitudinalApoyo = new double();
            //this.aceroLongitudinalApoyo = aceroLongitudinalApoyo; no lo usamos
            this.numeroApoyo = Numero;
            this.NombreApoyo.Text = this.nombreApoyo;
            this.NumeroApoyo.Text = this.numeroApoyo.ToString();
            this.seccionTeorica = new List<double>();
            this.seccionTeorica = secteor;
            this.diametrosTeoricos = new List<double> { 1.588, 1.905, 2.222, 2.540, 3.581 };
            this.diametrosTeoricosPulgadas = new List<String> { "5/8", "3/4", "7/8", "1", "1.3/8" };
            IniciarGrid();
        }

        public void IniciarGrid()
        {
            this.tabla = new List<Opcion>();
            for (int i = 0; i < this.diametrosTeoricos.Count(); i++)
            {
                Opcion aux = new Opcion();
                aux.Seccion_Teorica = this.seccionTeorica[i];
                aux.Diametro_Teorico_Pulgadas = this.diametrosTeoricosPulgadas[i];
                aux.Diametro_Teorico = this.diametrosTeoricos[i];
                //aux.Area_De_Acero = this.aceroLongitudinalApoyo;
                aux.Numero_De_Cabillas = this.opcionesDeAceroLongitudinal[i];
                this.tabla.Add(aux);
            }
            this.GridOpciones.ItemsSource = tabla;
        }

        private void AceptarSeleccion(object sender, RoutedEventArgs e)
        {
            if (this.GridOpciones.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una opcion");
            }
            else
            {
                int opcion = this.GridOpciones.SelectedIndex;
                this.diametroTeoricocm = this.diametrosTeoricos[opcion];
                this.numeroCabillas = this.opcionesDeAceroLongitudinal[opcion];
                this.seccionTeoricaopcion = this.seccionTeorica[opcion];
                this.diametroTeoricopulg = this.diametrosTeoricosPulgadas[opcion];
                Close();
            }
        }

        public double DiametroTeoricoCM()
        {
            return diametroTeoricocm;
        }

        public int NumeroDeCabillas()
        {
            return numeroCabillas;
        }

        public double SeccionTeoricaEle()
        {
            return seccionTeoricaopcion;
        }

        public String DiametroTeoricoPULG()
        {
            return diametroTeoricopulg;
        }
    }
}
