using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            BlackjackGame game = new BlackjackGame();
            Deck carddeck = new Deck();
            bool exit = false;
            bool debugMode = false;

            while (!exit)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Setup game");
                if (debugMode)
                {
                    Console.WriteLine("2. Draw a card");
                    Console.WriteLine("3. Display draw pile");
                    Console.WriteLine("4. Display discard pile");
                    Console.WriteLine("5. Exit Debug Mode");
                }
                else
                {
                    Console.WriteLine("2. Enter Debug Mode");
                    Console.WriteLine("3. Exit");
                }

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        game.SetupGame();
                        break;
                    case "2":
                        if (debugMode)
                        {
                            Card drawnCard = carddeck.Draw();
                            Console.WriteLine($"Drawn Card: {drawnCard}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Entering Debug Mode...");
                            debugMode = true;
                        }

                        break;
                    case "3":
                        if (debugMode)
                        {
                            Console.WriteLine("Draw Pile (Top to Bottom):");
                            carddeck.DrawPile.DisplayInOrder();
                            Console.WriteLine();
                        }
                        else
                        {
                            exit = true;
                        }

                        break;
                    case "4":
                        if (debugMode)
                        {
                            Console.WriteLine("Discard Pile (Top to Bottom):");
                            carddeck.DiscardPile.DisplayInOrder();
                            Console.WriteLine();
                        }
                        else
                        {
                            exit = true;
                        }

                        break;
                    case "5":
                        if (debugMode)
                        {
                            debugMode = false;
                            Console.WriteLine("Exiting Debug Mode.");
                        }
                        else
                        {
                            exit = true;
                        }

                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
