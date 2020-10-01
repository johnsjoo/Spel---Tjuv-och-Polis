using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Xml.XPath;
using System.Runtime.InteropServices;

namespace Spel___Tjuv_och_Polis
{
    class Program
    {
        static void Main(string[] args)
        {
            int robedPeople = 0;
            int thiefCaught = 0;

            string[,] board = DrawCity();
            //Lista som hämtar slumpmässiga positioner för varje enskild person i x och y-led.
            List<Person> personsInCity = MakePerson();
            while (true)
            {
                board = DrawCity();

                foreach (var p in personsInCity)
                {

                    if (p.GetType().Name == "Thief")
                    {
                        board[p.Xposition, p.Yposition] += "T";


                    }
                    else if (p.GetType().Name == "Police")
                    {

                        board[p.Xposition, p.Yposition] += "P";
                    }
                    else if (p.GetType().Name == "Citizen")
                    {

                        board[p.Xposition, p.Yposition] += "M";
                    }

                }

                foreach (var p in personsInCity)
                {
                    if (board[p.Xposition, p.Yposition].Contains("T") && board[p.Xposition, p.Yposition].Contains("M"))
                    {
                        
                        board[p.Xposition, p.Yposition] = "X";
                        Console.WriteLine("Tjuv rånar medborgare!");
                        robedPeople++;


                    }
                    else if (board[p.Xposition, p.Yposition].Contains("T") && board[p.Xposition, p.Yposition].Contains("P"))
                    {
                        board[p.Xposition, p.Yposition] = "X";
                        Console.WriteLine("Tjuv möter polis!");
                        thiefCaught++;

                    }
                    else if (board[p.Xposition, p.Yposition].Contains("M") && board[p.Xposition, p.Yposition].Contains("P"))
                    {
                        board[p.Xposition, p.Yposition] = "X";
                        Console.WriteLine("Medborgare möter polis, inget händer");
                    }

                }
  
                PrintPeople(board);
                //Listan personInCity med alla våra personer är lika med en funktion som uppdaterar personernas kordinater.
                personsInCity = UpdatePosition(personsInCity);
                Counter(robedPeople, thiefCaught);
                Thread.Sleep(2000);
                Console.Clear();

            }
        }
        //Räknar rånade invånare och fångade tjuvar.
        private static void Counter(int robedPeople, int thiefCaught)
        {
            Console.WriteLine("*******************");
            Console.WriteLine($"Fångade tjuvar: {thiefCaught}");
            Console.WriteLine($"Rånade medborgare: {robedPeople}");
        }

        private static List<Person> UpdatePosition(List<Person> personsInCity)
        {
            Random rnd = new Random();
            foreach (var p in personsInCity)
            {
                if (p.Xposition >= 99)
                {
                    p.Xposition -= 1;

                }
                else if (p.Xposition <= 0)
                {
                    p.Xposition += 1;
                }
                else
                {
                    p.Xposition += Move(p.Xposition, rnd);
                }
                if (p.Yposition <= 0)
                {
                    p.Yposition += 1;
                }
                else if (p.Yposition >= 24)
                {
                    p.Yposition -= 1;
                }
                else
                {
                    p.Yposition += Move(p.Yposition, rnd);
                }

            }
            return personsInCity;

        }

        public static int Move(int number, Random rnd)
        {

            number = rnd.Next(-1, 2);
            return number;

        }
        //Skriver ut våra bokstäver på spelplanen (P,M,T)
        private static void PrintPeople(string[,] board)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 100; j++)
                {

                    Console.Write(board[j, i]);
                }
                Console.WriteLine();
            }
        }

        private static string[,] DrawCity()
        {
            string[,] board = new string[100, 25];

            //Sätt alla platser på spelplanen till 1 space
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    board[j, i] = " ";

                }

            }

            return board;
        }

        public static List<Person> MakePerson()
        {
            Random rnd = new Random();
            var city = new List<Person>();
            for (int i = 0; i < 10; i++)
            {

                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person t = new Thief(x, y);
                city.Add(t);

            }
            for (int i = 0; i < 20; i++)
            {

                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person m = new Citizen(x, y);
                city.Add(m);
            }
            for (int i = 0; i < 30; i++)
            {

                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person p = new Police(x, y);
                city.Add(p);
            }
            return city;
        }
        public static void StealRandomItem(List<Item> citzenInventory, Random rnd)
        {

            int index = rnd.Next(0,citzenInventory.Count);
            citzenInventory.RemoveAt(index);

        }
     
      


    }
    
    class Person 
    {
        

        public Person(int xPosition, int yPosition) 
        {
            Xposition = xPosition;
            Yposition = yPosition;

        }
        
        
        
        public int Xposition { get; set; }
        public int Yposition { get; set; }
        public string Inventory { get; set; }

    }
    class Thief : Person
    {
        public Thief (int xPosition, int yPosition):base (xPosition, yPosition)
        { 

            List<string> thiefInventory = new List<string>();

        }

    }
    class Police : Person 
    {
        public Police (int xPosition, int yPosition) : base(xPosition, yPosition)
        {
            List<string> policeInventory = new List<string>();


        }

    }
    class Citizen : Person 
    {
        
        public Citizen(int xPosition, int yPosition) : base(xPosition, yPosition)
        {
            List<Item> citzenInventory = new List<Item>();

            citzenInventory.Add(new Item("Nycklar"));
            citzenInventory.Add(new Item("Mobiltelefon"));
            citzenInventory.Add(new Item("Pengar"));
            citzenInventory.Add(new Item("Klocka"));

        }

    }
    class Item
    {
        public Item(string citicenzItems)
        {
            CiticenzItems = citicenzItems;

        }
        public string StolenItems { get; set; }
        public string PoliceItems { get; set; }
        public string CiticenzItems { get; set; }

    }
}
