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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(ObservableCollection<Apartment> apartments)
        {
            InitializeComponent();

            int room_1 =0, room_2 = 0, room_3 = 0, room_4_and_more = 0;
            decimal p_500K = 0, p500K_2KK = 0, p2KK_4KK = 0, p4KK = 0;
            decimal sum = 0;

            for(int i=0;i<apartments.Count;i++)
            {
                if (apartments[i].SoldOut == true)
                {
                    if (apartments[i].CountRooms == 1)
                        room_1++;
                    if (apartments[i].CountRooms == 2)
                        room_2++;
                    if (apartments[i].CountRooms == 3)
                        room_3++;
                    if (apartments[i].CountRooms >= 4)
                        room_4_and_more++;

                    if (apartments[i].Price < 500000)
                        p_500K++;
                    if (apartments[i].Price > 500000 && apartments[i].Price < 2000000)
                        p500K_2KK++;
                    if (apartments[i].Price > 2000000 && apartments[i].Price < 4000000)
                        p2KK_4KK++;
                    if (apartments[i].Price > 4000000)
                        p4KK++;

                    sum += apartments[i].Price;
                }
            }

            TextBlockRoom1.Text = room_1.ToString();
            TextBlockRoom2.Text = room_2.ToString();
            TextBlockRoom3.Text = room_3.ToString();
            TextBlockRoom4.Text = room_4_and_more.ToString();

            TextBlockP500.Text = p_500K.ToString();
            TextBlockP500_2.Text = p500K_2KK.ToString();
            TextBlockP2_4.Text = p2KK_4KK.ToString();
            TextBlockP4.Text = p4KK.ToString();

            TotalSum.Text = sum.ToString();

        }
    }
}
