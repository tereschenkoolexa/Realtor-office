﻿using System;
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

    }
    public partial class MenuWindow : Window
    {
    int index = 0;
        ObservableCollection<Apartment> apartments = new ObservableCollection<Apartment>();
        public MenuWindow()
        {
            InitializeComponent();
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Apartment>));
            using (Stream reader = new FileStream("Apartment.xml", FileMode.Open))
            {

               apartments = (ObservableCollection<Apartment>)serializer.Deserialize(reader);
            }
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
        private void Add_Click(object sender, RoutedEventArgs e)
        {

            apartments.Add(new Apartment {
                Number = int.Parse(NumberTextBox.Text),
                Square = float.Parse(SquareTextBox.Text),
                CountRooms = int.Parse(CountRoomsTextBox.Text),
                Storey = int.Parse(StoreyTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text),
                Reservation = ReservationCheckBox.IsChecked.Value
            });


            XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Apartment>));
            using (var fStream = new FileStream("Apartment.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xml.Serialize(fStream, apartments);
            }

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        { index++;ShowList(); }

        private void Previous_Click(object sender, RoutedEventArgs e)
        { index--; ShowList(); }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            apartments[index].Number = int.Parse(NumberTextBox.Text);
            apartments[index].Square = float.Parse(SquareTextBox.Text);
            apartments[index].CountRooms = int.Parse(CountRoomsTextBox.Text);
            apartments[index].Storey = int.Parse(StoreyTextBox.Text);
            apartments[index].Price = decimal.Parse(PriceTextBox.Text);
            apartments[index].Reservation = ReservationCheckBox.IsChecked.Value;
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
