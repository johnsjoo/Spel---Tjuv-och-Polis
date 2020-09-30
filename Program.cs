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
                        
                        board[p.Xposition, p.Yposition] = "T";
                    }
                    else if (p.GetType().Name == "Police")
                    {
                        //en if else, är den ledig så skriver vi ut "p" annars "X" 
                        board[p.Xposition, p.Yposition] = "P";
                    }
                    else if (p.GetType().Name == "Citizen")
                    {
                        //en if else, är den ledig så skriver vi ut "" annars "X" 
                        board[p.Xposition, p.Yposition] = "M";
                    }

                }



                //Skriver ut våra bokstäver på spelplanen (P,M,T)
                PrintPeople(board);
                personsInCity = UpdatePosition(personsInCity);

                Thread.Sleep(2000);
                Console.Clear();
            }
        }

        private static List<Person> UpdatePosition(List<Person> personsInCity)
        {
            Random rnd = new Random();
            foreach (var p in personsInCity)
            {
                if (p.Xposition>=99)
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
                if (p.Yposition<=0)
                {
                    p.Yposition += 1;
                }
                else if (p.Yposition >= 24)
                {
                    p.Yposition -= 1;
                }
                else
                {
                    p.Yposition += Move(p.Yposition,rnd);
                }

            }
            return personsInCity;

        }

        public static int Move(int number, Random rnd) 
        {
            
            number = rnd.Next(-1,2);
            return number;

        }

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
                Person t = new Thief(x,y);
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
        public int Xdirection { get; set; }
        public int Ydirection { get; set; }
        public string Inventory { get; set; }

    }
    class Thief : Person
    {
        public Thief (int xPosition, int yPosition):base (xPosition, yPosition)
        { 

            List<string> thiefinventory = new List<string>();

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
            List<string> citzeninventory = new List<string>();
            citzeninventory.Add("nycklar");
            citzeninventory.Add("mobiltelefon");
            citzeninventory.Add("pengar");
            citzeninventory.Add("klocka");

        }
        

    }
    class Item
    {

        public string StolenProperty { get; set; }
        public string SeizedItems { get; set; }
        public string CiticenzProperty { get; set; }

    }
}
