using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Hamnen
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            List<Boat> boatList = new List<Boat>();

            FillList(boatList);

            if (!FileIsEmpty())
            {
                LoadListFromFile(boatList);
            }


            while (true)
            {

                GetNewInfo(boatList);
                ShowDockList(boatList);
                GoToNextDay(boatList);
                BoatsDepart(boatList);
                NewBoatsToDock(boatList);
                SaveListToFile(boatList);
            }



        }
        private static bool FileIsEmpty()
        {
            var info = new FileInfo("HamnenBåtLista.txt");
            if (info.Length <6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void FillList(List<Boat> boatList)
        {
            for (int i = 1; i <= 64; i++)
            {
                Boat b = new Boat();
                b.Place = i;
                b.BoatType = "Tomt";
                b.InDock = false;
                b.UniqueProperty = "-";
                b.IDNumber = "-";
                boatList.Add(b);
            }
        }

        private static void GetNewInfo(List<Boat> boatList)
        {
            Dock.AmountOfFreePlaces = GetAmountOfFreePlaces(boatList);
            Dock.AverageOfMaxSpeed = GetAverageMaxSpeed(boatList);
            Dock.TotalWeightInPort =GetTotalWeightInPort(boatList);
            Dock.AmountOfCargoShips = GetAmountOfCargoShips(boatList);
            Dock.AmountOfMotorBoats = GetAmountOfMotorBoats(boatList);
            Dock.AmountOfRowBoats = GetAmountOfRowBoats(boatList);
            Dock.AmountOfSailBoats = GetAmountOfSailBoats(boatList);
        }

        private static int GetAmountOfSailBoats(List<Boat> boatList)
        {
            int count = 0;
            for (int i = 0; i < boatList.Count; i++)
            {
                if (boatList[i].BoatType == "Segelbåt")
                {
                    count++;
                }

            }
            if (count is 0)
            {
                return 0;
            }
            return count / 2;
        }

        private static int GetAmountOfRowBoats(List<Boat> boatList)
        {
            int count = 0;
            for (int i = 0; i < boatList.Count; i++)
            {
                if (boatList[i].BoatType == "Roddbåt")
                {
                    count++;
                }


            }
            return count;
        }

        private static int GetAmountOfMotorBoats(List<Boat> boatList)
        {
            int count = 0;
            for (int i = 0; i < boatList.Count; i++)
            {
                if (boatList[i].BoatType == "Motorbåt")
                {
                    count++;
                }


            }
            return count;
        }

        private static int GetAmountOfCargoShips(List<Boat> boatList)
        {
            int count = 0;
            for (int i = 0; i < boatList.Count; i++)
            {
                if (boatList[i].BoatType== "Lastfartyg")
                {
                    count++;
                }

            }
            if (count is 0)
            {
                return 0;
            }
            return count/4;
        }

        private static int GetTotalWeightInPort(List<Boat> boatList)
        {
            
            int totalweight = 0;
            for (int i = 0; i < boatList.Count; i++)
            {
                
                if (boatList[i].InDock)
                {
                    totalweight += boatList[i].Weight;
                   
                }
                
            }
            
            return totalweight;
        }

        private static double GetAverageMaxSpeed(List<Boat> boatList)
        {
            int count = 0;
            double speedcombined = 0;
            for (int i = 0; i < boatList.Count; i++)
            {
               
                if (boatList[i].InDock)
                {
                    count++;
                    speedcombined += boatList[i].MaxSpeed;
                }
                

            }
            
            
            double result = speedcombined / count;
            result = result * 1.852;
            result = Math.Round(result, 2);
            return result;
        }

       

        private static void LoadListFromFile(List<Boat> boatList)
        {
            string text2 = File.ReadAllText("HamnenBåtLista.txt");

            int a = 0;
            int count = 0;

            string[] dataLines = text2.Split('\n');

            foreach (string data in dataLines)
            {
                
                string[] keyValue = data.Split('+');
                if (keyValue.Length > 7)
                {


                    boatList[a].Place = Convert.ToDouble(keyValue[0]);
                    boatList[a].IDNumber = keyValue[1];
                    boatList[a].BoatType = keyValue[2];
                    boatList[a].Weight = Convert.ToInt32(keyValue[3]);
                    boatList[a].MaxSpeed = Convert.ToInt32(keyValue[4]);
                    boatList[a].DaysInDock = Convert.ToInt32(keyValue[5]);
                    boatList[a].AmountOfDockPlace = Convert.ToDouble(keyValue[6]);
                    boatList[a].InDock = Convert.ToBoolean(keyValue[7]);
                    boatList[a].UniqueProperty = keyValue[8];
                    a++;
                    count++;
                    if (count > 63)
                    {
                        Boat b = new Boat();

                        b.BoatType = "Tomt";
                        b.InDock = false;
                        b.UniqueProperty = "-";
                        b.IDNumber = "-";
                        boatList.Add(b);

                    }
                }
            }
            List<int> k = new List<int>();
            for (int i = 0; i < boatList.Count; i++)
            {
                if (boatList[i].Place == 666)
                {
                    k.Add(i);
                }
            }
            if (k.Count>0)
            {
                for (int i = 0; i < k.Count; i++)
                {
                    boatList.RemoveAt(i);
                }
            }

        }

        private static void SaveListToFile(List<Boat> boatList)
        {
            File.WriteAllText("HamnenBåtLista.txt", String.Empty);

            using (StreamWriter sw = new StreamWriter("HamnenBåtLista.txt", true))
            {
                
                foreach (Boat r in boatList)
                {
                    sw.WriteLine(r.Place+"+"+r.IDNumber+"+"+r.BoatType+"+"+r.Weight + "+" + r.MaxSpeed + "+" + r.DaysInDock + "+" + r.AmountOfDockPlace + "+" + r.InDock + "+" + r.UniqueProperty);
                    
                }
                sw.Close();
            }
        }

        private static void GoToNextDay(List<Boat> boatList)
        {
            Console.ReadKey();
        }

        private static void BoatsDepart(List<Boat> boatList)
        {
          
            for (int i = 0; i < boatList.Count; i++)
            {
                bool remove = false;
                boatList[i].DaysInDock--;

                if (boatList[i].DaysInDock ==0 && boatList[i].BoatType == "Roddbåt")
                {
                    if (boatList[i].Place%1 ==0.5)
                    {
                        remove = true;
                        
                        
                    }
                    else
                    {
                        SetToEmpty(boatList, i);
                        
                    }
                    
                }
                else if (boatList[i].DaysInDock == 0 && boatList[i].BoatType == "Lastfartyg")
                {
                    for (int j = 0; j < boatList.Count; j++)
                    {
                        if (boatList[j].IDNumber == boatList[i].IDNumber && boatList[i].Place == boatList[j].Place)
                        {
                            SetToEmpty(boatList, j);
                        }
                    }
                    SetToEmpty(boatList, i);
                    
                }
                else if (boatList[i].DaysInDock == 0 && boatList[i].BoatType == "Motorbåt")
                {
                    SetToEmpty(boatList, i);
                    
                }
                else if (boatList[i].DaysInDock == 0 && boatList[i].BoatType == "Segelbåt")
                {
                    for (int j = 0; j < boatList.Count; j++)
                    {
                        if (boatList[j].IDNumber == boatList[i].IDNumber && boatList[i].Place == boatList[j].Place)
                        {
                            SetToEmpty(boatList, j);
                        }
                    }
                    SetToEmpty(boatList, i);
                    
                }
                if (boatList[i].DaysInDock<0)
                {
                    boatList[i].DaysInDock = 0;
                }
                if (remove)
                {
                    boatList.RemoveAt(i);
                    remove = false;
                    
                }
            }
            
        }

        private static void SetToEmpty(List<Boat> boatList, int i)
        {
            boatList[i].AmountOfDockPlace = 0;
            boatList[i].BoatType = "Tomt";
            boatList[i].IDNumber = "-";
            boatList[i].InDock = false;
            boatList[i].MaxSpeed = 0;
            boatList[i].UniqueProperty = "-";
            boatList[i].Weight = 0;
        }

        private static void ShowDockList(List<Boat> boatList)
        {
            Console.Clear();
            int count = 0;
            var q = boatList
                .OrderBy(p => p.Place);

            Console.WriteLine($"Plats\t\tBåttyp\t\tNr\t\tVikt\t\tMaxhast\t\tÖvrigt");
            
            foreach (var item in q)
            {
                double place;
                string boatType;
                string id = "";
                int weight = 0;
                string uniquep = "";
                double MaxSpeed;
                int e = 0;
                string f = "";
                string k = "";
                
                if (item.BoatType == "Tomt"|| item.BoatType == "Roddbåt")
                {
                    k = "\t";
                }


                place = item.Place;
                boatType = item.BoatType;
                id = item.IDNumber;
                 weight = item.Weight;
                 uniquep = item.UniqueProperty;
                 MaxSpeed = Math.Round(item.MaxSpeed * 1.852);
                if (count==0)
                {
                    foreach (var item2 in q)
                    {
                        if (id == item2.IDNumber && weight == item2.Weight && uniquep == item2.UniqueProperty && item.Place != item2.Place && boatType != "Tomt")
                        {
                            e++;
                            count++;
                        }

                    }
                }
                
                
                f = "-" + (place+e).ToString();
                string pp = place.ToString() + f;
                if (count>0 && e>0)
                {
                    Console.WriteLine($"{pp}\t\t{boatType}{k}\t{id}\t\t{weight}Kg\t\t{MaxSpeed}Km/h\t\t{uniquep}");
                }
                else if (count==0)
                {
                    Console.WriteLine($"{place}\t\t{boatType}{k}\t{id}\t\t{weight}Kg\t\t{MaxSpeed}Km/h\t\t{uniquep}");
                }
                else if (count>0)
                {
                    count--;
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Antal Roddbåtar: {Dock.AmountOfRowBoats}.\tAntal Motorbåtar: {Dock.AmountOfMotorBoats}.\tAntal Segelbåtar: {Dock.AmountOfSailBoats}.\tAntal Lastfartyg: {Dock.AmountOfCargoShips}.");
            Console.WriteLine();
            Console.WriteLine($"Total vikt i Hamn: {Dock.TotalWeightInPort}Kg.\tMedelhastighet av alla båtar: {Dock.AverageOfMaxSpeed}Km/h.");
            Console.WriteLine($"Antal lediga platser: { Dock.AmountOfFreePlaces}.\tAntal avisade båtar: { Dock.AmountOfRejectedBoats}.");
        }


        private static void NewBoatsToDock(List<Boat> boatlist)
        {
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                BoatType boatType = (BoatType)r.Next(1, 4 + 1);

                if (boatType == (BoatType)1)
                {
                    if (GetAmountOfFreePlaces(boatlist) > 0)
                    {
                        AddRowBoatToList(boatlist);
                       
                        
                    }
                    else
                    {
                        Dock.AmountOfRejectedBoats++;
                    }
                }
                if (boatType == (BoatType)2)
                {
                    if (GetAmountOfFreePlaces(boatlist) > 0.5)
                    {
                        AddMotorBoatToList(boatlist);


                    }
                    else
                    {
                        Dock.AmountOfRejectedBoats++;
                    }

                }
                if (boatType == (BoatType)3)
                {
                    if (GetAmountOfFreePlaces(boatlist) > 1.5)
                    {
                        if (IsThereTwoAdjecentSpaces(boatlist))
                        {
                            AddSailBoatToList(boatlist);

                        }
                        else
                        {
                            Dock.AmountOfRejectedBoats++;
                        }
                        


                    }
                    else
                    {
                        Dock.AmountOfRejectedBoats++;
                    }

                }
                if (boatType == (BoatType)4)
                {
                    if (GetAmountOfFreePlaces(boatlist) > 3.5)
                    {
                        if (IsThereFourAdjecentSpaces(boatlist))
                        {
                            AddCargoShiptToList(boatlist);
                        }
                        else
                        {
                            Dock.AmountOfRejectedBoats++;
                        }
                       


                    }
                    else
                    {
                        Dock.AmountOfRejectedBoats++;
                    }

                }
            }


        }

        private static void AddCargoShiptToList(List<Boat> boatlist)
        {
            for (int i = boatlist.Count-1; i >= 4; i--)
            {
                if (boatlist[i].InDock is false && boatlist[i - 1].InDock is false && boatlist[i - 2].InDock is false && boatlist[i - 3].InDock is false&&boatlist[i].Place%1 ==0 && boatlist[i-1].Place % 1 == 0 && boatlist[i-2].Place % 1 == 0 && boatlist[i-3].Place % 1 == 0)
                    
                {
                    CargoShip cargoShip = CargoShip.CreateCargoShip();
                    boatlist[i].BoatType = cargoShip.BoatType;
                    boatlist[i].IDNumber = cargoShip.IDNumber;
                    boatlist[i].Weight = cargoShip.Weight;
                    boatlist[i].MaxSpeed = cargoShip.MaxSpeed;
                    boatlist[i].DaysInDock = cargoShip.DaysInDock;
                    boatlist[i].AmountOfDockPlace = cargoShip.AmountOfDockPlace;
                    boatlist[i].InDock = cargoShip.InDock;
                    boatlist[i].UniqueProperty = cargoShip.UniqueProperty;
                    boatlist[i-1].BoatType = cargoShip.BoatType;
                    boatlist[i-1].IDNumber = cargoShip.IDNumber;
                    boatlist[i-1].Weight = cargoShip.Weight;
                    boatlist[i-1].MaxSpeed = cargoShip.MaxSpeed;
                    boatlist[i-1].DaysInDock = cargoShip.DaysInDock;
                    boatlist[i-1].AmountOfDockPlace = cargoShip.AmountOfDockPlace;
                    boatlist[i-1].InDock = cargoShip.InDock;
                    boatlist[i-1].UniqueProperty = cargoShip.UniqueProperty;
                    boatlist[i-2].BoatType = cargoShip.BoatType;
                    boatlist[i-2].IDNumber = cargoShip.IDNumber;
                    boatlist[i-2].Weight = cargoShip.Weight;
                    boatlist[i-2].MaxSpeed = cargoShip.MaxSpeed;
                    boatlist[i-2].DaysInDock = cargoShip.DaysInDock;
                    boatlist[i-2].AmountOfDockPlace = cargoShip.AmountOfDockPlace;
                    boatlist[i-2].InDock = cargoShip.InDock;
                    boatlist[i-2].UniqueProperty = cargoShip.UniqueProperty;
                    boatlist[i-3].BoatType = cargoShip.BoatType;
                    boatlist[i-3].IDNumber = cargoShip.IDNumber;
                    boatlist[i-3].Weight = cargoShip.Weight;
                    boatlist[i-3].MaxSpeed = cargoShip.MaxSpeed;
                    boatlist[i-3].DaysInDock = cargoShip.DaysInDock;
                    boatlist[i-3].AmountOfDockPlace = cargoShip.AmountOfDockPlace;
                    boatlist[i-3].InDock = cargoShip.InDock;
                    boatlist[i-3].UniqueProperty = cargoShip.UniqueProperty;

                    
                    return;
                }
            }
        }

        private static void AddSailBoatToList(List<Boat> boatlist)
        {
            for (int i = 24; i < boatlist.Count-1; i++)
            {
                
                if (boatlist[i].InDock is false &&boatlist[i+1].InDock is false&&boatlist[i].Place%1 ==0 && boatlist[i+1].Place % 1 == 0)
                {
                     SailBoat sailBoat = SailBoat.CreateSailBoat();

                    boatlist[i].BoatType = sailBoat.BoatType;
                    boatlist[i].IDNumber = sailBoat.IDNumber;
                    boatlist[i].Weight = sailBoat.Weight;
                    boatlist[i].MaxSpeed = sailBoat.MaxSpeed;
                    boatlist[i].DaysInDock = sailBoat.DaysInDock;
                    boatlist[i].AmountOfDockPlace = sailBoat.AmountOfDockPlace;
                    boatlist[i].InDock = sailBoat.InDock;
                    boatlist[i].UniqueProperty = sailBoat.UniqueProperty;
                    boatlist[i+1].BoatType = sailBoat.BoatType;
                    boatlist[i+1].IDNumber = sailBoat.IDNumber;
                    boatlist[i+1].Weight = sailBoat.Weight;
                    boatlist[i+1].MaxSpeed = sailBoat.MaxSpeed;
                    boatlist[i+1].DaysInDock = sailBoat.DaysInDock;
                    boatlist[i+1].AmountOfDockPlace = sailBoat.AmountOfDockPlace;
                    boatlist[i+1].InDock = sailBoat.InDock;
                    boatlist[i+1].UniqueProperty = sailBoat.UniqueProperty;
                   
                    return;
                }
            }
            for (int i = 24; i >= 1; i--)
            {
                if (boatlist[i].InDock is false && boatlist[i - 1].InDock is false && boatlist[i].Place % 1 == 0 && boatlist[i-1].Place % 1 == 0)
                {
                    SailBoat sailBoat = SailBoat.CreateSailBoat();
                    boatlist[i].BoatType = sailBoat.BoatType;
                    boatlist[i].IDNumber = sailBoat.IDNumber;
                    boatlist[i].Weight = sailBoat.Weight;
                    boatlist[i].MaxSpeed = sailBoat.MaxSpeed;
                    boatlist[i].DaysInDock = sailBoat.DaysInDock;
                    boatlist[i].AmountOfDockPlace = sailBoat.AmountOfDockPlace;
                    boatlist[i].InDock = sailBoat.InDock;
                    boatlist[i].UniqueProperty = sailBoat.UniqueProperty;
                    boatlist[i - 1].BoatType = sailBoat.BoatType;
                    boatlist[i - 1].IDNumber = sailBoat.IDNumber;
                    boatlist[i - 1].Weight = sailBoat.Weight;
                    boatlist[i - 1].MaxSpeed = sailBoat.MaxSpeed;
                    boatlist[i - 1].DaysInDock = sailBoat.DaysInDock;
                    boatlist[i - 1].AmountOfDockPlace = sailBoat.AmountOfDockPlace;
                    boatlist[i - 1].InDock = sailBoat.InDock;
                    boatlist[i - 1].UniqueProperty = sailBoat.UniqueProperty;
                   
                    return;
                }

            }
        }

        private static void AddMotorBoatToList(List<Boat> boatlist)
        {
            for (int i = 0; i < boatlist.Count; i++)
            {
                if (boatlist[i].InDock is false && boatlist[i].Place %1 ==0)
                {
                    MotorBoat motorBoat = MotorBoat.CreateMotorBoat();
                    boatlist[i].BoatType = motorBoat.BoatType;
                    boatlist[i].IDNumber = motorBoat.IDNumber;
                    boatlist[i].Weight = motorBoat.Weight;
                    boatlist[i].MaxSpeed = motorBoat.MaxSpeed;
                    boatlist[i].DaysInDock = motorBoat.DaysInDock;
                    boatlist[i].AmountOfDockPlace = motorBoat.AmountOfDockPlace;
                    boatlist[i].InDock = motorBoat.InDock;
                    boatlist[i].UniqueProperty = motorBoat.UniqueProperty;
                    
                    return;
                }
               
            }
        }

        private static void AddRowBoatToList(List<Boat> boatlist)
        {
            for (int i = 0; i < boatlist.Count; i++)
            {
                if (boatlist[i].Place % 1 == 0.5)
                {
                    RowBoat rowBoat = RowBoat.CreateRowBoat();
                    boatlist[i].BoatType = rowBoat.BoatType;
                    boatlist[i].IDNumber = rowBoat.IDNumber;
                    boatlist[i].Weight = rowBoat.Weight;
                    boatlist[i].MaxSpeed = rowBoat.MaxSpeed;
                    boatlist[i].DaysInDock = rowBoat.DaysInDock;
                    boatlist[i].AmountOfDockPlace = rowBoat.AmountOfDockPlace;
                    boatlist[i].InDock = rowBoat.InDock;
                    boatlist[i].UniqueProperty = rowBoat.UniqueProperty;
                    
                    return;

                }
            }
            for (int i = 0; i < boatlist.Count; i++)
            {
                
                if (boatlist[i].InDock is false)
                {
                    if (boatlist[i].Place % 1 == 0)
                    {
                        Boat b = new Boat();
                        b.Place = boatlist[i].Place+0.5;
                        b.BoatType = "Tomt";
                        b.InDock = false;
                        b.UniqueProperty = "-";
                        b.IDNumber = "-";
                        boatlist.Add(b);

                        RowBoat rowBoat = RowBoat.CreateRowBoat();
                        boatlist[i].BoatType = rowBoat.BoatType;
                        boatlist[i].IDNumber = rowBoat.IDNumber;
                        boatlist[i].Weight = rowBoat.Weight;
                        boatlist[i].MaxSpeed = rowBoat.MaxSpeed;
                        boatlist[i].DaysInDock = rowBoat.DaysInDock;
                        boatlist[i].AmountOfDockPlace = rowBoat.AmountOfDockPlace;
                        boatlist[i].InDock = rowBoat.InDock;
                        boatlist[i].UniqueProperty = rowBoat.UniqueProperty;
                       
                        return;

                    }
                   
                }
               
            }
            

        }

        enum BoatType
        {
            Rowboat =1,MotorBoat =2, Sailboat =3, Cargoship = 4
        }
       public static double GetAmountOfFreePlaces(List<Boat> boatlist)
        {
           double freespace = 0;
            foreach (var item in boatlist)
            {
                if (item.InDock is false)
                {


                    if (item.Place == (int)item.Place)
                    {
                        freespace++;
                    }
                    else freespace += 0.5;
                }
            }
            return freespace;
        }
        public static bool IsThereTwoAdjecentSpaces(List<Boat> a )
        {
            int count = 0;
            for (int i = 0; i < a.Count; i++)
            {
                
                if (a[i].InDock is false && a[i].Place % 1 == 0)
                {
                    if (count ==0)
                    {
                        count++;
                    }
                    if (count==1)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }
        public static bool IsThereFourAdjecentSpaces(List<Boat> a)
        {
            int count=0;
           
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].InDock is false && a[i].Place % 1 == 0)
                {
                    if (count == 0)
                    {
                        
                        count++;
                    }
                    if (count ==1)
                    {
                        
                        count++;
                    }
                    if (count ==2)
                    {
                        
                        count++;
                    }
                    if (count ==3)
                    {
                        return true;
                    }

                }
                else
                {
                    count=0;
                }
            }

            return false;
        }
    }
}
