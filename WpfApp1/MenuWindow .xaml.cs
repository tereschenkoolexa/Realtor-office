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
using _Class;

namespace WpfApp1
{
    

    public partial class MenuWindow : Window
    {
        int index = 0;

        User Subsidiary_user = new User();

        ObservableCollection<Apartment> apartments = new ObservableCollection<Apartment>();
        ObservableCollection<Apartment> Rezerv_apartments = new ObservableCollection<Apartment>();

        public MenuWindow(User user)
        {
            Subsidiary_user = user;
            
            InitializeComponent();

            if (File.Exists("Apartment.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Apartment>));

                using (Stream reader = new FileStream("Apartment.xml", FileMode.Open))
                {
                    apartments = (ObservableCollection<Apartment>)serializer.Deserialize(reader);
                }

                Checking_the_apartment();
                User_verification(user);

                ListApartaments.ItemsSource = apartments;
                ShowList();

                for (int i = 0; i < apartments.Count; i++)
                {
                    Rezerv_apartments.Add(apartments[i]);
                }
            }
            
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
            if (Subsidiary_user.GetType().ToString() == "_Class.Shopper")
            {
                if (apartments[index].Code == Subsidiary_user.Code
                    && ReservationCheckBox.IsChecked == true
                    && apartments[index].SoldOut == true)
                {
                    Sell.IsEnabled = false;
                }
                else
                {
                    if (apartments[index].Code != Subsidiary_user.Code
                        && ReservationCheckBox.IsChecked == false)
                    {
                        ReservationCheckBox.IsEnabled = true;
                        Sell.IsEnabled = false;
                    }
                    if (apartments[index].Code == Subsidiary_user.Code 
                        && ReservationCheckBox.IsChecked == true)
                    {
                        ReservationCheckBox.IsEnabled = true;
                        Sell.IsEnabled = true;
                    }
                    if (apartments[index].Code != Subsidiary_user.Code 
                        && ReservationCheckBox.IsChecked == true)
                    {
                        ReservationCheckBox.IsEnabled = false;
                        Sell.IsEnabled = false;
                    }
                }
            }
            if (Subsidiary_user.GetType().ToString() == "_Class.Realtor")
            {
                if (apartments[index].SoldOut == true)
                {
                    ReservationCheckBox.IsEnabled = false;
                    Sell.IsEnabled = false;
                }
                else
                {
                    ReservationCheckBox.IsEnabled = true;
                    Sell.IsEnabled = true;
                }
            }
        }

        int i = 0;
        private void CheckedCheckBox(object sender, RoutedEventArgs e)
        {

            if (i < 1)
                i++;
            else
                apartments[index].Code = Subsidiary_user.Code; Checking_the_apartment(); Save_Click(sender, e);
            //MessageBox.Show("llll");
            
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
                SoldOut = false,
                Code = "0"
            });

            Serializer();

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            index++;
            ShowList();
            Checking_the_apartment();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            index--;
            ShowList();
            Checking_the_apartment();
        }

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

                if (Subsidiary_user.GetType().ToString() == "_Class.Realtor")
                {
                    apartments[index].SoldOut = true;
                    Checking_the_apartment();
                    Save_Click(sender, e);
                }
                else
                {
                    if (apartments[index].Code == Subsidiary_user.Code && ReservationCheckBox.IsChecked == true)
                    {
                        apartments[index].SoldOut = true;
                        Checking_the_apartment();
                        Save_Click(sender, e);
                        Sell.IsEnabled = false;
                    }

                    MessageBox.Show("You bought an apartment "+apartments[index].Number.ToString());
                }
        }  
        
        public void Change_bool(bool _bool)
        {
            NumberTextBox.IsEnabled = _bool;
            SquareTextBox.IsEnabled = _bool;
            CountRoomsTextBox.IsEnabled = _bool;
            StoreyTextBox.IsEnabled = _bool;
            PriceTextBox.IsEnabled = _bool;
        }

        public void Checking_the_apartment()
        {
            if (Subsidiary_user.GetType().ToString() == "_Class.Realtor" ||
                Subsidiary_user.GetType().ToString() == "_Class.Shopper")
            {
                Change_bool(false);
            }
            if (Subsidiary_user.GetType().ToString() == "_Class.Admin"
                && apartments[index].SoldOut != true)
            {
                Change_bool(true);
            }
            if (Subsidiary_user.GetType().ToString() == "_Class.Admin"
                && apartments[index].SoldOut == true || apartments[index].Reservation == true)
            {
                Change_bool(false);
            }
        }

        public void User_verification(User u)
        {


            if(u.GetType().ToString()=="_Class.Admin")
            {
                Save.IsEnabled = true;
                Add.IsEnabled = true;
                Sell.IsEnabled = false;
                Statistics.IsEnabled = true;
                ReservationCheckBox.IsEnabled = false;
            }
            if (u.GetType().ToString() == "_Class.Realtor")
            {
                Save.IsEnabled = false;
                Add.IsEnabled = false;
                Sell.IsEnabled = true;
                Statistics.IsEnabled = true;
                Sell.Content = "Sell";
            }
            if (u.GetType().ToString() == "_Class.Shopper")
            {
                Save.IsEnabled = false;
                Add.IsEnabled = false;
                Sell.IsEnabled = true;
                Statistics.IsEnabled = false;
                Sell.Content = "Buy";
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Rezerv_apartments.Count; i++)
            {
                apartments.Add(Rezerv_apartments[i]);
            }
            ListApartaments.ItemsSource = apartments;
            ObservableCollection<Apartment> apartmentsList = new ObservableCollection<Apartment>();
            for (int i = 0; i < apartments.Count; i++)
            {
               
                Apartment apartment_Find = new Apartment();
                if (ReservedRadioButton.IsChecked == true &&
                    apartments[i].Reservation == true &&
                    apartments[i].SoldOut == false)
                {
                    apartment_Find = apartments[i];
                    apartmentsList.Add(apartment_Find);
                }
                if (NotBookedRadioButton.IsChecked == true &&
                        apartments[i].Reservation == false &&
                        apartments[i].SoldOut == false)
                {
                    apartment_Find = apartments[i];
                    apartmentsList.Add(apartments[i]);
                }
                if (BoughtRadioButton.IsChecked == true &&
                        apartments[i].SoldOut == true)
                {
                    apartment_Find = apartments[i];
                    apartmentsList.Add(apartments[i]);
                }
            }
            apartments.Clear();
            apartments = apartmentsList;
            if (AllRadioButton.IsChecked == true)
            {
                for (int i = 0; i < Rezerv_apartments.Count; i++)
                {
                    apartments.Add(Rezerv_apartments[i]);
                }
                ListApartaments.ItemsSource = apartments;
            }
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
            Checking_the_apartment();
            ShowList();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(apartments);
            statisticsWindow.Show();
        }
    }
}
