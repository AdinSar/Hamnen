using System;
using System.Text;

namespace Hamnen
{
    class SailBoat : Boat
    {
        public int BoatLenght { get; set; }

        public static SailBoat CreateSailBoat()
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
            SailBoat sailBoat = new SailBoat();
            sailBoat.IDNumber = "S-" + stringBuilder.ToString();
            sailBoat.MaxSpeed = r.Next(1, 12 + 1);
            sailBoat.Weight = r.Next(800, 6000 + 1);
            sailBoat.AmountOfDockPlace = 2;
            sailBoat.UniqueProperty = "BåtLängd: "+Math.Round(r.Next(10, 60 + 1)*0.3048,1)+"m";
            sailBoat.DaysInDock = 4;
            sailBoat.BoatType = "Segelbåt";
            sailBoat.InDock = true;


            return sailBoat;
        }
    }
}
