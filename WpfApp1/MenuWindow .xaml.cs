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
        User Suser = new User();
        ObservableCollection<Apartment> apartments = new ObservableCollection<Apartment>();
        public MenuWindow(User user)
        {
            Suser = user;
            
            InitializeComponent();

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Apartment>));
            using (Stream reader = new FileStream("Apartment.xml", FileMode.Open))
            {

                apartments = (ObservableCollection<Apartment>)serializer.Deserialize(reader);
            }
            User_verification(user);
            Checking_the_apartment();
            ShowList();

            ListApartaments.ItemsSource = apartments;
            
            //for(int i=0;i<apartments.Count;i++)
            //{
            //    ListApartaments.Items.Add(apartments[i]);
            //}

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
            if (Suser.GetType().ToString() == "WpfApp1.Shopper")
                for (int i = 0; i < Suser.Number.Count; i++)
            {
                if (apartments[index].Number != Suser.Number[i] && ReservationCheckBox.IsChecked==true)
                {
                    ReservationCheckBox.IsEnabled = false;
                }

            }

        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {


            apartments.Add(new Apartment
            {
                Number = int.Parse(NumberTextBox.Text),
                Square = float.Parse(SquareTextBox.Text),
                CountRooms = int.Parse(CountRoomsTextBox.Text),
                Storey = int.Parse(StoreyTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text),
                Reservation = false,
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
            ObservableCollection<Apartment> nullapartments = new ObservableCollection<Apartment>();
            //ListApartaments.Items.Clear();
            ListApartaments.ItemsSource = nullapartments;
            ListApartaments.ItemsSource = apartments;

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
            if (Suser.GetType().ToString() == "WpfApp1.Realtor")
            {
                apartments[index].SoldOut = true; Checking_the_apartment(); Save_Click(sender, e);
            }
            else
            {
                for (int i = 0; i < Suser.Number.Count; i++)
                {
                    if (apartments[index].Number == Suser.Number[i])
                    {
                        apartments[index].SoldOut = true; Checking_the_apartment(); Save_Click(sender, e);break;
                    }
                    
                }
                MessageBox.Show("lol");
            }

        }

        


        public void foo(bool TF)
        {

            NumberTextBox.IsEnabled = TF;
            SquareTextBox.IsEnabled = TF;
            CountRoomsTextBox.IsEnabled = TF;
            StoreyTextBox.IsEnabled = TF;
            PriceTextBox.IsEnabled = TF;

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


            if(u.GetType().ToString()=="WpfApp1.Admin")
            {
                Save.IsEnabled = true;
                Add.IsEnabled = true;
                Sell.IsEnabled = false;
                ReservationCheckBox.IsEnabled = false;
            }
            if (u.GetType().ToString() == "WpfApp1.Realtor")
            {
                Save.IsEnabled = false;
                Add.IsEnabled = false;
                Sell.IsEnabled = true;
                Sell.Content = "Sell";
            }
            if (u.GetType().ToString() == "WpfApp1.Shopper")
            {
                Save.IsEnabled = false;
                Add.IsEnabled = false;
                Sell.IsEnabled = true;
                Sell.Content = "Buy";
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {

            ObservableCollection<Apartment> apartmentsList = new ObservableCollection<Apartment>();
            for (int i = 0; i < apartments.Count; i++)
            {

                    if (ReservedRadioButton.IsChecked == true && 
                        apartments[i].Reservation == true &&
                        apartments[i].SoldOut == false)
                    {
                        apartmentsList.Add(apartments[i]);
                    }
                    if (NotBookedRadioButton.IsChecked == true &&
                        apartments[i].Reservation == false &&
                        apartments[i].SoldOut == false)
                    {
                        apartmentsList.Add(apartments[i]);
                    }
                    if (BoughtRadioButton.IsChecked == true &&
                        apartments[i].SoldOut == true)
                    {
                        apartmentsList.Add(apartments[i]);
                    }


            }
            if (AllRadioButton.IsChecked == true)
                ListApartaments.ItemsSource = apartments;
            else
                ListApartaments.ItemsSource = apartmentsList;
        }

        public void Serializer()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Apartment>));
            using (var fStream = new FileStream("Apartment.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xml.Serialize(fStream, apartments);
            }
        }

        public void ListApartaments_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

            index=ListApartaments.SelectedIndex;
            ShowList();

        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(apartments);
            statisticsWindow.Show();
        }

    }
}
