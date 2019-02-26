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

        /// <summary>
        /// Crea e inicializa las componentes de la tabla de opciones de acero
        /// </summary>
        /// <param name="Opciones">Lista de numeros enteros de las opciones de numero de cabillas</param>
        /// <param name="nombreApoyo">Nombre del apoyo para el cual se asignan las opciones</param>
        /// <param name="Numero">Numero del apoyo</param>
        /// <param name="secteor">Lista de valores de la seccion teorica anteriormente calculada en opciones de acero</param>
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

        /// <summary>
        /// Inserta los parametros calculados en el Grid de la ventana interfaz
        /// </summary>
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

        /// <summary>
        /// Acepta la seleccion que el usuario marca en cuanto a las opciones
        /// </summary>
        /// <param name="sender"> Instancia del control que lanza el evento</param>
        /// <param name="e">Argumentos enviados por el evento</param>
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


        /// <summary>
        /// Get del diametro teorico seleccionado
        /// </summary>
        /// <returns>retorna el diametro teorico seleccionado</returns>
        public double DiametroTeoricoCM()
        {
            return diametroTeoricocm;
        }

        /// <summary>
        /// Get del numero de cabillas seleccionado
        /// </summary>
        /// <returns>retorna el numero de cabillas</returns>
        public int NumeroDeCabillas()
        {
            return numeroCabillas;
        }

        /// <summary>
        /// Get de la seccion teorica seleccionada
        /// </summary>
        /// <returns>retorna la seccion teorica seleccionada en la interfaz</returns>
        public double SeccionTeoricaEle()
        {
            return seccionTeoricaopcion;
        }

        /// <summary>
        /// Get del diametro teorico en pulgadas
        /// </summary>
        /// <returns>retorna un string del diametro teorico en pulgadas</returns>
        public String DiametroTeoricoPULG()
        {
            return diametroTeoricopulg;
        }
    }
}
