using System;
using System.Text;

namespace Hamnen
{
    class MotorBoat : Boat
    {
        

        public static MotorBoat CreateMotorBoat()
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
            MotorBoat motorBoat = new MotorBoat();
            motorBoat.IDNumber = "M-" + stringBuilder.ToString();
            motorBoat.MaxSpeed = r.Next(1, 60 + 1);
            motorBoat.Weight = r.Next(200, 3000 + 1);
            motorBoat.AmountOfDockPlace = 1;
            motorBoat.UniqueProperty ="Hästkrafter: "+ r.Next(10, 1000 + 1)+"hk";
            motorBoat.DaysInDock = 3;
            motorBoat.BoatType = "Motorbåt";
            motorBoat.InDock = true;


            return motorBoat;
        }

    }
}
