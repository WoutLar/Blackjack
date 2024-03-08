using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Draw a card");
                Console.WriteLine("2. Display draw pile");
                Console.WriteLine("3. Display discard pile");
                Console.WriteLine("4. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Card drawnCard = deck.Draw();
                        Console.WriteLine($"Drawn Card: {drawnCard}");
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Draw Pile (Top to Bottom):");
                        deck.DrawPile.DisplayInOrder();
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("Discard Pile (Top to Bottom):");
                        deck.DiscardPile.DisplayInOrder();
                        Console.WriteLine();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}
