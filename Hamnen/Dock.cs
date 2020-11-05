using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    static class Dock
    {
        public static List<Boat> BoatList { get; set; }

        public static double DockPlaces { get { return 64;} }

        public static int AmountOfRowBoats { get; set; }
        public static int AmountOfSailBoats { get; set; }
        public static int AmountOfMotorBoats { get; set; }
        public static int AmountOfCargoShips { get; set; }

        public static int TotalWeightInPort { get; set; }
        public static double AverageOfMaxSpeed { get; set; }

        public static double AmountOfFreePlaces { get; set; }
        

        public static int AmountOfRejectedBoats { get; set; }
        






    }
}
