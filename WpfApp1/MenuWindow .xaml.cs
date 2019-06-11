using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;

namespace WpfApp1
{
    [Serializable]
    public class Apartment
    {

        public int Number { get; set; }
        public float Square { get; set; }
        public int CountRooms { get; set; }
        public int Storey { get; set; }
        public decimal Price { get; set; }
        public bool Reservation { get; set; }
        public bool SoldOut { get; set; }

    }
    public partial class MenuWindow : Window
    {
    int index = 0;
        ObservableCollection<Apartment> apartments = new ObservableCollection<Apartment>();
        public MenuWindow(User user)
        {

            InitializeComponent();
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Apartment>));
            using (Stream reader = new FileStream("Apartment.xml", FileMode.Open))
            {

               apartments = (ObservableCollection<Apartment>)serializer.Deserialize(reader);
            }
            User_verification(user);
            Checking_the_apartment();
            ShowList();

        }

        public void ShowList()
        {
            if (index < 0)
                index = apartments.Count - 1;
            if (index > apartments.Count - 1)
                index = 0;

            NumberTextBox.Text = apartments[index].Number.ToString();
            SquareTextBox.Text = apartments[index].Square.ToString();
            CountRoomsTextBox.Text = apartments[index].CountRooms.ToString();
            StoreyTextBox.Text = apartments[index].Storey.ToString();
            PriceTextBox.Text = apartments[index].Price.ToString();
            ReservationCheckBox.IsChecked = apartments[index].Reservation;
        }

        public void Serializer()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Apartment>));
            using (var fStream = new FileStream("Apartment.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xml.Serialize(fStream, apartments);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            apartments.Add(new Apartment {
                Number = int.Parse(NumberTextBox.Text),
                Square = float.Parse(SquareTextBox.Text),
                CountRooms = int.Parse(CountRoomsTextBox.Text),
                Storey = int.Parse(StoreyTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text),
                Reservation = ReservationCheckBox.IsChecked.Value,
                SoldOut = false
            });

            Serializer();


        }

        private void Next_Click(object sender, RoutedEventArgs e)
        { index++;ShowList(); Checking_the_apartment(); }

        private void Previous_Click(object sender, RoutedEventArgs e)
        { index--; ShowList(); Checking_the_apartment(); }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            apartments[index].Number = int.Parse(NumberTextBox.Text);
            apartments[index].Square = float.Parse(SquareTextBox.Text);
            apartments[index].CountRooms = int.Parse(CountRoomsTextBox.Text);
            apartments[index].Storey = int.Parse(StoreyTextBox.Text);
            apartments[index].Price = decimal.Parse(PriceTextBox.Text);
            apartments[index].Reservation = ReservationCheckBox.IsChecked.Value;
            Serializer();
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {

            apartments[index].SoldOut = true; Checking_the_apartment();

        }
        public void foo(bool TF)
        {

            NumberTextBox.IsEnabled = TF;
            SquareTextBox.IsEnabled = TF;
            CountRoomsTextBox.IsEnabled = TF;
            StoreyTextBox.IsEnabled = TF;
            PriceTextBox.IsEnabled = TF;
            ReservationCheckBox.IsEnabled = TF;

        }
        public void Checking_the_apartment()
        {
            if (apartments[index].SoldOut == true)
            {
                foo(false);
            }
            else
            {
                foo(true);
            }
        }

        public void User_verification(User u)
        {

            string s = u.GetType().ToString();
            MessageBox.Show(s);
        }
    }
}
