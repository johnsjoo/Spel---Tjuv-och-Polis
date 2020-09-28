using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using System.ComponentModel;

namespace Spel___Tjuv_och_Polis
{
    class Program
    {
        static void Main(string[] args)
        {
            string [,] board = new string[100,25];

            //Sätt alla platser på bordet till 1 space
            for (int i = 0; i <100; i++)
            { 
                for (int j = 0; j <25; j++)
                {
                    board[i, j] = " ";
                   // Console.Write(myArray[i,j]);
                }
                //Console.WriteLine();
            }

            //När personen skapas så rör den sig slumpmässigt. 
            List<Person> personsInCity = MakePerson();
            
            foreach (var p in personsInCity)
            {
                
                if(p.GetType().Name == "Thief")
                {
                        board[p.Xposition, p.Yposition] = "T";
                }
                else if (p.GetType().Name == "Police")
                {
                        board[p.Xposition, p.Yposition] = "P";
                }
                else if (p.GetType().Name == "Citizen")
                {
                        board[p.Xposition, p.Yposition] = "M";
                }


            }
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    //board[i, j] = " ";
                     Console.Write(board[i,j]);
                }
                Console.WriteLine();
            }
            /*
            while (true)
            {

                Console.Write($"Antal rånade medborgare: ");
                Console.Write($"Antal gripna tjuvar: ");
                //Pausar programmet
                Thread.Sleep(2000);
            }
            */

            /*if (//Police xPosition == Thief xPosition && Police yPosition == Thief yPosition)
            {
                //Thief Inventory == 0;
                
            }
            if (//Citizen xPosition == Thief xPosition && Citizen yPosition == Thief yPosition)
            {
                //Add Random Thief to inventory
            }*/
            Console.ReadKey(true);
        }

        private static List<Person> MakePerson()
        {
            Random rnd = new Random();
            var city = new List<Person>();
            for (int i = 0; i <= 10; i++)
            {
                
                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person t = new Thief(x,y);
                city.Add(t);

            }
            for (int i = 0; i <= 20; i++)
            {
                
                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person m = new Citizen(x,y);
                city.Add(m);
            }
            for (int i = 0; i <= 30; i++)
            {
                
                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person p = new Police(x,y);
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

            List<string> Thiefinventory = new List<string>();

        }

    }
    class Police : Person 
    {
        public Police (int xPosition, int yPosition) : base(xPosition, yPosition)
        {
            List<string> PoliceInventory = new List<string>();


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
