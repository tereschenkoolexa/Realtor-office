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
using _Class;

namespace WpfApp1
{

    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(ObservableCollection<Apartment> apartments)
        {
            InitializeComponent();

            int one_room =0, two_room = 0, three_rooms = 0, four_rooms_and_more = 0;
            decimal up_to_500k = 0, from_500k_to_2kk = 0, from_2kk_to_4kk = 0, more_than_4kk = 0;
            decimal sum = 0;

            for(int i=0;i<apartments.Count;i++)
            {
                if (apartments[i].SoldOut == true)
                {
                    if (apartments[i].CountRooms == 1)
                        one_room++;
                    if (apartments[i].CountRooms == 2)
                        two_room++;
                    if (apartments[i].CountRooms == 3)
                        three_rooms++;
                    if (apartments[i].CountRooms >= 4)
                        four_rooms_and_more++;

                    if (apartments[i].Price < 500000)
                        up_to_500k++;
                    if (apartments[i].Price > 500000 && apartments[i].Price < 2000000)
                        from_500k_to_2kk++;
                    if (apartments[i].Price > 2000000 && apartments[i].Price < 4000000)
                        from_2kk_to_4kk++;
                    if (apartments[i].Price > 4000000)
                        more_than_4kk++;

                    sum += apartments[i].Price;
                }
            }

            TextBlockRoom1.Text = one_room.ToString();
            TextBlockRoom2.Text = two_room.ToString();
            TextBlockRoom3.Text = three_rooms.ToString();
            TextBlockRoom4.Text = four_rooms_and_more.ToString();

            TextBlockP500.Text = up_to_500k.ToString();
            TextBlockP500_2.Text = from_500k_to_2kk.ToString();
            TextBlockP2_4.Text = from_2kk_to_4kk.ToString();
            TextBlockP4.Text = more_than_4kk.ToString();

            TotalSum.Text = sum.ToString();

        }
    }
}
