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

namespace WpfApp1
{

    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {



        }
    }

    class Apartment
    {

        public int Number { get; set; }
        public float Square { get; set; }
        public int CountRooms { get; set; }
        public int Storey { get; set; }
        public double Price { get; set; }
        public bool Reservation { get; set; }

    }
}
