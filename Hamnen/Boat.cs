using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class Boat
    {
        public double Place { get; set; }
        public string IDNumber { get; set; }
        public string BoatType { get; set; }
        public int Weight { get; set; }
        public int MaxSpeed { get; set; }
        public int DaysInDock { get; set; }
        public double AmountOfDockPlace { get; set; }

        
        public bool InDock { get; set; }

        public string UniqueProperty { get; set; }


    }
}
