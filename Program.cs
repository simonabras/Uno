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
            Clean();
            Console.WriteLine("         Appuyez sur une touche pour jouer");
            Console.ReadLine();
            
            // Génération des inventaires et de la table
            string[,] inventories = new string[2, 15];
            Distribute(ref inventories);
            string table = "";
            do
            {
                table = Generate();
            }
            while(table.Contains("+2") || table.Contains("~") || table.Contains("^"));

            // Début du jeu
            while(CountPlayerCards(inventories) > 0 && CountBotCards(inventories) > 0)
            {
                Clean();
                DisplayGame(inventories, table);
                Console.WriteLine("\n         Choisissez une option\n         1. Joueur une carte\n         2. Piocher une carte");

                string choice1 = "";
                while(choice1 != "1" && choice1 != "2")
                {
                    choice1 = Console.ReadLine();
                }
                if(choice1 == "1")
                {
                    Clean();
                    DisplayGame(inventories, table);
                    Console.WriteLine("\n         Choisissez une carte à jouer");
                    DisplayPlayerCardsList(inventories);

                    string choice2 = "";
                    choice2 = Console.ReadLine();
                }
                else {

                }
            }
            if(CountPlayerCards(inventories) == 0)
            {
                Console.WriteLine("         Bien joué ! Vous avez gagné la partie :)");
            }
            else {
                Console.WriteLine("         Dommage ! Vous avez perdu la partie :(");
            }
            Console.ReadLine();
        }

        // Nettoyer la console
        static void Clean()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
            __   __  __    _  _______ 
            |  | |  ||  |  | ||       |
            |  | |  ||   |_| ||   _   |
            |  |_|  ||       ||  | |  |
            |       ||  _    ||  |_|  |
            |       || | |   ||       |
            |_______||_|  |__||_______|
            ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Afficher le jeu
        static void DisplayGame(string[,] inventories, string table)
        {
            int botCardsCount = CountBotCards(inventories);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"         Votre inventaire : ");
            DisplayPlayerCards(inventories);
            Console.WriteLine($"\n         Table : {table}");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n         Votre adversaire possède {botCardsCount} carte(s)");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // G�n�rer une cartes
        static string Generate()
        {
            string[] cards = {
                "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B+2", "B~", "B^",
                "V0", "V1", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V+2", "V~", "V^",
                "R0", "R1", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R+2", "R~", "R^",
                "J0", "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J+2", "J~", "J^",
            };
            Random rd = new Random();
            int number = rd.Next(0, cards.Length);
            return cards[number];
        }

        // Distribuer des cartes au joueur et au robot
        static void Distribute(ref string[,] inventories)
        {
            for(int i=0; i<inventories.GetLength(0); i++)
            {
                for (int j=0; j<8; j++)
                {
                    inventories[i, j] = Generate();
                }
            }
        }

        // Ajouter une carte � l'inventaire du joueur ou du robot
        static void AddCard(int id, string card, ref string[,] inventories)
        {
            bool added = false;
            for (int i=0; i<inventories.GetLength(1); i++)
            {
                if(!added && string.IsNullOrEmpty(inventories[id, i]))
                {
                    if(card == "")
                    {
                        inventories[id, i] = Generate();
                    }
                    else {
                        inventories[id, i] = card;
                    }
                    added = true;
                }
            }
        }

        // Retirer une carte de l'inventaire du joueur ou du robot
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

        // V�rifier si une carte peut �tre jou�e
        static bool CheckCardTable(string card, string table)
        {
            string cardColor = card.Substring(0, 1);
            string cardSymbol = card.Substring(1);
            string tableColor = table.Substring(0, 1);
            string tableSymbol = table.Substring(1);
            if(cardColor == tableColor || cardSymbol == tableSymbol)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Afficher les cartes du joueur
        static void DisplayPlayerCards(string[,] inventories)
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
            Console.Write(results);
        }

        // Afficher les cartes du joueur sous forme de liste
        static void DisplayPlayerCardsList(string[,] inventories)
        {
            for(int i=0; i<inventories.GetLength(1); i++)
            {
                if(!string.IsNullOrEmpty(inventories[0, i]))
                {
                    Console.Write($"         {i+1}. ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{inventories[0, i]}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Compter le nombre de cartes dans l'inventaire du joueur
        static int CountPlayerCards(string[,] inventories)
        {
            int count = 0;
            for(int i=0; i<inventories.GetLength(1); i++)
            {
                if(!string.IsNullOrEmpty(inventories[0, i]))
                {
                    count++;
                }
            }
            return count;
        }
        // Compter le nombre de cartes dans l'inventaire du robot
        static int CountBotCards(string[,] inventories)
        {
            int count = 0;
            for(int i=0; i<inventories.GetLength(1); i++)
            {
                if(!string.IsNullOrEmpty(inventories[1, i]))
                {
                    count++;
                }
            }
            return count;
        }
    }
}