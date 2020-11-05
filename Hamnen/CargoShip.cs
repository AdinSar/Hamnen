using System;
using System.Text;

namespace Hamnen
{
    class CargoShip : Boat
    {
        

        public static CargoShip CreateCargoShip()
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
            CargoShip cargoShip = new CargoShip();
            cargoShip.IDNumber = "L-" + stringBuilder.ToString();
            cargoShip.MaxSpeed = r.Next(1, 20 + 1);
            cargoShip.Weight = r.Next(3000, 20000 + 1);
            cargoShip.AmountOfDockPlace = 4;
            cargoShip.UniqueProperty ="Containers: "+r.Next(0, 500 + 1);
            cargoShip.DaysInDock = 6;
            cargoShip.BoatType = "Lastfartyg";
            cargoShip.InDock = true;


            return cargoShip;
        }
    }
}
