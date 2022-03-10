using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] inventories = new string[2, 15];
            Distribute(ref inventories);
            Console.ReadLine();
        }

        static string[] Generate(int count)
        {
            string[] cards = {
                "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B+2", "B~", "B^",
                "V0", "V1", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V+2", "V~", "V^",
                "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R+2", "R~", "R^",
                "J0", "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J+2", "J~", "J^",
                "*", "$",
            };
            string[] results = new string[count];
            for(int i=0; i<count; i++)
            {
                Random rd = new Random();
                int number = rd.Next(0, cards.Length);
                results[i] = cards[number];   
            }
            return results;
        }

        static void Distribute(ref string[,] inventories)
        {
            for(int i=0; i<inventories.GetLength(0); i++)
            {
                for (int j=0; j<8; j++)
                {
                    string[] results = Generate(1);
                    inventories[i, j] = results[0];
                }
            }
        }

        static string Display(string[,] inventories)
        {
            string results = "";
            for(int i=0; i<inventories.GetLength(1); i++)
            {
                if(!string.IsNullOrEmpty(inventories[0, i]))
                {
                    results = results + ", " + inventories[0, i];
                }
            }
            results = results.Substring(2);
            return results;
        }

        static void AddCard(int id, string card, ref string[,] inventories)
        {
            bool added = false;
            for (int i=0; i<inventories.GetLength(1); i++)
            {
                if(!added && string.IsNullOrEmpty(inventories[id, i]))
                {
                    if(card == "")
                    {
                        string[] results = Generate(1);
                        inventories[id, i] = results[0];
                    }
                    else {
                        inventories[id, i] = card;
                    }
                    added = true;
                }
            }
        }

        static void RemoveCard(int id, string card, ref string[,] inventories)
        {
            bool removed = false;
            for (int i=0; i<inventories.GetLength(1); i++)
            {
                if(!removed && inventories[id, i] == card)
                {
                    inventories[id, i] = null;
                    removed = true;
                }
            }
        }
    }
}