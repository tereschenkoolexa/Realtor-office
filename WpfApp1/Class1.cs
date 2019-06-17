using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Class
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
        public string Code { get; set; }

    }

    public class User
    {

        public string Login { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }

    public class Admin : User { }

    public class Realtor : User { }

    public class Shopper : User { }

}
