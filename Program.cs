using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;

namespace Spel___Tjuv_och_Polis
{
    class Program
    {
        static void Main(string[] args)
        {
            //När personen skapas så rör den sig slumpmässigt. 
            List<Person> AllPeople = new List<Person>();

            

            //Inventory
            List<string> items = new List<string>();
            items.Add("Nycklar");
            items.Add("Mobiltelefon");
            items.Add("Pengar");
            items.Add("Klocka");

            while (true)
            {
                
                Console.Write($"Antal rånade medborgare: ");
                Console.Write($"Antal gripna tjuvar: ");
                
                //Pausar programmet
                Thread.Sleep(2000);
            }

        }
    }
    class Person 
    {
        


        public int Xposition { get; set; }
        public int Yposition { get; set; }
        public int Xdirection { get; set; }
        public int Ydirection { get; set; }
        public string Inventory { get; set; }
    }
    class Thief : Person
    {
        

    }
    class Police : Person 
    {

    }
    class Citizen : Person 
    {


    }
    class Inventory
    {


    }
}
