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
            //Skapar en array med listor i.
            List<Person>[,] board = new List<Person>[100, 25];

            List<Person> personsInCity = MakePerson();
            while (true)
            {
                ListsInArray(board, personsInCity);
                DrawCity(board);
                personsInCity = UpdatePosition(personsInCity);
                Thread.Sleep(2000);
                Console.Clear();

            }
        }

        private static void ListsInArray(List<Person>[,] board, List<Person> personsInCity)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    board[i, j] = new List<Person>();
                }

            }

            foreach (var p in personsInCity)
            {

                board[p.Xposition, p.Yposition].Add(p);

            }
        }
        //Uppdatering av slumpmässig position samt kollar så ingen person hamnar utanför staden
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
        //Genererar och returnerar en slumpmässig kordinat.
        public static int Move(int number, Random rnd)
        {

            number = rnd.Next(-1, 2);
            return number;

        }

        private static void DrawCity(List<Person>[,] board)
        {
            

            string messege = "";
            
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 100; j++)

                {
                    List<Person> currentList = board[j, i];
                    if (!currentList.Any())
                    {
                        Console.Write(" ");
                    }
                    else if (currentList.Count == 1)
                    {
                        Person currentPerson = currentList.First();
                        if (currentPerson.GetType().Name == "Thief")
                        {
                            Console.Write("T");
                        }
                        else if (currentPerson.GetType().Name == "Citizen")
                        {
                            Console.Write("M");
                        }
                        else if (currentPerson.GetType().Name == "Police")
                        {
                            Console.Write("P");
                        }
                    }
                    
                    else
                    {
                        Console.Write("X");
                        if (currentList.OfType<Thief>().Any() && currentList.OfType<Citizen>().Any())
                        {
                            Item stolenItem=StealRandomItem(currentList);
                            messege +=("Tjuv rånar medborgare på " + stolenItem.CiticenzItems + "\n");
                            
                        }
                        else if (currentList.OfType<Thief>().Any() && currentList.OfType<Police>().Any())
                        {
                            ClearThiefInventory(currentList);
                            messege+=("Tjuv möter polis!, Allt stöldgods beslagtogs \n");
                            
                        }
                        
                    }
                }
                Console.WriteLine();
                
            }
            Console.WriteLine(messege);
            
        }
        

        public static List<Person> MakePerson()
        {
            Random rnd = new Random();
            var city = new List<Person>();
            for (int i = 0; i < 10; i++)
            {

                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person t = new Thief(x, y, new List<Item>());
                city.Add(t);

            }
            for (int i = 0; i < 20; i++)
            {

                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person m = new Citizen(x, y, new List<Item>());
                city.Add(m);
            }
            for (int i = 0; i < 30; i++)
            {

                int x = rnd.Next(1, 100);
                int y = rnd.Next(1, 25);
                Person p = new Police(x, y, new List<Item>());
                city.Add(p);
            }
            return city;
        }
        //Stjäl ett slumpmässigt Item-Objekt och lägger det i tjuvent inventory.
        public static Item StealRandomItem(List<Person> currentList)
        {
            List<Item> thiefInventory = new List<Item>();
            List<Item> citzenInventory = new List<Item>();
            foreach (var p in currentList)
            {
                if (p.GetType().Name == "Thief")
                {
                    thiefInventory = p.Inventory;
                }
                else if (p.GetType().Name == "Citizen")
                {
                    citzenInventory = p.Inventory;
    
                    
                }
            }
            Random rnd = new Random();
            int index = rnd.Next(0, citzenInventory.Count);
 
            thiefInventory.Add(citzenInventory[index]);
            citzenInventory.RemoveAt(index);
            return citzenInventory[index];
        }
        public static void ClearThiefInventory(List<Person> currentList)
        {
            
            List<Item> thiefInventory = new List<Item>();

            foreach (var p in currentList)
            {
               if (p.GetType().Name == "Thief")
               {
                    thiefInventory = p.Inventory;
               }
            }
            thiefInventory.Clear();


        }
        
        
    }
    
    class Person 
    {
        

        public Person(int xPosition, int yPosition, List<Item> inventory) 
        {
            Xposition = xPosition;
            Yposition = yPosition;
            Inventory = inventory;
        }
        
        
        
        public int Xposition { get; set; }
        public int Yposition { get; set; }
        public List<Item> Inventory { get; set; }

    }
    class Thief : Person
    {


        public Thief (int xPosition, int yPosition, List<Item> thiefInventory) :base (xPosition, yPosition, thiefInventory)
        { 

            

        }

    }
    class Police : Person 
    {
        public Police (int xPosition, int yPosition, List<Item> policeInventory) : base(xPosition, yPosition, policeInventory)
        {


        }

    }
    class Citizen : Person 
    {
        
        public Citizen(int xPosition, int yPosition, List<Item> citzenInventory) : base(xPosition, yPosition, citzenInventory)
        {
           

            citzenInventory.Add(new Item("nycklar"));
            citzenInventory.Add(new Item("mobiltelefon"));
            citzenInventory.Add(new Item("pengar"));
            citzenInventory.Add(new Item("klocka"));

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
