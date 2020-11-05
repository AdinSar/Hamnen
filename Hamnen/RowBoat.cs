using System;
using System.Text;

namespace Hamnen
{
    class RowBoat : Boat
    {
        public int MaxPassangers { get; set; }


        public static RowBoat CreateRowBoat()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random r = new Random();
            char letter;
            int lenght = 3;
            for (int i = 0; i < lenght; i++)
            {
                double flt = r.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                stringBuilder.Append(letter);
            }
            RowBoat rowBoat = new RowBoat();
            rowBoat.IDNumber = "R-" + stringBuilder.ToString();
            rowBoat.MaxSpeed = r.Next(1, 3 + 1);
            rowBoat.Weight = r.Next(100, 300 + 1);
            rowBoat.AmountOfDockPlace = 0.5;
            rowBoat.UniqueProperty = "Max Antal pers: "+ r.Next(1, 6 + 1);
            rowBoat.DaysInDock = 1;
            rowBoat.BoatType = "Roddbåt";
            rowBoat.InDock = true;


            return rowBoat;
        }
    }


}
