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
    /// Interaction logic for Cabillas.xaml
    /// </summary>
    public partial class Cabillas : Window
    {
        List<int> opcionesDeAceroLongitudinal;
        public Cabillas(List<int> Opciones)
        {
            InitializeComponent();
            this.opcionesDeAceroLongitudinal = Opciones;
            IniciarGrid();
        }

        public void IniciarGrid()
        {
            DataGridTextColumn diametroTeorico = new DataGridTextColumn();
            diametroTeorico.Header = "Diametro Teorico";
            this.DataGridOpciones.Columns.Add(diametroTeorico);
            DataGridTextColumn areaAcero = new DataGridTextColumn();
            areaAcero.Header = "Area de Acero (total)";
            this.DataGridOpciones.Columns.Add(areaAcero);
            DataGridTextColumn nCabillas = new DataGridTextColumn();
            nCabillas.Header = "Numero de Cabillas";
            this.DataGridOpciones.Columns.Add(nCabillas);



            ObservableCollection<int> obsCollection = new ObservableCollection<int>(opcionesDeAceroLongitudinal);
                DataGridEstratos.DataContext = obsCollection;
                DataGridEstratos.Columns[0].IsReadOnly = true;
                DataGridEstratos.Columns[1].Header = "Espesor (m)";
                DataGridEstratos.Columns[2].Header = "Descripcion";
                DataGridEstratos.Columns[3].Header = "Angulo de Friccion";
                DataGridEstratos.Columns[4].Header = "Cohesion (Ton/m²)";
                DataGridEstratos.Columns[5].Header = "Peso Unitario (Ton/m²)";
        }
    }
}
